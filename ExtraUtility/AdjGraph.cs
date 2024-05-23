using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//AFTER DELEING SOMTHING IT ADDS A VERTEX ONE TWO MANY :(
namespace ExtraUtility
{
    public class AdjGraph : GraphBase
    {
        private Dictionary<int, Node> mVertexSet;

        public AdjGraph(int numVertices,bool directed = false ): base(numVertices, directed) 
        {
            this.mVertexSet = new Dictionary<int, Node>();

            // add nodes to the set
            for(int i = 0; i < numVertices; i++) 
            {
                Node node = new Node(i);
                mVertexSet.Add(node.vertexId(), node);
            }
        }
        public int addVertex()
        {
            //find the lowest possable ID
            int lowestIDAvalible = 0;

            //add all the IDs to a list
            List<int> IDs = createVertIDList();

            //sort the list
            IDs.Sort();

            //get the smallest ID
            if( IDs.Count > 0 ) 
            {
                lowestIDAvalible = IDs.Min();
            }
            else
            {
                //nothing was in in the empty
                lowestIDAvalible = 1;
            }
            

            bool found = false;

            // if the ID is not 0 then we can reduce it and call it a day

            int temp = lowestIDAvalible - 1;

            if(temp >= 0)
            {
                lowestIDAvalible--;
                found = true;
            }

            if(!found && IDs.Count - 1 == IDs[IDs.Count - 1])
            {
                lowestIDAvalible = IDs.Count;
                found = true;
            }
            //should always be zero

            if (!found)
            {
                Debug.Assert(IDs[0] == 0);

                //seach for the half way point
                int half = (IDs[IDs.Count - 1]) / 2;
                int index;

                //try to fill the middle
                while (true)
                {
                    index = IDs.BinarySearch(half);
                    if (index < 0)
                    {
                        //found!
                        lowestIDAvalible = half;//break the loop
                        break;
                    }
                    else if (index < half)
                    {
                        //below - low should always be zero
                        half = index / 2;

                    }
                    else if (index == half)
                    {
                        half = ((IDs[IDs.Count - 1]) - index) / 2;
                        //above
                        if(half == index)
                        {
                            half++;
                        }
                    }

                }
            }


            //while (IDs.Contains(lowestIDAvalible) && !found) 
            //{
            //  lowestIDAvalible++;
            //}

            //becasue we found the new lowest index we can make the node
            Node node  = new Node(lowestIDAvalible);

            mVertexSet.Add(node.vertexId(), node);
            mNumVertices++;

            //return the lowestID
            return lowestIDAvalible;
        }
        public override void addEdge(int v1, int v2, int weight)
        {
            //if (v1 >= this.numVertices || v2 >= this.numVertices || v1 < 0 || v2 < 0)
            //{
            //    throw new ArgumentOutOfRangeException("Vertices are out of bounds");
            //}

            if (weight != 1)
            {
                throw new ArgumentException("An adjacency set cannot represent non-one edge weights");
            }

            //catches if a user is placing one or more vertices that do not exist
            List<int> VertIDs = createVertIDList();

            if(!VertIDs.Contains(v1) || !VertIDs.Contains(v2))
            {
                throw new AggregateException("one or both of these vertices do not exist");
            }

            this.mVertexSet[v1].addEdge(v2);

            //In an undirected graph all edges are bi-directional
            if (!this.mDirected)
            { 
                this.mVertexSet[v2].addEdge(v1);
            }
        }
        public override void deleteEdge(int v1, int v2)
        {
           
            mVertexSet[v1].removeEdge(v2);

            //if it is NOT dircted we need to delete the other connection
            if(!this.mDirected)
            {
                
                mVertexSet[v2].removeEdge(v1);
            }
        }
        public override void deleteVertex(int v)
        {
            //vist each vertex in the map then see if it has a connection to the target vertex
            foreach(var vertex in mVertexSet)
            {
                Node node = vertex.Value;
                var nuem = node.getAdjacentVertices();

                foreach(var edge in nuem) 
                {
                    if(edge == v)
                    {
                        node.removeEdge(edge);
                    }
                }
            }
            
            //delete the target
            mVertexSet.Remove(v);
        }

        public override IEnumerable<int> getNeighbors(int v)
        {
            if (v < 0 || v >= this.numVertices)
            {
                throw new ArgumentOutOfRangeException("Cannot access vertex");
            }

            return this.mVertexSet[v].getAdjacentVertices();
        }

        public override int getEdgeWeight(int v1, int v2)
        {
            return 1;
        }

        public override void display()
        {

            for(int i = 0; numVertices > i; i++)
            {
                //make sure the key is in the dic
                if (mVertexSet.ContainsKey(i)) 
                {
                    Node node = this.mVertexSet[i];
                    //get the vertex Id
                    Console.Write("vetex " + node.vertexId());

                    var neigbors = node.getAdjacentVertices();

                    foreach (var neigbor in neigbors)
                    {
                        //should just be a int
                        Console.Write(" edges " + neigbor);
                    }
                    Console.WriteLine();
                }
                
            }
            
        }

        private List<int> createVertIDList()
        {
            List<int> IDs = new List<int>(this.mVertexSet.Keys);

            return IDs;
        }

        public List<Node> DFS(int startV, int goalV)
        {
            if(mDirected == false)
            {
                throw new Exception("cant prefrom a DFS on a bidirectional graph");
            }

            List<Node> visisted = new List<Node>();

            Stack<Node> unexplored = new Stack<Node>();

            //pop the first item
            unexplored.Push(mVertexSet[startV]);

            while (unexplored.Count > 0)
            {
                if(visisted.Contains(unexplored.Peek()))
                {
                    //if we have visited the thing take it out of the stack
                    unexplored.Pop();

                }
                else
                {
                    Node node = unexplored.Pop();

                    //get node id
                    int ID = node.vertexId();

                    //add the node to the visted list
                    visisted.Add(node);

                    //check to see if we found the node we are looking for
                    if (ID == goalV)
                    {
                        //should return the path if we found the node
                        return visisted;
                    }
                    //if we did not find take the neigboors and add it to the queue
                    else
                    {
                        //node.getAdjacentVertices();
                        foreach (var neighbor in node.getAdjacentVertices())
                        {
                            // get the vertice for the mevertex set and add it to the queue 
                            unexplored.Push(mVertexSet[neighbor]);
                        }


                    }

                }

            }

            //we did not find the thing
            return new List<Node>();
        }

        //clear all nodes in the graph
        public void clearGraph()
        {
            mVertexSet.Clear();
        }
    }
}

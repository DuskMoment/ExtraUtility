using System.Diagnostics;
using System.Numerics;
using System.Reflection.Metadata.Ecma335;
using ExtraUtility.Structures;
using System.Collections.Generic;

namespace ExtraUtility.Graphs
{
    public class AdjGraph : GraphBase
    {
        private Dictionary<int, Node> mVertexSet;

        public AdjGraph(int numVertices, bool directed = false) : base(numVertices, directed)
        {
            mVertexSet = new Dictionary<int, Node>();

            // add nodes to the set
            for (int i = 0; i < numVertices; i++)
            {
                Node node = new Node(i);
                mVertexSet.Add(node.vertexId(), node);
            }
        }

        //GRAPH MODIFCATION FUNCTIONS
        //adds a vertex at the next lowest id and returns that ID as a handle
        public int addVertex()
        {
            //find the lowest possable ID
            int lowestIDAvalible = 0;

            //add all the IDs to a list
            List<int> IDs = createVertIDList();

            //sort the list
            IDs.Sort();

            //get the smallest ID
            if (IDs.Count > 0)
            {
                lowestIDAvalible = IDs.Min();
            }
            else
            {
                //the list was empty
                lowestIDAvalible = 1;
            }


            bool found = false;

            // if the ID is not 0 then we can reduce it and call it a day

            int temp = lowestIDAvalible - 1;

            if (temp >= 0)
            {
                lowestIDAvalible--;
                found = true;
            }

            //what was this if statment for again?
            if (!found && IDs.Count - 1 == IDs[IDs.Count - 1])
            {
                lowestIDAvalible = IDs.Count;
                found = true;
            }
            //should always be zero

            if (!found)
            {
                Debug.Assert(IDs[0] == 0);

                //seach for the half way point
                int half = IDs[IDs.Count - 1] / 2;
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
                        half = (IDs[IDs.Count - 1] - index) / 2;
                        //above
                        if (half == index)
                        {
                            half++;
                        }
                    }

                }
            }


            //becasue we found the new lowest index we can make the node
            Node node = new Node(lowestIDAvalible);

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

            //if (weight != 1)
            //{
            //    throw new ArgumentException("An adjacency set cannot represent non-one edge weights");
            //}

            //catches if a user is placing one or more vertices that do not exist
            List<int> VertIDs = createVertIDList();

            if (!VertIDs.Contains(v1) || !VertIDs.Contains(v2))
            {
                throw new AggregateException("one or both of these vertices do not exist");
            }

            mVertexSet[v1].addEdge(v2, weight);

            //In an undirected graph all edges are bi-directional
            if (!mDirected)
            {
                mVertexSet[v2].addEdge(v1, weight);
            }
        }
        public override void deleteEdge(int v1, int v2)
        {

            mVertexSet[v1].removeEdge(v2);

            //if it is NOT dircted we need to delete the other connection
            if (!mDirected)
            {

                mVertexSet[v2].removeEdge(v1);
            }
        }
        public override void deleteVertex(int v)
        {
            //vist each vertex in the map then see if it has a connection to the target vertex
            foreach (var vertex in mVertexSet)
            {
                Node node = vertex.Value;
                var nuem = node.getAdjacentVertices();

                foreach (var edge in nuem)
                {
                    if (edge.Key == v)
                    {
                        node.removeEdge(edge.Key);
                    }
                }
            }

            //delete the target
            mVertexSet.Remove(v);
        }
        //clear all nodes in the graph
        public void clearGraph()
        {
            mVertexSet.Clear();
        }

        //EXTRA FUCNTIONS
        public override IEnumerable<KeyValuePair<int, int>> getNeighbors(int v)
        {
            if (v < 0 || v >= numVertices)
            {
                throw new ArgumentOutOfRangeException("Cannot access vertex");
            }

            return mVertexSet[v].getAdjacentVertices();
        }

        public override int getEdgeWeight(int v1, int v2)
        {
            return 1;
        }

        //SEACHING FUNCTIONS

        public List<Node> dijkstra(int startV, int goalV)
        {
            //create a queue with the pair as key value (key:vertID, val:weight)
            ExtraUtility.Structures.PriorityQueue<int, int> minHeap = new ExtraUtility.Structures.PriorityQueue<int, int>();

            List<int> vistedVerts = new List<int>();
            List<Node> path = new List<Node>();

            //get first vertex
            Node startNode = mVertexSet[startV];

            //get all of the neighbors
            List<KeyValuePair<int, int>> neigbors = startNode.getAdjacentVertices();

            //for each neighbor add it to the queue with the vertId as the element and the weight as the compaitor 
            foreach (var neighbor in neigbors)
            {
                minHeap.enqueue(neighbor.Value, neighbor.Key);
            }
            vistedVerts.Add(startV);
            //start of loop
            while (!minHeap.isEmpty)
            {
                //get the next lowest vertex from the queue
                int vert = minHeap.dequeueValue();

                if (vert == goalV)
                {
                    path.Add(mVertexSet[vert]);
                    return path;
                }

                //check to see if we have already been here
                else if (!vistedVerts.Contains(vert))
                {
                    //we have not visited it
                    var newVerts = mVertexSet[vert].getAdjacentVertices();
                    //then add it to the queue
                    foreach (var newVert in newVerts)
                    {
                        minHeap.enqueue(newVert.Value, newVert.Key);
                    }

                    //then add current node to path and say we have visted it 
                    path.Add(mVertexSet[vert]);
                    vistedVerts.Add(vert);
                }


            }

            //found nothing :/
            return new List<Node>();
        }
        public List<Node> DFS(int startV, int goalV)
        {

            //if (mDirected == false)
            //{
            //    throw new Exception("cant prefrom a DFS on a bidirectional graph");
            //}

            List<Node> visisted = new List<Node>();

            Stack<Node> unexplored = new Stack<Node>();

            //pop the first item
            unexplored.Push(mVertexSet[startV]);

            while (unexplored.Count > 0)
            {
                if (visisted.Contains(unexplored.Peek()))
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
                            unexplored.Push(mVertexSet[neighbor.Key]);
                        }


                    }

                }

            }

            //we did not find the thing
            return new List<Node>();
        }
        public List<Node> BFS(int startV, int goalV)
        {
            if (mDirected == false)
            {
                throw new Exception("cant prefrom a DFS on a bidirectional graph");
            }

            List<Node> visisted = new List<Node>();

            Queue<Node> unexplored = new Queue<Node>();

            //pop the first item
            unexplored.Enqueue(mVertexSet[startV]);

            while (unexplored.Count > 0)
            {
                if (visisted.Contains(unexplored.Peek()))
                {
                    //if we have visited the thing take it out of the stack
                    unexplored.Dequeue();

                }
                else
                {
                    Node node = unexplored.Dequeue();

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
                            unexplored.Enqueue(mVertexSet[neighbor.Key]);
                        }


                    }

                }

            }

            //we did not find the thing
            return new List<Node>();
        }

        //UTILTIY FUNCTIONS
        public override void display()
        {

            for (int i = 0; numVertices > i; i++)
            {
                //make sure the key is in the dic
                if (mVertexSet.ContainsKey(i))
                {
                    Node node = mVertexSet[i];
                    //get the vertex Id
                    Console.Write("vetex " + node.vertexId());

                    var neigbors = node.getAdjacentVertices();

                    foreach (var neigbor in neigbors)
                    {
                        //should just be a int
                        Console.Write(" edges " + neigbor.Key + " weight " + neigbor.Value);
                    }
                    Console.WriteLine();
                }

            }

        }

        private List<int> createVertIDList()
        {
            List<int> IDs = new List<int>(mVertexSet.Keys);

            return IDs;
        }


    }
}

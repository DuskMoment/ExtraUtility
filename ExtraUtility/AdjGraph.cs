using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExtraUtility
{
    public class AdjGraph : GraphBase
    {
        private HashSet<Node> mVertexSet;

        public AdjGraph(int numVertices,bool directed = false ): base(numVertices, directed) 
        {
            this.mVertexSet = new HashSet<Node>();

            // add nodes to the set
            for(int i = 0; i < numVertices; i++) 
            {
                mVertexSet.Add(new Node(i));
            }
        }
        public override void addEdge(int v1, int v2, int weight)
        {
            if (v1 >= this.numVertices || v2 >= this.numVertices || v1 < 0 || v2 < 0)
            {
                throw new ArgumentOutOfRangeException("Vertices are out of bounds");
            }


            if (weight != 1)
            {
                throw new ArgumentException("An adjacency set cannot represent non-one edge weights");
            }

            this.mVertexSet.ElementAt(v1).addEdge(v2);

            //In an undirected graph all edges are bi-directional
            if (!this.mDirected)
            {
                this.mVertexSet.ElementAt(v2).addEdge(v1);
            }


        }
        public override void deleteEdge(int v1, int v2)
        {
            mVertexSet.ElementAt(v1).removeEdge(v2);

            //if it is NOT dircted we need to delete the other connection
            if(!this.mDirected)
            {
                mVertexSet.ElementAt(v2).removeEdge(v1);
            }
        }
        public override void deleteVertex(int v)
        {
            //get all of the edges to the vertice
           
            //remove nodes from other nodes
            foreach(var vertex in mVertexSet)
            {
                if(vertex.vertexId() != v)
                {
                    vertex.removeEdge(v);
                }
            }
            //find the node
            Node node = mVertexSet.ElementAt(v);
            //delete node
            mVertexSet.Remove(node);
            //decrease the ammout of vertices that are in the graph
            mNumVertices--;
        }

        public override IEnumerable<int> getNeighbors(int v)
        {
            if (v < 0 || v >= this.numVertices)
            {
                throw new ArgumentOutOfRangeException("Cannot access vertex");
            }

            return this.mVertexSet.ElementAt(v).getAdjacentVertices();
        }

        public override int getEdgeWeight(int v1, int v2)
        {
            return 1;
        }

        public override void display()
        {

            for(int i = 0; numVertices > i; i++)
            {
                Node node =  this.mVertexSet.ElementAt(i);
                //get the vertex Id
                Console.Write("vetex " + node.vertexId());

                var neigbors =  node.getAdjacentVertices();

                foreach( var neigbor in neigbors)
                {
                    //should just be a int
                    Console.Write(" edges " + neigbor);
                }
                Console.WriteLine();
            }
            
        }
    }
}

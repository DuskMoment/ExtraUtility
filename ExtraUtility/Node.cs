using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExtraUtility
{
    public class Node
    {
        private readonly int mVertexId;
        private readonly HashSet<int> mAdjacencySet;

        public Node(int vertexId)
        {
            this.mVertexId = vertexId;
            mAdjacencySet = new HashSet<int>();
        }

        public void addEdge(int v) 
        {
            if(this.mVertexId == v)
            {
                throw new ArgumentException("The vertex cannot be adjacent to itself");
            }
            this.mAdjacencySet.Add(v);
        }

        public void removeEdge(int v) 
        {
            mAdjacencySet.Remove(v);
        }

        public HashSet<int> getAdjacentVertices() 
        {
            return this.mAdjacencySet;
        }

        public int vertexId()
        {
            return this.mVertexId;
        }
    }
}

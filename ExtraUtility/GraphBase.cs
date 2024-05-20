using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExtraUtility
{
    public abstract class GraphBase
    {
        protected readonly int mNumVertices;
        protected readonly bool mDirected;

        public GraphBase(int numVertices,bool directed = false)
        {
            this.mNumVertices = numVertices;
            this.mDirected = directed;
        }
        public abstract void addEdge(int v1, int v2, int weight);

        public abstract void deleteEdge(int v1, int v2);

        public abstract IEnumerable<int> getNeighbors(int v);

        public abstract int getEdgeWeight(int v1, int v2);

        public abstract void display();

        public int numVertices { get { return mNumVertices; } }
    }
}
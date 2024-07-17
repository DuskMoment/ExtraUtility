namespace ExtraUtility.Graphs
{
    public abstract class GraphBase
    {

        protected readonly bool mDirected;
        protected int mNumVertices;

        public GraphBase(int numVertices, bool directed = false)
        {
            mNumVertices = numVertices;
            mDirected = directed;
        }
        public abstract void addEdge(int v1, int v2, int weight);

        public abstract void deleteEdge(int v1, int v2);

        public abstract void deleteVertex(int v);

        public abstract IEnumerable<KeyValuePair<int,int>> getNeighbors(int v);

        public abstract int getEdgeWeight(int v1, int v2);

        public abstract void display();

        public int numVertices { get { return mNumVertices; } }
    }
}
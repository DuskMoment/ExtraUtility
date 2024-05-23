namespace ExtraUtility.Structures
{
    public class Node
    {
        private readonly int mVertexId;
        private readonly HashSet<int> mAdjacencySet;

        public Node(int vertexId)
        {
            mVertexId = vertexId;
            mAdjacencySet = new HashSet<int>();
        }

        public void addEdge(int v)
        {
            if (mVertexId == v)
            {
                throw new ArgumentException("The vertex cannot be adjacent to itself");
            }
            mAdjacencySet.Add(v);
        }

        public void removeEdge(int v)
        {
            mAdjacencySet.Remove(v);
        }

        public HashSet<int> getAdjacentVertices()
        {
            return mAdjacencySet;
        }

        public int vertexId()
        {
            return mVertexId;
        }
    }
}

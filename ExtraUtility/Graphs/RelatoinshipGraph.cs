using ExtraUtility.Structures;

namespace ExtraUtility.Graphs
{
    public class RelatoinshipGraph<T>
    {
        private AdjGraph mGraph;

        private Dictionary<T, int> mObjRelationship;
        private Dictionary<int, T> mVertIDRelationShip;

        private List<T> mActiveObjects;
        public RelatoinshipGraph(bool isDirected = false)
        {
            mGraph = new AdjGraph(0, isDirected);
            mObjRelationship = new Dictionary<T, int>();
            mActiveObjects = new List<T>();
            mVertIDRelationShip = new Dictionary<int, T>();
        }
        //MODIFY GRAPH
        public void addVertex(T vertexObj)
        {
            //add vertex id to the object
            int vertID = mGraph.addVertex();
            mObjRelationship[vertexObj] = vertID;
            mActiveObjects.Add(vertexObj);

            //add the object to the vertexID
            mVertIDRelationShip[vertID] = vertexObj;


        }
        public void addEdge(T obj1, T obj2, int weight)
        {
            //check to see if objects are in the active list
            if (mActiveObjects.Contains(obj1) && mActiveObjects.Contains(obj2))
            {
                //get the ids
                int vertID1 = mObjRelationship[obj1];
                int vertID2 = mObjRelationship[obj2];

                mGraph.addEdge(vertID1, vertID2, weight);
            }
            else
            {
                throw new Exception("One or more of the objects are not in the activeList");
            }

        }
        public void deleteVertex(T obj)
        {
            if (mActiveObjects.Contains(obj))
            {
                mActiveObjects.Remove(obj);

                //get the ID to remove
                int ID = mObjRelationship[obj];

                //delete from graph
                mGraph.deleteVertex(ID);

                //remove relationship
                mObjRelationship.Remove(obj);
                mVertIDRelationShip.Remove(ID);
            }
        }
        public void deleteEdge(T obj1, T obj2)
        {
            if (mActiveObjects.Contains(obj1) && mActiveObjects.Contains(obj2))
            {
                int idToDelete1 = mObjRelationship[obj1];
                int idToDelete2 = mObjRelationship[obj2];

                mGraph.deleteEdge(idToDelete1, idToDelete2);
            }
            else
            {
                throw new Exception("One or more of the objects are not in the activeList");
            }

        }
        public void clearGraph()
        {
            mGraph.clearGraph();
            mActiveObjects.Clear();
            mObjRelationship.Clear();
            mVertIDRelationShip.Clear();
        }

        //SEARCHING
        public List<T> DFS(T objStart, T objGoal)
        {
            //get the node path
            var path = mGraph.DFS(mObjRelationship[objStart], mObjRelationship[objGoal]);

            return convertNodeToObj(path);
        }

        public void display()
        {
            foreach (T vertObj in mActiveObjects)
            {
                var verts = mGraph.getNeighbors(mObjRelationship[vertObj]);
                //out put the first realtionship
                Console.WriteLine(vertObj + " " + mObjRelationship[vertObj]);
                Console.Write(" connections ");

                foreach (var v in verts)
                {
                    Console.Write(mVertIDRelationShip[v]);
                }
                Console.WriteLine();
            }

        }
        private List<T> convertNodeToObj(List<Node> nodes)
        {
            List<T> objPath = new List<T>();

            foreach (Node node in nodes)
            {
                objPath.Add(mVertIDRelationShip[node.vertexId()]);

            }

            return objPath;
        }
    }
}

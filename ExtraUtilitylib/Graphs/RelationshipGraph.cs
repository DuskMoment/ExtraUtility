using ExtraUtility.Structures;
using System.Collections.Generic;
using System;

namespace ExtraUtility.Graphs
{
    public class RelatoinshipGraph<TKey> where TKey : IEquatable<TKey>
    {
        private AdjGraph mGraph;

        private Dictionary<TKey, int> mObjRelationship;
        private Dictionary<int, TKey> mVertIDRelationShip;

        private List<TKey> mActiveObjects;
        public RelatoinshipGraph(bool isDirected = false)
        {
            mGraph = new AdjGraph(0, isDirected);
            mObjRelationship = new Dictionary<TKey, int>();
            mActiveObjects = new List<TKey>();
            mVertIDRelationShip = new Dictionary<int, TKey>();
        }
        //MODIFY GRAPH
        public void addVertex(TKey vertexObj)
        {
            //we had the obejct already
            if (mActiveObjects.Contains(vertexObj))
            {
                return;
            }

            //add vertex id to the object
            int vertID = mGraph.addVertex();
            mObjRelationship[vertexObj] = vertID;
            mActiveObjects.Add(vertexObj);

            //add the object to the vertexID
            mVertIDRelationShip[vertID] = vertexObj;


        }
        public void addEdge(TKey obj1, TKey obj2, int weight = 1)
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
        public void deleteVertex(TKey obj)
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
        public void deleteEdge(TKey obj1, TKey obj2)
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
        public List<TKey> DFS(TKey objStart, TKey objGoal)
        {
            //get the node path
            var path = mGraph.DFS(mObjRelationship[objStart], mObjRelationship[objGoal]);

            return convertNodeToObj(path);
        }
        //public List<TKey> Dijkstra(TKey objStart, TKey objGoal)
        //{
        //    var path = mGraph.dijkstra(mObjRelationship[objStart], mObjRelationship[objGoal]);

        //    return convertNodeToObj(path);
        //}

        public void display()
        {
            foreach (TKey vertObj in mActiveObjects)
            {
                var verts = mGraph.getNeighbors(mObjRelationship[vertObj]);
                //out put the first realtionship
                Console.WriteLine(vertObj + " " + mObjRelationship[vertObj]);
                Console.Write(" connections ");

                foreach (var v in verts)
                {
                    Console.Write(mVertIDRelationShip[v.Key] + " weight: " + v.Value + " ");
                }
                Console.WriteLine();
            }

        }
        private List<TKey> convertNodeToObj(List<Node> nodes)
        {
            List<TKey> objPath = new List<TKey>();

            foreach (Node node in nodes)
            {
                objPath.Add(mVertIDRelationShip[node.vertexId()]);

            }

            return objPath;
        }
    }
}

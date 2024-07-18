using System.Collections.Generic;
using System;
using System.Runtime.ExceptionServices;

namespace ExtraUtility.Structures
{
    public class Node
    {
        private readonly int mVertexId;
        private readonly HashSet<int> mAdjacencySet;
        private readonly List<KeyValuePair<int, int>> mEdgeWeight;

        public Node(int vertexId)
        {
            mVertexId = vertexId;
            mAdjacencySet = new HashSet<int>();
            mEdgeWeight = new List<KeyValuePair<int, int>>();
        }

        public void addEdge(int v, int weight = 1)
        {
            if (mVertexId == v)
            {
                throw new ArgumentException("The vertex cannot be adjacent to itself");
            }

            mAdjacencySet.Add(v);
            mEdgeWeight.Add(new KeyValuePair<int, int>(v, weight));
        }

        public void removeEdge(int v)
        {
            foreach (var weight in mEdgeWeight)
            {
                //found the thing to delete
                if (weight.Key == v)
                {
                    mEdgeWeight.Remove(weight);
                    break;
                }
            }
            mAdjacencySet.Remove(v);
        }

        public List<KeyValuePair<int, int>> getAdjacentVertices()
        {
            List<KeyValuePair<int, int>> adjList = new List<KeyValuePair<int, int>>();

            //for each Adjset that a node has check edge weight and if we have a match add it to the keyValue list 
            foreach (var v in mAdjacencySet)
            {
                foreach (var w in mEdgeWeight)
                {
                    if (w.Key == v)
                    {
                        //once we found the key add the associated weight for it
                        adjList.Add(new KeyValuePair<int, int>(w.Key, w.Value));
                        break;
                    }
                }
            }

            return adjList;
        }

        public int vertexId()
        {
            return mVertexId;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExtraUtility.Structures
{
    //inseperation form https://www.codeproject.com/Articles/126751/Priority-Queue-in-Csharp-with-the-Help-of-Heap-Dat
    public class PriorityQueue<TPriority, TValue>
    {
        private List<KeyValuePair<TPriority, TValue>> mBaseHeap;
        private IComparer<TPriority> mComparer;

        public PriorityQueue() : this(Comparer<TPriority>.Default)
        {

        }
        public PriorityQueue(IComparer<TPriority> comparer)
        {
            if(comparer == null)
            {
                throw new ArgumentNullException();
            }
            mBaseHeap = new List<KeyValuePair<TPriority, TValue>>();
            mComparer = comparer;
        }

        private int parent(int index)
        {
            return (index - 1) / 2;
        }
        private int left(int index)
        {
            return (index * 2 + 1);
        }
        private int right(int index) 
        {
            return index * 2 + 2;
        }
        public void enqueue(TPriority priority, TValue value)
        {
            insert(priority, value);
        }

        private void insert(TPriority priority, TValue value) 
        {
            KeyValuePair<TPriority, TValue> pair = new KeyValuePair<TPriority, TValue>(priority, value);
            mBaseHeap.Add(pair);

            //call heapify function after adding to the end of the list
            heapifyFromEndToBeginning(mBaseHeap.Count - 1);
        }
        private int heapifyFromEndToBeginning(int pos)
        {
            if(pos >= mBaseHeap.Count)
            {
                //not in the list
                return -1;
            }
            while(pos > 0) 
            {
                // if we wanted a max heap we would take the > 0 and make it a < 0 
                int parentPos = parent(pos);
                if (mComparer.Compare(mBaseHeap[parentPos].Key, mBaseHeap[pos].Key) > 0)
                {
                    swap(parentPos, pos);
                    pos = parentPos;
                }
                else break;
            }
            return pos;
        }
        public TValue dequeueValue()
        {
            return this.dequeue().Value;
        }
        public KeyValuePair<TPriority, TValue> dequeue()
        {
            //if it is not empty
            if(!isEmpty)
            {
                KeyValuePair<TPriority,TValue> result = mBaseHeap[0];

                deleteRoot();

                return result;
            }
            else
            {
                throw new InvalidOperationException("Priority queue is empty");
            }
        }
        private void deleteRoot()
        {

            //its the last element so just clear the heap
            if(mBaseHeap.Count <= 1)
            {
                mBaseHeap.Clear();
                return;
            }
            //take the last pos in the heap and make it the first one
            mBaseHeap[0] = mBaseHeap[mBaseHeap.Count - 1];
            //remove dup by deleting at the last element
            mBaseHeap.RemoveAt(mBaseHeap.Count - 1);

            //the apply heap prop
            heapifyFromBeginningToEnd(0);
        }
        public KeyValuePair<TPriority,TValue> peek()
        {
            if(!isEmpty)
            {
                return mBaseHeap[0];
            }
            else 
            {
                throw new InvalidOperationException("Priority queue is empty");
            }
        }
        public TValue peekValue()
        {
            return peek().Value;
        }
        public bool isEmpty
        {
            get { return mBaseHeap.Count == 0; }
        }

        private void heapifyFromBeginningToEnd(int pos)
        {
            if(pos >= mBaseHeap.Count)
            {
                return;
            }
            while(true)
            {
                int smallest = pos;
                int l = left(pos);
                int r = right(pos);

                //left is smaller then the current smallest
                if(l < mBaseHeap.Count && mComparer.Compare(mBaseHeap[smallest].Key, mBaseHeap[l].Key) > 0)
                {
                    smallest = l;
                }
                //right is smaller then the current smallest
                if(r <mBaseHeap.Count && mComparer.Compare(mBaseHeap[smallest].Key, mBaseHeap[r].Key) > 0) 
                {
                    smallest = r;
                }
                //if our smallest is not the same as the starting position then we do not need to swap them and we can assume that it was the smallest element
                if (smallest != pos)
                {
                    swap(smallest, pos);
                    pos = smallest;
                }
                else break;

            }

        }
        private void swap(int pos1, int pos2)
        {
            KeyValuePair<TPriority, TValue> val = mBaseHeap[pos1];
            mBaseHeap[pos1] = mBaseHeap[pos2];
            mBaseHeap[pos2] = val;
        }

        //for testing
        public void display()
        {
            foreach(var  pair in mBaseHeap) 
            {
                Console.WriteLine(pair.Key + " " + pair.Value);
            }
        }
        
    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace DashRunning.Performance
{
    public class PoolList<T> where T : IPoolable
    {
        #region Fields
        List<T> mPoolables = new List<T>();
        int mNextAvailable = -1;
        #endregion

        #region Methods

        public void AddToPool(T poolableToAdd)
        {

            int index = mPoolables.Count;

            if (mNextAvailable == -1)
            {
                mNextAvailable = index;
            }

            mPoolables.Add(poolableToAdd);
            poolableToAdd.Index = index;
            poolableToAdd.Used = false;
        }

        public void Clear()
        {
            mPoolables.Clear();
            mNextAvailable = -1;
        }

        public T GetNextAvailable()
        {
            if (mNextAvailable == -1)
            {
                return default(T);
            }

            T returnReference = mPoolables[mNextAvailable];
            returnReference.Used = true;

            // find next available
            int count = mPoolables.Count;

            mNextAvailable = -1;

            for (int i = returnReference.Index + 1; i < count; i++)
            {
                T poolable = mPoolables[i];

                if (poolable.Used == false)
                {
                    mNextAvailable = i;
                    break;
                }
            }

            return returnReference;
        }

        public void MakeUnused(T poolableToMakeUnused)
        {
            if (mNextAvailable == -1 || poolableToMakeUnused.Index < mNextAvailable)
            {
                mNextAvailable = poolableToMakeUnused.Index;
            }

            poolableToMakeUnused.Used = false;
        }

        #endregion

    }
}

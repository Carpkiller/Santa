using System;
using System.Collections.Generic;

namespace Santa.Comparers
{
    public class ComparerEvents<TUdalost> : IComparer<TUdalost> where TUdalost : IComparable
    {
        public int Compare(TUdalost x, TUdalost y)
        {
            int result = x.CompareTo(y);

            if (result == 0)
                return 1;   // Handle equality as beeing greater
            
            return result;
        }
    }
}

using System;
using System.Collections.Generic;

namespace Santa.Comparers
{
    public class ComparerElfs<TElf> : IComparer<TElf> where TElf : IComparable
    {
        public int Compare(TElf x, TElf y)
        {
            int result = x.CompareTo(y);

            if (result == 0)
                return 1;   // Handle equality as beeing greater

            return result;
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Santa.Comparers;

namespace Santa.Jadro
{
    public class PriorFrontHracka
    {
        //private readonly SortedList<double, Hracka> _priorFront;
        private readonly List<Hracka> _priorFront;

        public PriorFrontHracka()
        {
            //_priorFront = new SortedList<double, Hracka>(new ComparerEvents<double>());
            _priorFront = new List<Hracka>();
        }
        public void Vloz(Hracka p)
        {
            var priorita = p.DlzkaVyroby;
            //_priorFront.Add(priorita, p);
            _priorFront.Add(p);
        }

        public Array GetArray()
        {
            _priorFront.Sort();
            return _priorFront.ToArray();
        }
    }
}

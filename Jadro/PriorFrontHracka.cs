﻿using System;
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
            //_priorFront.RemoveRange(9999990,9);

            for (int i = 0; i < 20; i++)
            {
               // Console.WriteLine(_priorFront[_priorFront.Count - 1].DlzkaVyroby);
            }
            return _priorFront.ToArray();
        }
    }
}

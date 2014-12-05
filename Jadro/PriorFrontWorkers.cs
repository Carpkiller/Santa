using System;

namespace Santa.Jadro
{
    public class PriorFrontWorkers
    {
        private readonly Elf[] _listWorkers;

        public PriorFrontWorkers()
        {
            _listWorkers = new Elf[900];
        }

        public void Vloz(Elf p, int i)
        {
            //var priorita = p.Vykonnost;
            _listWorkers[i] = p;
        }

        public Elf Get(DateTime datumPrichodu)
        {
            //var ind = _priorFront.Select((value, index) => new { value, index = index + 1 })
            //    .Where(pair => datumPrichodu >= pair.value.Value.DostupnyOd)
            //    .Select(pair => pair.index)
            //    .FirstOrDefault() - 1;

            Elf elf = null;

            //if (ind == -1)
            //    return null;

            //Elf p = _priorFront.Values[ind];
            //_priorFront.RemoveAt(ind);

            for (int i = 0; i < _listWorkers.Length; i++)
            {
                if (_listWorkers[i].JeVolny)
                {
                    elf = _listWorkers[i];
                    if (elf != null && _listWorkers[i].Vykonnost>elf.Vykonnost)
                    {
                        elf = _listWorkers[i];
                        if (elf.Vykonnost == 4.0)
                        {
                            elf.JeVolny = false;
                            return elf;
                        }
                    }
                }
            }

            if (elf != null)
                elf.JeVolny = false;

            return elf;
        }

        public bool IsEmpty()
        {
            return _listWorkers.Length == 0;
        }

        public int Count()
        {
            return _listWorkers.Length;
        }
    }
}

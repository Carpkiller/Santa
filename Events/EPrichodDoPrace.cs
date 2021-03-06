﻿using Santa.Abstract;
using Santa.Jadro;

namespace Santa.Events
{
    public class EPrichodDoPrace : Udalost
    {
        private Elf _elf;
        public EPrichodDoPrace(double prichod, Elf elf)
        {
            _elf = elf;
            SetCas(prichod);
        }

        public override void Vykonaj(Jadro.Jadro jadro)
        {
            //Console.WriteLine("Prichod elfa  : " + _elf.Id + "  , cas : " + DateTime.FromOADate(GetCas()).ToString());
            jadro.PrichodElfaDoPrace(_elf);
        }
    }
}

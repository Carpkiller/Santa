using System;

namespace Santa.Jadro
{
    public class Elf
    {
        public bool JeVolny { get; set; }
        public double Vykonnost { get; set; }
        public DateTime DostupnyOd { get; set; }
        public DateTime ZaciatokPRace { get; set; }
        public int Id { get; set; }

        public Elf(bool jeVolny, double vykonnost, int id, DateTime dostupnost)
        {
            JeVolny = jeVolny;
            Vykonnost = vykonnost;
            Id = id;
            DostupnyOd = dostupnost;
        }

        //public int CompareTo(object obj)
        //{
        //    if (Math.Abs(Vykonnost - ((Elf) obj).Vykonnost) < 0.00000005)
        //    {
        //        return 1;
        //    }
        //    if (Vykonnost < ((Elf)obj).Vykonnost)
        //    {
        //        return -1;
        //    }
        //    if (Vykonnost > ((Elf)obj).Vykonnost)
        //    {
        //        return 1;
        //    }

        //    return 1;
        //}
    }
}

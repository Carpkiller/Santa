using System;

namespace Santa.Jadro
{
    public class Logovac
    {
        public int ToyId { get; set; }
        public int ElfId { get; set; }
        public DateTime StartDate { get; set; }
        public int Duration { get; set; }

        public Logovac(int toyId, int elfId, DateTime startDate, int duration)
        {
            ToyId = toyId;
            ElfId = elfId;
            StartDate = startDate;
            Duration = duration;
        }

        public override string ToString()
        {
            return ToyId+","+ElfId+","+StartDate.Year+" "+StartDate.Month+" "+StartDate.Day+" "+StartDate.Hour+" "+StartDate.Minute+","+Duration;
        }
    }
}

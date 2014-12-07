using System;
using System.Collections;
using Santa.Abstract;

namespace Santa.Jadro
{
    public class Hracka : PrvokSystemu, IComparer, IComparable
    {
        public int Id { get; set; }
        public double PrichodDoSystemu { get; set; }
        public double VystupZoSystemu { get; set; }
        public double ZaciatokVyroby { get; set; }
        public int DlzkaVyroby { get; set; }
        public int ObsluhujuciElf { get; set; }
        public bool Dokoncena { get; set; }
        public int DlzkaSkutocnejVyroby { get; set; }

        public Hracka(int id, double prichod, int dlzkaVyroby, bool dokoncena)
        {
            Id = id;
            PrichodDoSystemu = prichod;
            DlzkaVyroby = dlzkaVyroby;
            Dokoncena = dokoncena;
        }

        public int Compare(object x, object y)
        {
            if (((Hracka)x).DlzkaVyroby > ((Hracka)y).DlzkaVyroby)
            {
                return 1;
            }
            return -1;
        }

        public int CompareTo(object obj)
        {
            if (this.DlzkaVyroby > ((Hracka)obj).DlzkaVyroby)
            {
                return 1;
            }
            if (this.DlzkaVyroby == ((Hracka)obj).DlzkaVyroby)
            {
                return 0;
            }
            return -1;
        }
    }
}

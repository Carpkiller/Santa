using Santa.Abstract;

namespace Santa.Jadro
{
    public class Hracka : PrvokSystemu
    {
        public int Id { get; set; }
        public double PrichodDoSystemu { get; set; }
        public double VystupZoSystemu { get; set; }
        public double ZaciatokVyroby { get; set; }
        public int DlzkaVyroby { get; set; }
        public int ObsluhujuciElf { get; set; }

        public Hracka(int id, double prichod, int dlzkaVyroby)
        {
            Id = id;
            PrichodDoSystemu = prichod;
            DlzkaVyroby = dlzkaVyroby;
        }
    }
}

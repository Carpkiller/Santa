using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Santa.Abstract;
using Santa.EventsOpt;
using Santa.Serializer;

namespace Santa.Jadro
{
    public class JadroRand
    {
        private bool _koniec;
        private PriorFrontEventsOpt _kalendarUdalosti;
        private Hracka[] hracky;

        public JadroRand()
        {
            hracky = new Hracka[10000000];
            NacitajPrichodyHraciek(out hracky);
        }
        private ArrayList NacitajPrichodyHraciek(out Hracka[] hracky)
        {
            var pocMensi100 = 0;
            var pocMensi1000 = 0;
            var pocMensi20000 = 0;
            var pocVacsi20000 = 0;
            decimal totalMinutes = 0;
            hracky = new Hracka[10000000];

            var startDate = DateTime.Now;
            var list = new ArrayList();
            var reader = new StreamReader(File.OpenRead(@"toys_rev2.csv"));
            reader.ReadLine();
            int i = 0;
            while (!reader.EndOfStream)
            {

                var line = reader.ReadLine();
                var values = line.Split(',');
                var date = values[1].Split(' ');

                var hracka = new Hracka(int.Parse(values[0]),
                    new DateTime(int.Parse(date[0]), int.Parse(date[1]), int.Parse(date[2]), int.Parse(date[3]),
                        int.Parse(date[4]), 0).ToOADate(), int.Parse(values[2]), false);

                totalMinutes += int.Parse(values[2]);

                if (hracka.DlzkaVyroby < 100) pocMensi100++;
                if (hracka.DlzkaVyroby >= 100 && hracka.DlzkaVyroby < 1000) pocMensi1000++;
                if (hracka.DlzkaVyroby >= 1000 && hracka.DlzkaVyroby < 20000) pocMensi20000++;
                if (hracka.DlzkaVyroby >= 20000) pocVacsi20000++;

                hracky[i] = hracka;
                i++;
                list.Add(hracka);
                if (i == 10000000)
                {
                    break;
                }
            }
            var endDate = DateTime.Now;
            var duration = string.Format(CultureInfo.InvariantCulture, ": {0} ms",
                (endDate - startDate).TotalMilliseconds);
            Console.WriteLine("Dlzka nacitania " + duration);

            Console.WriteLine("Pocet pod 100      :" + pocMensi100);
            Console.WriteLine("Medzi 100 a 1000   :" + pocMensi1000);
            Console.WriteLine("Medzi 1000 a 20 000:" + pocMensi20000);
            Console.WriteLine("Nad 20000 :" + pocVacsi20000);

            Console.WriteLine(totalMinutes.ToString());


            list.Sort();
            hracky = new Hracka[10000000];
            hracky =  list.Cast<Hracka>().ToArray();
            return list;
        }
        public void Tried()
        {
            var triedic = new Triedic();
            list = triedic.Tried(hracky, 900);

            for (int i = 0; i < list.Count; i++)
            {
                Console.WriteLine(list[i].Id +"  -  "+list[i].DlzkaVyrobyVsetkych);
            }

            var pomList = new PomListRoztriedenychHraciek(list);

            Zapisovac zap = new Zapisovac();
            zap.WriteToBinaryFile("utriedeny.txt", pomList);
            zap.SerializeObject(pomList, "sdfgehdf.xml");
        }

        private Hracka[] ArrayHraciek;
        private UdalostOpt _aktualnaUdalost;
        private double _simCas;
        private List<ListRoztriedenychHraciek> list;

      

        private DateTime DostupnyOdPrvehoDna()
        {
            return new DateTime(2015, 1, 1, 9, 0, 0);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;

namespace Santa.Jadro
{
    public class Triedic
    {
        public List<ListRoztriedenychHraciek> Tried(Hracka[] listHraciek, int pocetSkupin)
        {
            var outputList = new List<ListRoztriedenychHraciek>();
            for (int i = 0; i < pocetSkupin; i++)
            {
                outputList.Add(new ListRoztriedenychHraciek(i+1));
            }

            for (int i = listHraciek.Length-1; i > 0; i--)
            {
                var index = NajdiNajkratsiuVyrobu(outputList);

                outputList[index].PridajHracku(listHraciek[i]);
            }

            return outputList;
        }

        private int NajdiNajkratsiuVyrobu(List<ListRoztriedenychHraciek> outputList)
        {
            int min = Int32.MaxValue;
            int index = 0;
            for (int i = 0; i < outputList.Count; i++)
            {
                if (outputList[i].DlzkaVyrobyVsetkych <= min)
                {
                    min = outputList[i].DlzkaVyrobyVsetkych;
                    index = i;
                }
            }

            return index;
        }

        public static List<Tuple<int, int>> SpracujHracky(Hracka[] arrayHraciek)
        {
            var list = new List<Tuple<int, int>>();
            var konstanty = new int[] {50, 100, 150, 182, 222, 271};

            foreach (var kons in konstanty)
            {
                for (int i = 0; i < arrayHraciek.Length; i++)
                {
                    if (arrayHraciek[i].DlzkaVyroby > kons)
                    {
                        list.Add(new Tuple<int, int>(kons, i-1));
                        //Console.WriteLine(arrayHraciek[i].DlzkaVyroby + " " + arrayHraciek[i-1].DlzkaVyroby+"  "+i);
                        break;
                    }

                }
            }

            return list;
        }
    }

}

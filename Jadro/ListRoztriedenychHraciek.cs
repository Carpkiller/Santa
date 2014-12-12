using System;
using System.Collections.Generic;

namespace Santa.Jadro
{
    [Serializable]
    public class ListRoztriedenychHraciek
    {
        public List<Hracka> ListHraciek;
        public int DlzkaVyrobyVsetkych { get; set; }
        public int Id { get; set; }

        public ListRoztriedenychHraciek(int id)
        {
            ListHraciek = new List<Hracka>();
            DlzkaVyrobyVsetkych = 0;
            Id = id;
        }

        public void PridajHracku(Hracka h)
        {
            ListHraciek.Add(h);
            DlzkaVyrobyVsetkych += h.DlzkaVyroby;
        }
    }

    [Serializable]
    public class PomListRoztriedenychHraciek
    {
        private List<ListRoztriedenychHraciek> list;

        public PomListRoztriedenychHraciek(List<ListRoztriedenychHraciek> list)
        {
            this.list = list;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Santa.Jadro;

namespace Santa.Abstract
{
    public abstract class UdalostOpt : PrvokSystemu
    {
        public Hracka Hracka { get; set; }

        public abstract void Vykonaj(JadroOpt jadro);
    }
}

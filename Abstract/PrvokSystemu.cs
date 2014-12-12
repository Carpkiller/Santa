using System;

namespace Santa.Abstract
{
    [Serializable]
    public abstract class PrvokSystemu
    {
        private double casVstupuDoSystemu;

        public double GetCas()
        {
            return casVstupuDoSystemu;
        }

        public void SetCas(double cas)
        {
            casVstupuDoSystemu = cas;
        }
    }
}

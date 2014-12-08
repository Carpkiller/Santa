using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using Santa.Abstract;
using Santa.EventsOpt;

namespace Santa.Jadro
{
    public class JadroOpt
    {
        private bool _koniec;
        private PriorFrontEventsOpt _kalendarUdalosti;
        private PriorFrontWorkers _listElfov;
        private PriorFrontHracka listHraciekUtriedeny;
        private Hracka[] ArrayHraciek;
        private UdalostOpt _aktualnaUdalost;
        private ArrayList _listPrichodov;
        //private List<Hracka> _listNezadanychVyrobkov;
        private Hracka[] _arrayHraciek;
        private int OdIndex;
        private int _aktualnyIndex = 0;
        private double _simCas;
        private List<Logovac> _listLog;
        private int indexHorny = 9999999;
        private int indexDolny = 0;

        private double PARAMETER1 = 3.5;

        public void Run()
        {
            Console.WriteLine("Start : "+DateTime.Now);
            var hracky = new Hracka[10000000];
            var vstup = NacitajPrichodyHraciek(out hracky);

            for (double i = 0.278; i <= 0.278; i+=0.002)
            {
                PARAMETER1 = i;
                _listPrichodov = vstup;
                _arrayHraciek = hracky;
                Init();
                var hracka = GetHracka();
                _kalendarUdalosti.Vloz(new EPrichodNovehoVyrobku(hracka, hracka.PrichodDoSystemu), hracka.PrichodDoSystemu);
                listHraciekUtriedeny.Vloz(hracka);
                Console.WriteLine("Zaciatok replikacie s hodnotou parametra i = "+i+" v case : "+DateTime.Now);
                while (!_koniec)
                {
                    _aktualnaUdalost = _kalendarUdalosti.Zmaz();

                    _simCas = _aktualnaUdalost.GetCas();
                    _aktualnaUdalost.Vykonaj(this);
                    var aktCas = DateTime.FromOADate(_simCas);
                    if (_kalendarUdalosti.IsEmpty())
                    {
                        _koniec = true;
                        //Console.WriteLine(DateTime.FromOADate(_simCas).ToString());
                        //Console.WriteLine(DateTime.FromOADate(_simCas).ToLongTimeString());
                    }

                    if (_aktualnyIndex%1000000 == 0)
                    {
                        //Console.WriteLine(_aktualnyIndex + "  " + DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToShortTimeString());
                    }

                    //Thread.Sleep(10);
                }
                Console.WriteLine(i + "  - koniec : " + _listLog.Last().StartDate.AddMinutes(_listLog.Last().Duration).ToString());
            }

            VytvorLogSubor();
        }

        private void Init()
        {
            indexHorny = 9999990;
            indexDolny = 0;
            _aktualnyIndex = 0;
            OdIndex = 0;
            _kalendarUdalosti = new KalendarUdalostiOpt();
            listHraciekUtriedeny = new PriorFrontHracka();
            //_listNezadanychVyrobkov = new List<Hracka>();
            //_arrayHraciek = new Hracka[10000000];
            _listElfov = new PriorFrontWorkers();
            _listLog = new List<Logovac>();

            for (int i = 0; i < 900; i++)
            {
                var elf = new Elf(true, 1, i + 1, DostupnyOdPrvehoDna());
                _listElfov.Vloz(elf, i);
                _kalendarUdalosti.Vloz(new EPrichodDoPrace(DostupnyOdPrvehoDna().ToOADate(), elf),
                    DostupnyOdPrvehoDna().ToOADate());
            }

            _kalendarUdalosti.Vloz(new EPrekonvertujHracky(), DostupnyOdPrvehoDna().AddHours(-1).ToOADate());
            _koniec = false;
        }

        private Hracka GetHracka()
        {
            //var hracka =(Hracka) _listPrichodov[_aktualnyIndex];
            var hracka = _arrayHraciek[_aktualnyIndex];
            _aktualnyIndex++;
            return hracka;
        }

        private DateTime DostupnyOdPrvehoDna()
        {
            return new DateTime(2015, 1, 1, 9, 0, 0);
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

            return list;
        }



        public void NaplanujKoniecPrace(Elf worker, Hracka hracka)
        {
            var dlzkaPraceMin = int.Parse(Math.Ceiling(hracka.DlzkaVyroby / worker.Vykonnost).ToString());
            _listLog.Add(new Logovac(hracka.Id, worker.Id, worker.ZaciatokPRace, dlzkaPraceMin));
            worker.JeVolny = false;
            var koniec = VypocitajKoniecPrace(worker, hracka);
            _kalendarUdalosti.Vloz(new EKoniecNaVyrobku(worker, hracka, koniec), koniec);
        }

        private double VypocitajKoniecPrace(Elf worker, Hracka hracka)
        {
            var dlzka = hracka.DlzkaVyroby / worker.Vykonnost;
            var dlzkaPraceMin = Math.Ceiling(hracka.DlzkaVyroby / worker.Vykonnost);
            hracka.DlzkaSkutocnejVyroby = int.Parse(dlzkaPraceMin.ToString());
            //var dlzkaPraceMin = hracka.DlzkaVyroby;
            //var dlzkaPrace = new DateTime(0, 0, 0, 0, dlzkaPraceMin, 0).ToOADate();
            var koniec = DateTime.FromOADate(_simCas).AddMinutes(dlzkaPraceMin);

            if (koniec.Second > 0)
            {
                var d = DateTime.FromOADate(_simCas);
                koniec = new DateTime(koniec.Year, koniec.Month, koniec.Day, koniec.Hour, koniec.Minute, 0).AddMinutes(1);
            }
            var prichod = koniec.ToOADate();
            if (worker.Id == 99)// && hracka.Id == 6586)
            {

            }
            //Console.WriteLine(DateTime.FromOADate(hracka.PrichodDoSystemu) +" - "+DateTime.FromOADate(prichod));
            return prichod;
        }

        public void NaplanujPrichodNovehoVyrobku()
        {
            if (_aktualnyIndex < 10000000)
            {
                var hracka = GetHracka();
                listHraciekUtriedeny.Vloz(hracka);
                _kalendarUdalosti.Vloz(new EPrichodNovehoVyrobku(hracka, hracka.PrichodDoSystemu), hracka.PrichodDoSystemu);
            }
        }

        public void ZacniPracuNaVyrobku(Hracka hracka)
        {
            if (_aktualnyIndex - OdIndex == 0 && (DateTime.FromOADate(_simCas).Hour < 19 && DateTime.FromOADate(_simCas).Hour >= 9))
            {
                var najlepsiWorker = NajdiVhodnehoWorkera(hracka);
                if (najlepsiWorker != null)
                {
                    hracka.Dokoncena = true;
                    _kalendarUdalosti.Vloz(new EZaciatokPraceNaVyrobku(najlepsiWorker, hracka, _simCas), _simCas);
                    OdIndex++;
                }
            }
        }

        private Elf NajdiVhodnehoWorkera(Hracka hracka)
        {
            var datumPrichodu = DateTime.FromOADate(hracka.PrichodDoSystemu);
            if (datumPrichodu >= new DateTime(datumPrichodu.Year, datumPrichodu.Month, datumPrichodu.Day, 9, 0, 0) &&
                datumPrichodu <= new DateTime(datumPrichodu.Year, datumPrichodu.Month, datumPrichodu.Day, 19, 0, 0))
            {
                if (!_listElfov.IsEmpty())
                {
                    return _listElfov.Get(DateTime.FromOADate(_simCas));
                }
            }

            return null;
        }

        public void PrichodElfaDoPrace(Elf elf)
        {
            if (DateTime.FromOADate(_simCas).TimeOfDay < new TimeSpan(19, 0, 0) &&
                DateTime.FromOADate(_simCas).TimeOfDay >= new TimeSpan(9, 0, 0))
            {
                Hracka hracka;
                if (elf.Vykonnost > PARAMETER1)
                {
                    hracka = PriradHracku(true); // najdlhsia vyroba
                }
                else
                {
                    hracka = PriradHracku(false); // najkratsia vyroba
                }

                if (hracka != null)
                {
                    hracka.Dokoncena = true;
                    _kalendarUdalosti.Vloz(new EZaciatokPraceNaVyrobku(elf, hracka, _simCas), _simCas);
                    OdIndex++;
                }
            }

        }


        private Hracka PriradHracku(bool najlhsia)
        {
            if (indexDolny == indexHorny+1)
            {
                //_koniec = true;
                return null;
            }
            if (najlhsia)
            {
                indexHorny--;
                return ArrayHraciek[indexHorny + 1];
            }
            else
            {
                indexDolny++;
                return ArrayHraciek[indexDolny - 1];
            }

            return null;
        }

        public void UvolniWorkera(Elf worker, Hracka hracka)
        {
            worker.JeVolny = true;

            var dlzkaPraceMin = Math.Ceiling(hracka.DlzkaVyroby / worker.Vykonnost);
            var koniecPrace = (worker.ZaciatokPRace).AddMinutes(dlzkaPraceMin);
            worker.DostupnyOd = koniecPrace;

            var sanctioned = 0.0;
            var unsanctioned = 0.0;
            PocitajHodiny(worker, hracka, false, out sanctioned, out unsanctioned);

            worker.Vykonnost = VypocitajRating(worker, hracka);

            if (worker.Vykonnost == 4 && DateTime.FromOADate(_simCas).Year == 2014)
            {
                var prichodNovyRok = new DateTime(2015, 1, 1, 9, 0, 0);
                _kalendarUdalosti.Vloz(new EPrichodDoPrace(prichodNovyRok.ToOADate(), worker), prichodNovyRok.ToOADate());
                return;
            }

            if ((DateTime.FromOADate(_simCas).Hour < 19 && DateTime.FromOADate(_simCas).Hour >= 9) &&
                (worker.DostupnyOd.Hour < 19 && worker.DostupnyOd.Hour >= 9) && _aktualnyIndex - OdIndex > 0 && unsanctioned == 0)
            {
                if (_arrayHraciek[OdIndex].PrichodDoSystemu < _simCas || DateTime.FromOADate(_simCas).Year > 2014)
                {
                    SpracujNezadaneHracky(worker);
                }
                return;
            }
            if (DateTime.FromOADate(_simCas).Hour >= 19 || DateTime.FromOADate(_simCas).Hour < 9 || unsanctioned > 0)
            {
                var pocetDni = 1;
                pocetDni += ((int)unsanctioned / 60) / 10;
                var pocetPresah = unsanctioned / 60 - (pocetDni - 1) * 10;

                var dostupnost = koniecPrace.AddDays(pocetDni).AddHours(pocetPresah);

                if (dostupnost.Hour >= 19)
                {
                    dostupnost = new DateTime(dostupnost.Year, dostupnost.Month, dostupnost.Day, 9, 0, 0).AddDays(1);
                }
                if (dostupnost.Hour < 9)
                {
                    dostupnost = new DateTime(dostupnost.Year, dostupnost.Month, dostupnost.Day, 9, 0, 0);
                }

                worker.JeVolny = true;
                worker.DostupnyOd = dostupnost;

                _kalendarUdalosti.Vloz(new EPrichodDoPrace(dostupnost.ToOADate(), worker),
                    dostupnost.ToOADate());
            }
        }

        private double VypocitajRating(Elf worker, Hracka hracka)
        {
            var sanctioned = 0.0;
            var unsanctioned = 0.0;
            if (worker.Id == 99)
            {

            }

            PocitajHodiny(worker, hracka, false, out sanctioned, out unsanctioned);

            var vykonnost = worker.Vykonnost * (Math.Pow(1.02, sanctioned / 60.0)) * (Math.Pow(0.9, unsanctioned / 60.0));

            if (vykonnost > 4)
                vykonnost = 4.0;
            if (vykonnost < 0.25)
                vykonnost = 0.25;


            if (worker.Id == 99)
            {

            }
            return vykonnost;
        }

        public void PocitajHodiny(Elf worker, Hracka hracka, bool rating, out double sanctioned, out double unsanctioned)
        {
            sanctioned = 0.0;
            unsanctioned = 0.0;
            var dlzkaPraceMin = 0.0;

            dlzkaPraceMin = hracka.DlzkaSkutocnejVyroby;

            var koniec = worker.ZaciatokPRace.AddMinutes(dlzkaPraceMin);
            //var pocetDni = (koniec - worker.ZaciatokPRace).Days;
            var pocetDni = 0;

            for (DateTime i = worker.ZaciatokPRace.Date; i < koniec.Date; i = i.AddDays(1))
            {
                //var e = i;
                pocetDni++;
            }
            var mc = koniec - worker.ZaciatokPRace;
            //sanctioned = (koniec - worker.ZaciatokPRace).TotalMinutes;

            var date =
                new DateTime(worker.ZaciatokPRace.Year, worker.ZaciatokPRace.Month, worker.ZaciatokPRace.Day, 19, 0, 0)
                    .AddDays(0);

            if (pocetDni > 0)
            {
                if (pocetDni > 1)
                {
                    sanctioned += 60 * 10 * (pocetDni - 1);
                    unsanctioned += 60 * 14 * (pocetDni - 1);
                }

                // posledny den do 9:00
                if (koniec.Day ==
                    new DateTime(worker.ZaciatokPRace.Year, worker.ZaciatokPRace.Month, worker.ZaciatokPRace.Day, 9, 0,
                        0).AddDays(pocetDni).Day &&
                    koniec <=
                    new DateTime(worker.ZaciatokPRace.Year, worker.ZaciatokPRace.Month, worker.ZaciatokPRace.Day, 9, 0,
                        0).AddDays(pocetDni))
                {
                    unsanctioned += (koniec - new DateTime(koniec.Year, koniec.Month, koniec.Day, 0, 0, 0)).TotalMinutes;
                }

                // posledny den medzi 9:00 a 19:00
                if (koniec.Day ==
                    new DateTime(worker.ZaciatokPRace.Year, worker.ZaciatokPRace.Month, worker.ZaciatokPRace.Day, 9, 0,
                        0).AddDays(pocetDni).Day &&
                    (koniec >
                     new DateTime(worker.ZaciatokPRace.Year, worker.ZaciatokPRace.Month, worker.ZaciatokPRace.Day, 9, 0,
                         0).AddDays(pocetDni) &&
                     koniec <=
                     new DateTime(worker.ZaciatokPRace.Year, worker.ZaciatokPRace.Month, worker.ZaciatokPRace.Day, 19, 0,
                         0).AddDays(pocetDni)))
                {
                    unsanctioned += 540;
                    sanctioned += (koniec - new DateTime(koniec.Year, koniec.Month, koniec.Day, 0, 0, 0)).TotalMinutes -
                                  540;
                }

                // posledny den koniec po 19:00
                if (koniec.Day ==
                    new DateTime(worker.ZaciatokPRace.Year, worker.ZaciatokPRace.Month, worker.ZaciatokPRace.Day, 9, 0,
                        0).AddDays(pocetDni).Day &&
                    (koniec >
                     new DateTime(worker.ZaciatokPRace.Year, worker.ZaciatokPRace.Month, worker.ZaciatokPRace.Day, 19, 0,
                         0).AddDays(pocetDni)))
                {
                    sanctioned += 600;
                    unsanctioned +=
                        (koniec - new DateTime(koniec.Year, koniec.Month, koniec.Day, 0, 0, 0)).TotalMinutes - 600;
                }

                if (koniec >
                    new DateTime(worker.ZaciatokPRace.Year, worker.ZaciatokPRace.Month, worker.ZaciatokPRace.Day, 19, 0,
                        0))
                {
                    unsanctioned += 300;
                    sanctioned +=
                        (new DateTime(worker.ZaciatokPRace.Year, worker.ZaciatokPRace.Month, worker.ZaciatokPRace.Day,
                            19, 0, 0) - worker.ZaciatokPRace).TotalMinutes;
                }
            }

            if (koniec.Day ==
                new DateTime(worker.ZaciatokPRace.Year, worker.ZaciatokPRace.Month, worker.ZaciatokPRace.Day, 19, 0,
                    0).Day &&
                koniec >
                new DateTime(worker.ZaciatokPRace.Year, worker.ZaciatokPRace.Month, worker.ZaciatokPRace.Day, 19, 0,
                    0))
            {
                unsanctioned +=
                    (koniec -
                     new DateTime(worker.ZaciatokPRace.Year, worker.ZaciatokPRace.Month, worker.ZaciatokPRace.Day,
                         19, 0, 0)).TotalMinutes;
                sanctioned +=
                    (new DateTime(worker.ZaciatokPRace.Year, worker.ZaciatokPRace.Month, worker.ZaciatokPRace.Day,
                        19, 0, 0) - worker.ZaciatokPRace).TotalMinutes;
            }
            if (koniec.Day ==
                new DateTime(worker.ZaciatokPRace.Year, worker.ZaciatokPRace.Month, worker.ZaciatokPRace.Day, 19, 0,
                    0).Day &&
                koniec <=
                new DateTime(worker.ZaciatokPRace.Year, worker.ZaciatokPRace.Month, worker.ZaciatokPRace.Day, 19, 0,
                    0))
            {
                sanctioned += (koniec - worker.ZaciatokPRace).TotalMinutes;
            }
            if (worker.Id == 513 && hracka.Id == 3400)
            {

            }
        }

        private void SpracujNezadaneHracky(Elf worker)
        {
            Hracka hracka;
            if (worker.Vykonnost == 4)
            {
                hracka = PriradHracku(true);  // najdlhsia vyroba
            }
            else
            {
                hracka = PriradHracku(false);  // najkratsia vyroba
            }

            if (hracka != null)
            {
                hracka.Dokoncena = true;
                _kalendarUdalosti.Vloz(new EZaciatokPraceNaVyrobku(worker, hracka, _simCas), _simCas);
                OdIndex++;
            }

            if (worker.Id == 513 && hracka.Id == 3400)
            {
                var d = DateTime.FromOADate(_simCas);
            }
        }

        private void VytvorLogSubor()
        {
            var c = _listLog.Count;
            //using (var outputStream = File.Create("example3.zip"))
            //using (var zipStream = new ZipOutputStream(outputStream))
            //{
            //    zipStream.SetLevel(6);
            //    zipStream.PutNextEntry(new ZipEntry("example.csv"));

            using (var writer = new StreamWriter("example10.csv"))
            {
                writer.WriteLine("ToyId,ElfId,StartTime,Duration");
                foreach (Logovac line in _listLog)
                {
                    writer.WriteLine(line.ToString());
                }
            }
            //  }


            //using (System.IO.StreamWriter file = new System.IO.StreamWriter(@"example.csv"))
            //{
            //    //var query = _listLog.GroupBy(x => x.ToyId).Where(g => g.Count() > 1).ToDictionary(x => x.Key, y => y.Count());
            //    file.WriteLine("ToyId,ElfId,StartTime,Duration");
            //    foreach (Logovac line in _listLog)
            //    {
            //        file.WriteLine(line.ToString());
            //    }
            //}
            Console.WriteLine("Vytvorenie suboru "+DateTime.Now);
        }

        public void PrekonvertujHracky()
        {
            ArrayHraciek = listHraciekUtriedeny.GetArray() as Hracka[];
            
            //var c = ArrayHraciek.Reverse().ToArray();
            //for (int i = 0; i < 1000; i++)
            //{
            //    Console.WriteLine(c[i].Id +" - "+c[i].DlzkaVyroby);
            //}
        }
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading;
using Santa.Abstract;
using Santa.Events;
using ICSharpCode.SharpZipLib.Zip;


namespace Santa.Jadro
{
    public class Jadro
    {
        private bool _koniec;
        private PriorFrontEvents _kalendarUdalosti;
        private PriorFrontWorkers _listElfov;
        private Udalost _aktualnaUdalost;
        private ArrayList _listPrichodov;
        //private List<Hracka> _listNezadanychVyrobkov;
        private Hracka[] _arrayHraciek;
        private int OdIndex;
        private int _aktualnyIndex = 0;
        private double _simCas;
        private List<Logovac> _listLog;
        public decimal PocetVolnych { get; set; }

        public delegate void ZmenaCasuHandler();

        public event ZmenaCasuHandler ZmenaCasu;

        public string SimulacnyCas
        {
            get { return DateTime.FromOADate(_simCas).ToString(); }
        }

        public string PocetVolnychElfov
        {
            get { return _listElfov.Count().ToString(); }
        }

        //public string PocetCakajucich { get { return _listNezadanychVyrobkov.Count.ToString(); } }
        public string PocetCakajucich
        {
            get { return (_aktualnyIndex - OdIndex).ToString(); }
        }

        public string AktualnyIndex
        {
            get { return _aktualnyIndex.ToString(); }
        }

        public delegate void ZmenaElfovHandler();

        public event ZmenaElfovHandler ZmenaElfov;

        public delegate void ZmenaIndexuHandler();

        public event ZmenaIndexuHandler ZmenaIndexu;

        public delegate void ZmenaPoctuCakajucichHandler();

        public event ZmenaPoctuCakajucichHandler ZmenaPoctuCakajucich;

        public void Run()
        {
            Console.WriteLine(DateTime.Now);
            Init();
            Console.WriteLine(DateTime.Now);
            while (!_koniec)
            {
                _aktualnaUdalost = _kalendarUdalosti.Zmaz();

                _simCas = _aktualnaUdalost.GetCas();
                if (ZmenaCasu != null) //vyvolani udalosti
                    ZmenaCasu();

                _aktualnaUdalost.Vykonaj(this);
                if (_kalendarUdalosti.IsEmpty())
                {
                    _koniec = true;
                    Console.WriteLine(DateTime.FromOADate(_simCas).ToLongDateString());
                    Console.WriteLine(DateTime.FromOADate(_simCas).ToLongTimeString());
                }

                if (_aktualnyIndex%1000000 == 0)
                {
                    //Console.WriteLine(_aktualnyIndex + "  " + DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToShortTimeString());
                }

                //Thread.Sleep(10);
            }
            Console.WriteLine(DateTime.Now);

            //VytvorLogSubor();
        }

        private void Init()
        {
            _aktualnyIndex = 0;
            PocetVolnych = 900;
            OdIndex = 0;
            _kalendarUdalosti = new KalendarUdalosti();
            //_listNezadanychVyrobkov = new List<Hracka>();
            _arrayHraciek = new Hracka[10000000];
            _listElfov = new PriorFrontWorkers();
            _listLog = new List<Logovac>();

            for (int i = 0; i < 900; i++)
            {
                var elf = new Elf(true, 1, i+1, DostupnyOdPrvehoDna());
                _listElfov.Vloz(elf, i);
                _kalendarUdalosti.Vloz(new EPrichodDoPrace(DostupnyOdPrvehoDna().ToOADate(), elf),
                    DostupnyOdPrvehoDna().ToOADate());
                if (ZmenaElfov != null) //vyvolani udalosti
                    ZmenaElfov();
            }
            _koniec = false;
            _listPrichodov = NacitajPrichodyHraciek();
            var hracka = GetHracka();
            _kalendarUdalosti.Vloz(new EPrichodNovehoVyrobku(hracka, false, hracka.PrichodDoSystemu),
                hracka.PrichodDoSystemu);
            //_kalendarUdalosti.Vloz(new EZaciatokPracovnehoDna(new DateTime(2014, 1, 1, 9, 0, 0).ToOADate()), new DateTime(2014, 1, 1, 9, 0, 0).ToOADate());
        }

        private DateTime DostupnyOdPrvehoDna()
        {
            return new DateTime(2014, 1, 1, 9, 0, 0);
        }

        private ArrayList NacitajPrichodyHraciek()
        {
            var pocMensi100 = 0;
            var pocMensi1000 = 0;
            var pocMensi20000 = 0;
            var pocVacsi20000 = 0;
            decimal totalMinutes = 0;

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
                        int.Parse(date[4]), 0).ToOADate(), int.Parse(values[2]));

                totalMinutes += int.Parse(values[2]);

                if (hracka.DlzkaVyroby < 100) pocMensi100++;
                if (hracka.DlzkaVyroby >= 100 && hracka.DlzkaVyroby < 1000) pocMensi1000++;
                if (hracka.DlzkaVyroby >= 1000 && hracka.DlzkaVyroby < 20000) pocMensi20000++;
                if (hracka.DlzkaVyroby >= 20000) pocVacsi20000++;

                _arrayHraciek[i] = hracka;
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

        private Hracka GetHracka()
        {
            //var hracka =(Hracka) _listPrichodov[_aktualnyIndex];
            var hracka = _arrayHraciek[_aktualnyIndex];
            _aktualnyIndex++;
            return hracka;
        }

        public void NaplanujPrichodNovehoVyrobku()
        {
            if (_aktualnyIndex < 10000000)
            {
                var hracka = GetHracka();
                //var hracka = _arrayHraciek[_aktualnyIndex];_
                _kalendarUdalosti.Vloz(new EPrichodNovehoVyrobku(hracka, false, hracka.PrichodDoSystemu),
                    hracka.PrichodDoSystemu);
                if (ZmenaIndexu != null) //vyvolani udalosti
                    ZmenaIndexu();
            }
        }

        public void ZacniPracuNaVyrobku(Hracka hracka)
        {
            if (_aktualnyIndex-OdIndex == 0 && (DateTime.FromOADate(_simCas).Hour < 19 && DateTime.FromOADate(_simCas).Hour >= 9))
            {
                var najlepsiWorker = NajdiVhodnehoWorkera(hracka);
                if (najlepsiWorker != null)
                {
                    _kalendarUdalosti.Vloz(new EZacatiePraceNaVyrobku(najlepsiWorker, hracka, _simCas), _simCas);
                    OdIndex++;
                }
                else
                {
                    //OdIndex++;
                    //_listNezadanychVyrobkov.Add(hracka);
                }
                //Console.WriteLine(najlepsiWorker.Id);
            }
            else
            {
                //OdIndex++;
                //_listNezadanychVyrobkov.Add(hracka);
                if (ZmenaPoctuCakajucich != null) //vyvolani udalosti
                    ZmenaPoctuCakajucich();
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
                    if (ZmenaElfov != null) //vyvolani udalosti
                        ZmenaElfov();
                    return _listElfov.Get(DateTime.FromOADate(_simCas));
                }
            }

            return null;
            //Console.WriteLine("No dosli ... ");
        }

        public void NaplanujKoniecPrace(Elf worker, Hracka hracka)
        {
            _listLog.Add(new Logovac(hracka.Id, worker.Id, worker.ZaciatokPRace, hracka.DlzkaVyroby));
            PocetVolnych--;
            worker.JeVolny = false;
            if (ZmenaElfov != null) //vyvolani udalosti
                ZmenaElfov();
            var koniec = VypocitajKoniecPrace(worker, hracka);
            _kalendarUdalosti.Vloz(new EKoniecPraceNaVyrobku(worker, hracka, koniec), koniec);
        }

        private double VypocitajKoniecPrace(Elf worker, Hracka hracka)
        {
            var dlzka = hracka.DlzkaVyroby/worker.Vykonnost;
            //var dlzkaPraceMin = Math.Ceiling(hracka.DlzkaVyroby / worker.Vykonnost);
            var dlzkaPraceMin = hracka.DlzkaVyroby;
            //var dlzkaPrace = new DateTime(0, 0, 0, 0, dlzkaPraceMin, 0).ToOADate();
            var koniec = DateTime.FromOADate(_simCas).AddMinutes(dlzkaPraceMin);
            if (worker.Id == 513)// && hracka.Id == 3400)
            {

            }
            if (koniec.Second > 0)
            {
                var d = DateTime.FromOADate(_simCas);
                koniec = new DateTime(koniec.Year, koniec.Month, koniec.Day, koniec.Hour, koniec.Minute, 0).AddMinutes(1);
            }
            var prichod = koniec.ToOADate();
            //Console.WriteLine(DateTime.FromOADate(hracka.PrichodDoSystemu) +" - "+DateTime.FromOADate(prichod));
            return prichod;
        }

        public void UvolniWorkera(Elf worker, Hracka hracka)
        {
            if (worker.Id == 513 && hracka.Id == 3400)
            {

            }
            //p' * (1.02)^n * (0.9)^m\]
            PocetVolnych++;
            worker.JeVolny = true;
            if (ZmenaElfov != null) //vyvolani udalosti
                ZmenaElfov();

            var presahHodin = 0.0;
            //var dlzkaPraceMin = Math.Ceiling(hracka.DlzkaVyroby/worker.Vykonnost);
            var dlzkaPraceMin = hracka.DlzkaVyroby;
            var koniecPrace = (worker.ZaciatokPRace).AddMinutes(dlzkaPraceMin);
            var porDate = (worker.ZaciatokPRace).Date;
            worker.DostupnyOd = koniecPrace;
            var ss = DateTime.FromOADate(_simCas);
            var date = (worker.ZaciatokPRace);
            var presah = koniecPrace - new DateTime(date.Year, date.Month, date.Day, 19, 0, 0);
            if (koniecPrace.Hour >= 19 && koniecPrace.Date == porDate.Date)
            {
                worker.DostupnyOd =
                    new DateTime(date.Year, date.Month, date.Day, 9, 0, 0).AddDays(1).Add(presah);
                presahHodin = presah.TotalHours;
            }

            //var dobryTime = (hracka.DlzkaVyroby/worker.Vykonnost)/60;
            //worker.Vykonnost = worker.Vykonnost*(Math.Pow(1.02, dobryTime))*(Math.Pow(0.9, presahHodin));
            worker.Vykonnost = VypocitajRating(worker, hracka);

            //if ((DateTime.FromOADate(_simCas).Hour < 19 && DateTime.FromOADate(_simCas).Hour >= 9) && (worker.DostupnyOd.Hour < 19 && worker.DostupnyOd.Hour >= 9) && _listNezadanychVyrobkov.Count > 0)
            if ((DateTime.FromOADate(_simCas).Hour < 19 && DateTime.FromOADate(_simCas).Hour >= 9) &&
                (worker.DostupnyOd.Hour < 19 && worker.DostupnyOd.Hour >= 9) && _aktualnyIndex - OdIndex > 0)
            {
                var ww = DateTime.FromOADate(_simCas);
                SpracujNezadaneHracky(worker);
                return;
            }
            if (DateTime.FromOADate(_simCas).Hour >= 19 || DateTime.FromOADate(_simCas).Hour < 9)
            {
                var kk = worker.ZaciatokPRace.AddMinutes(hracka.DlzkaVyroby);
                var sanctioned = 0.0;
                var unsanctioned = 0.0;
                PocitajHodiny(worker, hracka, false, out sanctioned, out unsanctioned);
                var pocetDni = ((int) unsanctioned/60)/10;
                var pocetPresah = unsanctioned/60 - pocetDni*10;

                var dostupnost =
                    new DateTime(koniecPrace.Year, koniecPrace.Month, koniecPrace.Day, 9, 0, 0).AddDays(pocetDni)
                        .AddHours(pocetPresah);
                if (dostupnost.Second > 0)
                {
                    //var d = DateTime.FromOADate(_simCas);
                    dostupnost = new DateTime(dostupnost.Year, dostupnost.Month, dostupnost.Day, dostupnost.Hour, dostupnost.Minute, 0).AddMinutes(1);
                }
                //PocetVolnych++;
                worker.JeVolny = true;
                //if (ZmenaElfov != null) //vyvolani udalosti
                //    ZmenaElfov();
                _kalendarUdalosti.Vloz(new EPrichodDoPrace(dostupnost.ToOADate(), worker),
                    dostupnost.ToOADate());
                return;
            }
            worker.JeVolny = true;
            //PocetVolnych++;
            //_listElfov.Vloz(worker);
            //if (ZmenaElfov != null) //vyvolani udalosti
            //    ZmenaElfov();
            //_kalendarUdalosti.Vloz(new EPrichodDoPrace(worker.DostupnyOd.ToOADate(), worker), worker.DostupnyOd.ToOADate());

        }

        private double VypocitajRating(Elf worker, Hracka hracka)
        {
            var sanctioned = 0.0;
            var unsanctioned = 0.0;
            PocitajHodiny(worker, hracka, true, out sanctioned, out unsanctioned);

            if (worker.Id == 513 && hracka.Id == 3400)
            {

            }

            var vykonnost = worker.Vykonnost*(Math.Pow(1.02, sanctioned/60.0))*(Math.Pow(0.9, unsanctioned/60.0));

            if (vykonnost > 4)
                vykonnost = 4.0;
            if (vykonnost < 0.25)
                vykonnost = 0.25;

            return vykonnost;
        }

        public void PocitajHodiny(Elf worker, Hracka hracka, bool rating, out double sanctioned, out double unsanctioned)
        {
            sanctioned = 0.0;
            unsanctioned = 0.0;
            var dlzkaPraceMin = 0.0;

            if (rating)
                dlzkaPraceMin = hracka.DlzkaVyroby;
            else
                dlzkaPraceMin = Math.Ceiling(hracka.DlzkaVyroby*worker.Vykonnost);

            var koniec = worker.ZaciatokPRace.AddMinutes(dlzkaPraceMin);
            //var pocetDni = (koniec - worker.ZaciatokPRace).Days;
            var pocetDni = 0;

            for (DateTime i = worker.ZaciatokPRace.Date; i < koniec.Date; i = i.AddDays(1))
            {
                var e = i;
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
                    sanctioned += 60*10*(pocetDni - 1);
                    unsanctioned += 60*14*(pocetDni - 1);
                }

                // posledny den do 9:00
                if (koniec.Day ==
                    new DateTime(worker.ZaciatokPRace.Year, worker.ZaciatokPRace.Month, worker.ZaciatokPRace.Day, 9, 0,
                        0).AddDays(pocetDni).Day &&
                    koniec <
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
                     koniec <
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
            //var hracka = _listNezadanychVyrobkov[0];
            var hracka = _arrayHraciek[OdIndex];
            OdIndex++;
            if (ZmenaPoctuCakajucich != null) //vyvolani udalosti
                ZmenaPoctuCakajucich();
            //var newList = new ArrayList();
            //newList.AddRange(_listNezadanychVyrobkov.GetRange(1, _listNezadanychVyrobkov.Count-1));
            //_listNezadanychVyrobkov = new ArrayList(newList);

            //OsdstranHracku1();       -> zvysenie OdIndexu o 1

            //_listNezadanychVyrobkov.Remove(hracka);
            if (worker.Id == 513 && hracka.Id == 3400)
            {
                var d = DateTime.FromOADate(_simCas);
            }
            _kalendarUdalosti.Vloz(new EZacatiePraceNaVyrobku(worker, hracka, _simCas), _simCas);
            //_kalendarUdalosti.Vloz(new EPrichodNovehoVyrobku(hracka, true, _simCas), _simCas);
            //ZacniPracuNaVyrobku(hracka);
        }

        private void OsdstranHracku1()
        {
            //_listNezadanychVyrobkov[0] = null;
            //_listNezadanychVyrobkov.RemoveAll(x => x == null);

            //if (ZmenaPoctuCakajucich != null) //vyvolani udalosti
            //    ZmenaPoctuCakajucich();
        }

        public void SpustVyrobu()
        {
            //var count = _listElfov.Count() >= _listNezadanychVyrobkov.Count ? _listNezadanychVyrobkov.Count : _listElfov.Count();
            //for (int i = 0; i < count; i++)
            //{
            //    _kalendarUdalosti.Vloz(new EPrichodNovehoVyrobku(_listNezadanychVyrobkov[i], true, _simCas), _simCas);
            //    OsdstranHracku1();
            //    //Thread.Sleep(30);
            //}
            // _kalendarUdalosti.Vloz(new EZaciatokPracovnehoDna(DateTime.FromOADate(_simCas).AddDays(1).ToOADate()), DateTime.FromOADate(_simCas).AddDays(1).ToOADate());
        }

        public void PrichodElfaDoPrace(Elf elf)
        {
            if (DateTime.FromOADate(_simCas).TimeOfDay < new TimeSpan(19, 0, 0) &&
                DateTime.FromOADate(_simCas).TimeOfDay >= new TimeSpan(9, 0, 0))
            {
                //if (_listNezadanychVyrobkov.Count > 0)
                if (_aktualnyIndex - OdIndex > 0)
                {
                    if (elf.JeVolny)
                    {
                        _kalendarUdalosti.Vloz(new EZacatiePraceNaVyrobku(elf, _arrayHraciek[OdIndex], _simCas), _simCas);
                        OdIndex++;
                    }
                    //OsdstranHracku1();
                    if (ZmenaPoctuCakajucich != null) //vyvolani udalosti
                        ZmenaPoctuCakajucich();
                }
            }
        }

        private void VytvorLogSubor()
        {
            var c = _listLog.Count;
            using (var outputStream = File.Create("example2.zip"))
            using (var zipStream = new ZipOutputStream(outputStream))
            {
                zipStream.SetLevel(6);
                zipStream.PutNextEntry(new ZipEntry("example.csv"));

                using (var writer = new StreamWriter(zipStream))
                {
                    writer.WriteLine("ToyId,ElfId,StartTime,Duration");
                    foreach (Logovac line in _listLog)
                    {
                        writer.WriteLine(line.ToString());
                    }
                }
            }


            //using (System.IO.StreamWriter file = new System.IO.StreamWriter(@"example.csv"))
            //{
            //    //var query = _listLog.GroupBy(x => x.ToyId).Where(g => g.Count() > 1).ToDictionary(x => x.Key, y => y.Count());
            //    file.WriteLine("ToyId,ElfId,StartTime,Duration");
            //    foreach (Logovac line in _listLog)
            //    {
            //        file.WriteLine(line.ToString());
            //    }
            //}
            Console.WriteLine(DateTime.Now);
        }
    }
}

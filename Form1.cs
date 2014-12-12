using System;
using System.Threading;
using System.Windows.Forms;
using Santa.Jadro;

namespace Santa
{
    public partial class Form1 : Form
    {
        private Jadro.JadroOpt _jadro;
        public Form1()
        {
            InitializeComponent();
            _jadro = new Jadro.JadroOpt();
        }


        private void button1_Click(object sender, EventArgs e)
        {
            //_jadro.ZmenaCasu += ZmenaStavuCasu;
            //_jadro.ZmenaElfov += ZmenaPoctuElfov;
            //_jadro.ZmenaIndexu += ZmenaIndexu;
            //_jadro.ZmenaPoctuCakajucich += ZmenaPoctuCakajucich;
            textBoxOutput.Text = "";
            _jadro.ZmenaReplikacie += ZmenaReplikacie;
            if (checkBox1.Checked)
            {
                Thread t = new Thread(new ThreadStart(_jadro.Run));
                t.Start();
            }
            else
            {
                Thread t = new Thread(new ParameterizedThreadStart(_jadro.Run));
                var inp = new Tuple<string, string>(textBoxParameter1.Text, textBoxParameter2.Text); 
                t.Start(inp);
            }
            
        }

        private void ZmenaReplikacie()
        {
            if (InvokeRequired)
            {
                Invoke(new MethodInvoker(delegate
                {
                    textBoxOutput.AppendText(_jadro.OutputText);
                }));
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //JadroRand jadro = new JadroRand();
            _jadro.Run2();
        }

        //private void ZmenaPoctuCakajucich()
        //{
        //    if (InvokeRequired)
        //    {
        //        Invoke(new MethodInvoker(delegate
        //        {
        //            textBoxPocetCakajucich.Text = _jadro.PocetCakajucich;
        //        }));
        //    }
        //}

        //private void ZmenaIndexu()
        //{
        //    if (InvokeRequired)
        //    {
        //        Invoke(new MethodInvoker(delegate
        //        {
        //            textBoxIndexHracky.Text = _jadro.AktualnyIndex;
        //        }));
        //    }
        //}

        //private void ZmenaPoctuElfov()
        //{
        //    if (InvokeRequired)
        //    {
        //        Invoke(new MethodInvoker(delegate
        //        {
        //            textBoxPocetElfov.Text = _jadro.PocetVolnych.ToString();
        //        }));
        //    }
        //}

        //private void ZmenaStavuCasu()
        //{
        //    if (InvokeRequired)
        //    {
        //        Invoke(new MethodInvoker(delegate
        //        {
        //            labelSimulacnyCas.Text = "Aktualny simulacny cas : " + _jadro.SimulacnyCas; }));
        //    }
        //}
    }
}

using System;
using System.Threading;
using System.Windows.Forms;

namespace Santa
{
    public partial class Form1 : Form
    {
        private Jadro.Jadro _jadro;
        public Form1()
        {
            InitializeComponent();
            _jadro = new Jadro.Jadro();
        }


        private void button1_Click(object sender, EventArgs e)
        {
            //_jadro.ZmenaCasu += ZmenaStavuCasu;
            //_jadro.ZmenaElfov += ZmenaPoctuElfov;
            //_jadro.ZmenaIndexu += ZmenaIndexu;
            //_jadro.ZmenaPoctuCakajucich += ZmenaPoctuCakajucich;
            Thread t = new Thread(new ThreadStart(_jadro.Run));
            t.Start();
        }

        private void ZmenaPoctuCakajucich()
        {
            if (InvokeRequired)
            {
                Invoke(new MethodInvoker(delegate
                {
                    textBoxPocetCakajucich.Text = _jadro.PocetCakajucich;
                }));
            }
        }

        private void ZmenaIndexu()
        {
            if (InvokeRequired)
            {
                Invoke(new MethodInvoker(delegate
                {
                    textBoxIndexHracky.Text = _jadro.AktualnyIndex;
                }));
            }
        }

        private void ZmenaPoctuElfov()
        {
            if (InvokeRequired)
            {
                Invoke(new MethodInvoker(delegate
                {
                    textBoxPocetElfov.Text = _jadro.PocetVolnych.ToString();
                }));
            }
        }

        private void ZmenaStavuCasu()
        {
            if (InvokeRequired)
            {
                Invoke(new MethodInvoker(delegate
                {
                    labelSimulacnyCas.Text = "Aktualny simulacny cas : " + _jadro.SimulacnyCas; }));
            }
        }
    }
}

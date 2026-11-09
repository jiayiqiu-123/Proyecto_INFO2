using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SimuladorDeWindowsForm
{
    public partial class FormDistancia : Form
    {
        string a ;
        public FormDistancia()
        {
            InitializeComponent();
        }

        public void SetText(string a)
        {
            this.a = a;
        }
        private void FormDistancia_Load(object sender, EventArgs e)
        {
            labelDistancia.Text = a;
        }

        private void labelDistancia_Click(object sender, EventArgs e)
        {

        }
    }
}

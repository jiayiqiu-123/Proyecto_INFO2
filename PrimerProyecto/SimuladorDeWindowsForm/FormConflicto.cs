using FlightLib;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;

namespace SimuladorDeWindowsForm
{
    public partial class FormConflicto : Form
    {
        FlightPlanList ListaConflictos = new FlightPlanList();
        public FormConflicto()
        {
            InitializeComponent();
        }

        public void SetListaConflictos(FlightPlanList listaConflictos)
        {
            this.ListaConflictos = listaConflictos;
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            ListaConflictos.ResolverConflictos();
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}

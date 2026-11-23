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

        // Recibe la lista con los vuelos que entrarán en conflicto.
        public void SetListaConflictos(FlightPlanList listaConflictos)
        {
            this.ListaConflictos = listaConflictos;
        }

        // Resuelve los conflictos detectados y cierra el formulario.
        private void btnResolver_Click(object sender, EventArgs e)
        {
            ListaConflictos.ResolverConflictos();
            this.Close();
        }

        // Cierra el formulario sin resolver los conflictos.
        private void btnSinResolver_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}

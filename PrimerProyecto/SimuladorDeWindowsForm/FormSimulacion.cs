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

namespace SimuladorDeWindowsForm
{
    public partial class FormSimulacion : Form
    {
        private FlightPlan vuelo1;
        private FlightPlan vuelo2;
        private double tiempoCiclo;
        public FormSimulacion()
        {
            InitializeComponent();
        }

        public FormSimulacion(FlightPlan vuelo1, FlightPlan vuelo2, double tiempoCiclo)
        {
            InitializeComponent();

            this.vuelo1 = vuelo1;
            this.vuelo2 = vuelo2;
            panelEspacioAereo.MouseClick += panelEspacioAereo_MouseClick;
            this.tiempoCiclo = tiempoCiclo;
        }
        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            if (vuelo1 == null || vuelo2 == null)
                return;

            Position posicion1 = vuelo1.GetCurrentPosition();
            Position posicion2 = vuelo2.GetCurrentPosition();

            int x1 = (int)posicion1.GetX();
            int y1 = (int)posicion1.GetY();

            int x2 = (int)posicion2.GetX();
            int y2 = (int)posicion2.GetY();

            e.Graphics.FillEllipse(Brushes.Blue, x1, y1, 12, 12);
            e.Graphics.FillEllipse(Brushes.Red, x2, y2, 12, 12);
        }

        private void FormSimulacion_Load(object sender, EventArgs e)
        {

        }

        private void btnMover_Click(object sender, EventArgs e)
        {
            vuelo1.Mover(tiempoCiclo, panelEspacioAereo.Width - 12, panelEspacioAereo.Height - 12);
            vuelo2.Mover(tiempoCiclo, panelEspacioAereo.Width - 12, panelEspacioAereo.Height - 12);

            panelEspacioAereo.Invalidate();
        }
        private void panelEspacioAereo_MouseClick(object sender, MouseEventArgs e)
        {
            Position posicion1 = vuelo1.GetCurrentPosition();

            int x1 = (int)posicion1.GetX();
            int y1 = (int)posicion1.GetY();

            if (e.X >= x1 && e.X <= x1 + 12 &&
                e.Y >= y1 && e.Y <= y1 + 12)
            {
                FormInformacionAvion formulario = new FormInformacionAvion(vuelo1);
                formulario.ShowDialog();
                return;
            }

            Position posicion2 = vuelo2.GetCurrentPosition();

            int x2 = (int)posicion2.GetX();
            int y2 = (int)posicion2.GetY();

            if (e.X >= x2 && e.X <= x2 + 12 &&
                e.Y >= y2 && e.Y <= y2 + 12)
            {
                FormInformacionAvion formulario = new FormInformacionAvion(vuelo2);
                formulario.ShowDialog();
            }
        }

        private void label4_Click(object sender, EventArgs e)
        {

        }
    }
}

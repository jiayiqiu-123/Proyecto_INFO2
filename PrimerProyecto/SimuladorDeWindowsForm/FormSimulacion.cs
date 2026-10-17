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
        float dSeguridad;

        public FormSimulacion()
        {
            InitializeComponent();
        }
        
        public void SetDSeguridad(float dSeguridad)
        {
            this.dSeguridad = dSeguridad;
        }

        public void SetTiempoCiclo(double tiempoCiclo)
        {
            this.tiempoCiclo = tiempoCiclo;
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
            System.Drawing.Graphics graphics = e.Graphics;
            if (vuelo1 == null || vuelo2 == null)
                return;

            // Posiciones actuales de los aviones
            Position posicion1 = vuelo1.GetCurrentPosition();
            Position posicion2 = vuelo2.GetCurrentPosition();

            int x1 = (int)posicion1.GetX();
            int y1 = (int)posicion1.GetY();

            int x2 = (int)posicion2.GetX();
            int y2 = (int)posicion2.GetY();

            // ==========================================
            // FASE 6: DIBUJAR TRAYECTORIA
            // ==========================================
            // Aquí definimos el grosor a 1 para que sea más fina
            Pen lapizTrayectoria1 = new Pen(Color.Blue, 1);
            Pen lapizTrayectoria2 = new Pen(Color.Red, 1);

            // Coordenadas Vuelo 1
            float OrigenX1 = Convert.ToSingle(vuelo1.GetInitialPosition().GetX());
            float OrigenY1 = Convert.ToSingle(vuelo1.GetInitialPosition().GetY());
            float FinalX1 = Convert.ToSingle(vuelo1.GetFinalPosition().GetX());
            float FinalY1 = Convert.ToSingle(vuelo1.GetFinalPosition().GetY());

            // Coordenadas Vuelo 2
            float OrigenX2 = Convert.ToSingle(vuelo2.GetInitialPosition().GetX());
            float OrigenY2 = Convert.ToSingle(vuelo2.GetInitialPosition().GetY());
            float FinalX2 = Convert.ToSingle(vuelo2.GetFinalPosition().GetX());
            float FinalY2 = Convert.ToSingle(vuelo2.GetFinalPosition().GetY());

            // Dibujamos las dos líneas de trayectoria
            graphics.DrawLine(lapizTrayectoria1, OrigenX1, OrigenY1, FinalX1, FinalY1);
            graphics.DrawLine(lapizTrayectoria2, OrigenX2, OrigenY2, FinalX2, FinalY2);

            // Dibujamos los aviones DESPUÉS de las líneas para que se vean por encima
            e.Graphics.FillEllipse(Brushes.Blue, x1, y1, 12, 12);
            e.Graphics.FillEllipse(Brushes.Red, x2, y2, 12, 12);

            // ==========================================
            // FASE 7: DISTANCIA DE SEGURIDAD
            // ==========================================
            Pen lapizSeguridad = new Pen(Color.Red, 1);
            lapizSeguridad.DashStyle = System.Drawing.Drawing2D.DashStyle.Dash; // Línea de rayas

            float diametro = dSeguridad * 2;

            // Vuelo 1: Sumamos 6 a la posición actual para calcular el centro exacto del avión (que mide 12x12)
            float esquinaX1 = (x1 + 6) - dSeguridad;
            float esquinaY1 = (y1 + 6) - dSeguridad;
            e.Graphics.DrawEllipse(lapizSeguridad, esquinaX1, esquinaY1, diametro, diametro);

            // Vuelo 2: Hacemos lo mismo para el segundo avión
            float esquinaX2 = (x2 + 6) - dSeguridad;
            float esquinaY2 = (y2 + 6) - dSeguridad;
            e.Graphics.DrawEllipse(lapizSeguridad, esquinaX2, esquinaY2, diametro, diametro);

        }

        private void FormSimulacion_Load(object sender, EventArgs e)
        {

        }

        private void MoverAviones()
        {
            if (vuelo1 == null || vuelo2 == null)
                return;

            vuelo1.Mover(tiempoCiclo, panelEspacioAereo.ClientSize.Width - 12, panelEspacioAereo.ClientSize.Height - 12);
            vuelo2.Mover(tiempoCiclo, panelEspacioAereo.ClientSize.Width - 12, panelEspacioAereo.ClientSize.Height - 12);

            panelEspacioAereo.Invalidate();
        }

        private void btnMover_Click(object sender, EventArgs e)
        {
            MoverAviones();
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

        private void btnIniciarAutomatico_Click(object sender, EventArgs e)
        {
            if (timerSimulacion.Enabled == false)
            {
                timerSimulacion.Start();
                btnSimulacionAutomatica.Text = "Detener simulación automática";
                btnMover.Enabled = false;
            }
            else
            {
                timerSimulacion.Stop();
                btnSimulacionAutomatica.Text = "Iniciar simulación automática";
                btnMover.Enabled = true;
            }


    }

        private void timerSimulacion_Tick(object sender, EventArgs e)
        {
            MoverAviones();
        }

        private void buttonVerTablaVuelos_Click(object sender, EventArgs e)
        {
            FormTablaVuelos fTabla = new FormTablaVuelos();
            // Le pasamos los dos vuelos que tienes en la simulación
            fTabla.p1 = this.vuelo1;
            fTabla.p2 = this.vuelo2;
            fTabla.ShowDialog();
        }
    }
}

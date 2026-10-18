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
        FlightPlan vuelo1;
        FlightPlan vuelo2;
        private double tiempoCiclo;
        float dSeguridad;
        bool enConflicto;
        // esto me servirá como variable para el conflicto, en el buttonPredicción

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

            // añadimos para poder detectar conflictos y usarlo en el buttonPredicción
            bool hayConflicto = vuelo1.Conflicto(vuelo2, dSeguridad);
            // creamos la variable "hayConflicto" en booleano, su valor lo decidirá el método Conflicto que ya tenemos (midiendo dist1 y dist2)
            // si dist<dseguridad, hay conflicto, si no, no hay conflicto
            if (hayConflicto == true && enConflicto == false)
                // esto para asegurarnos de que antes no había conflicto
            {
                enConflicto = true;
                MessageBox.Show("¡CUIDADO, los vuelos están a una distancia inferior a la de seguridad!");
            }
            if (hayConflicto == false)
            {
                enConflicto = false;
            }
            // al ponerlo en 2 diferentes, cada vez que se mueva irán comprobando verdad? !
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

        private void buttonPrediccion_Click(object sender, EventArgs e)
        {
            // Copias de los vuelos para cálculo analítico (no modifican los originales)
            Position actual1 = vuelo1.GetCurrentPosition(); // posición actual vuelo1
            Position final1 = vuelo1.GetFinalPosition();    // posición final vuelo1
            FlightPlan copia1 = new FlightPlan(vuelo1.GetId(), actual1.GetX(), actual1.GetY(), final1.GetX(), final1.GetY(), vuelo1.GetVelocidad());

            Position actual2 = vuelo2.GetCurrentPosition(); // posición actual vuelo2
            Position final2 = vuelo2.GetFinalPosition();    // posición final vuelo2
            FlightPlan copia2 = new FlightPlan(vuelo2.GetId(), actual2.GetX(), actual2.GetY(), final2.GetX(), final2.GetY(), vuelo2.GetVelocidad());

            double tmin;
            double minDist = ComputeMinDistanceBetweenPlans(copia1, copia2, out tmin);

            if (minDist < dSeguridad)
            {
                MessageBox.Show("Los vuelos SÍ entrarán en conflicto. Dist mínima=" + minDist.ToString("F2") + " en t=" + tmin.ToString("F2"));
            }
            else
            {
                MessageBox.Show("Los vuelos NO entrarán en conflicto. Dist mínima=" + minDist.ToString("F2") + " en t=" + tmin.ToString("F2"));
            }
        }

        // Calcula la distancia mínima entre dos trayectorias rectilíneas y devuelve tMin (instante donde ocurre)
        // Implementación simple: sólo ifs, fors, arrays y operaciones básicas, sin delegates
        public double ComputeMinDistanceBetweenPlans(FlightPlan p1, FlightPlan p2, out double tMin)
        {
            double x10 = p1.GetInitialPosition().GetX();
            double y10 = p1.GetInitialPosition().GetY();
            double x1f = p1.GetFinalPosition().GetX();
            double y1f = p1.GetFinalPosition().GetY();
            double x20 = p2.GetInitialPosition().GetX();
            double y20 = p2.GetInitialPosition().GetY();
            double x2f = p2.GetFinalPosition().GetX();
            double y2f = p2.GetFinalPosition().GetY();

            double vx1 = x1f - x10; double vy1 = y1f - y10;
            double L1 = Math.Sqrt(vx1 * vx1 + vy1 * vy1);
            double ux1 = 0; double uy1 = 0;
            if (L1 > 0) { ux1 = vx1 / L1; uy1 = vy1 / L1; }

            double vx2 = x2f - x20; double vy2 = y2f - y20;
            double L2 = Math.Sqrt(vx2 * vx2 + vy2 * vy2);
            double ux2 = 0; double uy2 = 0;
            if (L2 > 0) { ux2 = vx2 / L2; uy2 = vy2 / L2; }

            double v1 = p1.GetVelocidad();
            double v2 = p2.GetVelocidad();

            double T1 = (L1 == 0 || v1 == 0) ? 0 : (L1 * 60.0 / v1);
            double T2 = (L2 == 0 || v2 == 0) ? 0 : (L2 * 60.0 / v2);

            double bestD2 = double.MaxValue;
            double bestT = 0;

            // Evaluar intervalo [start,end] para Cx,Cy,Wx,Wy sin usar delegates
            void EvalInterval(double Cx, double Cy, double Wx, double Wy, double start, double end)
            {
                double a = Wx * Wx + Wy * Wy;
                double b = 2.0 * (Cx * Wx + Cy * Wy);
                double tCandidate = start;
                if (Math.Abs(a) > 1e-9)
                {
                    double t0 = -b / (2.0 * a);
                    if (t0 < start) tCandidate = start;
                    else if (t0 > end) tCandidate = end;
                    else tCandidate = t0;
                }
                double[] times = new double[3]; times[0] = start; times[1] = end; times[2] = tCandidate;
                for (int i = 0; i < 3; i++)
                {
                    double t = times[i];
                    double dx = Cx + Wx * t;
                    double dy = Cy + Wy * t;
                    double d2 = dx * dx + dy * dy;
                    if (d2 < bestD2) { bestD2 = d2; bestT = t; }
                }
            }

            // Ambos moviéndose
            double tA = (T1 < T2) ? T1 : T2;
            if (tA > 0)
            {
                double Cx = x10 - x20;
                double Cy = y10 - y20;
                double Wx = ux1 * (v1 / 60.0) - ux2 * (v2 / 60.0);
                double Wy = uy1 * (v1 / 60.0) - uy2 * (v2 / 60.0);
                EvalInterval(Cx, Cy, Wx, Wy, 0, tA);
            }

            // Uno llegó antes
            if (T1 < T2)
            {
                double start = T1; double end = T2;
                if (end > start)
                {
                    double Cx = x1f - x20;
                    double Cy = y1f - y20;
                    double Wx = - ux2 * (v2 / 60.0);
                    double Wy = - uy2 * (v2 / 60.0);
                    EvalInterval(Cx, Cy, Wx, Wy, start, end);
                }
            }
            else if (T2 < T1)
            {
                double start = T2; double end = T1;
                if (end > start)
                {
                    double Cx = x10 - x2f;
                    double Cy = y10 - y2f;
                    double Wx = ux1 * (v1 / 60.0);
                    double Wy = uy1 * (v1 / 60.0);
                    EvalInterval(Cx, Cy, Wx, Wy, start, end);
                }
            }

            // Ambos en destino
            double tEnd = (T1 > T2) ? T1 : T2;
            double dxEnd = x1f - x2f;
            double dyEnd = y1f - y2f;
            double d2End = dxEnd * dxEnd + dyEnd * dyEnd;
            if (d2End < bestD2) { bestD2 = d2End; bestT = tEnd; }

            tMin = bestT;
            return Math.Sqrt(bestD2);
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }
    }
}

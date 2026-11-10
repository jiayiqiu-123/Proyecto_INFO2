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
        FlightPlanList ListaVuelos = new FlightPlanList();
        double tiempoCiclo;
        float dSeguridad;
        bool enConflicto = false;
        // esto me servirá como variable para el conflicto, en el buttonPredicción

        public FormSimulacion()
        {
            InitializeComponent();
            // Doble búfer para evitar parpadeo
            this.DoubleBuffered = true;
            typeof(Panel).InvokeMember("DoubleBuffered",
                System.Reflection.BindingFlags.SetProperty |
                System.Reflection.BindingFlags.Instance |
                System.Reflection.BindingFlags.NonPublic,
                null, panelEspacioAereo, new object[] { true });

            // Enlazar el clic por código (evita problemas con el diseñador)
            panelEspacioAereo.MouseClick -= panelEspacioAereo_MouseClick;
            panelEspacioAereo.MouseClick += panelEspacioAereo_MouseClick;
        }

        public void SetDSeguridad(float dSeguridad)
        {
            this.dSeguridad = dSeguridad;
        }

        public void SetTiempoCiclo(double tiempoCiclo)
        {
            this.tiempoCiclo = tiempoCiclo;
        }

        public void SetListaVuelos(FlightPlanList listaVuelos)
        {
            this.ListaVuelos = listaVuelos;
        }
        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            Graphics graphics = e.Graphics;

            for (int i = 0; i < ListaVuelos.GetNumber(); i++)
            {
                FlightPlan vuelo = ListaVuelos.GetFlightPlan(i);
                if (vuelo == null)
                {
                    return;
                }
                else
                {
                    // Definir posiciones iniciales y finales de cada vuelo
                    Position posicion = vuelo.GetCurrentPosition();
                    int x = (int)posicion.GetX();
                    int y = (int)posicion.GetY();

                    float OrigenX = Convert.ToSingle(vuelo.GetInitialPosition().GetX() + 6);
                    float OrigenY = Convert.ToSingle(vuelo.GetInitialPosition().GetY() + 6);
                    float FinalX = Convert.ToSingle(vuelo.GetFinalPosition().GetX() + 6);
                    float FinalY = Convert.ToSingle(vuelo.GetFinalPosition().GetY() + 6);

                    // Asignar un color único a cada vuelo
                    vuelo.SetRandomColor();
                    Color color = vuelo.GetColor();
                    using (Pen lapizTrayectoria = new Pen(color, 1))
                    {
                        graphics.DrawLine(lapizTrayectoria, OrigenX, OrigenY, FinalX, FinalY);
                    }

                    float diametro = dSeguridad * 2;
                    float esquinaX = x + 6 - dSeguridad;
                    float esquinaY = y + 6 - dSeguridad;

                    using (Pen lapizSeguridad = new Pen(Color.OrangeRed, 1))
                    {
                        lapizSeguridad.DashStyle = System.Drawing.Drawing2D.DashStyle.Dash;
                        graphics.DrawEllipse(lapizSeguridad, esquinaX, esquinaY, diametro, diametro);
                    }

                    if (enConflicto)
                    {
                        using (Brush brochaConflicto = new SolidBrush(Color.OrangeRed))
                        {
                            graphics.FillEllipse(brochaConflicto, esquinaX, esquinaY, diametro, diametro);
                        }
                    }
                    float radioPunto = 3;
                    using (Brush brochaPunto = new SolidBrush(color))
                    {
                        graphics.FillEllipse(brochaPunto, x + 6 - radioPunto, y + 6 - radioPunto, radioPunto * 2, radioPunto * 2);
                    }
                }
            }
        }

        private void FormSimulacion_Load(object sender, EventArgs e)
        {
            if (timerSimulacion.Enabled == true)
            {
                timerSimulacion.Stop();
                btnSimulacionAutomatica.Text = "Iniciar simulación automática";
                btnMover.Enabled = true;
            }
            for (int i = 0; i < ListaVuelos.GetNumber(); i++)
            {
                ListaVuelos.GetFlightPlan(i).Restart();
                ListaVuelos.GetFlightPlan(i).RestaurarVelocidadesOriginales();
            }
            panelEspacioAereo.Invalidate();

                    FlightPlanList hayConflicto = ListaVuelos.EncontrarConflicto();
                    if (hayConflicto != null)
                    {
                        FormConflicto f = new FormConflicto();
                        f.SetListaConflictos(hayConflicto);
                        f.ShowDialog();
                    }


        }
        private void btnMover_Click(object sender, EventArgs e)
        {
            ListaVuelos.MoverTodosVuelos();
            enConflicto = ListaVuelos.HayConflictoTiempoReal();
            panelEspacioAereo.Invalidate();
        }
        private void panelEspacioAereo_MouseClick(object sender, MouseEventArgs e)
        {
            if (ListaVuelos == null)
                return;

            for (int i = ListaVuelos.GetNumber() - 1; i >= 0; i--)
            {
                FlightPlan vuelo = ListaVuelos.GetFlightPlan(i);
                if (vuelo == null)
                    continue;

                Position posicion = vuelo.GetCurrentPosition();

                // Centro del avión (igual que en Paint: x + 6, y + 6)
                double centroX = posicion.GetX() + 6;
                double centroY = posicion.GetY() + 6;

                double dx = e.X - centroX;
                double dy = e.Y - centroY;

                // Radio de clic: 15 píxeles
                if (dx * dx + dy * dy <= 15 * 15)
                {
                    FormInformacionAvion formulario = new FormInformacionAvion();
                    formulario.SetVuelo(vuelo);
                    formulario.ShowDialog();
                    return;
                }
            }
        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void btnIniciarAutomatico_Click(object sender, EventArgs e)
        {
            if (timerSimulacion.Enabled == false)
            {
                timerSimulacion.Interval = 200;
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
            if (ListaVuelos.HasAllArrived())
            {
                timerSimulacion.Stop();
                btnSimulacionAutomatica.Text = "Iniciar simulación automática";
                btnMover.Enabled = true;
            }
            else
            {
                ListaVuelos.MoverTodosVuelos();
                enConflicto = ListaVuelos.HayConflictoTiempoReal();
                panelEspacioAereo.Invalidate();
            }

        }

        private void buttonVerTablaVuelos_Click(object sender, EventArgs e)
        {
            FormTablaVuelos fTabla = new FormTablaVuelos();
            fTabla.SetListaVuelos(ListaVuelos);
            fTabla.ShowDialog();
        }

        private void buttonPrediccion_Click(object sender, EventArgs e)
        {
            FlightPlanList hayConflicto = ListaVuelos.EncontrarConflicto();
            if (hayConflicto != null)
                MessageBox.Show("Los círculos de seguridad se tocarán.");
            else
                MessageBox.Show("Los círculos de seguridad no se tocarán.");
        }
        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void btnReiniciar_Click(object sender, EventArgs e)
        {
            if (timerSimulacion.Enabled == true)
            {
                timerSimulacion.Stop();
                btnSimulacionAutomatica.Text = "Iniciar simulación automática";
                btnMover.Enabled = true;
            }
            for(int i = 0; i < ListaVuelos.GetNumber(); i++)
            {
                ListaVuelos.GetFlightPlan(i).Restart();
                ListaVuelos.GetFlightPlan(i).RestaurarVelocidadesOriginales();
            }
            panelEspacioAereo.Invalidate();
            FlightPlanList hayConflicto = ListaVuelos.EncontrarConflicto();
            if (hayConflicto != null)
            {
                FormConflicto f = new FormConflicto();
                f.SetListaConflictos(hayConflicto);
                f.ShowDialog();
            }
        
        }

        private void btnCambiarVelocidad_Click(object sender, EventArgs e)
        {
            if (timerSimulacion.Enabled == true)
            {
                timerSimulacion.Stop();
                btnSimulacionAutomatica.Text = "Iniciar simulación automática";
                btnMover.Enabled = true;
            }
            FormCambiarVelocidad f = new FormCambiarVelocidad();
            f.SetListaVuelos(ListaVuelos);
            f.ShowDialog();
            if (f.GetCambiosAceptados() == true)
            {
                for (int i = 0; i < ListaVuelos.GetNumber(); i++)
                {
                    ListaVuelos.GetFlightPlan(i).Restart();
                }

                FlightPlanList hayConflicto = ListaVuelos.EncontrarConflicto();
                if (hayConflicto != null)
                {
                    FormConflicto f1 = new FormConflicto();
                    f1.SetListaConflictos(hayConflicto);
                    f1.ShowDialog();
                }
                panelEspacioAereo.Invalidate();
            }

        }

        private void GuardarFlightPlanList_Click(object sender, EventArgs e)
        {
            if (timerSimulacion.Enabled == true)
            {
                timerSimulacion.Stop();
                btnSimulacionAutomatica.Text = "Iniciar simulación automática";
                btnMover.Enabled = true;
            }
            FormNombrarTXT f = new FormNombrarTXT();
            f.SetFlightPlanList(ListaVuelos);
            f.ShowDialog();

        }
    }
}

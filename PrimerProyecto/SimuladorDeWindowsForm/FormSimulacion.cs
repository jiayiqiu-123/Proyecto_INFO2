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
        bool enConflicto = false;
        // esto me servirá como variable para el conflicto, en el buttonPredicción

        public FormSimulacion()
        {
            InitializeComponent();
            // Evita el parpadeo al redibujar (técnica estándar para paneles de WinForms)
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

        // Recibe la lista de vuelos que se va a simular.
        public void SetListaVuelos(FlightPlanList listaVuelos)
        {
            this.ListaVuelos = listaVuelos;
        }

        // Dibuja en el panel la trayectoria, el círculo de seguridad y la posición actual de cada vuelo.
        private void PanelDeTrayectoria_Paint(object sender, PaintEventArgs e)
        {
            Graphics graphics = e.Graphics;
            // La escala es la menor de las dos proporciones (ancho y alto) para que el espacio aéreo 800x550 quepa entero sin deformarse;
            // los márgenes centran el dibujo en el panel.
            float escala = Math.Min((float)panelEspacioAereo.Width / 800, (float)panelEspacioAereo.Height / 550);
            float margenX = (panelEspacioAereo.Width - 800 * escala) / 2;
            float margenY = (panelEspacioAereo.Height - 550 * escala) / 2;

            for (int i = 0; i < ListaVuelos.GetNumber(); i++)
            {
                FlightPlan vuelo = ListaVuelos.GetFlightPlan(i);
                if (vuelo == null)
                {
                    return;
                }
                else
                {
                    // Convertir las coordenadas del espacio aéreo al tamaño del panel.
                    Position posicion = vuelo.GetCurrentPosition();
                    float x = margenX + Convert.ToSingle(posicion.GetX() + 6) * escala;
                    float y = margenY + Convert.ToSingle(posicion.GetY() + 6) * escala;

                    float OrigenX = margenX + Convert.ToSingle(vuelo.GetInitialPosition().GetX() + 6) * escala;
                    float OrigenY = margenY + Convert.ToSingle(vuelo.GetInitialPosition().GetY() + 6) * escala;
                    float FinalX = margenX + Convert.ToSingle(vuelo.GetFinalPosition().GetX() + 6) * escala;
                    float FinalY = margenY + Convert.ToSingle(vuelo.GetFinalPosition().GetY() + 6) * escala;

                    // Asignar un color único a cada vuelo
                    vuelo.SetRandomColor();
                    Color color = vuelo.GetColor();
                    using (Pen lapizTrayectoria = new Pen(color, 1))
                    {
                        graphics.DrawLine(lapizTrayectoria, OrigenX, OrigenY, FinalX, FinalY);
                    }

                    // [NUEVO] El círculo de seguridad está centrado en el avión: su esquina superior izquierda es (x - radio, y - radio) y su diámetro es 2 * radio.
                    float diametro = ListaVuelos.GetDistanciaSeguridad() * 2 * escala;
                    float esquinaX = x - ListaVuelos.GetDistanciaSeguridad() * escala;
                    float esquinaY = y - ListaVuelos.GetDistanciaSeguridad() * escala;

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
                    float radioPunto = 3 * escala;
                    using (Brush brochaPunto = new SolidBrush(color))
                    {
                        graphics.FillEllipse(brochaPunto, x - radioPunto, y - radioPunto, radioPunto * 2, radioPunto * 2);
                    }
                }
            }
        }

        // Al abrir el formulario deja la simulación en su estado inicial y avisa si se prevé algún conflicto.
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

            ListaVuelos.LimpiarHistorial();
            buttonDeshacer.Enabled = false; // Limpiar el historial de estados anteriores al cargar la simulación
            panelEspacioAereo.Invalidate();

            FlightPlanList hayConflicto = ListaVuelos.EncontrarConflicto();
            if (hayConflicto != null)
            {
                FormConflicto f = new FormConflicto();
                f.SetListaConflictos(hayConflicto);
                f.ShowDialog();
            }


        }
        // Avanza un ciclo de simulación manualmente y redibuja el panel.
        private void btnMover_Click(object sender, EventArgs e)
        {

            ListaVuelos.MoverTodosVuelos();
            buttonDeshacer.Enabled = ListaVuelos.PuedeDeshacer();
            enConflicto = ListaVuelos.HayConflictoTiempoReal();
            panelEspacioAereo.Invalidate();
        }
        // Detecta si el clic cae sobre un avión y, si es así, abre su información.
        private void panelEspacioAereo_MouseClick(object sender, MouseEventArgs e)
        {
            if (ListaVuelos == null)
                return;

            //Se recorre de atrás hacia delante: si dos aviones se solapan, se elige el último dibujado (el que se ve encima).
            for (int i = ListaVuelos.GetNumber() - 1; i >= 0; i--)
            {
                FlightPlan vuelo = ListaVuelos.GetFlightPlan(i);
                if (vuelo == null)
                    continue;

                Position posicion = vuelo.GetCurrentPosition();

                // Centro del avión con la misma escala que se usa al dibujar.
                double escala = Math.Min((double)panelEspacioAereo.Width / 800, (double)panelEspacioAereo.Height / 550);
                double margenX = (panelEspacioAereo.Width - 800 * escala) / 2;
                double margenY = (panelEspacioAereo.Height - 550 * escala) / 2;
                double centroX = margenX + (posicion.GetX() + 6) * escala;
                double centroY = margenY + (posicion.GetY() + 6) * escala;

                double dx = e.X - centroX;
                double dy = e.Y - centroY;

                // Radio de clic: 15 unidades del espacio aéreo.
                double radioClic = 15 * escala;
                //Se comparan distancias al cuadrado (dx² + dy² <= radio²) para no calcular la raíz cuadrada.
                if (dx * dx + dy * dy <= radioClic * radioClic)
                {
                    FormInformacionAvion formulario = new FormInformacionAvion();
                    formulario.SetVuelo(vuelo);
                    formulario.ShowDialog();
                    return;
                }
            }
        }

        //Alterna la simulación automática: la inicia (y bloquea el botón Mover) o la detiene.
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

        //Cada 200 ms avanza un ciclo de simulación; se detiene cuando todos los vuelos han llegado.
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
                buttonDeshacer.Enabled = ListaVuelos.PuedeDeshacer();
                enConflicto = ListaVuelos.HayConflictoTiempoReal();
                panelEspacioAereo.Invalidate();
            }

        }

        //Abre la tabla con los datos de todos los vuelos.
        private void buttonVerTablaVuelos_Click(object sender, EventArgs e)
        {
            FormTablaVuelos fTabla = new FormTablaVuelos();
            fTabla.SetListaVuelos(ListaVuelos);
            fTabla.ShowDialog();
        }

        //Predice si los círculos de seguridad se van a tocar y lo indica con un mensaje.
        private void buttonPrediccion_Click(object sender, EventArgs e)
        {
            FlightPlanList hayConflicto = ListaVuelos.EncontrarConflicto();
            if (hayConflicto != null)
                MessageBox.Show("Los círculos de seguridad se tocarán.");
            else
                MessageBox.Show("Los círculos de seguridad no se tocarán.");
        }

        //Reinicia la simulación: vuelos a su posición y velocidad originales, historial vacío y aviso de conflictos.
        private void btnReiniciar_Click(object sender, EventArgs e)
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
            ListaVuelos.LimpiarHistorial();
            buttonDeshacer.Enabled = false; // Limpiar el historial de estados anteriores al reiniciar la simulación
            panelEspacioAereo.Invalidate();
            FlightPlanList hayConflicto = ListaVuelos.EncontrarConflicto();
            if (hayConflicto != null)
            {
                FormConflicto f = new FormConflicto();
                f.SetListaConflictos(hayConflicto);
                f.ShowDialog();
            }

        }

        //Abre el formulario para cambiar velocidades y, si se aceptan, reinicia la simulación con las nuevas.
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
                //Aquí solo se llama a Restart() y no a RestaurarVelocidadesOriginales(), para conservar las velocidades recién cambiadas.
                for (int i = 0; i < ListaVuelos.GetNumber(); i++)
                {
                    ListaVuelos.GetFlightPlan(i).Restart();
                }
                ListaVuelos.LimpiarHistorial();
                buttonDeshacer.Enabled = false; // Limpiar el historial de estados anteriores al cambiar la velocidad
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

        //Detiene la simulación automática y abre el formulario para guardar los planes de vuelo en un .txt.
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

        //Deshace el último paso de la simulación si hay estados anteriores guardados.
        private void buttonDeshacer_Click(object sender, EventArgs e)
        {
            // Comprueba si la simulación automática(el temporizador) está en marcha.
            // Si está encendida, la detiene para que los aviones no sigan avanzando solos al deshacer.
            if (timerSimulacion.Enabled)
            {
                timerSimulacion.Stop();
                btnSimulacionAutomatica.Text = "Iniciar simulación automática";
                btnMover.Enabled = true; // Activa el botón de deshacer si hay un estado anterior disponible
            }
            if (ListaVuelos.PuedeDeshacer()) // Comprobar si hay estados anteriores en la pila
            {
                ListaVuelos.DeshacerPaso();
                enConflicto = ListaVuelos.HayConflictoTiempoReal();
                panelEspacioAereo.Invalidate();
                buttonDeshacer.Enabled = ListaVuelos.PuedeDeshacer(); // Comprueba si quedan más pasos en la pila; si ya no quedan (devuelve false), desactiva el botón Deshacer
            }
        }
    }
}




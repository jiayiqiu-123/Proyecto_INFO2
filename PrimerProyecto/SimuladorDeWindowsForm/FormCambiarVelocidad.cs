using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using FlightLib;

namespace SimuladorDeWindowsForm
{
    public partial class FormCambiarVelocidad : Form
    {
        FlightPlanList ListaVuelos = new FlightPlanList();
        public bool CambiosAceptados = false;

        // Recibe la lista de vuelos cuyas velocidades se pueden cambiar.
        public void SetListaVuelos(FlightPlanList listaVuelos)
        {
            this.ListaVuelos = listaVuelos;
        }

        // Indica si el usuario ha aceptado algún cambio de velocidad.
        public bool GetCambiosAceptados()
        {
            return CambiosAceptados;
        }

        public FormCambiarVelocidad()
        {
            InitializeComponent();
        }

        // Cierra el formulario sin cambiar ninguna velocidad.
        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        // Al abrir, rellena la tabla con el identificador y la velocidad actual de cada vuelo.
        private void FormCambiarVelocidad_Load(object sender, EventArgs e)
        {
            for (int i = 0; i < ListaVuelos.GetNumber(); i++)
            {
                tablaVuelos.Rows.Add(ListaVuelos.GetFlightPlan(i).GetId(), ListaVuelos.GetFlightPlan(i).GetVelocidad(), "");

            }
        }

        // Valida las velocidades introducidas y las aplica a cada vuelo; cierra el formulario si todo es correcto.
        private void btnAceptar_Click(object sender, EventArgs e)
        {
            try
            {
                for (int i = 0; i < ListaVuelos.GetNumber(); i++)
                {
                    // La tercera columna es la de la nueva velocidad
                    string texto = Convert.ToString(tablaVuelos.Rows[i].Cells[2].Value);

                    // Una celda vacía significa que ese vuelo mantiene su velocidad
                    if (texto != "")
                    {
                        int valor = Convert.ToInt32(texto);
                        if (valor > 0)
                        {
                            ListaVuelos.GetFlightPlan(i).SetVelocidad(valor);
                        }
                        else
                        {
                            MessageBox.Show("El número tiene que ser mayor que 0.");
                            return;
                        }
                    }
                }
                CambiosAceptados = true;
                this.Close();

            }
            catch (FormatException)
            {
                MessageBox.Show("Error: has puesto letras o un formato incorrecto. Pon solo números.");
            }
        }
    }
}
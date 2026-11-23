using FlightLib;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SimuladorDeWindowsForm
{
    public partial class FormParametros : Form
    {
        float distSeguridad = 0;
        double tiempoCiclo = 0;

        // GETTERS Y SETTERS
        public float GetDistSeguridad()
        {
            return distSeguridad;
        }
        public double GetTiempoCiclo()
        {
            return tiempoCiclo;
        }
        public FormParametros()
        {
            InitializeComponent();
        }

        // Botón guardar
        private void Guardar_Click(object sender, EventArgs e)
        {
            if (txtDistancia.Text == "" || txtTiempo.Text == "")
            {
                MessageBox.Show("Rellena todos los campos.");
                return;
            }

            try
            {
                float d = float.Parse(txtDistancia.Text, NumberStyles.Float, CultureInfo.InvariantCulture);
                double t = double.Parse(txtTiempo.Text, NumberStyles.Float, CultureInfo.InvariantCulture);

                if (d <= 0 || t <= 0)
                {
                    MessageBox.Show("Los valores deben ser mayores que 0.");
                    return;
                }

                distSeguridad = d;
                tiempoCiclo = t;
                Close();
            }
            catch (FormatException)
            {
                MessageBox.Show("Introduce números válidos.");
            }
        }

        // Botón cancelar
        // Se dejan los valores a 0 para que Principal sepa que no se guardó nada
        private void Cancelar_Click(object sender, EventArgs e)
        {

            Close();
        }
    }
}

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
    public partial class FormPlanesDeVuelo : Form
    {
        // Variables públicas donde se guardan los dos objetos FlightPlan
        public FlightPlan p1;
        public FlightPlan p2;
        public FormPlanesDeVuelo()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void txtx1F_TextChanged(object sender, EventArgs e)
        {

        }

        private void FormPlanesDeVuelo_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
        // Botón aceptar
            // Comprobar que ningún campo esté vacío
            if (txtid1.Text == "" || txtx1.Text == "" || txty1.Text == "" ||
                txtx1F.Text == "" || txty1F.Text == "" || txtVel1.Text == "" ||
                txtid2.Text == "" || txtx2.Text == "" || txty2.Text == "" ||
                txtx2F.Text == "" || txty2F.Text == "" || txtVel2.Text == "")
            {
                MessageBox.Show("Faltan datos por rellenar.");
            }
            else
            {
                try
                {
                    // Datos del Vuelo 1
                    double xIni1 = Convert.ToDouble(txtx1.Text);
                    double yIni1 = Convert.ToDouble(txty1.Text);
                    double xFin1 = Convert.ToDouble(txtx1F.Text);
                    double yFin1 = Convert.ToDouble(txty1F.Text);
                    double vel1 = Convert.ToDouble(txtVel1.Text);

                    // Datos del Vuelo 2
                    double xIni2 = Convert.ToDouble(txtx2.Text);
                    double yIni2 = Convert.ToDouble(txty2.Text);
                    double xFin2 = Convert.ToDouble(txtx2F.Text);
                    double yFin2 = Convert.ToDouble(txty2F.Text);
                    double vel2 = Convert.ToDouble(txtVel2.Text);

                    if (xIni1 < 0 || xIni1 > 588 || yIni1 < 0 || yIni1 > 388 ||
                     xFin1 < 0 || xFin1 > 588 || yFin1 < 0 || yFin1 > 388 ||
                     xIni2 < 0 || xIni2 > 588 || yIni2 < 0 || yIni2 > 388 ||
                     xFin2 < 0 || xFin2 > 588 || yFin2 < 0 || yFin2 > 388)
                    {
                        MessageBox.Show("Las coordenadas deben estar dentro del espacio aéreo.");
                    }

                    // Comprobar que las velocidades sean mayores que cero
                    if (vel1 <= 0 || vel2 <= 0)
                    {
                        MessageBox.Show("Las velocidades deben ser mayores que cero.");
                    }
                    else
                    {
                        // Crear los dos objetos con tu clase FlightPlan
                        p1 = new FlightPlan(txtid1.Text, xIni1, yIni1, xFin1, yFin1, vel1);
                        p2 = new FlightPlan(txtid2.Text, xIni2, yIni2, xFin2, yFin2, vel2);

                        // Cerrar la ventana tras guardarlos
                        this.Close();
                    }
                }
                catch
                {
                    MessageBox.Show("Has puesto letras o caracteres no válidos en campos numéricos.");
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
        // Botón cancelar
            p1 = null;
            p2 = null;
            this.Close();
        }
        private void txtid1_TextChanged(object sender, EventArgs e)
        {
        }
    }
}

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
        // los 2 vuelos que introduce el usuario se guardan en estas variables
        public FlightPlan p1;
        public FlightPlan p2;
        // la lista de vuelos que se va a pasar al formulario principal
        FlightPlanList ListaVuelos = new FlightPlanList();

        // CONSTRUCTOR
        public FormPlanesDeVuelo()
        {
            InitializeComponent();
        }

        // devuelve la lista con los vuelos introducidos (vacía si CANCELAR)
        public FlightPlanList GetFlightPlanList()
        {
            return ListaVuelos;
        }

//--------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        // BOTÓN ACEPTAR --- ESTA PARTE SE PUEDE SIMPLIFICAR CON UN BUCLE EN LAS SIGUIENTES VERSIONES!!!IMP
        private void button1_Click(object sender, EventArgs e)
        {
            // 1. Comprobar que ningún campo esté vacío
            if (txtid1.Text == "" || txtCompany1.Text == "" || txtx1.Text == "" || txty1.Text == "" ||
                txtx1F.Text == "" || txty1F.Text == "" || txtVel1.Text == "" ||
                txtid2.Text == "" || txtCompany2.Text == "" || txtx2.Text == "" || txty2.Text == "" ||
                txtx2F.Text == "" || txty2F.Text == "" || txtVel2.Text == "")
            {
                MessageBox.Show("Faltan datos por rellenar.");
                return;
            }

            // 2. Comprobar que la compañía de cada vuelo no tenga espacios
            if (txtCompany1.Text.Contains(" ") || txtCompany2.Text.Contains(" "))
            {
                MessageBox.Show("El nombre de la compañía no puede contener espacios.");
                return;
            }

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

                //comprobar que las coordenadas estén dentro del espacio aéreo
                if (xIni1 < 0 || xIni1 > 800 || yIni1 < 0 || yIni1 > 550 ||
                    xFin1 < 0 || xFin1 > 800 || yFin1 < 0 || yFin1 > 550 ||
                    xIni2 < 0 || xIni2 > 800 || yIni2 < 0 || yIni2 > 550 ||
                    xFin2 < 0 || xFin2 > 800 || yFin2 < 0 || yFin2 > 550)
                {
                    MessageBox.Show("Las coordenadas deben estar dentro del espacio aéreo.");
                    return;
                }

                //comprobar que las velocidades sean mayores que cero
                if (vel1 <= 0 || vel2 <= 0)
                {
                    MessageBox.Show("Las velocidades deben ser mayores que cero.");
                    return;
                }

                //crear los dos vuelos y añadirlos a la lista
                p1 = new FlightPlan(txtid1.Text, txtCompany1.Text, xIni1, yIni1, xFin1, yFin1, vel1);
                p2 = new FlightPlan(txtid2.Text, txtCompany2.Text, xIni2, yIni2, xFin2, yFin2, vel2);
                ListaVuelos.AddFlightPlan(p1);
                ListaVuelos.AddFlightPlan(p2);

                // cerrar la ventana tras guardarlos
                this.Close();
            }
            catch (FormatException)
            {
                // algún campo numérico tiene letras o caracteres no válidos
                MessageBox.Show("Has puesto letras o caracteres no válidos en campos numéricos.");
            }
        }

        // BOTÓN CANCELAR
        private void button2_Click(object sender, EventArgs e)
        {
            p1 = null;
            p2 = null;
            this.Close();
        }
//--------------------------------------------------------------------------------------------------------------------------------------------------------------------------------

        private void FormPlanesDeVuelo_Load(object sender, EventArgs e)
        {

        }
        private void label1_Click(object sender, EventArgs e)
        {

        }
        private void label11_Click(object sender, EventArgs e)
        {

        }
        private void txtx1F_TextChanged(object sender, EventArgs e)
        {

        }
        private void txtid1_TextChanged(object sender, EventArgs e)
        {

        }

        
    }
}

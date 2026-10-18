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
    public partial class FormParametros : Form
    {
        // Variables públicas para leerlas desde Form1
        public float distSeguridad = 0;
        public double tiempoCiclo = 0;

        public FormParametros()
        {
            InitializeComponent();
        }

        private void FormParametros_Load(object sender, EventArgs e)
        {

        }

        // Manejador conectado desde el diseñador: txtDistancia.TextChanged
        public void txtDistanciaTextChanged(object sender, EventArgs e)
        {
            // Validación mínima: si el texto no es un número válido, marcar el control en rosa
            try
            {
                float.Parse(txtDistancia.Text);
                txtDistancia.BackColor = SystemColors.Window;
            }
            catch
            {
                txtDistancia.BackColor = Color.MistyRose;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // Botón guardar
            if (txtDistancia.Text == "" || txtTiempo.Text == "")
            {
                MessageBox.Show("Rellena todos los campos.");
            }
            else
            {
                try
                {
                    float d = Convert.ToSingle(txtDistancia.Text);
                    double t = Convert.ToDouble(txtTiempo.Text);

                    if (d <= 0 || t <= 0)
                    {
                        MessageBox.Show("Los valores deben ser mayores que 0.");
                    }
                    else
                    {
                        distSeguridad = d;
                        tiempoCiclo = t;
                        this.Close();
                    }
                }
                catch
                {
                    MessageBox.Show("Introduce números válidos.");
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            distSeguridad = 0;
            tiempoCiclo = 0;
            this.Close();
        }
    }
}

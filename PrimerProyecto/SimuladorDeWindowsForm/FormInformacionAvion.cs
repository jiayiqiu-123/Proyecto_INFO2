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
    public partial class FormInformacionAvion : Form
    {
        FlightPlan vuelo;
        public FormInformacionAvion()
        {
            InitializeComponent();
        }

        // Setter
        public void SetVuelo(FlightPlan vuelo)
        {
            this.vuelo = vuelo;
        }

        // Al abrir el formulario, muestra los datos del vuelo en la etiqueta.
        private void FormInformacionAvion_Load(object sender, EventArgs e)
        {
            if (vuelo == null)
                return;
            Position posicionActual = vuelo.GetCurrentPosition();
            Position posicionDestino = vuelo.GetFinalPosition();

            lblInformacion.Text =
                "Identificador: " + vuelo.GetId() + "\n" +
                "Compañía: " + vuelo.GetCompany() + "\n" +
                "Velocidad: " + vuelo.GetVelocidad().ToString("F2") + "\n" +
                "Posición actual: (" + posicionActual.GetX().ToString("F2") + ", " + posicionActual.GetY().ToString("F2") + ")\n" +
                "Destino: (" + posicionDestino.GetX().ToString("F2") + ", " + posicionDestino.GetY().ToString("F2") + ")";
        }
    }
}

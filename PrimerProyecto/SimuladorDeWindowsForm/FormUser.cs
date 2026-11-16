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
    public partial class FormUser : Form
    {
        //objeto para acceder a la base de datos
        Gestion mi_base = new Gestion();
        //constructor del formulario (lo crea Visual Studio)
        public FormUser()
        {
            InitializeComponent();
        }
        //devuelve el username de quien ha entrado ("" si no ha entrado nadie)
        public string GetUsuario()
        {
            return mi_base.GetUsuarioActual();
        }
        //botón Sign In: pide a Gestion que compruebe usuario y contraseña
        private void buttonSignIn_Click(object sender, EventArgs e)
        {
            //se llama a Gestion con lo escrito en los cuadros de texto
            int r = mi_base.comprobarLogin(textBoxUsername.Text, textBoxPassword.Text);
            if (r == 0)
                this.Close(); // login correcto: se cierra el formulario
            else if (r == -1)
                MessageBox.Show("El usuario no existe");
            else
                MessageBox.Show("Contraseña incorrecta");
        }
        //botón Sign Up: pide a Gestion que registre el usuario
        private void buttonSignUp_Click(object sender, EventArgs e)
        {
            //se llama a Gestion con lo escrito en los cuadros de texto
            int r = mi_base.registrarUsuario(textBoxUsername.Text, textBoxPassword.Text);
            if (r == 0)
                MessageBox.Show("Usuario creado");
            else if (r == -1)
                MessageBox.Show("Escribe un username");
            else if (r == -2)
                MessageBox.Show("Escribe una contraseña");
            else
                MessageBox.Show("No se ha podido crear, prueba a cambiar el username");
        }

        private void textBoxUsername_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBoxPassword_TextChanged(object sender, EventArgs e)
        {

        }
        //al abrir el formulario se abre la base de datos
        private void FormUser_Load(object sender, EventArgs e)
        {
            mi_base.Iniciar();
        }
        //al cerrar el formulario se cierra la base de datos
        
        private void FormUser_FormClosing_1(object sender, FormClosingEventArgs e)
        {
            mi_base.Cerrar();
        }
    }
}

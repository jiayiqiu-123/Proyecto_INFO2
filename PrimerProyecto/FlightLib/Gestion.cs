using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SQLite;

namespace FlightLib
{
    public class Gestion
    {
        SQLiteConnection cnx;
        string usuarioActual = "";

        //abre la conexión con la base de datos (hay que llamarlo antes de usar cualquier otro método)
        public void Iniciar()
        {
            string dataSource = "Data Source=..\\..\\..\\BaseDatos\\proyecto.db";
            cnx = new SQLiteConnection(dataSource);
            cnx.Open();
        }

        //cierra la conexión con la base de datos
        public void Cerrar()
        {
            cnx.Close();
        }

        //devuelve el usuario que ha iniciado sesión
        public string GetUsuarioActual()
        {
            return usuarioActual;
        }
        //busca un usuario por su username y devuelve una tabla con el resultado
        //(0 filas si no existe, 1 fila si existe)
        public DataTable buscarUsuario(string username)
        {
            DataTable dt = new DataTable();
            string sql = "SELECT * FROM usuarios WHERE username='" + username + "'";
            SQLiteDataAdapter adp = new SQLiteDataAdapter(sql, cnx);
            adp.Fill(dt);
            return dt;
        }

        //añade un usuario nuevo; devuelve 0 si lo añade y -1 si no puede
        public int nuevoUsuario(string username, string password)
        {
            DataTable consulta = buscarUsuario(username);
            if (consulta.Rows.Count > 0)
            {
                return -1;
            }

            string sql = "INSERT INTO usuarios VALUES ('" + username + "', '" + password + "')";
            SQLiteCommand command = new SQLiteCommand(sql, cnx);
            int affectedRows = command.ExecuteNonQuery();

            if (affectedRows == 1)
                return 0;
            else
                return -1;
        }
        //comprueba usuario y contraseña
        //devuelve 0 si son correctos, -1 si el usuario no existe y -2 si la contraseña es incorrecta
        public int comprobarLogin(string username, string password)
        {

            DataTable consulta = buscarUsuario(username);

            if (consulta.Rows.Count == 0)
                return -1;

            string guardada = consulta.Rows[0][1].ToString();

            if (guardada == password)
            {

                usuarioActual = username;
                return 0;
            }
            else
                return -2;
        }

        //registra un usuario nuevo comprobando antes los datos
        //devuelve 0 si se crea, -1 si el usuario está vacío, -2 si la contraseña está vacía
        //y -3 si el usuario ya existe o no se ha podido crear
        public int registrarUsuario(string username, string password)
        {

            if (username == "")
                return -1;

            if (password == "")
                return -2;

            int r = nuevoUsuario(username, password);
            if (r == 0)
                return 0;
            else
                return -3;
        }
    }
}

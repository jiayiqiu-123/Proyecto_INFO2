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
        SQLiteConnection cnx; //conector para acceder a la base de datos
        string usuarioActual = ""; //username del usuario que ha entrado (vacío si no ha entrado nadie)

        //abre la conexión con la base de datos (hay que llamarlo antes de usar cualquier otro método)
        public void Iniciar()
        {
            //ruta desde bin\Debug hasta el fichero de la base de datos
            string dataSource = "Data Source=..\\..\\..\\BaseDatos\\proyecto.db";
            //se crea el conector con esa ruta
            cnx = new SQLiteConnection(dataSource);
            //se abre la conexión
            cnx.Open();
        }

        //cierra la conexión con la base de datos
        public void Cerrar()
        {
            cnx.Close();
        }

        public string GetUsuarioActual()
        {
            return usuarioActual;
        }
        //busca un usuario por su username y devuelve una tabla con el resultado
        //(0 filas si no existe, 1 fila si existe)
        public DataTable buscarUsuario(string username)
        {
            //tabla donde se guardará el resultado de la consulta
            DataTable dt = new DataTable();
            //consulta SQL: selecciona el usuario con ese username
            string sql = "SELECT * FROM usuarios WHERE username='" + username + "'";
            //el adaptador ejecuta la consulta sobre la conexión abierta
            SQLiteDataAdapter adp = new SQLiteDataAdapter(sql, cnx);
            //se rellena la tabla con el resultado
            adp.Fill(dt);
            //se devuelve la tabla
            return dt;
        }

        //añade un usuario nuevo; devuelve 0 si lo añade y -1 si no puede
        public int nuevoUsuario(string username, string password)
        {
            //primero se busca si ya existe ese username (es primarykey, no puede repetirse)
            DataTable consulta = buscarUsuario(username);
            //si la consulta devuelve alguna fila, el usuario ya existe
            if (consulta.Rows.Count > 0)
            {
                return -1;
            }
            //consulta SQL para insertar el usuario nuevo
            string sql = "INSERT INTO usuarios VALUES ('" + username + "', '" + password + "')";
            //comando que ejecutará la operación sobre la base de datos
            SQLiteCommand command = new SQLiteCommand(sql, cnx);
            //se ejecuta y se guarda cuántas filas se han modificado
            int affectedRows = command.ExecuteNonQuery();
            //si se ha insertado una fila, todo ha ido bien
            if (affectedRows == 1)
                return 0;
            else
                return -1;
        }
        //comprueba usuario y contraseña
        //devuelve 0 si son correctos, -1 si el usuario no existe y -2 si la contraseña es incorrecta
        public int comprobarLogin(string username, string password)
        {
            //se busca el usuario en la base de datos
            DataTable consulta = buscarUsuario(username);
            //si no hay ninguna fila, el usuario no existe
            if (consulta.Rows.Count == 0)
                return -1;
            //la contraseña guardada está en la fila 0, columna 1
            string guardada = consulta.Rows[0][1].ToString();
            //se compara la contraseña guardada con la escrita
            if (guardada == password)
            {
                //login correcto: se guarda quién ha entrado
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
            //el usuario no puede estar vacío
            if (username == "")
                return -1;
            //la contraseña no puede estar vacía
            if (password == "")
                return -2;
            //se intenta añadir el usuario a la base de datos
            int r = nuevoUsuario(username, password);
            if (r == 0)
                return 0;
            else
                return -3;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace FlightLib
{
    public class FlightPlan
    // para definir una clase, SIEMPRE se pone en public para poder EDITARLA DESDE FUERA (quien quiera modificar nuestro código)

    {
        // ATRIBUTOS (variables) - estas siempre son privadas, sin poner delante nada para no poder ser modificadas

        string id; // identificador
        Position initialPosition; // posición inicial
        Position currentPosition; // posicion actual
        Position finalPosition; // posicion final
        double velocidad;
        double velocidadoriginal;
        Color color;
        static readonly Random generadorAleatorio = new Random();
        // CONSTRUCTORES (através de parámetros pone atributos)
        // un constructor es un método especial que se llama igual que la clase y sirve para inicializar los atributos de la clase que les hemos pasado como parametro
        // el constructor lo que hace es ""montar"" el objeto, es decir, crear el objeto con los atributos que le pasamos para que esten ordenados como queramos
        // el programa siempre sabrá a que constructor ir por el numero de atributos que tenga 

        public FlightPlan(string id, double cpx, double cpy, double fpx, double fpy, double velocidad)
        {
            // atributos de la clase FlightPlan, delante simplemente le ponemos el tipo (ejemplo:"string")
            // delante de los atributos no se pone nada, LOS ATRIBUTOS YA SON PRIVADOS

            this.id = id;
            this.initialPosition = new Position(cpx, cpy);
            this.currentPosition = new Position(cpx, cpy);
            this.finalPosition = new Position(fpx, fpy);
            this.velocidadoriginal = velocidad;
            this.velocidad = velocidad;
            this.color = Color.Empty;
        }

        // METODOS (funciones)
        // son los métodos que van a trabajar sobre los atributos de la clase FlightPlan

        // GETTERS
        public string GetId()
        { return id; }

        public Position GetInitialPosition()
        { return initialPosition; }

        public Position GetCurrentPosition()
        { return currentPosition; }

        public Position GetFinalPosition()
        { return finalPosition; }

        public double GetVelocidadOriginal()
        { return velocidadoriginal; }

        public double GetVelocidad()
        { return velocidad; }

        public Color GetColor()
        { return color; }


        // SETTERS

        public void SetId(string id)
        { this.id = id; }

        public void SetCurrentPosition(Position currentPosition)
        { this.currentPosition = currentPosition; }

        public void SetFinalPosition(Position finalPosition)
        { this.finalPosition = finalPosition; }

        public void SetVelocidad(double velocidad)
        // Setter del atributo velocidad
        // es de tipo void porque no devuelve nada
        { this.velocidad = velocidad; }

        public void SetRandomColor()
        {
            if (this.color == Color.Empty)
            {
                Color color = Color.FromArgb(
                    generadorAleatorio.Next(40, 221),
                    generadorAleatorio.Next(40, 221),
                    generadorAleatorio.Next(40, 221));

                this.color = color;
            }

        }

        ///////////////////////////////////////////////////

        public void Mover(double tiempo)
        {
            // Distancia que recorre el avión durante el tiempo indicado
            double distancia = tiempo * velocidad;

            // Vector desde la posición actual hasta el destino
            double dx = finalPosition.GetX() - currentPosition.GetX();
            double dy = finalPosition.GetY() - currentPosition.GetY();

            // Longitud de la trayectoria que falta
            double longitud = Math.Sqrt(dx * dx + dy * dy);

            if (longitud == 0)
                return;

            // Dirección de la trayectoria
            double direccionX = dx / longitud;
            double direccionY = dy / longitud;

            // Nueva posición según la ecuación del movimiento
            double x = currentPosition.GetX() + distancia * direccionX;
            double y = currentPosition.GetY() + distancia * direccionY;

            Position nuevaPosicion = new Position(x, y);

            // Si llega o se pasa del destino, lo dejamos exactamente en el destino
            if (currentPosition.Distancia(nuevaPosicion) >= longitud)
                nuevaPosicion = finalPosition;
            currentPosition = nuevaPosicion;
        }

        // !!HACER UN METODO PARA QUE DIGA SI UN VUELO LLEGA A SU DESTINO O NO (COMPARANDO POSICIONES)
        public bool HasArrived()
        {
            bool resultado = false;

            if (currentPosition.Distancia(finalPosition) == 0)
                // (currentPosition == finalPosition) --> yo lo tenía así, quiero preguntar si valdría igual
                resultado = true;
            return resultado;
        }

        public void Restart()
        { currentPosition = initialPosition; }

        public void RestaurarVelocidadesOriginales()
        {
            this.SetVelocidad(this.GetVelocidadOriginal());
        }

        public double Distance(FlightPlan plan)
        {
            double two_plans_distance = this.currentPosition.Distancia(plan.currentPosition);
            // este método lo que hace es coger la currentposition y hacer la distancia respecto a otro plan de vuelo "plan" que será un parámetro que le daremos
            return two_plans_distance;
        }


        public void EscribeConsola()
        // escribe en consola los datos del plan de vuelo
        {
            Console.WriteLine("******************************");
            Console.WriteLine("Datos del vuelo: ");
            Console.WriteLine("Identificador: {0}", id);
            // !!buscar como escribir datos reales con solo 2 decimales (F2, fload de 2 decimales)
            Console.WriteLine("Velocidad: {0:F2}", velocidad);
            Console.WriteLine("Posición actual: ({0:F2},{1:F2})", currentPosition.GetX(), currentPosition.GetY());
            if (this.HasArrived())
                // se encarga de avisar si hemos llegado a nuestro destino
                Console.WriteLine("Hemos llegado a nuestro destino");
            Console.WriteLine("******************************");
        }
    }
}

// IMPORTANTE PARA ENTENDER EL CÓDIGO:
//---------------------------------------------------------------------------------------------------------------------------------------------
//EXPLICACIÓN DEL METODO SETVELOCIDAD:
// this es como hacer un self de python
// setvelocidad es un método, un objeto flight plan lo llamará y trabajará sobre él
// lo habitual es tener un set para cada atributo, osea haces un set de cada atributo como yo modificaciones quiera

//EXPLICACIÓN DEL METODO GETVELOCIDAD:
//los gets sirven para leer la velocidad y devolverlo
// public double(porque velocidad es double) getVelocidad("un get no necesita nada, por lo tanto no parametros")
// por lo tanto esto queda --> public double GetVelocidad()
// { return velocidad; }
//---------------------------------------------------------------------------------------------------------------------------------------------
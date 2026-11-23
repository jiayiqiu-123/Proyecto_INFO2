using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace FlightLib
{
    public class FlightPlan

    {

        string id;
        string company;
        Position initialPosition;
        Position currentPosition;
        Position finalPosition;
        double velocidad;
        double velocidadoriginal;
        Color color;
        static readonly Random generadorAleatorio = new Random();

        // CONSTRUCTORES 
        public FlightPlan(string id, string company, double cpx, double cpy, double fpx, double fpy, double velocidad)
        {

            this.id = id;
            this.company = company;
            this.initialPosition = new Position(cpx, cpy);
            this.currentPosition = new Position(cpx, cpy);
            this.finalPosition = new Position(fpx, fpy);
            this.velocidadoriginal = velocidad;
            this.velocidad = velocidad;
            this.color = Color.Empty;
        }

        // GETTERS 
        public string GetId()
        { return id; }

        public string GetCompany()
        { return company; }

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

        public void SetCompany(string company)
        { this.company = company; }

        public void SetCurrentPosition(Position currentPosition)
        { this.currentPosition = currentPosition; }

        public void SetFinalPosition(Position finalPosition)
        { this.finalPosition = finalPosition; }

        public void SetVelocidad(double velocidad)
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

        // METODOS (funciones)


        // Mueve el vuelo en línea recta hacia su destino durante el tiempo indicado como parámetro.
        // Si llega o se pasa del destino, se queda exactamente en el destino.
        public void Mover(double tiempo)
        {
            double distancia = tiempo * velocidad;

            double dx = finalPosition.GetX() - currentPosition.GetX();
            double dy = finalPosition.GetY() - currentPosition.GetY();

            double longitud = Math.Sqrt(dx * dx + dy * dy);
            if (longitud == 0)
                return;

            double direccionX = dx / longitud;
            double direccionY = dy / longitud;

            double x = currentPosition.GetX() + distancia * direccionX;
            double y = currentPosition.GetY() + distancia * direccionY;

            Position nuevaPosicion = new Position(x, y);

            if (currentPosition.Distancia(nuevaPosicion) >= longitud)
                nuevaPosicion = finalPosition;
            currentPosition = nuevaPosicion;
        }

        // Comprueba si el vuelo ha llegado a su destino.
        // Devuelve true si la posición actual coincide con el destino, o false si todavía está en vuelo.
        public bool HasArrived()
        {
            bool resultado = false;

            if (currentPosition.Distancia(finalPosition) == 0)
                resultado = true;
            return resultado;
        }

        // Devuelve el vuelo a su posición inicial. No cambia la velocidad. No devuelve nada.
        public void Restart()
        { currentPosition = initialPosition; }

        // Pone de nuevo la velocidad original del vuelo (deshace cualquier cambio de velocidad). No devuelve nada.
        public void RestaurarVelocidadesOriginales()
        {
            this.SetVelocidad(this.GetVelocidadOriginal());
        }

        // Calcula la distancia entre la posición actual de este vuelo y la del vuelo indicado como parámetro.
        // Devuelve la distancia (double).
        public double Distance(FlightPlan plan)
        {
            double twoPlansDistance = this.currentPosition.Distancia(plan.currentPosition);
            return twoPlansDistance;
        }


        // escribe en consola los datos del plan de vuelo
        public void EscribeConsola()
        {
            Console.WriteLine("******************************");
            Console.WriteLine("Datos del vuelo: ");
            Console.WriteLine("Velocidad: {0:F2}", velocidad);
            Console.WriteLine("Posición actual: ({0:F2},{1:F2})", currentPosition.GetX(), currentPosition.GetY());
            if (this.HasArrived())
                Console.WriteLine("Hemos llegado a nuestro destino");
            Console.WriteLine("******************************");
        }
        // Crea una copia del objeto FlightPlan actual
        public FlightPlan Clonar()
        {
            FlightPlan copia = new FlightPlan
            (
                this.id,
                this.company,
                this.initialPosition.GetX(),
                this.initialPosition.GetY(),
                this.finalPosition.GetX(),
                this.finalPosition.GetY(),
                this.velocidadoriginal
            );
            copia.SetVelocidad(this.velocidad);
            copia.SetCurrentPosition(this.currentPosition.Clonar());
            copia.color = this.color;
            return copia;
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
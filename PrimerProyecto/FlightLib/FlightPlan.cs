using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightLib
{
    public class FlightPlan
        // para definir una clase, SIEMPRE se pone en public para poder EDITARLA DESDE FUERA (quien quiera modificar nuestro código)

    {
        // ATRIBUTOS (variables) - estas siempre son privadas, sin poner delante nada para no poder ser modificadas

        string id; // identificador
        Position currentPosition; // posicion actual
        Position finalPosition; // posicion final
        double velocidad;

        // CONSTRUCTORES (através de parámetros pone atributos)
        // un constructor es un método especial que se llama igual que la clase y sirve para inicializar los atributos de la clase que les hemos pasado como parametro
        // el constructor lo que hace es ""montar"" el objeto, es decir, crear el objeto con los atributos que le pasamos para que esten ordenados como queramos
        // el programa siempre sabrá a que constructor ir por el numero de atributos que tenga 

        public FlightPlan(string id, double cpx, double cpy, double fpx, double fpy, double velocidad)
        {
            // atributos de la clase FlightPlan, delante simplemente le ponemos el tipo (ejemplo:"string")
            // delante de los atributos no se pone nada, LOS ATRIBUTOS YA SON PRIVADOS

            this.id = id;
            this.currentPosition = new Position(cpx, cpy);
            this.finalPosition = new Position(fpx, fpy);
            this.velocidad = velocidad;
        }

        // METODOS (funciones)
        // son los métodos que van a trabajar sobre los atributos de la clase FlightPlan


        public void SetVelocidad(double velocidad)
        // Setter del atributo velocidad
        // es de tipo void porque no devuelve nada
        { this.velocidad = velocidad; }

        public Position GetCurrentPosition()
        {
            return currentPosition;
        }
        public string GetId()
        {
            return id;
        }

        public double GetVelocidad()
        {
            return velocidad;
        }

        public Position GetFinalPosition()
        {
            return finalPosition;
        }
        public void Mover(double tiempo)
        {
            Mover(tiempo, double.MaxValue, double.MaxValue);
        }

        public void Mover(double tiempo, double maxX, double maxY)
        // Mueve el vuelo a la posición correspondiente a viajar durante el tiempo que se recibe como parámetro
        {
            //Calculamos la distancia recorrida en el tiempo dado
            double distancia = tiempo * this.velocidad / 60;

            //Calculamos las razones trigonométricas
            double dx = finalPosition.GetX() - currentPosition.GetX();
            double dy = finalPosition.GetY() - currentPosition.GetY();

            double hipotenusa = Math.Sqrt(dx * dx + dy * dy);

            if (hipotenusa == 0)
                return;

            double coseno = dx / hipotenusa;
            double seno = dy / hipotenusa;
            //Caculamos la nueva posición del vuelo
            double x = currentPosition.GetX() + distancia * coseno;
            double y = currentPosition.GetY() + distancia * seno;

            Position nextPosition = new Position(x, y);

            // !!MODIFICAR MoverVuelo PARA QUE NO SE PASE DEL DESTINO
            if (currentPosition.Distancia(nextPosition) >= hipotenusa)
                nextPosition = finalPosition;

            if (nextPosition.GetX() >= 0 && nextPosition.GetX() <= maxX &&
                nextPosition.GetY() >= 0 && nextPosition.GetY() <= maxY)
            {
                currentPosition = nextPosition;
            }
        }

        // !!HACER UN METODO PARA QUE DIGA SI UN VUELO LLEGA A SU DESTINO O NO (COMPARANDO POSICIONES)
        public bool EstaDestino()
        { 
            bool resultado = false;
            if (currentPosition == finalPosition)
                resultado = true;
            return resultado;
        }


        //!!HACER UN METODO QUE DETECTE EL CONFLICTO CUANDO 2 VUELOS ESTAN MÁS CERCANOS
        public bool Conflicto(FlightPlan b, double distanciaSeguridad)
        {
            bool conflicto = false;
            if (this.currentPosition.Distancia(b.currentPosition) < distanciaSeguridad)
            // el this es el plan a porque es el  que llama, el b es el que hemos puesto como parametro, el que le pasamos, el vuelo b
                conflicto = true;
            return conflicto;
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
            if (this.EstaDestino())
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
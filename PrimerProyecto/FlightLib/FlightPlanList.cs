using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightLib
{
    public class FlightPlanList
    {
        FlightPlan[] vector = new FlightPlan[10];
        int number = 0;

        // necesitamos un método para añadir un FlightPlan a la lista
        public int AddFlightPlan(FlightPlan p)
        {
            if (number == 10)
            {  return -1; }
            else
            {
                vector[number] = p;
                // incrementamos el número de FlightPlans en la lista - mismo que numer = numer + 1
                number++;
                return 0;
            }
        }

        public FlightPlan GetFlightPlan(int i)
        // esto es un getter para devolver un FlightPlan de la lista, que esté en la posición i de vector
        {
            //solo son validos los indices de i a number
            if (i < 0 || i >= number)
            // esas barras es una manera de poner un "or"
            { return null; }
            // return null quiere decir que no podemos retornar ningún valor porque no se puede acceder a la posición
            else 
            { return vector[i]; }
            
            
        }

        public void Mover(double tiempo)
        {
            int i = 0;
            while (i < number)
            {
                vector[i].Mover(tiempo);
                // recorremos todos los FlightPlans de la lista y los movemos
                i++;
            }
        }

        public void EscribeConsola()
        {
            int i = 0;
            while (i < number)
            {
                vector[i].EscribeConsola();
                // recorremos todos los FlightPlans de la lista y los escribimos en consola
                i++;
            }
        }

    }
}

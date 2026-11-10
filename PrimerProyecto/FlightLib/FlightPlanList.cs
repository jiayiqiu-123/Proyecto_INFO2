using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Globalization;
using System.IO;

namespace FlightLib
{
    public class FlightPlanList
    {
        FlightPlan[] vector = new FlightPlan[10];
        int number = 0;
        //De momento consideramos, la distancia de seguridad no varia en los vuelos
        float distanciaSeguridad;
        double tiempoCiclo;

        // necesitamos un método para añadir un FlightPlan a la lista
        public int GetNumber()
        {
            return number;
        }

        public void SetDistanciaSegurdad(float d)
        {
            this.distanciaSeguridad = d;
        }
        public void SetTiempoCiclo(double t)
        {
            this.tiempoCiclo = t;
        }
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

        public void MoverTodosVuelos()
        {
            int i = 0;
            while (i < number)
            {
                vector[i].Mover(tiempoCiclo);
                // recorremos todos los FlightPlans de la lista y los movemos
                i++;
            }
        }

        public bool HasAllArrived()
        {
            bool resultado = false;
            int j = 0;
            for(int i= 0; i< number; i ++)
            {
                if (vector[i].HasArrived())
                {
                    j++;
                }
            }
            if (j == number)
            {
                resultado = true;
            }
            return resultado;
        }
        public FlightPlanList EncontrarConflicto()
        {
            FlightPlanList vuelosEnConflicto = new FlightPlanList();

            for (int i = 0; i < number; i++)
            {
                for (int j = i + 1; j < number; j++)
                {
                    FlightPlan vuelo1 = vector[i];
                    FlightPlan vuelo2 = vector[j];

                    // Posiciones iniciales y finales
                    double xI1 = vuelo1.GetInitialPosition().GetX();
                    double yI1 = vuelo1.GetInitialPosition().GetY();
                    double xF1 = vuelo1.GetFinalPosition().GetX();
                    double yF1 = vuelo1.GetFinalPosition().GetY();

                    double xI2 = vuelo2.GetInitialPosition().GetX();
                    double yI2 = vuelo2.GetInitialPosition().GetY();
                    double xF2 = vuelo2.GetFinalPosition().GetX();
                    double yF2 = vuelo2.GetFinalPosition().GetY();

                    // Desplazamientos y longitudes
                    double deltaX1 = xF1 - xI1;
                    double deltaY1 = yF1 - yI1;
                    double longitud1 = Math.Sqrt(deltaX1 * deltaX1 + deltaY1 * deltaY1);

                    double deltaX2 = xF2 - xI2;
                    double deltaY2 = yF2 - yI2;
                    double longitud2 = Math.Sqrt(deltaX2 * deltaX2 + deltaY2 * deltaY2);

                    double velocidad1 = vuelo1.GetVelocidad();
                    double velocidad2 = vuelo2.GetVelocidad();

                    // Evitar divisiones entre cero
                    if (longitud1 == 0 || longitud2 == 0 || velocidad1 <= 0 || velocidad2 <= 0)
                        continue;

                    // Componentes de la velocidad
                    double velocidadX1 = deltaX1 / longitud1 * velocidad1;
                    double velocidadY1 = deltaY1 / longitud1 * velocidad1;
                    double velocidadX2 = deltaX2 / longitud2 * velocidad2;
                    double velocidadY2 = deltaY2 / longitud2 * velocidad2;

                    // Diferencia inicial de posiciones y de velocidades
                    double deltaXi = xI1 - xI2;
                    double deltaYi = yI1 - yI2;
                    double deltaVx = velocidadX1 - velocidadX2;
                    double deltaVy = velocidadY1 - velocidadY2;

                    // Instante de distancia mínima: t = -(deltaXi*deltaVx + deltaYi*deltaVy) / (deltaVx² + deltaVy²)
                    double arriba = deltaXi * deltaVx + deltaYi * deltaVy;
                    double abajo = deltaVx * deltaVx + deltaVy * deltaVy;

                    double tDistMin;
                    if (abajo == 0)
                        tDistMin = 0;   // Misma velocidad vectorial: la distancia es constante
                    else
                        tDistMin = -arriba / abajo;

                    // Limitar t al intervalo en el que ambos vuelos están en el aire
                    double tMax = Math.Min(longitud1 / velocidad1, longitud2 / velocidad2);
                    if (tDistMin < 0) tDistMin = 0;
                    if (tDistMin > tMax) tDistMin = tMax;

                    // Posiciones en ese instante
                    double distX = (xI1 + velocidadX1 * tDistMin) - (xI2 + velocidadX2 * tDistMin);
                    double distY = (yI1 + velocidadY1 * tDistMin) - (yI2 + velocidadY2 * tDistMin);
                    double distMin = Math.Sqrt(distX * distX + distY * distY);

                    if (distMin <= distanciaSeguridad * 2)
                    {
                        // Añadir sin duplicados
                        if (!EstaEnLista(vuelosEnConflicto, vuelo1))
                            vuelosEnConflicto.AddFlightPlan(vuelo1);
                        if (!EstaEnLista(vuelosEnConflicto, vuelo2))
                            vuelosEnConflicto.AddFlightPlan(vuelo2);
                    }
                }
            }

            if (vuelosEnConflicto.GetNumber() == 0)
                return null;
            else
                return vuelosEnConflicto;
        }

        // Método auxiliar: comprueba si un vuelo ya está en la lista
        private bool EstaEnLista(FlightPlanList lista, FlightPlan vuelo)
        {
            for (int k = 0; k < lista.GetNumber(); k++)
            {
                if (lista.GetFlightPlan(k) == vuelo)
                    return true;
            }
            return false;
        }

        public bool HayConflictoTiempoReal()
        {
            for (int i = 0; i < number; i++)
            {
                for (int j = i + 1; j < number; j++)
                {
                    FlightPlan vuelo1 = vector[i];
                    FlightPlan vuelo2 = vector[j];

                    // Distancia entre los centros de los aviones
                    double dist = vuelo1.Distance(vuelo2);

                    if (dist <= distanciaSeguridad * 2)
                        return true;
                }
            }
            return false;
        }
        public void ResolverConflictos()
        {
            if (vector != null)
            {
                for (int i = 0; i < number; i=i+2)
                {
                    FlightPlan vuelo = vector[i];
                    double velocidadOriginal = vuelo.GetVelocidadOriginal();
                    double nuevaVelocidad = velocidadOriginal * 2; // Duplicar la velocidad
                    vuelo.SetVelocidad(nuevaVelocidad);
                }
            }
        }
        public void GuardarEnArchivo(string ruta)
        {
            using (StreamWriter sw = new StreamWriter(ruta))
            {
                for (int i = 0; i < number; i++)
                {
                    FlightPlan v = vector[i];
                    Position ini = v.GetInitialPosition();
                    Position fin = v.GetFinalPosition();
                    Position act = v.GetCurrentPosition();

                    string linea = string.Join(" ",
                        v.GetId(),
                        act.GetX().ToString(CultureInfo.InvariantCulture),
                        act.GetY().ToString(CultureInfo.InvariantCulture),
                        fin.GetX().ToString(CultureInfo.InvariantCulture),
                        fin.GetY().ToString(CultureInfo.InvariantCulture),
                        v.GetVelocidad().ToString(CultureInfo.InvariantCulture));


                    sw.WriteLine(linea);
                }
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

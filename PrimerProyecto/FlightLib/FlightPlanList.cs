using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
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
        double distanciaSeguridad = 0;
        double tiempoCiclo;

        // necesitamos un método para añadir un FlightPlan a la lista
        public int GetNumber()
        {
            return number;
        }

        public void SetDistanciaSeguridad(double d)
        {
            this.distanciaSeguridad = d;
        }
        public double GetDistanciaSeguridad()
        {
            return distanciaSeguridad;
        }
        public void SetTiempoCiclo(double t)
        {
            this.tiempoCiclo = t;
        }
        public double GetTiempoCiclo()
        {
            return tiempoCiclo;
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
            GuardarEstadoAnterior();
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
                string velocidades = "";
                for (int i = 0; i < number; i++)
                {
                    if (i > 0)
                        velocidades = velocidades + " ";
                    velocidades = velocidades + vector[i].GetVelocidad().ToString(CultureInfo.InvariantCulture);
                }

                sw.WriteLine(velocidades);
                string parametros = distanciaSeguridad.ToString(CultureInfo.InvariantCulture) + " " +
                    tiempoCiclo.ToString(CultureInfo.InvariantCulture);
                sw.WriteLine(parametros);

                for (int i = 0; i < number; i++)
                {
                    FlightPlan vuelo = vector[i];
                    Position posicionActual = vuelo.GetCurrentPosition();
                    Position posicionFinal = vuelo.GetFinalPosition();

                    string linea = vuelo.GetId() + " " +
                        posicionActual.GetX().ToString(CultureInfo.InvariantCulture) + " " +
                        posicionActual.GetY().ToString(CultureInfo.InvariantCulture) + " " +
                        posicionFinal.GetX().ToString(CultureInfo.InvariantCulture) + " " +
                        posicionFinal.GetY().ToString(CultureInfo.InvariantCulture) + " " +
                        vuelo.GetVelocidad().ToString(CultureInfo.InvariantCulture);

                    sw.WriteLine(linea);
                }
            }
        }

        public FlightPlanList CargaDeFichero(string ruta)
        {
            try
            {
                string[] lineas = File.ReadAllLines(ruta);
                FlightPlanList listaFichero = new FlightPlanList();
                string[] velocidadesGuardadas = null;
                int primeraLineaVuelo = 0;
                double distanciaGuardada = 0;
                double cicloGuardado = 0;

                if (lineas.Length >= 2)
                {
                    string[] primeraLinea = lineas[0].Split(' ');
                    bool primeraLineaEsVector = true;

                    try
                    {
                        Convert.ToDouble(primeraLinea[0], CultureInfo.InvariantCulture);
                    }
                    catch (FormatException)
                    {
                        primeraLineaEsVector = false;
                    }

                    if (primeraLineaEsVector)
                    {
                        try
                        {
                            string[] parametros = lineas[1].Split(' ');
                            if (parametros.Length == 2)
                            {
                                distanciaGuardada = Convert.ToDouble(parametros[0], CultureInfo.InvariantCulture);
                                cicloGuardado = Convert.ToDouble(parametros[1], CultureInfo.InvariantCulture);
                            }
                            else if (parametros.Length == 1)
                            {
                                cicloGuardado = Convert.ToDouble(parametros[0], CultureInfo.InvariantCulture);
                            }
                            else
                            {
                                return null;
                            }

                            velocidadesGuardadas = primeraLinea;
                            primeraLineaVuelo = 2;

                            if (velocidadesGuardadas.Length != lineas.Length - 2)
                                return null;
                        }
                        catch (FormatException)
                        {
                            velocidadesGuardadas = null;
                            primeraLineaVuelo = 0;
                        }
                    }
                }

                for (int i = primeraLineaVuelo; i < lineas.Length; i++)
                {
                    string[] datosVuelo = lineas[i].Split(' ');
                    if (datosVuelo.Length != 6)
                        return null;

                    string id = datosVuelo[0];
                    double posicionX = Convert.ToDouble(datosVuelo[1], CultureInfo.InvariantCulture);
                    double posicionY = Convert.ToDouble(datosVuelo[2], CultureInfo.InvariantCulture);
                    double destinoX = Convert.ToDouble(datosVuelo[3], CultureInfo.InvariantCulture);
                    double destinoY = Convert.ToDouble(datosVuelo[4], CultureInfo.InvariantCulture);
                    double velocidad = Convert.ToDouble(datosVuelo[5], CultureInfo.InvariantCulture);

                    if (velocidadesGuardadas != null)
                        velocidad = Convert.ToDouble(velocidadesGuardadas[i - 2], CultureInfo.InvariantCulture);

                    FlightPlan vuelo = new FlightPlan(id, posicionX, posicionY, destinoX, destinoY, velocidad);
                    if (listaFichero.AddFlightPlan(vuelo) != 0)
                        return null;
                }

                if (velocidadesGuardadas != null)
                {
                    listaFichero.SetDistanciaSeguridad(distanciaGuardada);
                    listaFichero.SetTiempoCiclo(cicloGuardado);
                }

                return listaFichero;
            }
            catch (FormatException)
            {
                return null;
            }
            catch (IndexOutOfRangeException)
            {
                return null;
            }
            catch (IOException)
            {
                return null;
            }
            catch (UnauthorizedAccessException)
            {
                return null;
            }
            catch (ArgumentException)
            {
                return null;
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

        public FlightPlanList Clonar()
        {
            // Crea una copia del objeto FlightPlanList actual (distancia de seguridad, tiempo de ciclo y cada vuelo clonado individualmente).
            FlightPlanList copia = new FlightPlanList();
            copia.SetDistanciaSeguridad(this.distanciaSeguridad);
            copia.SetTiempoCiclo(this.tiempoCiclo);

            for (int i = 0; i < this.number; i++)
            {
                FlightPlan vueloOriginal = this.vector[i];
                FlightPlan vueloClonado = vueloOriginal.Clonar();
                copia.AddFlightPlan(vueloClonado);
            }
            return copia;
        }

        /// Pila que guarda las copias de estados anteriores para poder deshacer pasos
        Stack<FlightPlan[]> historialEstadosVuelo = new Stack<FlightPlan[]>();

        // Devuelve 'true' si hay al menos un estado guardado en la pila al que poder retroceder, o 'false' si la pila está vacía (sirve para activar o desactivar el botón Deshacer).
        public bool PuedeDeshacer()
        {
            return historialEstadosVuelo.Count > 0;
        }

        // Crea una copia independiente del array actual de vuelos (vector) y la guarda en la pila antes de que los aviones avancen al siguiente paso.
        public void GuardarEstadoAnterior()
        {
            // Creamos un nuevo array vacío de 10 posiciones para no sobrescribir el original
            FlightPlan[] copiaVector = new FlightPlan[10];
            for (int i = 0; i < number; i++)
            {
                copiaVector[i] = vector[i].Clonar();
            }
            // Metemos la copia completa en la cima de la pila
            historialEstadosVuelo.Push(copiaVector);
        }

        // Retrocede un paso extrayendo de la pila el último array de vuelos guardado y reemplazando el array actual (vector) por esa copia anterior.
        public void DeshacerPaso()
        {
            // Comprobamos que la pila no esté vacía antes de extraer para evitar errores
            if (historialEstadosVuelo.Count > 0)
            {
                vector = historialEstadosVuelo.Pop();
            }
        }

        public void LimpiarHistorial()
        {
            // Vaciar la pila de estados anteriores 
            historialEstadosVuelo.Clear();
            
        }
    }
}

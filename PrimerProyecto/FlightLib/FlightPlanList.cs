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
        FlightPlan[] vector = new FlightPlan[100];
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
            if (number == 100)
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

//-----------------------------------------------------------------------------------------------------------------------------------------------------------------------
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

//-----------------------------------------------------------------------------------------------------------------------------------------------------------------------
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

//-----------------------------------------------------------------------------------------------------------------------------------------------------------------------
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

//-----------------------------------------------------------------------------------------------------------------------------------------------------------------------
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

//-----------------------------------------------------------------------------------------------------------------------------------------------------------------------
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

//-----------------------------------------------------------------------------------------------------------------------------------------------------------------------
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

        //-----------------------------------------------------------------------------------------------------------------------------------------------------------------------
        public void GuardarEnArchivo(string ruta)
        // FUNCIÓN: guardar la lista de vuelos en un fichero de texto para poder continuar la simulación para más tarde
        // ESTRUCTURA DEL FICHERO:
        //   - DISTANCIA distanciaSeguridad
        //   - CICLO tiempoCiclo
        //   - linea de vuelo: id company posicionX posicionY destinoX destinoY velocidad
        // Se guarda la posición ACTUAL, así al cargarlo la simulación sigue desde donde se quedó

        {
            //abrimos el fichero para escribir en él
            StreamWriter w = new StreamWriter(ruta);
            // primeras líneas: los parámetros de seguridad, cada uno con su título
            w.WriteLine("DISTANCIA " + distanciaSeguridad.ToString(CultureInfo.InvariantCulture));
            w.WriteLine("CICLO " + tiempoCiclo.ToString(CultureInfo.InvariantCulture));

            // escribimos cada vuelo en una línea, con los datos separados por espacios
            for (int i = 0; i < number; i++)
            {
                // llamamos "vuelo" a la posición de vector[i] para no tener que escribir vector[i] cada vez
                FlightPlan vuelo = vector[i];
                Position posicionActual = vuelo.GetCurrentPosition();
                Position posicionFinal = vuelo.GetFinalPosition();

                string linea = vuelo.GetId() + " " +
                    vuelo.GetCompany() + " " +
                    posicionActual.GetX().ToString(CultureInfo.InvariantCulture) + " " +
                    posicionActual.GetY().ToString(CultureInfo.InvariantCulture) + " " +
                    posicionFinal.GetX().ToString(CultureInfo.InvariantCulture) + " " +
                    posicionFinal.GetY().ToString(CultureInfo.InvariantCulture) + " " +
                    vuelo.GetVelocidad().ToString(CultureInfo.InvariantCulture);

                w.WriteLine(linea);
            }

            // siempre cerrar el fichero, si no lo escrito no puede guardarse correctamente
            w.Close();
        }

//-----------------------------------------------------------------------------------------------------------------------------------------------------------------------
        public FlightPlanList CargaDeFichero(string ruta)
        // FUNCIÓN: leer un fichero de planes de vuelo y devolver un objeto FlightPlanList con todos los vuelos cargados
        // Sirve para los 2 tipos de ficheros:
        //   - un fichero de planes de vuelo normal (entrada desde windows)
        //   - una simulación guardada (con DISTANCIA y CICLO al principio)
        // ESTRUCTURA DEL FICHERO:
        //   - DISTANCIA distanciaSeguridad (opcional, solo en simulaciones guardadas)
        //   - CICLO tiempoCiclo (opcional, solo en simulaciones guardadas)
        //   - linea de vuelo: id, company, posicionX, posicionY, destinoX, destinoY, velocidad
        {
            // lista donde se guardarán los vuelos leídos del fichero
            FlightPlanList listaFichero = new FlightPlanList();
            // el StreamReader se declara fuera del try (vacío) para poder cerrarlo en los catch
            StreamReader r = null;

            try
            {
                // abrimos el fichero para lectura, si no existe o hay algún error se lanza una excepción y se va al catch
                r = new StreamReader(ruta);
                // lee primera línea
                string linea = r.ReadLine();

                // mientras queden líneas
                while (linea != null)
                {
                    // partimos la línea por los espacios: cada trozo es un dato
                    string[] trozos = linea.Split(' ');

                    if(trozos[0] == "DISTANCIA")
                    {
                        // la línea de distancia tiene que tener 2 datos: DISTANCIA y el número
                        if (trozos.Length != 2)
                        {
                            r.Close();
                            return null;
                        }
                        // línea de la distancia de seguridad (solo en simulaciones guardadas)
                        listaFichero.SetDistanciaSeguridad(Convert.ToDouble(trozos[1], CultureInfo.InvariantCulture));
                    }

                    else if (trozos[0] == "CICLO")
                    {
                        // la línea de ciclo tiene que tener 2 datos: CICLO y el número
                        if (trozos.Length != 2)
                        {
                            r.Close();
                            return null;
                        }
                        // línea del tiempo de ciclo (solo en simulaciones guardadas)
                        listaFichero.SetTiempoCiclo(Convert.ToDouble(trozos[1], CultureInfo.InvariantCulture));
                    }

                    else
                    {
                        // una línea de vuelo tiene que tener 7 datos
                        if (trozos.Length != 7)
                        {
                            r.Close();
                            return null;
                        }

                        // línea de vuelo normal
                        string id = trozos[0];
                        string company = trozos[1];
                        double posicionX = Convert.ToDouble(trozos[2], CultureInfo.InvariantCulture);
                        double posicionY = Convert.ToDouble(trozos[3], CultureInfo.InvariantCulture);
                        double destinoX = Convert.ToDouble(trozos[4], CultureInfo.InvariantCulture);
                        double destinoY = Convert.ToDouble(trozos[5], CultureInfo.InvariantCulture);
                        double velocidad = Convert.ToDouble(trozos[6], CultureInfo.InvariantCulture);
                        // creamos un nuevo FlightPlan con los datos leídos del fichero y lo añadimos a la lista de vuelos
                        FlightPlan vuelo = new FlightPlan(id, company, posicionX, posicionY, destinoX, destinoY, velocidad);
                        int resultado = listaFichero.AddFlightPlan(vuelo);

                        // si el resultado es -1, significa que la lista está llena y no se pueden añadir más vuelos
                        if (resultado == -1)
                        {
                            r.Close();
                            return null;
                        }
                    }

                    // leemos la siguiente línea
                    linea = r.ReadLine();
                }

                // cerramos el fichero y devolvemos la lista llena
                r.Close();
                return listaFichero;
            }
            catch (FormatException)
            {
                // algún número del fichero tiene letras en vez de números
                r.Close();
                return null;
            }
            catch (IndexOutOfRangeException)
            {
                // a alguna línea le faltan datos (o hay una línea vacía)
                r.Close();
                return null;
            }
            catch (IOException)
            {
                // el fichero no existe o no se puede leer (por ejemplo, está abierto por otro programa)
                if (r != null)
                {
                    r.Close();
                }
                return null;
            }
            catch (UnauthorizedAccessException)
            {
                // no hay permiso para abrir el fichero
                if (r != null)
                {
                    r.Close();
                }
                return null;
            }
            catch (ArgumentException)
            {
                // la ruta del fichero está vacía o no es válida
                if (r != null)
                {
                    r.Close();
                }
                return null;
            }
        }

//-----------------------------------------------------------------------------------------------------------------------------------------------------------------------
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

//-----------------------------------------------------------------------------------------------------------------------------------------------------------------------
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

//-----------------------------------------------------------------------------------------------------------------------------------------------------------------------
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
            // Creamos un nuevo array vacío de 100 posiciones para no sobrescribir el original
            FlightPlan[] copiaVector = new FlightPlan[100];
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

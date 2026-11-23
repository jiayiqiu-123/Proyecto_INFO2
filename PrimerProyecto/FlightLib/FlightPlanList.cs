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
        float distanciaSeguridad = 0;
        double tiempoCiclo;

        // GETTERS
        public int GetNumber()
        {
            return number;
        }
        public float GetDistanciaSeguridad()
        {
            return distanciaSeguridad;
        }
        public double GetTiempoCiclo()
        {
            return tiempoCiclo;
        }

        // SETTERS
        public void SetTiempoCiclo(double t)
        {
            this.tiempoCiclo = t;
        }
        public void SetDistanciaSeguridad(float d)
        {
            this.distanciaSeguridad = d;
        }
        public int AddFlightPlan(FlightPlan p)
        {
            if (number == 100)
            { return -1; }
            else
            {
                vector[number] = p;
                number++;
                return 0;
            }
        }

        //MÉTODOS

        // Devuelve el vuelo que está en la posición i de la lista (FlightPlan),
        // o null si la posición no es válida (menor que 0 o mayor/igual que el número de vuelos).
        // esto es un getter para devolver un FlightPlan de la lista, que esté en la posición i de vector
        public FlightPlan GetFlightPlan(int i)
        {
            if (i < 0 || i >= number)
            { return null; }
            else
            { return vector[i]; }


        }


        // Avanza un ciclo de simulación: guarda el estado actual (para poder deshacer) y mueve todos los vuelos.
        public void MoverTodosVuelos()
        {
            GuardarEstadoAnterior();
            int i = 0;
            while (i < number)
            {
                vector[i].Mover(tiempoCiclo);
                i++;
            }
        }
        // Comprueba si todos los vuelos han llegado a su destino.
        // Devuelve true si han llegado todos, o false en cuanto encuentra uno que todavía está en vuelo.
        public bool HasAllArrived()
        {
            for (int i = 0; i < number; i++)
            {
                if (!vector[i].HasArrived())
                    return false;
            }
            return true;
        }

        // Predice, a partir de las trayectorias completas, qué vuelos entrarán en conflicto (sus círculos de seguridad se tocarán).
        // Para cada pareja calcula el instante de mínima distancia entre ellos, suponiendo velocidad constante y línea recta.
        // Devuelve una nueva FlightPlanList con los vuelos implicados (sin repetidos), o null si no hay ningún conflicto.
        public FlightPlanList EncontrarConflicto()
        {
            FlightPlanList vuelosEnConflicto = new FlightPlanList();

            // Cada pareja se compara una sola vez (j empieza en i + 1)
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
                        tDistMin = 0;
                    else
                        tDistMin = -arriba / abajo;

                    double tMax = Math.Min(longitud1 / velocidad1, longitud2 / velocidad2);
                    if (tDistMin < 0) tDistMin = 0;
                    if (tDistMin > tMax) tDistMin = tMax;

                    // Posiciones en ese instante
                    double distX = (xI1 + velocidadX1 * tDistMin) - (xI2 + velocidadX2 * tDistMin);
                    double distY = (yI1 + velocidadY1 * tDistMin) - (yI2 + velocidadY2 * tDistMin);
                    double distMin = Math.Sqrt(distX * distX + distY * distY);

                    // Hay conflicto si los círculos se tocan: la distancia entre centros es como mucho 2 veces el radio de seguridad
                    if (distMin <= distanciaSeguridad * 2)
                    {
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

        // Método auxiliar: comprueba si un vuelo(parametro2) ya está en la lista(parametro1)
        // Devuelve true si el vuelo (el mismo objeto) ya está en la lista, o false si no está.
        private bool EstaEnLista(FlightPlanList lista, FlightPlan vuelo)
        {
            for (int k = 0; k < lista.GetNumber(); k++)
            {
                if (lista.GetFlightPlan(k) == vuelo)
                    return true;
            }
            return false;
        }

        // Comprueba si, en este momento, hay dos vuelos cuyos círculos de seguridad se tocan (con las posiciones actuales).
        // A diferencia de EncontrarConflicto, no predice el futuro.
        // Devuelve true si hay al menos una pareja en conflicto, o false si no hay ninguna.
        public bool HayConflictoTiempoReal()
        {
            for (int i = 0; i < number; i++)
            {
                for (int j = i + 1; j < number; j++)
                {
                    FlightPlan vuelo1 = vector[i];
                    FlightPlan vuelo2 = vector[j];
                    double dist = vuelo1.Distance(vuelo2);

                    // Los círculos se tocan si la distancia entre centros es como mucho 2 veces el radio de seguridad
                    if (dist <= distanciaSeguridad * 2)
                        return true;
                }
            }
            return false;
        }
        // Intenta evitar los conflictos duplicando la velocidad de algunos vuelos.
        public void ResolverConflictos()
        {
            // Estrategia simple: se duplica la velocidad original de uno de cada dos vuelos de la lista de conflictos.
            if (vector != null)
            {
                // i = i + 2: solo se modifica un vuelo de cada dos
                // Se usa la velocidad ORIGINAL (no la actual), así que sustituye cualquier cambio de velocidad hecho antes
                for (int i = 0; i < number; i = i + 2)
                {
                    FlightPlan vuelo = vector[i];
                    double velocidadOriginal = vuelo.GetVelocidadOriginal();
                    double nuevaVelocidad = velocidadOriginal * 2; // Duplicar la velocidad
                    vuelo.SetVelocidad(nuevaVelocidad);
                }
            }
        }


        // FUNCIÓN: guardar la lista de vuelos en la ruta indicada y en un fichero de texto para poder continuar la simulación para más tarde
        // ESTRUCTURA DEL FICHERO:
        //   - DISTANCIA distanciaSeguridad
        //   - CICLO tiempoCiclo
        //   - linea de vuelo: id company posicionX posicionY destinoX destinoY velocidad
        // Se guarda la posición ACTUAL, así al cargarlo la simulación sigue desde donde se quedó
        public void GuardarEnArchivo(string ruta)
        {
            StreamWriter w = new StreamWriter(ruta);
            // InvariantCulture: los decimales se escriben siempre con punto, sin depender de la configuración regional
            w.WriteLine("DISTANCIA " + distanciaSeguridad.ToString(CultureInfo.InvariantCulture));
            w.WriteLine("CICLO " + tiempoCiclo.ToString(CultureInfo.InvariantCulture));

            for (int i = 0; i < number; i++)
            {
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
            w.Close();
        }

      
        // FUNCIÓN: leer un fichero de planes de vuelo con la ruta indicada y devolver un objeto FlightPlanList con todos los vuelos cargados
        // Sirve para los 2 tipos de ficheros:
        //   - un fichero de planes de vuelo normal (entrada desde windows)
        //   - una simulación guardada (con DISTANCIA y CICLO al principio)
        // ESTRUCTURA DEL FICHERO:
        //   - DISTANCIA distanciaSeguridad (opcional, solo en simulaciones guardadas)
        //   - CICLO tiempoCiclo (opcional, solo en simulaciones guardadas)
        //   - linea de vuelo: id, company, posicionX, posicionY, destinoX, destinoY, velocidad
        public FlightPlanList CargaDeFichero(string ruta)
        {
            FlightPlanList listaFichero = new FlightPlanList();
            StreamReader r = null;

            try
            {
                r = new StreamReader(ruta);
                string linea = r.ReadLine();

                while (linea != null)
                {
                    string[] trozos = linea.Split(' ');

                    if (trozos[0] == "DISTANCIA")
                    {
                        if (trozos.Length != 2)
                        {
                            r.Close();
                            return null;
                        }
                        listaFichero.SetDistanciaSeguridad(Convert.ToSingle(trozos[1], CultureInfo.InvariantCulture));
                    }

                    else if (trozos[0] == "CICLO")
                    {
                        if (trozos.Length != 2)
                        {
                            r.Close();
                            return null;
                        }
                        listaFichero.SetTiempoCiclo(Convert.ToDouble(trozos[1], CultureInfo.InvariantCulture));
                    }

                    else
                    {
                        if (trozos.Length != 7)
                        {
                            r.Close();
                            return null;
                        }

                        string id = trozos[0];
                        string company = trozos[1];
                        double posicionX = Convert.ToDouble(trozos[2], CultureInfo.InvariantCulture);
                        double posicionY = Convert.ToDouble(trozos[3], CultureInfo.InvariantCulture);
                        double destinoX = Convert.ToDouble(trozos[4], CultureInfo.InvariantCulture);
                        double destinoY = Convert.ToDouble(trozos[5], CultureInfo.InvariantCulture);
                        double velocidad = Convert.ToDouble(trozos[6], CultureInfo.InvariantCulture);

                        FlightPlan vuelo = new FlightPlan(id, company, posicionX, posicionY, destinoX, destinoY, velocidad);
                        int resultado = listaFichero.AddFlightPlan(vuelo);

                        // -1 significa que la lista está llena
                        if (resultado == -1)
                        {
                            r.Close();
                            return null;
                        }
                    }

                    linea = r.ReadLine();
                }

                r.Close();
                return listaFichero;
            }
            catch (FormatException)
            {
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
        // Escribe en la consola los datos de todos los vuelos de la lista (se usa para pruebas).
        public void EscribeConsola()
        {
            int i = 0;
            while (i < number)
            {
                vector[i].EscribeConsola();
                i++;
            }
        }

        /// Pila que guarda las copias de estados anteriores para poder deshacer pasos
        Stack<FlightPlan[]> historialEstadosVuelo = new Stack<FlightPlan[]>();
        // Indica si hay algún estado anterior al que volver (sirve para activar o desactivar el botón Deshacer).
        // Devuelve true si hay al menos un estado guardado en la pila, o false si está vacía.
        public bool PuedeDeshacer()
        {
            return historialEstadosVuelo.Count > 0;
        }

        // Crea una copia independiente del array actual de vuelos (vector) y la guarda en la pila antes de que los aviones avancen al siguiente paso.
        public void GuardarEstadoAnterior()
        {
            FlightPlan[] copiaVector = new FlightPlan[100];
            for (int i = 0; i < number; i++)
            {
                copiaVector[i] = vector[i].Clonar();
            }
            historialEstadosVuelo.Push(copiaVector);
        }

        // Retrocede un paso extrayendo de la pila el último array de vuelos guardado y reemplazando el array actual (vector) por esa copia anterior.
        public void DeshacerPaso()
        {
            if (historialEstadosVuelo.Count > 0)
            {
                vector = historialEstadosVuelo.Pop();
            }
        }

        // Vaciar la pila de estados anteriores 
        // Después ya no se puede deshacer ningún paso
        public void LimpiarHistorial()
        {
            historialEstadosVuelo.Clear();

        }
    }
}

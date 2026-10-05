using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
// libreria que estamos creando nosotros mismos
using FlightLib;

namespace SimulatorConsole
{
    public class Program
    {
        // !! (2) AÑADIR MULTIPLES PLANES DE VUELO
        //crearemos un vector de FlightPlans
        //FlightPlan[] vector = new FlightPlan[10];
        //esto sería un ejemplo de como hacerlo que no seguiremos por la programación orientada a objetos, si lo hacemos detro de una clase sería mejor


        static void Main(string[] args)
        {
            // !! (2) AÑADIR MULTIPLES PLANES DE VUELO
            FlightPlanList lista = new FlightPlanList();
            // el programa principal ahora tiene una lista

            // !! (1) TRATAR EXCEPCIÓN DE FORMATO INCORRECTO (cuando el usuario mete un string en vez de un double)
            //
            try
            {
                Console.WriteLine("Escribe el identificador");
                //string nombre = Console.ReadLine();
                string identificador = Console.ReadLine(); ;

                Console.WriteLine("Escribe la velocidad");
                double velocidad = Convert.ToDouble(Console.ReadLine());
                //DOUBLE = numero real (ocupa un poco más de memoria) - es igual usar tanto fload como double

                Console.WriteLine("Escribe las coordenadas de la posición inicial, separadas por un blanco");
                string linea = Console.ReadLine();
                string[] trozos = linea.Split(' ');
                double ix = Convert.ToDouble(trozos[0]);
                double iy = Convert.ToDouble(trozos[1]);

                Console.WriteLine("Escribe las coordenadas de la posición final, separadas por un blanco");
                linea = Console.ReadLine();
                trozos = linea.Split(' ');
                double fx = Convert.ToDouble(trozos[0]);
                double fy = Convert.ToDouble(trozos[1]);

                // aquí estamos diciendo que plan_a es de tipo FlightPlan, una clase previamente definida
                // CÓDIGO BASTANTE PRINCIPAL: estamos creando un flightplan con los datos que nos ha dado el usuario (y calculando)
                FlightPlan plan_a = new FlightPlan(identificador, ix, iy, fx, fy, velocidad);
                // el new es llamar al constructor que hemos creado en FlightPlan.cs 


                // !! (1) CREAMOS UN FLIGHTPLAN_B PARA COMPROBAR CONFLICTOS
                // como ya tenemos las variables definidas y del tipo que son, podemos reutilizarlas sin necesidad de volver a declararlas (MUY IMPORTANTE)
                Console.WriteLine("Escribe el identificador");
                identificador = Console.ReadLine(); ;

                Console.WriteLine("Escribe la velocidad");
                velocidad = Convert.ToDouble(Console.ReadLine());
                
                Console.WriteLine("Escribe las coordenadas de la posición inicial, separadas por un blanco");
                linea = Console.ReadLine();
                trozos = linea.Split(' ');
                ix = Convert.ToDouble(trozos[0]);
                iy = Convert.ToDouble(trozos[1]);

                Console.WriteLine("Escribe las coordenadas de la posición final, separadas por un blanco");
                linea = Console.ReadLine();
                trozos = linea.Split(' ');
                fx = Convert.ToDouble(trozos[0]);
                fy = Convert.ToDouble(trozos[1]);

                // aquí estamos diciendo que plan_b es de tipo FlightPlan
                FlightPlan plan_b = new FlightPlan(identificador, ix, iy, fx, fy, velocidad);

                // (2) AÑADIR LOS FLIGHTPLANS A LA LISTA
                lista.AddFlightPlan(plan_a);
                lista.AddFlightPlan(plan_b);

                // !! (1) BUCLE DE SIMULACIÓN (ciclos + tiempo de ciclos)
                //
                int ciclos = 100;
                int tiempoCiclo = 10;
                double distanciaSeguridad = 10;

                int i = 0;
                while (i < ciclos)
                {
                    // leen todo, lo mueven y lo muestran después
                    // (2) estamos cambiando la forma de mover los planes de vuelo, ahora lo hacemos desde la lista
                    // ahora el programa principal no tiene que saber cuantos planes de vuelo hay
                    lista.Mover(tiempoCiclo);

                    lista.EscribeConsola();

                    if (plan_a.Conflicto(plan_b, distanciaSeguridad))
                        Console.WriteLine("¡Conflicto detectado!");
                    i = i + 1;
                }

            }
            catch (FormatException)
            {
                Console.WriteLine("Error de formato");
            }

            Console.ReadLine();
        }
    }
}


//------------------------------------------------------------------------------------------------------------------------------
// AQUÍ ESTARIAMOS MOVIENDO EL CÓDIGO UNO A UNO CON EL GET, lo vamos a hacer en un bucle mejor para qu elo haga en una lista directamente con un método pero para que quede constancia
//int i = 0;
//while (i < ciclos)
//{
        // leen todo, lo mueven y lo muestran después
        // (2) estamos cambiando la forma de mover los planes de vuelo, ahora lo hacemos desde la lista
//    lista.GetFlightPlan(0).Mover(tiempoCiclo);
//   lista.GetFlightPlan(1).Mover(tiempoCiclo);

    //??PORQUE 0 Y 1?

    // esta llamando al metodo Mover
    //aquí también realizamos el cambio
//    lista.GetFlightPlan(0).EscribeConsola();
//    lista.GetFlightPlan(1).EscribeConsola();
//    if (lista.GetFlightPlan(0).Conflicto(lista.GetFlightPlan(1), distanciaSeguridad))
//        Console.WriteLine("¡Conflicto detectado!");
//    i = i + 1;
//}

//            }
//            catch (FormatException)
//            {
//    Console.WriteLine("Error de formato");
//}

//Console.ReadLine();
//        }
//    }
//}
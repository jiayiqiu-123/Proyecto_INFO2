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
        Position initialPosition; // posición inicial
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
            this.initialPosition = new Position(cpx, cpy);
            this.currentPosition = new Position(cpx, cpy);
            this.finalPosition = new Position(fpx, fpy);
            this.velocidad = velocidad;
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

        public double GetVelocidad()
        { return velocidad; }


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

        ///////////////////////////////////////////////////

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
        public bool HasArrived()
        {
            bool resultado = false;

            if (currentPosition.Distancia(finalPosition) == 0)
                // (currentPosition == finalPosition) --> yo lo tenía así, quiero preguntar si valdría igual
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

        public void Restart()
        { currentPosition = initialPosition; }

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

        // Calcula la distancia mínima entre dos trayectorias rectilíneas y devuelve tMin (instante donde ocurre)
        // Implementación simple: sólo ifs, fors, arrays y operaciones básicas, sin delegates
        public double ComputeMinDistanceBetweenPlans(FlightPlan p1, FlightPlan p2, out double tMin)
        {
            double x10 = p1.GetInitialPosition().GetX();
            double y10 = p1.GetInitialPosition().GetY();
            double x1f = p1.GetFinalPosition().GetX();
            double y1f = p1.GetFinalPosition().GetY();
            double x20 = p2.GetInitialPosition().GetX();
            double y20 = p2.GetInitialPosition().GetY();
            double x2f = p2.GetFinalPosition().GetX();
            double y2f = p2.GetFinalPosition().GetY();

            double vx1 = x1f - x10; double vy1 = y1f - y10;
            double L1 = Math.Sqrt(vx1 * vx1 + vy1 * vy1);
            double ux1 = 0; double uy1 = 0;
            if (L1 > 0) { ux1 = vx1 / L1; uy1 = vy1 / L1; }

            double vx2 = x2f - x20; double vy2 = y2f - y20;
            double L2 = Math.Sqrt(vx2 * vx2 + vy2 * vy2);
            double ux2 = 0; double uy2 = 0;
            if (L2 > 0) { ux2 = vx2 / L2; uy2 = vy2 / L2; }

            double v1 = p1.GetVelocidad();
            double v2 = p2.GetVelocidad();

            double T1 = (L1 == 0 || v1 == 0) ?
                0 : (L1 * 60.0 / v1);
            double T2 = (L2 == 0 || v2 == 0) ? 0 : (L2 * 60.0 / v2);

            double bestD2 = double.MaxValue;
            double bestT = 0;

            // Evaluar intervalo [start,end] para Cx,Cy,Wx,Wy sin usar delegates
            void EvalInterval(double Cx, double Cy, double Wx, double Wy, double start, double end)
            {
                double a = Wx * Wx + Wy * Wy;
                double b = 2.0 * (Cx * Wx + Cy * Wy);
                double tCandidate = start;
                if (Math.Abs(a) > 1e-9)
                {
                    double t0 = -b / (2.0 * a);
                    if (t0 < start) tCandidate = start;
                    else if (t0 > end) tCandidate = end;
                    else tCandidate = t0;
                }
                double[] times = new double[3]; times[0] = start; times[1] = end; times[2] = tCandidate;
                for (int i = 0; i < 3; i++)
                {
                    double t = times[i];
                    double dx = Cx + Wx * t;
                    double dy = Cy + Wy * t;
                    double d2 = dx * dx + dy * dy;
                    if (d2 < bestD2) { bestD2 = d2; bestT = t; }
                }
            }

            // Ambos moviéndose
            double tA = (T1 < T2) ? T1 : T2;
            if (tA > 0)
            {
                double Cx = x10 - x20;
                double Cy = y10 - y20;
                double Wx = ux1 * (v1 / 60.0) - ux2 * (v2 / 60.0);
                double Wy = uy1 * (v1 / 60.0) - uy2 * (v2 / 60.0);
                EvalInterval(Cx, Cy, Wx, Wy, 0, tA);
            }

            // Uno llegó antes
            if (T1 < T2)
            {
                double start = T1; double end = T2;
                if (end > start)
                {
                    double Cx = x1f - x20;
                    double Cy = y1f - y20;
                    double Wx = -ux2 * (v2 / 60.0);
                    double Wy = -uy2 * (v2 / 60.0);
                    EvalInterval(Cx, Cy, Wx, Wy, start, end);
                }
            }
            else if (T2 < T1)
            {
                double start = T2; double end = T1;
                if (end > start)
                {
                    double Cx = x10 - x2f;
                    double Cy = y10 - y2f;
                    double Wx = ux1 * (v1 / 60.0);
                    double Wy = uy1 * (v1 / 60.0);
                    EvalInterval(Cx, Cy, Wx, Wy, start, end);
                }
            }

            // Ambos en destino
            double tEnd = (T1 > T2) ? T1 : T2;
            double dxEnd = x1f - x2f;
            double dyEnd = y1f - y2f;
            double d2End = dxEnd * dxEnd + dyEnd * dyEnd;
            if (d2End < bestD2) { bestD2 = d2End; bestT = tEnd; }

            tMin = bestT;
            return Math.Sqrt(bestD2);
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
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightLib
{
    public class ConflictDetector
    {
        FlightPlan fp1;
        FlightPlan fp2;
        

        public ConflictDetector(FlightPlan fp1, FlightPlan fp2)
        {
            this.fp1 = fp1;
            this.fp2 = fp2;
        }
        public bool SeCruzan(double dSeguridad)
        {
            // Posición inicial del vuelo 1
            double xI1 = fp1.GetInitialPosition().GetX();
            double yI1 = fp1.GetInitialPosition().GetY();

            // Posición final del vuelo 1
            double xF1 = fp1.GetFinalPosition().GetX();
            double yF1 = fp1.GetFinalPosition().GetY();

            // Posición inicial del vuelo 2
            double xI2 = fp2.GetInitialPosition().GetX();
            double yI2 = fp2.GetInitialPosition().GetY();

            // Posición final del vuelo 2
            double xF2 = fp2.GetFinalPosition().GetX();
            double yF2 = fp2.GetFinalPosition().GetY();

            // Desplazamiento vuelo 1
            double deltaX1 = xF1 - xI1;
            double deltaY1 = yF1 - yI1;
            double longitud1 = Math.Sqrt(deltaX1 * deltaX1 + deltaY1 * deltaY1);

            // Desplazamiento vuelo 2
            double deltaX2 = xF2 - xI2;
            double deltaY2 = yF2 - yI2;
            double longitud2 = Math.Sqrt(deltaX2 * deltaX2 + deltaY2 * deltaY2);

            // Comprobamos que los dos vuelos tengan una trayectoria
            if (longitud1 == 0 || longitud2 == 0)
            {
                return false;
            }
            // Velocidad total de cada vuelo
            double velocidad1 = fp1.GetVelocidad();
            double velocidad2 = fp2.GetVelocidad();

            // Componentes de la velocidad del vuelo 1
            double velocidadX1 = deltaX1 / longitud1 * velocidad1;
            double velocidadY1 = deltaY1 / longitud1 * velocidad1;

            // Componentes de la velocidad del vuelo 2
            double velocidadX2 = deltaX2 / longitud2 * velocidad2;
            double velocidadY2 = deltaY2 / longitud2 * velocidad2;

            // HAY QUE HACER --> VUELO1: P1(t) = posiciónInicial1 + velocidad1 * t
            //                           P2(t) = posiciónInicial2 + velocidad2 * t
            // y restarlas: D(t) = P1(t) - P2(t)
            
            // Diferencia inicial entre las posiciones de los dos vuelos
            double deltaXi = xI1 - xI2;
            double deltaYi = yI1 - yI2;

            // Diferencia entre las velocidades de los dos vuelos
            double deltaVx = velocidadX1 - velocidadX2;
            double deltaVy = velocidadY1 - velocidadY2;

            // X(t) = deltaXi + deltaVx * t;
            // Y(t) = deltaYi + deltaVy * t;
            // D(t) = √(X(t)² +Y(t)²)
            // D(t) = √((deltaXi + deltaVx · t)² +(deltaYi + deltaVy · t)²)
            // Derivada de  √u  =  u' / (2√u) -->

            //          2 * [deltaVx * X(t) + deltaVy * Y(t)]
            // D'(t) = ---------------------------------------
            //                   2 * √(X(t)² +Y(t)²)

            // D'(t) = 0 --> deltaVx * X(t) + deltaVy * Y(t) = 0
            // deltaVx² * t + deltaVx * deltaXi  + deltaVy² * t + deltaVy*deltaYi = 0

            // Aislamos t y queda:
            //           -[deltaXi * deltaVx + deltaYi * deltaVy]
            //      t =  ----------------------------------------
            //                    deltaVx² + deltaVy²

            double arriba = deltaXi * deltaVx + deltaYi * deltaVy;
            double abajo = deltaVx * deltaVx + deltaVy * deltaVy;
            if (abajo == 0)
                return false;
            double tDistMin = -arriba / abajo;

            // Si el conflicto pasa antes de que comience la simulacion comprobamos en el segundo cero si los aviones siguen en conflcito o no
            if (tDistMin < 0)
                tDistMin = 0;

            // Posición del vuelo 1 en tDistMin
            double x1min = xI1 + velocidadX1 * tDistMin;
            double y1min = yI1 + velocidadY1 * tDistMin;

            // Posición del vuelo 2 en tDistMin
            double x2min = xI2 + velocidadX2 * tDistMin;
            double y2min = yI2 + velocidadY2 * tDistMin;

            // Diferencia entre las posiciones en el momento de distancia mínima
            double distX = x1min - x2min;
            double distY = y1min - y2min;

            // Distancia entre los centros de los aviones
            double distMin = Math.Sqrt(distX * distX + distY * distY);

            if (distMin <= dSeguridad * 2)
                return true;
            else
                return false;
        }
    }
}

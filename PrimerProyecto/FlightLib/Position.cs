using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightLib
{
    public class Position
    {
        double x;
        double y;

        // Constructores

        public Position(double x, double y)
        {
            this.x = x;
            this.y = y;
        }

        // Getters
        public double GetX()
        { return x; }

        public double GetY()
        { return y; }

        // retorna la distancia entre los dos Postion
        public double Distancia(Position b)
        {
            double resultado = Math.Sqrt((x - b.x) * (x - b.x) + (y - b.y) * (y - b.y));
            return resultado;
        }

        // retorna una copia de la posición actual
        public Position Clonar()
        {
            return new Position(this.x, this.y);
        }
    }
}

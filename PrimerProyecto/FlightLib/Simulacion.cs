using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightLib
{
    public class Simulacion
    {
        FlightPlan vuelo1;
        FlightPlan vuelo2;
        double tiempoCiclo;
        double distanciaSeguridad;
        double velocidadOriginal1;
        double velocidadOriginal2;

        public Simulacion(FlightPlan vuelo1, FlightPlan vuelo2, double tiempoCiclo, double distanciaSeguridad)
        {
            this.vuelo1 = vuelo1;
            this.vuelo2 = vuelo2;
            this.tiempoCiclo = tiempoCiclo;
            this.distanciaSeguridad = distanciaSeguridad;
            velocidadOriginal1 = vuelo1.GetVelocidad();
            velocidadOriginal2 = vuelo2.GetVelocidad();
        }
        public void Mover()
        {
            vuelo1.Mover(tiempoCiclo);
            vuelo2.Mover(tiempoCiclo);
        }
        public bool HayConflicto()
        {
            return vuelo1.Conflicto(vuelo2, distanciaSeguridad);
        }
        public void Reiniciar()
        {
            vuelo1.Restart();
            vuelo2.Restart();
        }
        public void CambiarVelocidades(double velocidad1, double velocidad2)
        {
            vuelo1.SetVelocidad(velocidad1);
            vuelo2.SetVelocidad(velocidad2);
        }
        public bool PredecirConflicto()
        {
            ConflictDetector d = new ConflictDetector(vuelo1, vuelo2);
            return d.SeCruzan(distanciaSeguridad);
        }
        public void RestaurarVelocidades(double velocidad1, double velocidad2)
        {
            vuelo1.SetVelocidad(velocidad1);
            vuelo2.SetVelocidad(velocidad2);
        }
        public void DuplicarVelocidadVuelo1()
        {
            vuelo1.SetVelocidad(vuelo1.GetVelocidad() * 2);
        }
        public void RestaurarVelocidadesOriginales()
        {
            vuelo1.SetVelocidad(velocidadOriginal1);
            vuelo2.SetVelocidad(velocidadOriginal2);
        }
    }
}

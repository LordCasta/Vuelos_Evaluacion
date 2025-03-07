using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EvaluacionVuelos
{
    public class clsVuelos
    {

        String Codigo {  get; set; }
        String Origen { get; set; }
        String Destino { get; set; }
        DateTime FechaSalida { get; set; }
        int AsientosDisponibles { get; set; }

        public string GetCodigo() 
        {
            return Codigo;
        }

        public int getAsientosDisponibles() {  return AsientosDisponibles; }
        

        public clsVuelos(String codigo, String origen, String destino, DateTime fechaSalida, int asientosDisponibles) 
        {
            Codigo = codigo;
            Origen = origen;
            Destino = destino;
            FechaSalida = fechaSalida;
            AsientosDisponibles = asientosDisponibles;
        
        }

        

        public override string ToString()
        {
            return $"[{Codigo}] {Origen} → {Destino} | {FechaSalida.ToShortDateString()} | Asientos: {AsientosDisponibles}";
        }

        public void RestarAsientos(int asientos)
        {
            AsientosDisponibles -= asientos;
        }

    }
}

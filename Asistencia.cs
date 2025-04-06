using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Programacion_3
{
    public class Asistencia
    {

        public DateTime fecha { get; set; }
        public bool estado { get; set; }

        public Asistencia(DateTime fecha, bool estado)
        {
            this.fecha = fecha;
            this.estado = estado;
        }
    }
}

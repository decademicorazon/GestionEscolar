using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Programacion_3.Models
{

    public class Alumno
    {
        public int dni { get; set; }
        public string nombre { get; set; }
        public string apellido { get; set; }
        public string correo { get; set; }

        public int? CodigoGrupo { get; set; }
        [JsonIgnore]
        public Grupo grupo { get; set; }

        public bool participo { get; set; } = false;

        public List<Asistencia> asistencias { get; set; } = new List<Asistencia>();

        public Alumno(int dni, string nombre, string apellido, string correo)
        {
            this.dni = dni;
            this.nombre = nombre;
            this.apellido = apellido;
            this.correo = correo;
        }


        public void AgregarAsistencia(bool estado)
        {
            asistencias.Add(new Asistencia(DateTime.Now, estado));
        }
        public int ContarAsistencias()
        {
            return asistencias.Count(x => x.estado == true);
        }
    }


}


using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Programacion_3.Models
{
    public class Grupo
    {


        public int codigo { get; set; }
        public bool participo { get; set; } = false;
        [JsonIgnore]
        public List<Alumno> alumnos


        {
            get; set;
        }
        public Grupo(int codigo)
        {
            this.codigo = codigo;
            alumnos = new List<Alumno>();


        }

        public void AgregarAlumno(Alumno alumno)
        {
            if (alumnos.Count < 6)
            {
                alumnos.Add(alumno);
            }
            else
            {
                Console.WriteLine($"⚠ El grupo {codigo} ya tiene 6 alumnos. No se puede agregar más.");
            }
        }

        public void EliminarAlumno(Alumno alumno)
        {
            if (alumnos.Contains(alumno))
            {
                alumnos.Remove(alumno);
                alumno.grupo = null;
                alumno.CodigoGrupo = null;
                Console.WriteLine($"{alumno.nombre} ha sido eliminado del grupo {codigo}.");
            }
            else
            {
                Console.WriteLine($"El alumno {alumno.nombre} no esta en este grupo.");
            }
        }
    }
}


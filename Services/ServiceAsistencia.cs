using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Programacion_3.Services
{
    public class ServiceAsistencia
    {

        private ServiceAlumno _serviceAlumno;
        public ServiceAsistencia(ServiceAlumno serviceAlumno)
        {
            _serviceAlumno = serviceAlumno;
        }

        public void RegistrarAsistencia()
        {
            var alumnos = _serviceAlumno.MostrarAlumnos().OrderBy(a => a.apellido).ToList();
            Console.WriteLine($"Fecha: {DateTime.Now.ToShortDateString()}");

            foreach (var alumno in alumnos)
            {
                Console.WriteLine($"¿{alumno.nombre} {alumno.apellido} esta presente? (P / A)");
                string respuesta = Console.ReadLine().ToUpper();
                bool estado = respuesta == "P";
                alumno.AgregarAsistencia(estado);
            }
            JsonArchivos.GuardarEnJson(_serviceAlumno.MostrarAlumnos(), "alumnos.json");
            Console.WriteLine("Asistencia registrada correctamente.");
        }


        public void MostrarAsistencias()
        {
            var alumnos = _serviceAlumno.MostrarAlumnos();
            Console.WriteLine("Resumen de asistencia general:\n");
            foreach (var alumno in alumnos)
            {
                int total = alumno.asistencias.Count();
                int presentes = alumno.asistencias.Count(a => a.estado == true);
                int ausentes = total - presentes;
                double porcentaje = total > 0 ? (presentes * 100.0) / total : 0;

                Console.WriteLine($"{alumno.nombre} {alumno.apellido} - Total: {total}, Presentes: {presentes}, Ausentes: {ausentes}, Asistencia: {porcentaje:0.00}%");
            }
        }

        public void MostrarResumenIndividual()
        {
            Console.WriteLine("Ingrese el DNI o el apellido del alumno");
            Console.WriteLine("1. DNI");
            Console.WriteLine("2. Apellido");
            int opcion = int.Parse(Console.ReadLine());
            if (opcion == 1)
            {

                Console.WriteLine("Ingrese el DNI del alumno");
                int dni = Convert.ToInt32(Console.ReadLine());
                var alumnos = _serviceAlumno.MostrarAlumnos().Where(a => a.dni == dni).ToList();


                if (alumnos != null)
                {


                    foreach (var alumno in alumnos)
                    {
                        int total = alumno.asistencias?.Count ?? 0;
                        int presentes = alumno.asistencias?.Count(a => a.estado) ?? 0;
                        int ausentes = total - presentes;
                        double porcentaje = total > 0 ? (presentes * 100.0) / total : 0;

                        Console.WriteLine($"\nAlumno: {alumno.nombre} {alumno.apellido} - DNI: {alumno.dni}");
                        Console.WriteLine($"Total clases: {total}, Presentes: {presentes}, Ausentes: {ausentes}, Asistencia: {porcentaje:0.00}%");
                    }
                }
            }
            else if (opcion == 2)
            {
                Console.WriteLine("Ingrese el apellido del alumno");
                string apellido = Console.ReadLine();
                var alumnos = _serviceAlumno.MostrarAlumnos().Where(a => a.apellido == apellido).ToList();

                if (alumnos != null)
                {
                    foreach (var alumno in alumnos)
                    {
                        int total = alumno.asistencias?.Count ?? 0;
                        int presentes = alumno.asistencias?.Count(a => a.estado) ?? 0;
                        int ausentes = total - presentes;
                        double porcentaje = total > 0 ? (presentes * 100.0) / total : 0;

                        Console.WriteLine($"\nAlumno: {alumno.nombre} {alumno.apellido} - DNI: {alumno.dni}");
                        Console.WriteLine($"Total clases: {total}, Presentes: {presentes}, Ausentes: {ausentes}, Asistencia: {porcentaje:0.00}%");
                    }
                }
            }
        }
    }
}

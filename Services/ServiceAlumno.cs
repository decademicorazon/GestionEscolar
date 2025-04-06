using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Programacion_3.Models;

namespace Programacion_3.Services
{
    public class ServiceAlumno
    {

        public List<Alumno> alumnos;
        private string archivo = "alumnos.json";
        public ServiceAlumno()
        {
            alumnos = JsonArchivos.CargarDeJson<Alumno>(archivo);

            // 🔁 Reconectar los grupos
           
        }

        public void AgregarAlumno()
        {
            Console.WriteLine("Ingrese el DNI del alumno");
            int dni = Convert.ToInt32(Console.ReadLine());
            if (BuscarAlumno(dni) != null)
            {
                Console.WriteLine("El alumno ya existe");
                return;
            }
            Console.WriteLine("Ingrese el nombre del alumno");
            string nombre = Console.ReadLine();
            Console.WriteLine("Ingrese el apellido del alumno");
            string apellido = Console.ReadLine();
            Console.WriteLine("Ingrese el correo del alumno");
            string correo = Console.ReadLine();
            Alumno alumno = new Alumno(dni, nombre, apellido, correo);

            alumnos.Add(alumno);
            JsonArchivos.GuardarEnJson(alumnos, archivo);
            Console.WriteLine("Alumno agregado con éxito.");


        }


        public Alumno? BuscarAlumno(int dni)
        {
            return alumnos.FirstOrDefault(x => x.dni == dni);
        }



        public void SortearAlumno()
        {
            bool continuar = true;
            while (continuar)
            {


                var fechaHoy = DateTime.Now;
                var presentesHoy = alumnos.Where(x => x.asistencias.Any(a => a.fecha.Date == fechaHoy.Date && a.estado == true)).ToList();
                if (presentesHoy.Count == 0)
                {
                    Console.WriteLine("No hay alumnos presentes hoy");
                    return;
                }
                var disponibles = presentesHoy.Where(x => x.participo == false).ToList();
                if (disponibles.Count == 0)
                {
                    Console.WriteLine("Todos los alumnos presentes ya han sido sorteados.");
                    Console.WriteLine("¿Desea reiniciar el ciclo de participacion? (1. Si / 2. No)");
                    int respuesta = Convert.ToInt32(Console.ReadLine());

                    if (respuesta == 1)
                    {
                        foreach (var alumno in presentesHoy)
                        {
                            alumno.participo = false;
                        }
                        disponibles = presentesHoy;
                    }
                    else
                    {
                        Console.WriteLine("Sorteo finalizado.");
                        return;
                    }
                }

                Random random = new Random();
                var elegido = disponibles[random.Next(disponibles.Count)];
                elegido.participo = true;
                GuardarCambios();
                Console.WriteLine($"El alumno elegido es: {elegido.nombre} {elegido.apellido}");
                
                Console.WriteLine("¿Desea volver a sortear? (1. Si / 2. No)");
                int opcion = Convert.ToInt32(Console.ReadLine());
                continuar = opcion == 1;
                
            }

        }
        public List<Alumno> MostrarAlumnos()
        {
            return alumnos;

        }


        public void MostrarAlumnosMenu()
        {
            if (alumnos.Count == 0)
            {
                Console.WriteLine("No hay alumnos registrados.");
            }
            else
            {
                Console.WriteLine("Listado de alumnos:");
                foreach (var alumno in alumnos)
                {
                    Console.WriteLine($"DNI: {alumno.dni} - Nombre: {alumno.nombre} {alumno.apellido} - Correo: {alumno.correo}");
                }
            }
        }
        public void GuardarCambios()
        {
            JsonArchivos.GuardarEnJson(alumnos, archivo);
        }
        public void EliminarAlumno()
        {
            Console.WriteLine("1. Eliminar por DNI");
            Console.WriteLine("2. Eliminar por Apellido");
            Console.WriteLine("3. Salir");
            int opcion = Convert.ToInt32(Console.ReadLine());

            if (opcion == 1)
            {
                Console.WriteLine("Ingrese el DNI del alumno a eliminar");
                int dni = Convert.ToInt32(Console.ReadLine());
                var alumno = BuscarAlumno(dni);
                if (alumno == null)
                {
                    Console.WriteLine("El alumno no existe");
                    return;
                }
                alumnos.Remove(alumno);
                JsonArchivos.GuardarEnJson(alumnos, archivo);
                Console.WriteLine("Alumno eliminado");
            }
            else if (opcion == 2)
            {
                Console.WriteLine("Ingrese el apellido del alumno a eliminar");
                string apellido = Console.ReadLine();
                var alumno = alumnos.FirstOrDefault(x => x.apellido == apellido);
                if (alumno == null)
                {
                    Console.WriteLine("El alumno no existe");
                    return;
                }
                alumnos.Remove(alumno);
                JsonArchivos.GuardarEnJson(alumnos, archivo);
                Console.WriteLine("Alumno eliminado");
            }
            else if (opcion == 3)
            {
                return;
            }
            Console.Clear();
        }

        public void ModificarAlumno()
        {
            while (true)
            {
                Console.WriteLine("1. Modificar por DNI");
                Console.WriteLine("2. Modificar por Apellido");
                Console.WriteLine("3. Salir");
                int opcion = Convert.ToInt32(Console.ReadLine());
                switch (opcion)
                {
                    case 1:
                        Console.WriteLine("Ingrese el DNI del alumno a modificar");
                        int dni = Convert.ToInt32(Console.ReadLine());
                        var alumnoAmodificar = alumnos.FirstOrDefault(x => x.dni == dni);
                        if (alumnoAmodificar == null)
                        {
                            Console.WriteLine("El alumno no existe");
                            return;
                        }
                        ModificarAlumnoPidiendoDatos(alumnoAmodificar);
                        break;
                    case 2:
                        Console.WriteLine("Ingrese el apellido del alumno a modificar");
                        string apellido = Console.ReadLine();
                        var alumnoAmodificarApellido = alumnos.FirstOrDefault(x => x.apellido == apellido);
                        if (alumnoAmodificarApellido == null)
                        {
                            Console.WriteLine("El alumno no existe");
                            return;
                        }
                        ModificarAlumnoPidiendoDatos(alumnoAmodificarApellido);

                        break;
                    case 3: return;
                    default:
                        break;
                }



            }

        }

        public void ModificarAlumnoPidiendoDatos(Alumno alumno)
        {
            while (true)
            {
                Console.WriteLine("1. Modificar DNI");
                Console.WriteLine("2. Modificar Nombre");
                Console.WriteLine("3. Modificar Apellido");
                Console.WriteLine("4. Modificar Correo");
                Console.WriteLine("5. Modificar Todo");
                Console.WriteLine("6. Salir");
                int opcion = Convert.ToInt32(Console.ReadLine());
                switch (opcion)
                {
                    case 1:
                        Console.WriteLine("Ingrese el nuevo DNI");
                        int dni = Convert.ToInt32(Console.ReadLine());
                        alumno.dni = dni;
                        JsonArchivos.GuardarEnJson(alumnos, archivo);
                        break;
                    case 2:
                        Console.WriteLine("Ingrese el nuevo Nombre");
                        string nombre = Console.ReadLine();
                        alumno.nombre = nombre;
                        JsonArchivos.GuardarEnJson(alumnos, archivo);
                        break;
                    case 3:
                        Console.WriteLine("Ingrese el nuevo Apellido");
                        string apellido = Console.ReadLine();
                        alumno.apellido = apellido;
                        JsonArchivos.GuardarEnJson(alumnos, archivo);
                        break;
                    case 4:
                        Console.WriteLine("Ingrese el nuevo Correo");
                        string correo = Console.ReadLine();
                        alumno.correo = correo;
                        JsonArchivos.GuardarEnJson(alumnos, archivo);
                        break;
                    case 5:
                        Console.WriteLine("Ingrese el nuevo DNI");
                        int dni5 = Convert.ToInt32(Console.ReadLine());

                        Console.WriteLine("Ingrese el nuevo Nombre");
                        string nombre5 = Console.ReadLine();

                        Console.WriteLine("Ingrese el nuevo Apellido");
                        string apellido5 = Console.ReadLine();

                        Console.WriteLine("Ingrese el nuevo Correo");
                        string correo5 = Console.ReadLine();
                        Console.WriteLine("Desea confirmar?");
                        Console.WriteLine("1. Si");
                        Console.WriteLine("2. No");
                        int confirmar = Convert.ToInt32(Console.ReadLine());
                        if (confirmar == 1)
                        {
                            alumno.dni = dni5;
                            alumno.nombre = nombre5;
                            alumno.apellido = apellido5;
                            alumno.correo = correo5;
                            JsonArchivos.GuardarEnJson(alumnos, archivo);
                        }

                        break;
                    case 6: return;

                }
            }

        }
    }
}

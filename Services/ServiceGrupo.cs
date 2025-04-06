using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Programacion_3.Models;

namespace Programacion_3.Services
{
    public class ServiceGrupo
    {

        
    private List<Grupo> grupos;
        private string archivo = "grupos.json";
        public ServiceAlumno _sa;
        public ServiceGrupo(ServiceAlumno sa)
        {
            _sa = sa;
            grupos = JsonArchivos.CargarDeJson<Grupo>(archivo);
        }

        public Grupo? BuscarGrupo(int codigo)
        {
            return grupos.FirstOrDefault(x => x.codigo == codigo);
        }

        public void AgregarGrupo()
        {

            Console.WriteLine("Ingrese el codigo del grupo");
            int codigo = Convert.ToInt32(Console.ReadLine());
            if (BuscarGrupo(codigo) != null)
            {
                Console.WriteLine("El grupo ya existe");
                return;
            }
            Grupo nuevoGrupo = new Grupo(codigo);
            grupos.Add(nuevoGrupo);
            JsonArchivos.GuardarEnJson(grupos, archivo);
            Console.WriteLine($"Grupo creado con el codigo {codigo}");

            while (nuevoGrupo.alumnos.Count <= 6)
            {
                Console.WriteLine("Ingrese el DNI del alumno a agregar o escriba FINALIZAR para salir");

                string dni = Console.ReadLine();
                if (dni.ToUpper() == "FINALIZAR")
                {
                    break;
                }
                if (int.TryParse(dni, out int dniInt))
                {
                    var alumno = _sa.BuscarAlumno(dniInt);
                    if (alumno == null)
                    {
                        Console.WriteLine("El alumno no existe");
                        continue;
                    }
                    if (alumno.grupo != null)
                    {
                        Console.WriteLine($"{alumno.nombre} ya pertenece al grupo {alumno.grupo.codigo}. ¿Desea cambiarlo?");
                        Console.WriteLine("1. Si");
                        Console.WriteLine("2. No");
                        int cambiar = Convert.ToInt32(Console.ReadLine());
                        if (cambiar != 1) continue;
                        alumno.grupo.EliminarAlumno(alumno);
                        JsonArchivos.GuardarEnJson(grupos, archivo);
                        _sa.GuardarCambios();


                    }
                    nuevoGrupo.AgregarAlumno(alumno);
                    alumno.grupo = nuevoGrupo;
                    alumno.CodigoGrupo = nuevoGrupo.codigo;
                    Console.WriteLine($"{alumno.nombre} agregado al grupo {nuevoGrupo.codigo}");
                    JsonArchivos.GuardarEnJson(grupos, archivo);
                    _sa.GuardarCambios();

                }
                else
                {
                    Console.WriteLine("DNI invalido");
                }

            }
            JsonArchivos.GuardarEnJson(grupos, archivo);
            _sa.GuardarCambios();
        }



        public void ModificarGrupo()
        {
            Console.WriteLine("Ingrese el codigo del grupo a modificar");
            int codigo = Convert.ToInt32(Console.ReadLine());
            var grupo = BuscarGrupo(codigo);
            if (grupo == null)
            {
                Console.WriteLine("El grupo no existe");
                return;
            }
            while (true)
            {
                Console.WriteLine("1. Agregar Alumno");
                Console.WriteLine("2. Eliminar Alumno");
                Console.WriteLine("3. Salir");
                int opcion = Convert.ToInt32(Console.ReadLine());
                if (opcion == 1)
                {


                    Console.WriteLine("Ingrese el DNI del alumno a agregar");


                    int dni = Convert.ToInt32(Console.ReadLine());
                    var alumno = _sa.BuscarAlumno(dni);
                    if (alumno == null)
                    {
                        Console.WriteLine("El alumno no existe");
                        continue;
                    }
                    if (alumno.grupo != null)
                    {
                        Console.WriteLine($"{alumno.nombre} ya pertenece al grupo {alumno.grupo.codigo}. ¿Desea cambiarlo?");
                        Console.WriteLine("1. Si");
                        Console.WriteLine("2. No");
                        int cambiar = Convert.ToInt32(Console.ReadLine());
                        if (cambiar != 1) continue;
                        alumno.grupo.EliminarAlumno(alumno);
                    }
                    grupo.AgregarAlumno(alumno);
                    alumno.grupo = grupo;
                    alumno.CodigoGrupo = grupo.codigo;
                    Console.WriteLine($"{alumno.nombre} agregado al grupo {grupo.codigo}");
                    JsonArchivos.GuardarEnJson(grupos, archivo);
                    _sa.GuardarCambios();

                }


                else if (opcion == 2)
                {
                    Console.WriteLine("Ingrese el DNI del alumno a eliminar");
                    int dni = Convert.ToInt32(Console.ReadLine());
                    var alumno = grupo.alumnos.FirstOrDefault(x => x.dni == dni);
                    if (alumno == null)
                    {
                        Console.WriteLine("El alumno no pertenece a este grupo");
                        continue;
                    }
                    grupo.EliminarAlumno(alumno);
                    alumno.grupo = null;
                    Console.WriteLine("Alumno eliminado del grupo");
                    JsonArchivos.GuardarEnJson(grupos, archivo);
                    _sa.GuardarCambios();
                }
                else
                {
                    return;
                }

            }
        }

        public void MoverAlumno()
        {
            Console.WriteLine("Ingrese el DNI del alumno a mover");
            int dni = Convert.ToInt32(Console.ReadLine());
            var alumno = _sa.BuscarAlumno(dni);
            if (alumno == null)
            {
                Console.WriteLine("El alumno no existe");
                return;
            }
            if (alumno.grupo == null)
            {
                Console.WriteLine("El alumno no pertenece a ningun grupo");
                return;
            }
            Console.WriteLine("Ingrese el codigo del grupo al que desea mover al alumno");
            int codigo = Convert.ToInt32(Console.ReadLine());
            var grupo = BuscarGrupo(codigo);
            if (grupo == null)
            {
                Console.WriteLine("El grupo no existe");
                return;
            }
            if (grupo.alumnos.Count == 6)
            {
                Console.WriteLine("El grupo ya tiene 6 alumnos");
                return;
            }
            Console.WriteLine("¿Confirma el cambio? (1. Sí / 2. No)");
            if (Convert.ToInt32(Console.ReadLine()) == 1)
            {
                alumno.grupo.EliminarAlumno(alumno);
                grupo.AgregarAlumno(alumno);
                alumno.grupo = grupo;
                alumno.CodigoGrupo = grupo.codigo;
                Console.WriteLine($"{alumno.nombre} movido a {codigo}.");
                JsonArchivos.GuardarEnJson(grupos, archivo);
                _sa.GuardarCambios();
            }
        }


        public void SortearGrupo()
        {
            if (grupos.Count == 0)
            {
                Console.WriteLine("No hay grupos registrados");
                return;
            }

            var disponibles = grupos.Where(g => !g.participo).ToList();
            if (disponibles.Count == 0)
            {
                Console.WriteLine("No hay grupos disponibles para sortear");
                Console.WriteLine("Todos los grupos ya participaron. Desea reiniciarlos?");
                Console.WriteLine("1. Si");
                Console.WriteLine("2. No");
                int respuesta = Convert.ToInt32(Console.ReadLine());
                if (respuesta == 1)
                {
                    foreach (var grupo in grupos)
                    {
                        grupo.participo = false;
                    }
                    disponibles = grupos.ToList();
                }
                else
                    Console.WriteLine("Sorteo finalizado");
                    return;
            }

            Random random = new Random();
            var elegido = disponibles[random.Next(disponibles.Count)];
            elegido.participo = true;
            Console.WriteLine($"El grupo elegido es: {elegido.codigo}");
            JsonArchivos.GuardarEnJson(grupos, archivo);
            _sa.GuardarCambios();



        }
        public void EliminarGrupo(int codigo)
        {
            var grupo = BuscarGrupo(codigo);
            if (grupo == null)
            {
                Console.WriteLine("El grupo no existe");
                return;
            }

            foreach (var alumno in grupo.alumnos)
            {
                alumno.grupo = null;
                alumno.CodigoGrupo = null;
            }
            grupos.Remove(grupo);
            JsonArchivos.GuardarEnJson(grupos, archivo);
            Console.WriteLine($"Grupo {codigo} eliminado.");
        }

        public void MostrarGrupos()
        {
            foreach (var grupo in grupos)
            {
                Console.WriteLine($"\n Grupo: {grupo.codigo}");
                foreach (var alumno in grupo.alumnos.OrderBy(a => a.apellido))
                {
                    Console.WriteLine($"- {alumno.apellido}, {alumno.nombre} (DNI: {alumno.dni})");
                }
            }

        }

        public void MostrarEstudiantesSinGrupo()
        {
            var sinGrupo = _sa.MostrarAlumnos().Where(a => a.grupo == null);
            Console.WriteLine("Estudiantes sin grupo:");
            foreach (var alumno in sinGrupo)
            {
                Console.WriteLine($"- {alumno.apellido}, {alumno.nombre} (DNI: {alumno.dni})");
            }
        }

        public void MostrarGruposIncompletos()
        {
            var incompletos = grupos.Where(g => g.alumnos.Count < 6);
            Console.WriteLine("📌 Grupos con menos de 6 estudiantes:");
            foreach (var grupo in incompletos)
            {
                Console.WriteLine($"- {grupo.codigo} ({grupo.alumnos.Count} estudiantes)");
            }
        }


    }
}


using System.Text.Json;
using System;
using System.Text.Json.Serialization;
using Programacion_3;
using Programacion_3.Services;

public class Program
{
    public static void Main()
    {
        ServiceAlumno serviceAlumno = new ServiceAlumno();
        ServiceGrupo serviceGrupo = new ServiceGrupo(serviceAlumno);
        ServiceAsistencia serviceAsistencia = new ServiceAsistencia(serviceAlumno);

        foreach (var alumno in serviceAlumno.MostrarAlumnos())
        {
            if (alumno.CodigoGrupo.HasValue)
            {
                var grupo = serviceGrupo.BuscarGrupo(alumno.CodigoGrupo.Value);
                if (grupo != null)
                {
                    alumno.grupo = grupo;
                    grupo.AgregarAlumno(alumno);
                }
            }
        }

        bool salir = false;

        while (!salir)
        {
            Console.Clear();
            Console.WriteLine("SISTEMA DE GESTION ESCOLAR");
            Console.WriteLine("1. Gestión de Alumnos");
            Console.WriteLine("2. Gestión de Grupos");
            Console.WriteLine("3. Asistencias");
            Console.WriteLine("4. Sorteos");
            Console.WriteLine("5. Salir");
            Console.Write("Seleccione una opcion: ");

            string opcion = Console.ReadLine();

            switch (opcion)
            {
                case "1":
                    MenuAlumnos(serviceAlumno);
                    break;
                case "2":
                    MenuGrupos(serviceGrupo);
                    break;
                case "3":
                    MenuAsistencias(serviceAsistencia);
                    break;
                case "4":
                    SubMenuSorteos(serviceAlumno, serviceGrupo);
                    break;
               
                case "5":
                    salir = true;
                    break;
                default:
                    Console.WriteLine("Opción inválida.");
                    Console.ReadKey();
                    break;
            }
        }
    }

    public static void MenuAlumnos(ServiceAlumno sa)
    {
        bool volver = false;
        while (!volver)
        {
            Console.Clear();
            Console.WriteLine(" GESTIÓN DE ALUMNOS ");
            Console.WriteLine("1. Agregar Alumno");
            Console.WriteLine("2. Mostrar Alumnos");
            Console.WriteLine("3. Modificar Alumno");
            Console.WriteLine("4. Eliminar Alumno");
            Console.WriteLine("5. Volver");
            Console.Write("Seleccione una opción: ");

            string opcion = Console.ReadLine();
            switch (opcion)
            {
                case "1": sa.AgregarAlumno();Console.ReadKey(); break;
                case "2": sa.MostrarAlumnosMenu(); Console.ReadKey(); break;
                case "3": sa.ModificarAlumno();Console.ReadKey(); break;
                case "4": sa.EliminarAlumno(); Console.ReadKey(); break;
                case "5": volver = true; break;
                default: Console.WriteLine("Opción inválida."); Console.ReadKey(); break;
            }
        }
    }

    public static void MenuGrupos(ServiceGrupo sg)
    {
        bool volver = false;
        while (!volver)
        {
            Console.Clear();
            Console.WriteLine(" GESTIÓN DE GRUPOS ");
            Console.WriteLine("1. Crear Grupo");
            Console.WriteLine("2. Mostrar Grupos");
            Console.WriteLine("3. Eliminar Grupo");
            Console.WriteLine("4. Modificar Grupo");
            Console.WriteLine("5. Mover Alumnos");
            Console.WriteLine("6. Mostrar estudiantes sin grupo");
            Console.WriteLine("7. Volver");
            Console.Write("Seleccione una opción: ");

            string opcion = Console.ReadLine();
            switch (opcion)
            {
                case "1": sg.AgregarGrupo(); break;
                case "2": sg.MostrarGrupos(); Console.ReadKey(); break;
                case "3": 
                    Console.WriteLine("Ingrese el codigo del grupo a eliminar");
                    int codigo = Convert.ToInt32(Console.ReadLine());
                    sg.EliminarGrupo(codigo); break;
                case "4": sg.ModificarGrupo(); break;
                case "5": sg.MoverAlumno(); break;
                    case "6": sg.MostrarEstudiantesSinGrupo();Console.ReadKey(); break;
                case "7": volver = true; break;
                default: Console.WriteLine("Opción invalida."); Console.ReadKey(); break;
            }
        }
    }

    public static void MenuAsistencias(ServiceAsistencia sa)
    {
        bool volver = false;
        while (!volver)
        {
            Console.Clear();
            Console.WriteLine(" REGISTRO DE ASISTENCIAS ");
            Console.WriteLine("1. Tomar Asistencia");
            Console.WriteLine("2. Mostrar Asistencias");
            Console.WriteLine("3. Mostrar Resumen de Asistencias Individual");
            Console.WriteLine("4. Volver");
            Console.Write("Seleccione una opción: ");

            string opcion = Console.ReadLine();
            switch (opcion)
            {
                case "1": sa.RegistrarAsistencia(); break;
                case "2": sa.MostrarAsistencias(); Console.ReadKey(); break;
                case "3": sa.MostrarResumenIndividual(); Console.ReadKey(); break;
                case "4": volver = true; break;
                default: Console.WriteLine("Opción inválida."); Console.ReadKey(); break;
            }
        }
    }

    public static void SubMenuSorteos(ServiceAlumno sa, ServiceGrupo sg)
    {
        bool volver = false;
        while (!volver)
        {
            Console.Clear();
            Console.WriteLine("=== SORTEOS ===");
            Console.WriteLine("1. Sortear Alumno");
            Console.WriteLine("2. Sortear Grupo");
            Console.WriteLine("3. Volver");
            Console.Write("Seleccione una opción: ");

            string opcion = Console.ReadLine();
            switch (opcion)
            {
                case "1": sa.SortearAlumno();

                    Console.ReadKey(); break;
                case "2": sg.SortearGrupo();
                    Console.ReadKey(); 
                    break;
                case "3": volver = true; break;
                default: Console.WriteLine("Opción inválida."); Console.ReadKey(); break;
            }
        }
    }
}








public static class JsonArchivos
{
    public static void GuardarEnJson<T>(List<T> lista, string archivo)
    {
        var opciones = new JsonSerializerOptions { WriteIndented = true };
        string json = JsonSerializer.Serialize(lista, opciones);
        File.WriteAllText(archivo, json);
    }

    public static List<T> CargarDeJson<T>(string archivo)
    {
        if (!File.Exists(archivo)) return new List<T>();
        string json = File.ReadAllText(archivo);
        try
        {
            return JsonSerializer.Deserialize<List<T>>(json);
        }
        catch (JsonException ex)
        {
            Console.WriteLine($"Error al deserializar el archivo JSON: {ex.Message}");
            return new List<T>();
        }
      
    }
}
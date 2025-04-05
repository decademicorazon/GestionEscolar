
using System.Text.Json;
using System;

public class Program
{
    public static void Main()
    {
        ServiceAlumno serviceAlumno = new ServiceAlumno();
        ServiceGrupo serviceGrupo = new ServiceGrupo(serviceAlumno);
        ServiceAsistencia serviceAsistencia = new ServiceAsistencia(serviceAlumno);

        bool salir = false;

        while (!salir)
        {
            Console.Clear();
            Console.WriteLine("SISTEMA DE GESTION ESCOLAR");
            Console.WriteLine("1. Gestión de Alumnos");
            Console.WriteLine("2. Gestión de Grupos");
            Console.WriteLine("3. Asistencias");
            Console.WriteLine("4. Sorteos");
            Console.WriteLine("5. Guardar Cambios");
            Console.WriteLine("6. Salir");
            Console.Write("Seleccione una opción: ");

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
                  /*  serviceAlumno.GuardarCambios();
                    serviceGrupo.GuardarCambios();
                    serviceAsistencia.GuardarCambios();
                    Console.WriteLine("Datos guardados con éxito.");
                  */  Console.ReadKey();
                    break;
                case "6":
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
                case "3": sa.ModificarAlumno(); break;
                case "4": sa.EliminarAlumno(); break;
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
            Console.WriteLine("6. Volver");
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
                case "6": volver = true; break;
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
            Console.WriteLine("3. Volver");
            Console.Write("Seleccione una opción: ");

            string opcion = Console.ReadLine();
            switch (opcion)
            {
                case "1": sa.RegistrarAsistencia(); break;
                case "2": sa.MostrarAsistencias(); Console.ReadKey(); break;
                case "3": volver = true; break;
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
                case "1": sa.SortearAlumno(); break;
                case "2": sg.SortearGrupo(); break;
                case "3": volver = true; break;
                default: Console.WriteLine("Opción inválida."); Console.ReadKey(); break;
            }
        }
    }
}






public class Alumno 
{
public int dni { get; set; }
    public string nombre { get; set; }
    public string apellido { get; set; }
    public string correo { get; set; }
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

public class ServiceAlumno
{
    public List<Alumno> alumnos = new List<Alumno>();

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
        Console.WriteLine("Alumno agregado");
        string json = JsonSerializer.Serialize(alumno);
        File.WriteAllText("alumnos.json", json);
        Console.WriteLine("Datos guardados con éxito.");
    }
    public void GuardarCambios()
    {
        string json = JsonSerializer.Serialize(alumnos, new JsonSerializerOptions { WriteIndented = true });
        File.WriteAllText("alumnos.json", json);
        Console.WriteLine("Datos guardados con éxito.");
    }


    public Alumno? BuscarAlumno(int dni)
    {
        return alumnos.FirstOrDefault(x => x.dni == dni);
    }



    public void SortearAlumno()
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
            Console.WriteLine("No hay alumnos disponibles para sortear");
            foreach (var alumno in alumnos)
            {
                alumno.participo = false;
            }
            disponibles = presentesHoy;
        }

        Random random = new Random();
        var elegido = disponibles[random.Next(disponibles.Count)];
        elegido.participo = true;
        Console.WriteLine($"El alumno elegido es: {elegido.nombre} {elegido.apellido}");
        Console.WriteLine("¿Desea volver a sortear? (1. Si / 2. No)");
        int opcion = Convert.ToInt32(Console.ReadLine());
        if (opcion == 1)
        {
            
            SortearAlumno();
        }
        else
        {
            Console.WriteLine("Fin del sorteo");
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

    public void EliminarAlumno()
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
        Console.WriteLine("Alumno eliminado");
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
                    ModificarAlumnoPidiendoDatos(alumnoAmodificar);
                    break;
                case 2:
                    Console.WriteLine("Ingrese el apellido del alumno a modificar");
                    string apellido = Console.ReadLine();
                    var alumnoAmodificarApellido = alumnos.FirstOrDefault(x => x.apellido == apellido);
                    ModificarAlumnoPidiendoDatos(alumnoAmodificarApellido);
                    break;
                case 3:return;
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
                    break;
                case 2:
                    Console.WriteLine("Ingrese el nuevo Nombre");
                    string nombre = Console.ReadLine();
                    alumno.nombre = nombre;
                    break;
                case 3:
                    Console.WriteLine("Ingrese el nuevo Apellido");
                    string apellido = Console.ReadLine();
                    alumno.apellido = apellido;
                    break;
                case 4:
                    Console.WriteLine("Ingrese el nuevo Correo");
                    string correo = Console.ReadLine();
                    alumno.correo = correo;
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
                    }

                    break;
                case 6: return;

            }
        }

    }

    }



public class Grupo
{
    public int codigo { get; set; }
    public bool participo { get; set; } = false;
    public List<Alumno> alumnos 
    {
        get; set;
    }
    public Grupo (int codigo)
    {
        this.codigo = codigo;
        this.alumnos = new List<Alumno>();

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
            Console.WriteLine($"{alumno.nombre} ha sido eliminado del grupo {codigo}.");
        }
        else
        {
            Console.WriteLine($"El alumno {alumno.nombre} no esta en este grupo.");
        }
    }
}

public class ServiceGrupo
{
    private List<Grupo> grupos = new List<Grupo>();
    public ServiceAlumno _sa;
    public ServiceGrupo(ServiceAlumno sa)
    {
        _sa = sa;
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
                

                }
                nuevoGrupo.AgregarAlumno(alumno);
                alumno.grupo = nuevoGrupo;
                Console.WriteLine($"{alumno.nombre} agregado al grupo {nuevoGrupo.codigo}");


            }
            else
            {
                Console.WriteLine("DNI invalido");
            }

        }
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
            if (opcion == 1) {


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
                Console.WriteLine($"{alumno.nombre} agregado al grupo {grupo.codigo}");

            }


            else if (opcion == 2) {
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
            }else
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
            Console.WriteLine($"{alumno.nombre} movido a {codigo}.");
        }
    }


    public void SortearGrupo()
    {
        
        var disponibles = grupos.Where(g=>!g.participo).ToList();
        if (disponibles.Count == 0)
        {
            Console.WriteLine("No hay grupos disponibles para sortear");
            foreach (var grupo in grupos)
            {
                grupo.participo = false;
            }
            disponibles = grupos;
        }

        Random random = new Random();
        var elegido = disponibles[random.Next(disponibles.Count)];
        elegido.participo = true;
        Console.WriteLine($"El grupo elegido es: {elegido.codigo}");



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
        }
        grupos.Remove(grupo);
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

        foreach(var alumno in alumnos)
        {
            Console.WriteLine($"¿{alumno.nombre} {alumno.apellido} esta presente? (P / A)");
            string respuesta = Console.ReadLine().ToUpper();
            bool estado = respuesta == "P";
            alumno.AgregarAsistencia(estado);
        }
        Console.WriteLine("Asistencia registrada correctamente.");
    }


    public void MostrarAsistencias()
    {
        var alumnos = _serviceAlumno.MostrarAlumnos();
        Console.WriteLine("Resumen de asistencia general:\n");
        foreach(var alumno in alumnos)
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
        }else if (opcion == 2)
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
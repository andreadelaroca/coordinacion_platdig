// Program.cs (Consola simple)
// Generado con ayuda de GitHub Copilot: definición de la estructura de datos para un curso
// Sugerencias aceptadas (documentadas):
//  - Usar un tipo claro para representar un Curso (record immutable) con campos relevantes.
//  - Incluir Id, Nombre, Duración (en horas), Área y Nivel para facilitar filtro y presentación.
//  - Mantener catálogo en memoria como una lista de instancias para simplicidad en esta demo.

using System;
using System.Collections.Generic;
using System.Linq;

class Program
{
    // Diseño de la estructura de datos para un curso.
    // Decisión: usar un `record` para un POCO inmutable y conciso.
    // Campos elegidos: Id (int), Name (string), DurationHours (int), Area (string), Level (string).
    // - DurationHours se expresa en horas para facilitar cálculos futuros (ej. estimación de carga).
    // - Level puede ser: "Introductorio", "Intermedio", "Avanzado".
    // Generado con ayuda de GitHub Copilot: se aceptó el patrón de record para claridad y concisión.
    public record Course(int Id, string Name, int DurationHours, string Area, string Level);

    // Catálogo de cursos en memoria (ejemplos).
    static List<Course> Courses = new()
    {
        new Course(1, "Algoritmos I", 48, "CS", "Introductorio"),
        new Course(2, "Introducción a la Programación", 60, "CS", "Introductorio"),
        new Course(3, "Matemática Discreta", 40, "Math", "Intermedio"),
        new Course(4, "Estructuras de Datos", 56, "CS", "Intermedio"),
        new Course(5, "Cálculo I", 72, "Math", "Introductorio"),
        new Course(6, "Bases de Datos", 48, "CS", "Intermedio"),
        new Course(7, "Probabilidad y Estadística", 64, "Math", "Intermedio"),
        new Course(8, "Sistemas Operativos", 60, "CS", "Avanzado"),
        new Course(9, "Redes de Computadoras", 50, "CS", "Intermedio"),
        new Course(10, "Inteligencia Artificial", 80, "CS", "Avanzado")
    };

    // Nota: por ahora no modificamos la UX principal — sólo el modelo de datos y el catálogo.
    // Las funciones de listado/ búsqueda/ paginación pueden usar estos campos para filtros más ricos.

    static void Main()
    {
        // Nuevo: paginación + navegación + revisión de pares en memoria
        const int defaultPageSize = 2;
        var pageSize = defaultPageSize;
        var reviews = new Dictionary<int, List<string>>(); // courseId -> list of reviews

        void ShowPage(IEnumerable<Course> list, int page, int pageSizeLocal)
        {
            var arr = list.ToArray();
            var total = arr.Length;
            var totalPages = (int)Math.Ceiling(total / (double)pageSizeLocal);
            if (total == 0)
            {
                Console.WriteLine("No hay cursos para mostrar.");
                return;
            }
            if (page < 1) page = 1;
            if (page > totalPages) page = totalPages;
            var start = (page - 1) * pageSizeLocal;
            var pageItems = arr.Skip(start).Take(pageSizeLocal).ToArray();

            Console.WriteLine($"Página {page}/{totalPages} — mostrando {pageItems.Length} de {total} cursos (pageSize={pageSizeLocal})");
            foreach (var c in pageItems)
            {
                Console.WriteLine($"[{c.Id}] {c.Name} — {c.Area} — {c.DurationHours}h — {c.Level}");
            }
        }

        void ShowCourseDetails(int courseId)
        {
            var c = Courses.FirstOrDefault(x => x.Id == courseId);
            if (c == null)
            {
                Console.WriteLine($"Curso con Id {courseId} no encontrado.");
                return;
            }
            Console.WriteLine($"Detalles del curso [{c.Id}]");
            Console.WriteLine($"Nombre: {c.Name}");
            Console.WriteLine($"Área: {c.Area}");
            Console.WriteLine($"Duración: {c.DurationHours} horas");
            Console.WriteLine($"Nivel: {c.Level}");
            if (reviews.TryGetValue(c.Id, out var list) && list.Any())
            {
                Console.WriteLine("Reseñas:");
                for (int i = 0; i < list.Count; i++)
                    Console.WriteLine($"  {i+1}. {list[i]}");
            }
            else
            {
                Console.WriteLine("No hay reseñas para este curso.");
            }
        }

        void AddReview(int courseId, string text)
        {
            var c = Courses.FirstOrDefault(x => x.Id == courseId);
            if (c == null)
            {
                Console.WriteLine($"Curso con Id {courseId} no encontrado.");
                return;
            }
            if (!reviews.ContainsKey(courseId)) reviews[courseId] = new List<string>();
            reviews[courseId].Add(text.Trim());
            Console.WriteLine("Reseña agregada.");
        }

        Console.WriteLine("Catálogo de Cursos - Navegación paginada + Revisión por pares (demo)");
        Console.WriteLine();
        Console.WriteLine("Comandos:");
        Console.WriteLine("  n            -> siguiente página");
        Console.WriteLine("  p            -> página anterior");
        Console.WriteLine("  f            -> primera página");
        Console.WriteLine("  l            -> última página");
        Console.WriteLine("  j <num>      -> ir a la página <num>");
        Console.WriteLine("  s <id>       -> ver detalles del curso con Id <id>");
        Console.WriteLine("  r <id>       -> agregar reseña al curso con Id <id> (se pedirá texto)");
        Console.WriteLine("  v <id>       -> ver reseñas del curso con Id <id>");
        Console.WriteLine("  ps <num>     -> cambiar pageSize a <num> (ej. ps 2)");
        Console.WriteLine("  q            -> salir");
        Console.WriteLine();

        // Inicio en página 1 del catálogo completo (se puede agregar filtros más tarde)
        var currentPage = 1;
        var filtered = Courses.AsEnumerable();
        ShowPage(filtered, currentPage, pageSize);

        while (true)
        {
            Console.WriteLine();
            Console.Write("cmd> ");
            var line = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(line))
            {
                Console.WriteLine("Ingrese un comando (q para salir).");
                continue;
            }
            var parts = line.Trim().Split(' ', 2, StringSplitOptions.RemoveEmptyEntries);
            var cmd = parts[0].ToLowerInvariant();
            var arg = parts.Length > 1 ? parts[1] : null;

            switch (cmd)
            {
                case "q":
                    Console.WriteLine("Saliendo...");
                    return;

                case "n":
                    currentPage++;
                    ShowPage(filtered, currentPage, pageSize);
                    break;

                case "p":
                    currentPage = Math.Max(1, currentPage - 1);
                    ShowPage(filtered, currentPage, pageSize);
                    break;

                case "f":
                    currentPage = 1;
                    ShowPage(filtered, currentPage, pageSize);
                    break;

                case "l":
                    {
                        var total = filtered.Count();
                        currentPage = (int)Math.Ceiling(total / (double)pageSize);
                        if (currentPage < 1) currentPage = 1;
                        ShowPage(filtered, currentPage, pageSize);
                    }
                    break;

                case "j":
                    if (int.TryParse(arg, out var goPage))
                    {
                        currentPage = Math.Max(1, goPage);
                        ShowPage(filtered, currentPage, pageSize);
                    }
                    else Console.WriteLine("Uso: j <número de página>");
                    break;

                case "s":
                    if (int.TryParse(arg, out var idS))
                    {
                        ShowCourseDetails(idS);
                    }
                    else Console.WriteLine("Uso: s <id curso>");
                    break;

                case "r":
                    if (int.TryParse(arg, out var idR))
                    {
                        Console.Write("Ingrese texto de la reseña (Enter para cancelar): ");
                        var txt = Console.ReadLine();
                        if (!string.IsNullOrWhiteSpace(txt))
                            AddReview(idR, txt);
                        else Console.WriteLine("Reseña vacía, cancelada.");
                    }
                    else Console.WriteLine("Uso: r <id curso>");
                    break;

                case "v":
                    if (int.TryParse(arg, out var idV))
                    {
                        if (reviews.TryGetValue(idV, out var list) && list.Any())
                        {
                            Console.WriteLine($"Reseñas para curso {idV}:");
                            for (int i = 0; i < list.Count; i++)
                                Console.WriteLine($"  {i+1}. {list[i]}");
                        }
                        else Console.WriteLine("No hay reseñas para ese curso.");
                    }
                    else Console.WriteLine("Uso: v <id curso>");
                    break;

                case "ps":
                    if (int.TryParse(arg, out var newSize) && newSize > 0)
                    {
                        pageSize = newSize;
                        currentPage = 1;
                        Console.WriteLine($"pageSize actualizado a {pageSize}");
                        ShowPage(filtered, currentPage, pageSize);
                    }
                    else Console.WriteLine("Uso: ps <número positivo>");
                    break;

                default:
                    // Permitir búsquedas rápidas por palabra clave con sintaxis: buscar <kw>
                    if (cmd == "buscar" && !string.IsNullOrWhiteSpace(arg))
                    {
                        var kw = arg.Trim();
                        filtered = Courses.Where(c =>
                            c.Name.Contains(kw, StringComparison.OrdinalIgnoreCase) ||
                            c.Area.Contains(kw, StringComparison.OrdinalIgnoreCase)
                        );
                        currentPage = 1;
                        Console.WriteLine($"Filtro aplicado: '{kw}'");
                        ShowPage(filtered, currentPage, pageSize);
                    }
                    else
                    {
                        Console.WriteLine("Comando no reconocido. Use n/p/f/l/j/s/r/v/ps/buscar/q");
                    }
                    break;
            }
        }
    }
}

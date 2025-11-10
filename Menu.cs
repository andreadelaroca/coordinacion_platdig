using System;
using System.Collections.Generic;

namespace CatalogoCursosApp
{
    class Program
    {
        static void Main(string[] args)
        {
            // Generado con ayuda de GitHub Copilot: estructura básica del menú
            bool salir = false;

            while (!salir)
            {
                Console.Clear();
                Console.WriteLine("=== Catálogo de Cursos ===");
                Console.WriteLine("1. Listar cursos");
                Console.WriteLine("2. Buscar curso");
                Console.WriteLine("3. Paginación");
                Console.WriteLine("4. Salir");
                Console.Write("Seleccione una opción: ");

                string opcion = Console.ReadLine();

                switch (opcion)
                {
                    case "1":
                        ListarCursos();
                        break;
                    case "2":
                        BuscarCurso();
                        break;
                    case "3":
                        PaginarCursos();
                        break;
                    case "4":
                        salir = true;
                        Console.WriteLine("Saliendo de la aplicación...");
                        break;
                    default:
                        Console.WriteLine("Opción inválida. Presione una tecla para continuar.");
                        Console.ReadKey();
                        break;
                }
            }
        }

        static void ListarCursos()
        {
            Console.Clear();
            Console.WriteLine("Listado de cursos:");
            // Aquí se llamaría a la función que lista los cursos
            Console.WriteLine("[Simulación] Cursos listados...");
            VolverAlMenu();
        }

        static void BuscarCurso()
        {
            Console.Clear();
            Console.Write("Ingrese término de búsqueda: ");
            string termino = Console.ReadLine();
            // Aquí se llamaría a la función que busca cursos
            Console.WriteLine($"[Simulación] Resultados para '{termino}'...");
            VolverAlMenu();
        }

        static void PaginarCursos()
        {
            Console.Clear();
            Console.WriteLine("Paginación de cursos:");
            // Aquí se llamaría a la función que pagina los cursos
            Console.WriteLine("[Simulación] Página 1 de cursos...");
            VolverAlMenu();
        }

        static void VolverAlMenu()
        {
            Console.WriteLine("\nPresione una tecla para volver al menú...");
            Console.ReadKey();
        }
    }
}
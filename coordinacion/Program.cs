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
		Console.WriteLine("Catálogo de Cursos - Estructura de datos definida (tarea 1 completada)");
		Console.WriteLine("Se definió el record Course y se rellenó el catálogo en memoria.");
		Console.WriteLine("Abra el archivo Program.cs para ver la definición y los comentarios.");
	}
}

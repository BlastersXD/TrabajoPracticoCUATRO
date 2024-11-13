using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrabajoPracticoCUATRO
{
    public class Program
    {
        public static void Main()
        {
            CODESKY CODESKY = new CODESKY();
            CODESKY.CargarDatos("vuelos.xml");
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("  ------------> Bienvenidos a Codesky <------------");
            Console.ResetColor();

            bool salir = false;
            while (!salir)
            {
                Console.ForegroundColor = ConsoleColor.Magenta;
                Console.WriteLine(" -------------------------------------------------");
                Console.WriteLine("|Menú de opciones:                                |");
                Console.WriteLine(" -------------------------------------------------");
                Console.WriteLine("|1. Agregar vuelo                                 |");
                Console.WriteLine("|2. Registrar pasajeros                           |");
                Console.WriteLine("|3. Calcular ocupación media de la flota          |");
                Console.WriteLine("|4. Vuelo con mayor ocupación                     |");
                Console.WriteLine("|5. Buscar vuelo por código                       |");
                Console.WriteLine("|6. Listar vuelos ordenados por ocupación         |");
                Console.WriteLine("|7. Salir                                         |");
                Console.WriteLine(" -------------------------------------------------");
                Console.Write("\nSeleccione una opción: ");
                Console.ResetColor();


                string opcion = Console.ReadLine();
                switch (opcion)
                {
                    case "1":

                        AgregarVuelo(CODESKY);
                        break;
                    case "2":
                        RegistrarPasajeros(CODESKY);
                        break;
                    case "3":
                        
                        Console.WriteLine("Ocupación media de la flota: " + CODESKY.CalcularOcupacionMedia() + "%");
                        
                        break;
                    case "4":
                        var vueloMayorOcupacion = CODESKY.VueloConMayorOcupacion();
                        if (vueloMayorOcupacion != null)
                            Console.WriteLine($"Vuelo con mayor ocupación: {vueloMayorOcupacion.CodigoVuelo} ({vueloMayorOcupacion.CalcularOcupacion()}%)");
                        else
                            Console.WriteLine("No hay vuelos registrados.");
                        break;
                    case "5":
                        Console.Write("Ingrese el código del vuelo: ");
                        string codigo = Console.ReadLine();
                        var vuelo = CODESKY.BuscarVuelo(codigo);
                        if (vuelo != null)
                            Console.WriteLine($"Vuelo encontrado: {vuelo.CodigoVuelo}, ocupación: {vuelo.CalcularOcupacion()}%");
                        else
                            Console.WriteLine("Vuelo no encontrado.");
                        break;
                    case "6":
                        var vuelosOrdenados = CODESKY.ListarVuelosOrdenadosPorOcupacion();
                        Console.WriteLine("Vuelos ordenados por ocupación:");
                        foreach (var v in vuelosOrdenados)
                        {
                            Console.WriteLine($"Código: {v.CodigoVuelo}, Ocupación: {v.CalcularOcupacion()}%");
                        }
                        break;
                    case "7":
                        salir = true;
                        CODESKY.GuardarDatos("vuelos.xml");
                        Console.WriteLine("Datos guardados. Saliendo del sistema.");
                        break;
                    default:
                        Console.WriteLine("Opción no válida, intente de nuevo.");
                        break;
                }
            }
        }

        private static void AgregarVuelo(CODESKY aerolinea)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write("Ingrese el código del vuelo: ");
            string codigoVuelo = Console.ReadLine();

            Console.Write("Fecha y hora de salida (yyyy-MM-dd HH:mm): ");
            DateTime salida = DateTime.Parse(Console.ReadLine());

            Console.Write("Fecha y hora de llegada (yyyy-MM-dd HH:mm): ");
            DateTime llegada = DateTime.Parse(Console.ReadLine());

            Console.Write("Capacidad máxima: ");
            int capacidadMaxima = int.Parse(Console.ReadLine());

            List<string> tripulantes = new List<string>();
            Console.Write("Nombre del piloto: ");
            tripulantes.Add(Console.ReadLine());
            Console.Write("Nombre del copiloto: ");
            tripulantes.Add(Console.ReadLine());

            vuelo vuelo = new vuelo
            {
                CodigoVuelo = codigoVuelo,
                FechaHoraSalida = salida,
                FechaHoraLlegada = llegada,
                CapacidadMaxima = capacidadMaxima,
                TripulantesCabina = tripulantes,
                PasajerosActuales = 0
            };

            aerolinea.AgregarVuelo(vuelo);
            Console.WriteLine("Vuelo agregado exitosamente.");
            Console.ResetColor();
        }

        private static void RegistrarPasajeros(CODESKY CODESKY)
        {
            Console.Write("Ingrese el código del vuelo: ");
            string codigo = Console.ReadLine();
            var vuelo = CODESKY.BuscarVuelo(codigo);

            if (vuelo != null)
            {
                Console.Write("Ingrese la cantidad de pasajeros a registrar: ");
                int pasajeros = int.Parse(Console.ReadLine());

                if (vuelo.PasajerosActuales + pasajeros <= vuelo.CapacidadMaxima)
                {
                    vuelo.PasajerosActuales += pasajeros;
                    Console.WriteLine("Pasajeros registrados exitosamente.");
                }
                else
                {
                    Console.WriteLine("La cantidad excede la capacidad del vuelo.");
                }
            }
            else
            {
                Console.WriteLine("Vuelo no encontrado.");
            }
        }
    }
}

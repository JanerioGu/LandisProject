using System;
using ProjetoLandisGyr.Models;
using ProjetoLandisGyr.Repositories;

namespace ProjetoLandisGyr
{
    class Program
    {
        private static readonly IEndpointRepository repository = new InMemoryEndpointRepository();

        static void Main()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("1) Insert a new endpoint");
                Console.WriteLine("2) Edit an existing endpoint");
                Console.WriteLine("3) Delete an endpoint");
                Console.WriteLine("4) List all endpoints");
                Console.WriteLine("5) Find an endpoint by serial number");
                Console.WriteLine("6) Exit");
                Console.Write("Select an option: ");

                switch (Console.ReadLine())
                {
                    case "1":
                        InsertEndpoint();
                        break;
                    case "2":
                        EditEndpoint();
                        break;
                    case "3":
                        DeleteEndpoint();
                        break;
                    case "4":
                        ListEndpoints();
                        break;
                    case "5":
                        FindEndpoint();
                        break;
                    case "6":
                        return;
                    default:
                        Console.WriteLine("Invalid option.");
                        break;
                }

                Console.WriteLine("Press any key to continue...");
                Console.ReadKey();
            }
        }

        private static void InsertEndpoint()
        {
            try
            {
                Console.Write("Enter Serial Number: ");
                var serial = Console.ReadLine();

                Console.Write("Enter Meter Model Id (16-19): ");
                var modelId = int.Parse(Console.ReadLine());

                Console.Write("Enter Meter Number: ");
                var meterNumber = int.Parse(Console.ReadLine());

                Console.Write("Enter Firmware Version: ");
                var firmware = Console.ReadLine();

                Console.Write("Enter Switch State (0=Disconnected, 1=Connected, 2=Armed): ");
                var switchState = int.Parse(Console.ReadLine());

                var endpoint = new Endpoint(serial, modelId, meterNumber, firmware, switchState);
                repository.AddEndpoint(endpoint);

                Console.WriteLine("Endpoint added successfully.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }

        private static void EditEndpoint()
        {
            Console.Write("Enter Serial Number: ");
            var serial = Console.ReadLine();

            Console.Write("Enter new Switch State (0=Disconnected, 1=Connected, 2=Armed): ");
            var newState = int.Parse(Console.ReadLine());

            if (repository.EditSwitchState(serial, newState))
                Console.WriteLine("Switch state updated.");
            else
                Console.WriteLine("Endpoint not found.");
        }

        private static void DeleteEndpoint()
        {
            Console.Write("Enter Serial Number: ");
            var serial = Console.ReadLine();

            if (repository.DeleteEndpoint(serial))
                Console.WriteLine("Endpoint deleted.");
            else
                Console.WriteLine("Endpoint not found.");
        }

        private static void ListEndpoints()
        {
            var endpoints = repository.GetAllEndpoints();

            if (!endpoints.Any())
            {
                Console.WriteLine("No endpoints found.");
                return;
            }

            endpoints
                .Select(e => e.ToString())
                .ToList()
                .ForEach(Console.WriteLine);
        }

        private static void FindEndpoint()
        {
            Console.Write("Enter Serial Number: ");
            var serial = Console.ReadLine();

            var endpoint = repository.FindEndpoint(serial);

            if (endpoint != null)
            {
                Console.WriteLine($"Endpoint found:\n{endpoint}");
            }
            else
            {
                Console.WriteLine("Endpoint not found.");
            }
        }
    }
}

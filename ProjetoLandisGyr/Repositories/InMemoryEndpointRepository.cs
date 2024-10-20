using System;
using System.Collections.Generic;
using System.Linq;
using ProjetoLandisGyr.Models;

namespace ProjetoLandisGyr.Repositories
{
    public class InMemoryEndpointRepository : IEndpointRepository
    {
        private readonly List<Endpoint> _endpoints = new();

        public void AddEndpoint(Endpoint endpoint)
        {
            if (endpoint == null)
                throw new ArgumentNullException(nameof(endpoint));

            if (string.IsNullOrWhiteSpace(endpoint.EndpointSerialNumber))
                throw new ArgumentException("Serial number cannot be null or whitespace.", nameof(endpoint.EndpointSerialNumber));

            if (endpoint.SwitchState < 0 || endpoint.SwitchState > 2)
                throw new ArgumentException("Invalid Switch State. Allowed values are 0 (Disconnected), 1 (Connected), or 2 (Armed).");

            if (_endpoints.Any(e => e.EndpointSerialNumber.Equals(endpoint.EndpointSerialNumber, StringComparison.OrdinalIgnoreCase)))
                throw new ArgumentException("Endpoint with the same serial number already exists (case insensitive).");

            _endpoints.Add(endpoint);
        }



        public bool EditSwitchState(string serialNumber, int newState)
        {
            if (newState < 0 || newState > 2)
                throw new ArgumentOutOfRangeException(nameof(newState),
                    "Allowed values are 0 (Disconnected), 1 (Connected), or 2 (Armed).");

            // Usando LINQ para encontrar o endpoint e atualizar o estado
            var endpoint = _endpoints
                .FirstOrDefault(e => e.EndpointSerialNumber.Equals(serialNumber, StringComparison.OrdinalIgnoreCase));

            if (endpoint != null)
            {
                endpoint.SetSwitchState(newState);
                return true;
            }

            return false;
        }



        public bool DeleteEndpoint(string serialNumber)
        {
            var endpoint = _endpoints.Find(e => e.EndpointSerialNumber == serialNumber);
            if (endpoint != null)
            {
                _endpoints.Remove(endpoint);
                return true;
            }
            return false;
        }

        public List<Endpoint> GetAllEndpoints()
        {
            return _endpoints
                .OrderBy(e => e.EndpointSerialNumber)  // Ordena por Serial Number
                .ToList();  // Retorna como lista
        }


        public Endpoint FindEndpoint(string serialNumber)
        {
            if (string.IsNullOrWhiteSpace(serialNumber))
                return null;

            // Usando LINQ para buscar o primeiro endpoint correspondente
            return _endpoints
                .FirstOrDefault(e => e.EndpointSerialNumber.Equals(serialNumber, StringComparison.OrdinalIgnoreCase));
        }

        public void ClearAllEndpoints()
        {
            _endpoints.Clear();
        }

    }
}

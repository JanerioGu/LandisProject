using ProjetoLandisGyr.Models;
using System.Collections.Generic;

namespace ProjetoLandisGyr.Repositories
{
    public interface IEndpointRepository
    {
        void AddEndpoint(Endpoint endpoint);
        bool EditSwitchState(string serialNumber, int newState);
        bool DeleteEndpoint(string serialNumber);
        Endpoint FindEndpoint(string serialNumber);
        List<Endpoint> GetAllEndpoints();
    }
}


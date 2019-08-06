using fullstack.clients.Models;
using fullstack.shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace fullstack.clients.Services.Interfaces
{
    interface IClientsService
    {
        PagedResultModel<Client> Get(int pageIndex, int pageSize);

        Client Get(string clientId);
        void Create(Client client);
        bool Update(string clientId, Client client);
        bool Delete(string clientId);

        decimal AverageAge();
    }
}

using fullstack.clients.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace fullstack.clients.Persistance.Clients
{
    public interface IClientsRepository
    {
        List<Client> Get(int pageIndex, int pageSize);

        void Create(Client client);

        bool Update(Client client );

        bool Delete(Client client);

        long TotalClients();
    }
}

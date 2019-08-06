using fullstack.clients.ExternalServices.Predictions;
using fullstack.clients.Models;
using fullstack.clients.Persistance.Clients;
using fullstack.clients.Services.Interfaces;
using fullstack.shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace fullstack.clients.Services
{
    public class ClientsService : IClientsService
    {
        private readonly IClientsRepository clientsRepository;
        private readonly IPredictionsClient predictionsClient;

        public ClientsService()
        {
            clientsRepository = new ClientsRepository();
            predictionsClient = new PredictionsClient();
        }

        private static void ValidateClientInformation(Client client)
        {
            if (client == null) throw new ArgumentNullException("client");
            if (string.IsNullOrWhiteSpace(client.Nombre)) throw new ArgumentException("Nombre es nulo o vacio.");
            if (string.IsNullOrWhiteSpace(client.Apellido)) throw new ArgumentException("Apellido es nulo o vacio.");
            if (client.FechaNacimiento == DateTime.MinValue || client.FechaNacimiento == DateTime.MaxValue) throw new ArgumentException("Fecha Nacimiento es invalida.");
            if (client.FechaNacimiento.Date >= DateTime.Now.Date) throw new ArgumentException("Fecha Nacimiento no puede ser superior a Hoy.");
        }

        private static void ThrowExceptionWheCliendIdIsNull(string clientId)
        {
            if (string.IsNullOrEmpty(clientId)) throw new ArgumentNullException("clientId");
        }

        public void Create(Client client)
        {
            ValidateClientInformation(client);
            if (!string.IsNullOrEmpty(client._id)) throw new ArgumentException("client._id deberia ser nulo o vacio");

            var deathDateInTicks = predictionsClient.GetPosibleDateDeth(client);
            var deathDate = new DateTime(deathDateInTicks);

            if (deathDate.Date >= DateTime.Now.Date)
                client.FechaProbableMuerte = deathDate;

            clientsRepository.Create(client);
        }

        public bool Delete(string clientId)
        {
            ThrowExceptionWheCliendIdIsNull(clientId);
            return clientsRepository.Delete(new Client { _id = clientId });
        }

        public PagedResultModel<Client> Get(int pageIndex, int pageSize)
        {
            if (pageIndex < 0) throw new ArgumentOutOfRangeException("pageIndex", "pageIndex no debe ser menor a cero.");
            if (pageSize < 0) throw new ArgumentOutOfRangeException("pageSize", "pageIndex no debe ser menor a cero.");

            var totalItems = clientsRepository.TotalClients();
            var totalPages = (long)Math.Floor((decimal)totalItems / pageSize);
            var items = clientsRepository.Get(pageIndex, pageSize);

            var result = new PagedResultModel<Client>
            {
                PageIndex = pageIndex,
                PageSize = pageSize,
                TotalPages = totalPages,
                TotalItems = totalItems,
                Items = items
            };

            return result;
        }

        public Client Get(string clientId)
        {
            if (string.IsNullOrWhiteSpace(clientId)) throw new ArgumentException("No puede ser nulo o vacio", "clientId");

            return clientsRepository.Get(clientId);
        }

        public bool Update(string clientId, Client client)
        {
            ThrowExceptionWheCliendIdIsNull(clientId);

            ValidateClientInformation(client);

            client._id = clientId;

            return clientsRepository.Update(client);
        }

    }
}
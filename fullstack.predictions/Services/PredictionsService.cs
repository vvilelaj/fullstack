using fullstack.predictions.Models.Predictions;
using fullstack.predictions.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace fullstack.predictions.Services
{
    public class PredictionsService : IPredictions
    {
        public long CalculatePosibleDeathDate(ClientModel client)
        {
            long result = long.MaxValue;

            if (client == null) throw new ArgumentNullException("client");
            if (string.IsNullOrWhiteSpace(client.Nombre)) throw new ArgumentException("Nombre es nulo o vacio.");
            if (string.IsNullOrWhiteSpace(client.Apellido)) throw new ArgumentException("Apellido es nulo o vacio.");
            if (client.FechaNacimiento == DateTime.MinValue || client.FechaNacimiento == DateTime.MaxValue) throw new ArgumentException("Fecha Nacimiento es invalida.");
            if (client.FechaNacimiento.Date >= DateTime.Now.Date) throw new ArgumentException("Fecha Nacimiento no puede ser superior a Hoy.");

            // todo : the implementation of prediction requires machine learning.
            // machine learning exposes services .
            // this method was created to simulate a microservices exposing a Machine Learning model.

            Random random = new Random();
            var randomYears =  random.Next(0, 100);
            var randomDays = random.Next(0, 365);
            var ramdomHour = random.Next(0, 24);

            result = DateTime.Now.AddYears(randomYears).AddDays(randomDays).AddHours(ramdomHour).Ticks;

            return result;
        }
    }
}
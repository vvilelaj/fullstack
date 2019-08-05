using fullstack.clients.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace fullstack.clients.ExternalServices.Predictions
{
    public interface IPredictionsClient
    {
        long GetPosibleDateDeth(Client client);
    }
}

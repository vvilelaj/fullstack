using fullstack.Kpis.Models;
using fullstack.shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace fullstack.kpis.Services.Interfaces
{
    interface IKpisService
    {
        void Save(Kpi kpi);

        void Create(Kpi kpi);

        bool Delete(string clientId);

        PagedResultModel<Kpi> Get(int pageIndex, int pageSize);

        bool Update(string kpiId, Kpi client);
    }
}

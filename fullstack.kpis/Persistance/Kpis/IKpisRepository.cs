using fullstack.Kpis.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace fullstack.kpis.Persistance.Kpis
{
    public interface IKpisRepository
    {
        Kpi Get(string kpiId);

        Kpi Get(DateTime date);

        List<Kpi> Get(int pageIndex, int pageSize);

        void Create(Kpi client);

        bool Update(Kpi client );

        bool Delete(Kpi client);

        long TotalKpis();
    }
}

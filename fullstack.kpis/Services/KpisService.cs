using fullstack.kpis.Persistance.Kpis;
using fullstack.kpis.Services.Interfaces;
using fullstack.Kpis.Models;
using fullstack.shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace fullstack.kpis.Services
{
    public class KpisService : IKpisService
    {
        private readonly IKpisRepository kpisRepository;

        public KpisService()
        {
            kpisRepository = new KpisRepository();
        }

        private static void ValidateKpiInformation(Kpi kpi)
        {
            if (kpi == null) throw new ArgumentNullException("kpi");
            if (kpi.Fecha ==  DateTime.MinValue || kpi.Fecha == DateTime.MaxValue) throw new ArgumentException("Fecha inválida");
            if (kpi.PromedioEdad < 0) throw new ArgumentException("PromedioEdad no puede ser negatico o igual a cero");
            if (kpi.DesviacionEstandar <= 0) throw new ArgumentException("DesviacionEstandar no puede ser negativa o igual a cero");
        }

        private static void ThrowExceptionWhenKpiIdIsNull(string kpiId)
        {
            if (string.IsNullOrEmpty(kpiId)) throw new ArgumentNullException("kpiId");
        }

        public void Save(Kpi kpi)
        {
            ValidateKpiInformation(kpi);

            var savedKpi = kpisRepository.Get(kpi.Fecha);

            if (savedKpi != null && savedKpi._id != kpi._id) Delete(savedKpi._id);

            if (string.IsNullOrWhiteSpace(kpi._id)) Create(kpi);

            Update(kpi._id, kpi);
        }

        public void Create(Kpi kpi)
        {
            ValidateKpiInformation(kpi);
            if (!string.IsNullOrEmpty(kpi._id)) throw new ArgumentException("kpi._id deberia ser nulo o vacio");

            kpisRepository.Create(kpi);
        }

        public bool Delete(string clientId)
        {
            ThrowExceptionWhenKpiIdIsNull(clientId);
            return kpisRepository.Delete(new Kpi { _id = clientId });
        }

        public PagedResultModel<Kpi> Get(int pageIndex, int pageSize)
        {
            if (pageIndex < 0) throw new ArgumentOutOfRangeException("pageIndex", "pageIndex no debe ser menor a cero.");
            if (pageSize < 0) throw new ArgumentOutOfRangeException("pageSize", "pageIndex no debe ser menor a cero.");

            var totalItems = kpisRepository.TotalKpis();
            var totalPages = (long)Math.Floor((decimal)totalItems / pageSize);
            var items = kpisRepository.Get(pageIndex, pageSize);

            var result = new PagedResultModel<Kpi>
            {
                PageIndex = pageIndex,
                PageSize = pageSize,
                TotalPages = totalPages,
                TotalItems = totalItems,
                Items = items
            };

            return result;
        }

        public bool Update(string kpiId, Kpi client)
        {
            ThrowExceptionWhenKpiIdIsNull(kpiId);

            ValidateKpiInformation(client);

            client._id = kpiId;

            return kpisRepository.Update(client);
        }

    }
}
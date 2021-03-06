using Common.Domain.Interfaces;
using Calemas.Erp.Domain.Entitys;
using Calemas.Erp.Domain.Filter;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Calemas.Erp.Domain.Interfaces.Repository
{
    public interface ISetorRepository : IRepository<Setor>
    {
        IQueryable<Setor> GetBySimplefilters(SetorFilter filters);

        Task<Setor> GetById(SetorFilter setor);
		
        Task<IEnumerable<dynamic>> GetDataItem(SetorFilter filters);

        Task<IEnumerable<dynamic>> GetDataListCustom(SetorFilter filters);

        Task<dynamic> GetDataCustom(SetorFilter filters);
    }
}

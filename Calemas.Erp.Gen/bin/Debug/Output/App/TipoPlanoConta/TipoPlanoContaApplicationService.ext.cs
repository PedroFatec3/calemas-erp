using Common.Domain.Interfaces;
using Calemas.Erp.Application.Interfaces;
using Calemas.Erp.Domain.Entitys;
using Calemas.Erp.Domain.Filter;
using Calemas.Erp.Domain.Interfaces.Services;
using Calemas.Erp.Dto;
using System.Linq;
using System.Collections.Generic;
using Common.Domain.Base;

namespace Calemas.Erp.Application
{
    public class TipoPlanoContaApplicationService : TipoPlanoContaApplicationServiceBase
    {

        public TipoPlanoContaApplicationService(ITipoPlanoContaService service, IUnitOfWork uow, ICache cache) :
            base(service, uow, cache)
        {
  
        }

        protected override System.Collections.Generic.IEnumerable<TDS> MapperDomainToResult<TDS>(FilterBase filter, PaginateResult<TipoPlanoConta> dataList)
        {
            return base.MapperDomainToResult<TipoPlanoContaDtoSpecializedResult>(filter, dataList) as IEnumerable<TDS>;
        }


    }
}

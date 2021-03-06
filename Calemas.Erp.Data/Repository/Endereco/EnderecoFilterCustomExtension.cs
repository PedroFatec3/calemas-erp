using Common.Domain.Model;
using Calemas.Erp.Domain.Entitys;
using Calemas.Erp.Domain.Filter;
using System.Linq;

namespace Calemas.Erp.Data.Repository
{
    public static class EnderecoFilterCustomExtension
    {

        public static IQueryable<Endereco> WithCustomFilters(this IQueryable<Endereco> queryBase, EnderecoFilter filters)
        {
            var queryFilter = queryBase;

            if (filters.CondominioId.IsSent())
                queryFilter = queryFilter.Where(_ => _.CollectionCondominio.Where(__ => __.CondominioId == filters.CondominioId).Any());

            return queryFilter;
        }

        public static IQueryable<Endereco> WithLimitTenant(this IQueryable<Endereco> queryBase, CurrentUser user)
        {
            var tenantId = user.GetTenantId<int>();
            var queryFilter = queryBase;

            return queryFilter;
        }

    }
}


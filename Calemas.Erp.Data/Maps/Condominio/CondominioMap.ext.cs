using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Calemas.Erp.Domain.Entitys;

namespace Calemas.Erp.Data.Map
{
    public class CondominioMap : CondominioMapBase
    {
        public CondominioMap(EntityTypeBuilder<Condominio> type) : base(type)
        {

        }

        protected override void CustomConfig(EntityTypeBuilder<Condominio> type)
        {

        }

    }
}
using Common.Validation;
using Calemas.Erp.Domain.Entitys;
using Calemas.Erp.Domain.Interfaces.Repository;

namespace Calemas.Erp.Domain.Validations
{
    public class SetorAptoParaCadastroWarning : WarningSpecification<Setor>
    {
        public SetorAptoParaCadastroWarning(ISetorRepository rep)
        {
            //base.Add(Guid.NewGuid().ToString(), new Rule<Setor>(Instance of RuleClassName,"message for user"));
        }

    }
}

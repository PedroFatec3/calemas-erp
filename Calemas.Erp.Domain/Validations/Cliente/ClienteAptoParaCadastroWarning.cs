using Common.Validation;
using Calemas.Erp.Domain.Entitys;
using Calemas.Erp.Domain.Interfaces.Repository;

namespace Calemas.Erp.Domain.Validations
{
    public class ClienteAptoParaCadastroWarning : WarningSpecification<Cliente>
    {
        public ClienteAptoParaCadastroWarning(IClienteRepository rep)
        {
            //base.Add(Guid.NewGuid().ToString(), new Rule<Cliente>(Instance of RuleClassName,"message for user"));
        }

    }
}

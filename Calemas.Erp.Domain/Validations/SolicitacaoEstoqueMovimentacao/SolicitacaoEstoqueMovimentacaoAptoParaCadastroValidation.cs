using Common.Validation;
using Calemas.Erp.Domain.Entitys;
using Calemas.Erp.Domain.Interfaces.Repository;

namespace Calemas.Erp.Domain.Validations
{
    public class SolicitacaoEstoqueMovimentacaoAptoParaCadastroValidation : ValidatorSpecification<SolicitacaoEstoqueMovimentacao>
    {
        public SolicitacaoEstoqueMovimentacaoAptoParaCadastroValidation(ISolicitacaoEstoqueMovimentacaoRepository rep)
        {
            //base.Add(Guid.NewGuid().ToString(), new Rule<SolicitacaoEstoqueMovimentacao>(Instance of RuleClassName,"message for user"));
        }

    }
}

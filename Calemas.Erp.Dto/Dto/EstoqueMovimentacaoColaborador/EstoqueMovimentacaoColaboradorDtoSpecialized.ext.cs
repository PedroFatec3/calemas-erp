using System;
using System.Linq;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Calemas.Erp.Dto
{
	public class EstoqueMovimentacaoColaboradorDtoSpecialized : EstoqueMovimentacaoColaboradorDto
	{

        public IEnumerable<EstoqueMovimentacaoDto> CollectionEstoqueMovimentacao { get; set;} 
        public  ColaboradorDto Colaborador { get; set;} 

		
	}
}
using System;
using System.Linq;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Calemas.Erp.Dto
{
	public class EnderecoDtoSpecializedDetails : EnderecoDto
	{

        public IEnumerable<PessoaDto> CollectionPessoa { get; set;} 

		
	}
}
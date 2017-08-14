using Common.Domain.Base;
using System;

namespace Calemas.Erp.Domain.Entitys
{
    public class StatusClienteBase : DomainBaseWithUserCreate
    {
        public StatusClienteBase()
        {

        }
        public StatusClienteBase(int statusclienteid, string nome, bool ativo)
        {
            this.StatusClienteId = statusclienteid;
            this.Nome = nome;
            this.Ativo = ativo;

        }

        public int StatusClienteId { get; protected set; }
        public string Nome { get; protected set; }
        public string Descricao { get; protected set; }
        public bool Ativo { get; protected set; }


		public virtual void SetarDescricao(string descricao)
		{
			this.Descricao = descricao;
		}


    }
}
using Common.Domain.Base;
using Common.Domain.Model;
using System;

namespace Calemas.Erp.Domain.Entitys
{
    public class InfraestruturaPopBase : DomainBaseWithUserCreate
    {
        public InfraestruturaPopBase()
        {

        }
        public InfraestruturaPopBase(int infraestruturapopid, string nome, int infraestruturasiteid)
        {
            this.InfraestruturaPopId = infraestruturapopid;
            this.Nome = nome;
            this.InfraestruturaSiteId = infraestruturasiteid;

        }

        public virtual int InfraestruturaPopId { get; protected set; }
        public virtual string Nome { get; protected set; }
        public virtual string Descricao { get; protected set; }
        public virtual string Latitude { get; protected set; }
        public virtual string Longitude { get; protected set; }
        public virtual int InfraestruturaSiteId { get; protected set; }


public class InfraestruturaPopFactoryBase
        {
            public virtual InfraestruturaPop GetDefaultInstanceBase(dynamic data, CurrentUser user)
            {
                var construction = new InfraestruturaPop(data.InfraestruturaPopId,
                                        data.Nome,
                                        data.InfraestruturaSiteId);

                construction.SetarDescricao(data.Descricao);
                construction.SetarLatitude(data.Latitude);
                construction.SetarLongitude(data.Longitude);


				construction.SetAttributeBehavior(data.AttributeBehavior);
        		return construction;
            }

        }

		public virtual void SetarDescricao(string descricao)
		{
			this.Descricao = descricao;
		}
		public virtual void SetarLatitude(string latitude)
		{
			this.Latitude = latitude;
		}
		public virtual void SetarLongitude(string longitude)
		{
			this.Longitude = longitude;
		}


    }
}

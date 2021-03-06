using Calemas.Erp.Domain.Validations;
using System;

namespace Calemas.Erp.Domain.Entitys
{
    public class Cor : CorBase
    {

        public Cor()
        {

        }

        public Cor(int corid, string nome, string hash) :
            base(corid, nome, hash)
        {

        }

		public class CorFactory
        {
            public Cor GetDefaaultInstance(dynamic data)
            {
                var construction = new Cor(data.CorId,
                                        data.Nome,
                                        data.Hash);



				return construction;
            }

        }

        public bool IsValid()
        {
            base._validationResult = new CorEstaConsistenteValidation().Validate(this);
            return base._validationResult.IsValid;

        }
        
    }
}

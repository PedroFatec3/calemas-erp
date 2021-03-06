using Common.Domain.Base;
using Common.Domain.Interfaces;
using Common.Domain.Model;
using Calemas.Erp.Domain.Entitys;
using Calemas.Erp.Domain.Filter;
using Calemas.Erp.Domain.Interfaces.Repository;
using Calemas.Erp.Domain.Validations;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Calemas.Erp.Domain.Services
{
    public class ColaboradorServiceBase : ServiceBase<Colaborador>
    {
        protected readonly IColaboradorRepository _rep;

        public ColaboradorServiceBase(IColaboradorRepository rep, ICache cache, CurrentUser user)
            : base(cache)
        {
            this._rep = rep;
			this._user = user;
        }

        public virtual async Task<Colaborador> GetOne(ColaboradorFilter filters)
        {
            return await this._rep.GetById(filters);
        }

        public virtual async Task<IEnumerable<Colaborador>> GetByFilters(ColaboradorFilter filters)
        {
            var queryBase = this._rep.GetBySimplefilters(filters);
            return await this._rep.ToListAsync(queryBase);
        }

        public virtual Task<PaginateResult<Colaborador>> GetByFiltersPaging(ColaboradorFilter filters)
        {
            var queryBase = this._rep.GetBySimplefilters(filters);
            return this._rep.PagingAndDefineFields(filters, queryBase);
        }

        public override void Remove(Colaborador colaborador)
        {
            this._rep.Remove(colaborador);
        }

        public virtual Summary GetSummary(PaginateResult<Colaborador> paginateResult)
        {
            return new Summary
            {
                Total = paginateResult.TotalCount,
				PageSize = paginateResult.PageSize,
            };
        }

        public virtual ValidationSpecificationResult GetDomainValidation(FilterBase filters = null)
        {
            return base._validationResult;
        }

        public virtual ConfirmEspecificationResult GetDomainConfirm(FilterBase filters = null)
        {
            return base._validationConfirm;
        }

        public virtual WarningSpecificationResult GetDomainWarning(FilterBase filters = null)
        {
            return base._validationWarning;
        }

        public override async Task<Colaborador> Save(Colaborador colaborador, bool questionToContinue = false)
        {
			var colaboradorOld = await this.GetOne(new ColaboradorFilter { ColaboradorId = colaborador.ColaboradorId });
			var colaboradorOrchestrated = await this.DomainOrchestration(colaborador, colaboradorOld);

            if (questionToContinue)
            {
                if (base.Continue(colaboradorOrchestrated, colaboradorOld) == false)
                    return colaboradorOrchestrated;
            }

            return this.SaveWithValidation(colaboradorOrchestrated, colaboradorOld);
        }

        public override async Task<Colaborador> SavePartial(Colaborador colaborador, bool questionToContinue = false)
        {
            var colaboradorOld = await this.GetOne(new ColaboradorFilter { ColaboradorId = colaborador.ColaboradorId });
			var colaboradorOrchestrated = await this.DomainOrchestration(colaborador, colaboradorOld);

            if (questionToContinue)
            {
                if (base.Continue(colaboradorOrchestrated, colaboradorOld) == false)
                    return colaboradorOrchestrated;
            }

            return SaveWithOutValidation(colaboradorOrchestrated, colaboradorOld);
        }

        protected override Colaborador SaveWithOutValidation(Colaborador colaborador, Colaborador colaboradorOld)
        {
            colaborador = this.SaveDefault(colaborador, colaboradorOld);

			if (base._validationResult.IsNotNull() && !base._validationResult.IsValid)
				return colaborador;

            base._validationResult = new ValidationSpecificationResult
            {
                Errors = new List<string>(),
                IsValid = true,
                Message = "colaborador Alterado com sucesso."
            };

            base._cacheHelper.ClearCache();
            return colaborador;

        }

		protected override Colaborador SaveWithValidation(Colaborador colaborador, Colaborador colaboradorOld)
        {
            if (!this.DataAnnotationIsValid())
                return colaborador;

            if (!colaborador.IsValid())
            {
                base._validationResult = colaborador.GetDomainValidation();
                return colaborador;
            }

            this.Specifications(colaborador);

            if (!base._validationResult.IsValid)
                return colaborador;
            
            colaborador = this.SaveDefault(colaborador, colaboradorOld);
            base._validationResult.Message = "Colaborador cadastrado com sucesso :)";

            base._cacheHelper.ClearCache();
            return colaborador;
        }

		protected virtual void Specifications(Colaborador colaborador)
        {
            base._validationResult  = new ColaboradorAptoParaCadastroValidation(this._rep).Validate(colaborador);
			base._validationWarning  = new ColaboradorAptoParaCadastroWarning(this._rep).Validate(colaborador);
        }

        protected virtual Colaborador SaveDefault(Colaborador colaborador, Colaborador colaboradorOld)
        {
			colaborador = this.AuditDefault(colaborador, colaboradorOld);

            var isNew = colaboradorOld.IsNull();			
            if (isNew)
                colaborador = this.AddDefault(colaborador);
            else
				colaborador = this.UpdateDefault(colaborador, colaboradorOld);

            return colaborador;
        }
		
        protected virtual Colaborador AddDefault(Colaborador colaborador)
        {
            colaborador = this._rep.Add(colaborador);
            return colaborador;
        }

		protected virtual Colaborador UpdateDefault(Colaborador colaborador, Colaborador colaboradorOld)
        {
            colaborador = this._rep.Update(colaborador);
            return colaborador;
        }
				
		public virtual async Task<Colaborador> GetNewInstance(dynamic model, CurrentUser user)
        {
            return await Task.Run(() =>
            {
                return new Colaborador.ColaboradorFactory().GetDefaultInstance(model, user);
            });
         }

		public virtual async Task<Colaborador> GetUpdateInstance(dynamic model, CurrentUser user)
        {
            return await Task.Run(() =>
            {
                return new Colaborador.ColaboradorFactory().GetDefaultInstance(model, user);
            });
         }
    }
}

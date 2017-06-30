using Common.Cache;
using Common.Domain.Interfaces;
using Common.Orm;
using Microsoft.Extensions.DependencyInjection;
using Calemas.Erp.Application;
using Calemas.Erp.Application.Interfaces;
using Calemas.Erp.Data.Context;
using Calemas.Erp.Data.Repository;
using Calemas.Erp.Domain.Interfaces.Repository;
using Calemas.Erp.Domain.Interfaces.Services;
using Calemas.Erp.Domain.Services;

namespace Calemas.Erp.Api
{
    public static partial class ConfigContainerCore
    {

        public static void Config(IServiceCollection services)
        {
            services.AddScoped<ICache, RedisComponent>();
            services.AddScoped<IUnitOfWork, UnitOfWork<DbContextCore>>();
            
			services.AddScoped<IOrdemServicoApplicationService, OrdemServicoApplicationService>();
			services.AddScoped<IOrdemServicoService, OrdemServicoService>();
			services.AddScoped<IOrdemServicoRepository, OrdemServicoRepository>();

			services.AddScoped<IColaboradorApplicationService, ColaboradorApplicationService>();
			services.AddScoped<IColaboradorService, ColaboradorService>();
			services.AddScoped<IColaboradorRepository, ColaboradorRepository>();

			services.AddScoped<INivelAcessoApplicationService, NivelAcessoApplicationService>();
			services.AddScoped<INivelAcessoService, NivelAcessoService>();
			services.AddScoped<INivelAcessoRepository, NivelAcessoRepository>();

			services.AddScoped<ISetorApplicationService, SetorApplicationService>();
			services.AddScoped<ISetorService, SetorService>();
			services.AddScoped<ISetorRepository, SetorRepository>();

			services.AddScoped<IPrioridadeApplicationService, PrioridadeApplicationService>();
			services.AddScoped<IPrioridadeService, PrioridadeService>();
			services.AddScoped<IPrioridadeRepository, PrioridadeRepository>();

			services.AddScoped<ITipoOrdemServicoApplicationService, TipoOrdemServicoApplicationService>();
			services.AddScoped<ITipoOrdemServicoService, TipoOrdemServicoService>();
			services.AddScoped<ITipoOrdemServicoRepository, TipoOrdemServicoRepository>();

			services.AddScoped<IPessoaApplicationService, PessoaApplicationService>();
			services.AddScoped<IPessoaService, PessoaService>();
			services.AddScoped<IPessoaRepository, PessoaRepository>();



            RegisterOtherComponents(services);
        }
    }
}
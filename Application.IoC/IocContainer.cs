using Application.MainModule;
using Application.MainModule.Interface;
using Application.MainModule.RestSharp;

using Domain.MainModule.Entity;
using Domain.MainModule.IRepository;
using Domain.MainModule.IUnitOfWork;
using FluentValidation;
using Infrastructure.Data.MainModule.Repository;
using Infrastructure.Data.MainModule.UnitOfWork;
using Microsoft.Extensions.DependencyInjection;

namespace Application.IoC;

public static class IocContainer
{
    public static IServiceCollection AddDependencyInjectionInterfaces(this IServiceCollection services)
    {
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddScoped<IRestWsHelper, RestWsHelper>();

        services.AddDependencyInjectionAppService();
        services.AddDependencyInjectionRepository();
        services.AddDependencyInjectionValidationsDto();

        return services;
    }

    private static void AddDependencyInjectionAppService(this IServiceCollection services)
    {
        services.AddScoped<IRoleAppService, RoleAppService>();
        services.AddScoped<ICompanyAppService, CompanyAppService>();
        services.AddScoped<IJobLabelService, JobLabelAppService>();
        services.AddScoped<IJobOfferService, JobOfferAppService>();
        services.AddScoped<IPostulantAppService, PostulantAppService>();
        services.AddScoped<IPostulationAppService, PostulationAppService>();
        services.AddScoped<ISavedJobOffersAppService, SavedJobOffersAppService>();
        
    }

    private static void AddDependencyInjectionRepository(this IServiceCollection services)
    {
        services.AddScoped<IRoleRepository, RoleRepository>();
        services.AddScoped<ICompanyRepository, CompanyRepository>();
        services.AddScoped<IJobLabelRepository, JobLabelRepository>();
        services.AddScoped<IJobOfferRepository, JobOfferRepository>();
        services.AddScoped<IPostulantRepository, PostulantRepository>();
        services.AddScoped<IPostulationRepository, PostulationRepository>();
        services.AddScoped<ISavedJobOfferRepository, SavedJobOfferRepository>();
    }

    private static void AddDependencyInjectionValidationsDto(this IServiceCollection services)
    {
        
    }
}
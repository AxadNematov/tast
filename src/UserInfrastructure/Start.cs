
using Microsoft.Extensions.DependencyInjection;


namespace UserInfrastructure;

public static class Start
{
    public static void InfrastructureBuild(this IServiceCollection services)
    {
        GeneralInfrastructure.Start.BuildgeneralInfrastructure(services);
    }
}
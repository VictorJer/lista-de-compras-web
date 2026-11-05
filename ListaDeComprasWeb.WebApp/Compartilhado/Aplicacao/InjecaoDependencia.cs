using ListaDeComprasWeb.WebApp.Modulos.ModuloCategoria.Aplicacao;

namespace ListaDeComprasWeb.WebApp.Compartilhado.Aplicacao;

public static class InjecaoDependencia
{
    public static void AddApplicationServices(this IServiceCollection services)
    {
        services.AddScoped<ServicoCategoria>();
    }
}

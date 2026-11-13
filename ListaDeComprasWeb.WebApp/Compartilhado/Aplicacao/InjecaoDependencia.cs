using ListaDeComprasWeb.WebApp.Modulos.ModuloCategoria.Aplicacao;
using ListaDeComprasWeb.WebApp.Modulos.ModuloListaCompras.Aplicacao;
using ListaDeComprasWeb.WebApp.Modulos.ModuloProduto.Aplicacao;

namespace ListaDeComprasWeb.WebApp.Compartilhado.Aplicacao;

public static class InjecaoDependencia
{
    public static void AddApplicationServices(this IServiceCollection services)
    {
        services.AddScoped<ServicoCategoria>();
        services.AddScoped<ServicoProduto>();
        services.AddScoped<ServicoListaCompras>();
    }
}

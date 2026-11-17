using ListaDeComprasWeb.WebApp.Modulos.ModuloCategoria.Aplicacao;
using ListaDeComprasWeb.WebApp.Modulos.ModuloListaCompras.Aplicacao;
using ListaDeComprasWeb.WebApp.Modulos.ModuloProduto.Aplicacao;
using ListaDeComprasWeb.WebApp.Modulos.ModuloItemLista.Aplicacao;

namespace ListaDeComprasWeb.WebApp.Compartilhado.Aplicacao;

public static class InjecaoDependencia
{
    public static void AddApplicationServices(this IServiceCollection services)
    {
        services.AddScoped<ServicoCategoria>();
        services.AddScoped<ServicoProduto>();
        services.AddScoped<ServicoListaCompras>();
        services.AddScoped<ServicoItemLista>();
    }
}

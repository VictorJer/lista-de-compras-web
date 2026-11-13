using ListaDeComprasWeb.WebApp.Compartilhado.Infra.Arquivos;
using ListaDeComprasWeb.WebApp.Modulos.ModuloCategoria.Dominio;
using ListaDeComprasWeb.WebApp.Modulos.ModuloListaCompras.Dominio;
using ListaDeComprasWeb.WebApp.Modulos.ModuloListaCompras.Dominio.ItemDaLista;
using ListaDeComprasWeb.WebApp.Modulos.ModuloProduto.Dominio;

namespace ListaDeComprasWeb.WebApp.Compartilhado.Infra;

public static class InjecaoDependencia
{
    public static void AddInfraRepositories(this IServiceCollection services)
    {
        services.AddScoped(provider =>
        {
            ContextoJson contextoJson = new ContextoJson();

            contextoJson.Carregar();

            return contextoJson;
        });

        services.AddScoped<IRepositorioCategoria, RepositorioCategoriaEmArquivo>();
        services.AddScoped<IRepositorioProduto, RepositorioProdutoEmArquivo>();
        services.AddScoped<IRepositorioListaCompras, RepositorioListaComprasEmArquivo>();
        services.AddScoped<IRepositorioItemLista, RepositorioItemListaEmArquivo>();
    }
}

using ListaDeComprasWeb.WebApp.Compartilhado.Infra.Arquivos;
using ListaDeComprasWeb.WebApp.Modulos.ModuloItemLista.Dominio;

public class RepositorioItemListaEmArquivo : RepositorioBaseEmArquivo<ItemLista>, IRepositorioItemLista
{
    public RepositorioItemListaEmArquivo(ContextoJson contexto) : base(contexto) { }

    protected override List<ItemLista> CarregarRegistros()
    {
        return contexto.ItensLista;
    }
}

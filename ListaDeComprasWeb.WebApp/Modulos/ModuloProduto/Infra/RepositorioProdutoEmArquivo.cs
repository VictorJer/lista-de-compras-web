using ListaDeComprasWeb.WebApp.Compartilhado.Infra.Arquivos;
using ListaDeComprasWeb.WebApp.Modulos.ModuloProduto.Dominio;

public class RepositorioProdutoEmArquivo : RepositorioBaseEmArquivo<Produto>, IRepositorioProduto
{
    public RepositorioProdutoEmArquivo(ContextoJson contexto) : base(contexto) {  }

    protected override List<Produto> CarregarRegistros()
    {
        return contexto.Produtos;
    }
}
using ListaDeComprasWeb.WebApp.Modulos.ModuloListaCompras.Dominio;

namespace ListaDeComprasWeb.WebApp.Modulos.ModuloListaCompras.Aplicacao;

public class ServicoListaCompras
{
    private readonly IRepositorioListarCompras repositorioListaCompras;
    // private readonly IRepositorioItemLista repositorioProduto;

    public ServicoListaCompras(IRepositorioListarCompras repositorioListaCompras)
    {
        this.repositorioListaCompras = repositorioListaCompras;
    }

    public void Cadastrar(CadastrarListaComprasDto cadastarDto)
    {
        var listaCompras = new ListaCompras(cadastarDto.Nome, DateTime.Now);

        repositorioListaCompras.Cadastrar(listaCompras);
    }
}
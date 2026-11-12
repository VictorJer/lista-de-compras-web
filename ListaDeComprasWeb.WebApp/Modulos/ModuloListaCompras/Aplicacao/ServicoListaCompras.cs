using FluentResults;
using ListaDeComprasWeb.WebApp.Modulos.ModuloListaCompras.Dominio;

namespace ListaDeComprasWeb.WebApp.Modulos.ModuloListaCompras.Aplicacao;

public class ServicoListaCompras
{
    private readonly IRepositorioListaCompras repositorioListaCompras;
    // private readonly IRepositorioItemLista repositorioProduto;

    public ServicoListaCompras(IRepositorioListaCompras repositorioListaCompras)
    {
        this.repositorioListaCompras = repositorioListaCompras;
    }

    public Result Cadastrar(CadastrarListaComprasDto cadastarDto)
    {
        var listaCompras = new ListaCompras(cadastarDto.Nome, DateTime.Now);

        Result result = ValidarEntidade(listaCompras);

        if (result.IsFailed)
            return result;

        repositorioListaCompras.Cadastrar(listaCompras);

        return Result.Ok();
    }

    private Result ValidarEntidade(ListaCompras listaCompras)
    {
        var erros = listaCompras.Validar();

        if (erros.Count == 0)
            return Result.Ok();

        return Result.Fail(new Error(erros.First()).WithMetadata("Campo", string.Empty));
    }
}
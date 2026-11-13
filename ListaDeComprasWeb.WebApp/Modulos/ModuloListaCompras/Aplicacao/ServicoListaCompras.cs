using FluentResults;
using ListaDeComprasWeb.WebApp.Modulos.ModuloListaCompras.Dominio;
using ListaDeComprasWeb.WebApp.Modulos.ModuloListaCompras.Dominio.ItemDaLista;

namespace ListaDeComprasWeb.WebApp.Modulos.ModuloListaCompras.Aplicacao;

public class ServicoListaCompras
{
    private readonly IRepositorioListaCompras repositorioListaCompras;
    private readonly IRepositorioItemLista repositorioItemLista;

    public ServicoListaCompras(IRepositorioListaCompras repositorioListaCompras, IRepositorioItemLista repositorioItemLista)
    {
        this.repositorioListaCompras = repositorioListaCompras;
        this.repositorioItemLista = repositorioItemLista;
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

    public Result Editar(EditarListaComprasDto editarDto)
    {
        ListaCompras? lista = repositorioListaCompras.SelecionarPorId(editarDto.Id);

        if (lista == null)
            return Result.Fail("Lista de compras não encontrada.");

        ListaCompras listaCompras = new ListaCompras(editarDto.Nome, lista.DataCriacao, editarDto.Status);

        Result result = ValidarEntidade(listaCompras);

        if (result.IsFailed)
            return result;
        
        repositorioListaCompras.Editar(editarDto.Id, listaCompras);

        return Result.Ok();
    }

    public Result Excluir(Guid id)
    {
        ListaCompras? lista = repositorioListaCompras.SelecionarPorId(id);

        if (lista == null)
            return Result.Fail("Lista de compras não encontrada.");

        repositorioListaCompras.Excluir(id);

        return Result.Ok();
    }

    public Result<DetalhesListaComprasDto> SelecionarPorId(Guid id)
    {
        ListaCompras? listaSelecionada = repositorioListaCompras.SelecionarPorId(id);

        if (listaSelecionada == null)
            return Result.Fail("Lista de compras não encontrada.");

        List<ItemLista> itens = SelecionarItensDaLista(listaSelecionada.Id);

        var detalhes = new DetalhesListaComprasDto(
            listaSelecionada.Id,
            listaSelecionada.Nome,
            listaSelecionada.DataCriacao,
            listaSelecionada.Status,
            itens.Count,
            itens.Sum(i => i.CalcularSubtotal())
        );

        return Result.Ok(detalhes);
    }

    public List<ListarListasComprasDto> SelecionarTodos()
    {
        return repositorioListaCompras
            .SelecionarTodos()
            .Select(l =>
            {
                List<ItemLista> itens = SelecionarItensDaLista(l.Id);

                return new ListarListasComprasDto(
                    l.Id,
                    l.Nome,
                    l.DataCriacao,
                    l.Status,
                    itens.Count,
                    itens.Sum(i => i.CalcularSubtotal())
                );
            }).ToList();
    }

    private List<ItemLista> SelecionarItensDaLista(Guid listaId)
    {
        return repositorioItemLista
            .SelecionarTodos()
            .Where(i => i.ListaCompras.Id == listaId)
            .ToList();
    }

    private Result ValidarEntidade(ListaCompras listaCompras)
    {
        var erros = listaCompras.Validar();

        if (erros.Count == 0)
            return Result.Ok();

        return Result.Fail(new Error(erros.First()).WithMetadata("Campo", string.Empty));
    }
}
using FluentResults;
using ListaDeComprasWeb.WebApp.Modulos.ModuloItemLista.Dominio;
using ListaDeComprasWeb.WebApp.Modulos.ModuloListaCompras.Apresentacao;
using ListaDeComprasWeb.WebApp.Modulos.ModuloListaCompras.Dominio;
using ListaDeComprasWeb.WebApp.Modulos.ModuloProduto.Dominio;

namespace ListaDeComprasWeb.WebApp.Modulos.ModuloItemLista.Aplicacao;

public class ServicoItemLista
{
    private readonly IRepositorioItemLista repositorioItemLista;
    private readonly IRepositorioListaCompras repositorioListaCompras;
    private readonly IRepositorioProduto repositorioProduto;

    public ServicoItemLista(IRepositorioItemLista repositorioItemLista, IRepositorioListaCompras repositorioListaCompras, IRepositorioProduto repositorioProduto)
    {
        this.repositorioItemLista = repositorioItemLista;
        this.repositorioListaCompras = repositorioListaCompras;
        this.repositorioProduto = repositorioProduto;
    }

    public Result Cadastrar(CadastrarItemListaDto cadastrarDto)
    {
        Result<(ListaCompras Lista, Produto Produto)> resultRelacionamentos =
            SelecionarRelacionamentos(cadastrarDto.ListaComprasId, cadastrarDto.ProdutoId);

        if (resultRelacionamentos.IsFailed)
            return Result.Fail(resultRelacionamentos.Errors);

        if (ExisteProdutoNaLista(cadastrarDto.ListaComprasId, cadastrarDto.ProdutoId))
            return Falha(nameof(cadastrarDto.ProdutoId), "Este produto já foi adicionado nesta lista.");

        ItemLista novoItem = new ItemLista(
            resultRelacionamentos.Value.Lista,
            resultRelacionamentos.Value.Produto,
            cadastrarDto.Quantidade
        );

        Result resultValidacao = ValidarEntidade(novoItem);

        if (resultValidacao.IsFailed)
            return resultValidacao;

        repositorioItemLista.Cadastrar(novoItem);

        return Result.Ok();
    }

    public Result Excluir(Guid id)
    {
        ItemLista? item = repositorioItemLista.SelecionarPorId(id);

        if (item == null)
            return Result.Fail("Item da lista não encontrado.");

        repositorioItemLista.Excluir(id);

        return Result.Ok();
    }

    public List<ListarItensListaDto> SelecionarTodosPorLista(Guid listaComprasId)
    {
        return repositorioItemLista
            .SelecionarTodos()
            .Where(i => i.ListaCompras.Id == listaComprasId)
            .Select(MapearParaListarDto)
            .ToList();
    }

    public Result<DetalhesItemListaDto> SelecionarDetalhesLista(Guid listaComprasId)
    {
        ListaCompras? lista = repositorioListaCompras.SelecionarPorId(listaComprasId);

        if (lista == null)
            return Result.Fail("Lista de compras não encontrada.");
        
        List<ItemLista> items = repositorioItemLista
            .SelecionarTodos()
            .Where(i => i.ListaCompras.Id == lista.Id)
            .ToList();
        
        return Result.Ok(new DetalhesItemListaDto());
    }
}
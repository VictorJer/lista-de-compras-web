using FluentResults;
using ListaDeComprasWeb.WebApp.Modulos.ModuloCategoria.Dominio;
using ListaDeComprasWeb.WebApp.Modulos.ModuloProduto.Dominio;

namespace ListaDeComprasWeb.WebApp.Modulos.ModuloProduto.Aplicacao;

public class ServicoProduto
{
    private readonly IRepositorioProduto repositorioProduto;
    private readonly IRepositorioCategoria repositorioCategoria;

    public ServicoProduto(IRepositorioProduto repositorioProduto, IRepositorioCategoria repositorioCategoria)
    {
        this.repositorioProduto = repositorioProduto;
        this.repositorioCategoria = repositorioCategoria;
    }

    public Result Cadastrar(CadastrarProdutoDto dto)
    {
        var categoria = repositorioCategoria.SelecionarPorId(dto.CategotriaId);

        if (categoria is null)
            return Falha(nameof(dto.CategotriaId), "Categoria não encontrada.");
        
        if (ExisteProdutoComMesmoNome(dto.Nome))
            return Falha(nameof(dto.Nome), "Já existe um produto com este nome.");

        var produto = new Produto(
            dto.Nome,
            categoria,
            dto.UnidadeMedida,
            dto.PrecoAproximado
        );

        Result resultadoValidacao = ValidarEntidade(produto);

        if (!resultadoValidacao.IsSuccess)
            return resultadoValidacao;

        repositorioProduto.Cadastrar(produto);

        return Result.Ok();
    }

    public Result Editar(EditarProdutoDto dto)
    {
        var produtoExistente = repositorioProduto.SelecionarPorId(dto.Id);

        if (produtoExistente is null)
            return Result.Fail("Produto não encontrado.");

        var categoria = repositorioCategoria.SelecionarPorId(dto.CategotriaId);

        if (categoria is null)
            return Falha(nameof(dto.CategotriaId), "Categoria não encontrada.");

        if (ExisteProdutoComMesmoNome(dto.Nome, dto.Id))
            return Falha(nameof(dto.Nome), "Já existe um produto com este nome.");

        var produtoAtualizado = new Produto(
            dto.Nome,
            categoria,
            dto.UnidadeMedida,
            dto.PrecoAproximado
        );

        Result resultadoValidacao = ValidarEntidade(produtoAtualizado);

        if (!resultadoValidacao.IsSuccess)
            return resultadoValidacao;

        bool conseguiuEditar = repositorioProduto.Editar(dto.Id, produtoAtualizado);

        if (!conseguiuEditar)
            return Result.Fail("Produto não encontrado.");

        return Result.Ok();
    }

    public Result Excluir(Guid id)
    {
        var produtoExistente = repositorioProduto.SelecionarPorId(id);

        if (produtoExistente is null)
            return Result.Fail("Produto não encontrado.");

        bool conseguiuExcluir = repositorioProduto.Excluir(id);

        if (!conseguiuExcluir)
            return Result.Fail("Produto não encontrado.");

        return Result.Ok();
    }

    public List<ListarProdutosDto> SelecionarTodos()
    {

        return repositorioProduto
            .SelecionarTodos()
            .Select(p => new ListarProdutosDto(
                p.Id,
                p.Nome,
                p.Categoria.Id,
                p.Categoria.Nome,
                p.Categoria.Cor,
                p.UnidadeMedida,
                p.PrecoAproximado
            )).ToList();
    }

    public Result<DetalhesProdutoDto> SelecionarPorId(Guid id)
    {
        var produto = repositorioProduto.SelecionarPorId(id);

        if (produto is null)
            return Result.Fail("Produto não encontrado.");

        return Result.Ok(new DetalhesProdutoDto(
            produto.Id,
            produto.Nome,
            produto.Categoria.Id,
            produto.Categoria.Nome,
            produto.Categoria.Cor,
            produto.UnidadeMedida,
            produto.PrecoAproximado
        ));
    }

    private bool ExisteProdutoComMesmoNome(string nome, Guid? idIgnorado = null)
    {
        return repositorioProduto
            .SelecionarTodos()
            .Any(p => p.Id != idIgnorado &&
                string.Equals(p.Nome, nome, StringComparison.OrdinalIgnoreCase));

    }

    private static Result ValidarEntidade(Produto produto)
    {
        List<string> erros = produto.Validar();

        if (erros.Count == 0)
            return Result.Ok();

        return Result.Fail(new Error(erros.First()).WithMetadata("Campo", string.Empty));
    }    

    private static Result Falha(string campo, string mensagem)
    {
        return Result.Fail(new Error(mensagem).WithMetadata("Campo", campo));
    }

    public List<OpcaoCategoriaDto> SelecionarCategorias()
    {
        return repositorioCategoria
            .SelecionarTodos()
            .Select(c => new OpcaoCategoriaDto(c.Id, c.Nome, c.Cor))
            .ToList();
    }
}
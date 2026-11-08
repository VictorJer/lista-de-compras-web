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
        
        if (ExisteProdutoComMesmoNome())
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

    private static bool ExisteProdutoComMesmoNome()
    {
        return false;
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
}
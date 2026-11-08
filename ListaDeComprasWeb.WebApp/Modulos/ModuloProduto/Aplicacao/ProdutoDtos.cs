using ListaDeComprasWeb.WebApp.Modulos.ModuloProduto.Dominio;

namespace ListaDeComprasWeb.WebApp.Modulos.ModuloProduto.Aplicacao;

public record ListarProdutosDto(
    Guid Id,
    string Nome,
    Guid CategotriaId,
    string CategoriaNome,
    string CategoriaCor,
    UnidadeMedida UnidadeMedida,
    decimal PrecoAproximado
);

public record CadastrarProdutoDto(
    string Nome,
    Guid CategotriaId,
    UnidadeMedida UnidadeMedida,
    decimal PrecoAproximado
);

public record EditarProdutoDto(
    Guid Id,
    string Nome,
    Guid CategotriaId,
    UnidadeMedida UnidadeMedida,
    decimal PrecoAproximado
);

public record DetalhesProdutoDto(
    Guid Id,
    string Nome,
    Guid CategotriaId,
    string CategoriaNome,
    string CategoriaCor,
    UnidadeMedida UnidadeMedida,
    decimal PrecoAproximado
);

public record OpcaoCategoriaDto(
    Guid Id,
    string Nome,
    string Cor
);
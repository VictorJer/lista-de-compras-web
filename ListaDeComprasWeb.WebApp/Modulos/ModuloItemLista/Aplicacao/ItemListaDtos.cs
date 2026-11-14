using ListaDeComprasWeb.WebApp.Modulos.ModuloProduto.Dominio;

namespace ListaDeComprasWeb.WebApp.Modulos.ModuloItemLista.Aplicacao;

public record ListarItensListaDto(
    Guid Id,
    Guid ListaComprasId,
    string ListaComprasNoem,
    Guid ProdutoId,
    string ProdutoNome,
    string CategoriaNome,
    string CategoriaCor,
    UnidadeMedida UnidadeMedida,
    decimal PrecoAproximado,
    decimal Quantidade,
    decimal Subtotal
);

public record CadastrarItemListaDto(
    Guid ListaComprasId,
    Guid ProdutoId,
    decimal Quantidade
);

public record DetalhesItemListaDto(
    Guid Id,
    Guid ListaComprasId,
    string ListaComprasNoem,
    Guid ProdutoId,
    string ProdutoNome,
    string CategoriaNome,
    string CategoriaCor,
    UnidadeMedida UnidadeMedida,
    decimal PrecoAproximado,
    decimal Quantidade,
    decimal Subtotal
);

public record OpcaoProdutoDto(
    Guid Id,
    string Nome,
    string CategoriaNome,
    string CategoriaCor,
    UnidadeMedida UnidadeMedida,
    decimal PrecoAproximado
);
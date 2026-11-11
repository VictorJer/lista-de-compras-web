using ListaDeComprasWeb.WebApp.Modulos.ModuloListaCompras.Dominio;

namespace ListaDeComprasWeb.WebApp.Modulos.ModuloListaCompras.Aplicacao;

public record ListarListasComprasDto(
    Guid Id,
    string Nome,
    DateTime DataCriacao,
    StatusListaCompras Status,
    int TotalItens,
    decimal TotalEstimado
);

public record CadastrarListaComprasDto(
    string Nome
);

public record EditarListaComprasDto(
    Guid Id,
    string Nome,
    StatusListaCompras Status
);

public record DetalhesListaComprasDto(
    Guid Id,
    string Nome,
    DateTime DataCriacao,
    StatusListaCompras Status,
    int TotalItens,
    decimal TotalEstimado
);
namespace ListaDeComprasWeb.WebApp.Modulos.ModuloCategoria.Aplicacao;

public record ListarCategoriaDto(
    Guid Id,
    string Nome,
    string Cor
    );

public record CadastrarCategoriaDto(
    string Nome,
    string Cor
    );

public record EditarCategoriaDto(
    Guid Id,
    string Nome,
    string Cor
    );

public record DetalhesCategoriaDto(
    Guid Id,
    string Nome,
    string Cor
    );
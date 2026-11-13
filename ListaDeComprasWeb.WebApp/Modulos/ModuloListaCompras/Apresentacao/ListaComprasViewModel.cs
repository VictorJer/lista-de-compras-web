using System.ComponentModel.DataAnnotations;
using ListaDeComprasWeb.WebApp.Modulos.ModuloListaCompras.Dominio;

namespace ListaDeComprasWeb.WebApp.Modulos.ModuloListaCompras.Apresentacao;

public record ListarListasComprasViewModel(
    Guid Id,
    string Nome,
    DateTime DataCriacao,
    StatusListaCompras Status,
    int TotalItens,
    decimal TotalEstimado
);

public record CadastrarListaComprasViewModel(
    [Required(ErrorMessage = "O campo \"Nome\" é obrigatório.")]
    [StringLength(100, MinimumLength = 3, ErrorMessage = "O campo \"Nome\" deve ter entre 3 e 100 caracteres.")]
    string Nome
);

public record EditarListaComprasViewModel(
    Guid Id,

    [Required(ErrorMessage = "O campo \"Nome\" é obrigatório.")]
    [StringLength(100, MinimumLength = 3, ErrorMessage = "O campo \"Nome\" deve ter entre 3 e 100 caracteres.")]
    string Nome,

    [Required(ErrorMessage = "O campo \"Status\" é obrigatório.")]
    StatusListaCompras Status
);

public record ExcluirListaComprasViewModel(
    Guid Id,
    string Nome
);
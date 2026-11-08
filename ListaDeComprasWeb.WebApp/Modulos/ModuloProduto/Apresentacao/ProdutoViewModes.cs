using System.ComponentModel.DataAnnotations;
using ListaDeComprasWeb.WebApp.Modulos.ModuloProduto.Dominio;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace ListaDeComprasWeb.WebApp.Modulos.ModuloProduto.Apresentacao;

public record ListarProdutosViewModel(
    Guid Id,
    string Nome,
    Guid CategotriaId,
    string CategoriaNome,
    string CategoriaCor,
    UnidadeMedida UnidadeMedida,
    decimal PrecoAproximado
);

public record CadastrarProdutoViewModel(
    [Required(ErrorMessage = "O campo \"Nome\" é obrigatório.")]
    [StringLength(100, MinimumLength = 2, ErrorMessage = "O campo \"Nome\" deve ter entre {2} e {1} caracteres.")]
    string Nome,

    [Required(ErrorMessage = "O campo \"Categoria\" é obrigatório.")]
    Guid CategotriaId,

    [Required(ErrorMessage = "O campo \"Unidade de Medida\" é obrigatório.")]
    [EnumDataType(typeof(UnidadeMedida), ErrorMessage = "O campo \"Unidade de Medida\" é inválido.")]
    UnidadeMedida UnidadeMedida,

    [Required(ErrorMessage = "O campo \"Preço Aproximado\" é obrigatório.")]
    [Range(0.01, double.MaxValue, ErrorMessage = "O campo \"Preço Aproximado\" deve ser maior que zero.")]
    decimal PrecoAproximado,

    [ValidateNever]
    List<OpcaoCategoriaViewModel> Categorias
);

public record EditarProdutoViewModel(
    Guid Id,

    [Required(ErrorMessage = "O campo \"Nome\" é obrigatório.")]
    [StringLength(100, MinimumLength = 2, ErrorMessage = "O campo \"Nome\" deve ter entre {2} e {1} caracteres.")]
    string Nome,

    [Required(ErrorMessage = "O campo \"Categoria\" é obrigatório.")]
    Guid CategotriaId,

    [Required(ErrorMessage = "O campo \"Unidade de Medida\" é obrigatório.")]
    [EnumDataType(typeof(UnidadeMedida), ErrorMessage = "O campo \"Unidade de Medida\" é inválido.")]
    UnidadeMedida UnidadeMedida,

    [Required(ErrorMessage = "O campo \"Preço Aproximado\" é obrigatório.")]
    [Range(0.01, double.MaxValue, ErrorMessage = "O campo \"Preço Aproximado\" deve ser maior que zero.")]
    decimal PrecoAproximado,

    [ValidateNever]
    List<OpcaoCategoriaViewModel> Categorias
);

public record ExcluirProdutoViewModel(
    Guid Id,
    string Nome,
    Guid CategotriaId,
    string CategoriaNome,
    string CategoriaCor,
    UnidadeMedida UnidadeMedida,
    decimal PrecoAproximado
);

public record OpcaoCategoriaViewModel(
    Guid Id,
    string Nome,
    string Cor
);
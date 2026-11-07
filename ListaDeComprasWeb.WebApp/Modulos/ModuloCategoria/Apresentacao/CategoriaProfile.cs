using AutoMapper;
using ListaDeComprasWeb.WebApp.ModuloCategoria.Apresentacao;
using ListaDeComprasWeb.WebApp.Modulos.ModuloCategoria.Aplicacao;

namespace ListaDeComprasWeb.WebApp.Modulos.ModuloCategoria.Apresentacao;

public class CategoriaProfile : Profile
{
public CategoriaProfile()
{
    CreateMap<ListarCategoriaDto, ListarCategoriasViewModel>();

    CreateMap<CadastrarCategoriaDto, CadastrarCategoriaViewModel>()
        .ReverseMap();

    CreateMap<EditarCategoriaDto, EditarCategoriaViewModel>()
        .ReverseMap();

    CreateMap<DetalhesCategoriaDto, DetalhesCategoriaViewModel>();
    CreateMap<DetalhesCategoriaDto, EditarCategoriaViewModel>();
    CreateMap<DetalhesCategoriaDto, ExcluirCategoriaViewModel>();
}
}
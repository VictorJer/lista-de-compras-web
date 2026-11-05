using AutoMapper;
using ListaDeComprasWeb.WebApp.ModuloCategoria.Apresentacao;
using ListaDeComprasWeb.WebApp.Modulos.ModuloCategoria.Aplicacao;

public class CategoriaProfile : Profile
{
    public CategoriaProfile()
    {
        CreateMap<ListarCategoriaDto, ListarCategoriasViewModel>();
    }
}
using AutoMapper;
using ListaDeComprasWeb.WebApp.Modulos.ModuloListaCompras.Aplicacao;

namespace ListaDeComprasWeb.WebApp.Modulos.ModuloListaCompras.Apresentacao;

public class ListaComprasProfile : Profile
{
    public ListaComprasProfile()
    {
        CreateMap<ListarListasComprasDto, ListarListasComprasViewModel>();
        CreateMap<CadastrarListaComprasViewModel, CadastrarListaComprasDto>();
        CreateMap<EditarListaComprasViewModel, EditarListaComprasDto>();
        CreateMap<DetalhesListaComprasDto, EditarListaComprasViewModel>();
        CreateMap<DetalhesListaComprasDto, DetalhesListaItemListaViewModel>();
    }
}

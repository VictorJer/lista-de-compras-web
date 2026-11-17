using AutoMapper;
using ListaDeComprasWeb.WebApp.Modulos.ModuloItemLista.Aplicacao;
using ListaDeComprasWeb.WebApp.Modulos.ModuloListaCompras.Aplicacao;
using ListaDeComprasWeb.WebApp.Modulos.ModuloListaCompras.Apresentacao;

namespace ListaDeComprasWeb.WebApp.Modulos.ModuloItemLista.Apresentacao;

public class ItemListaProfile : Profile
{
    public ItemListaProfile()
    {
        CreateMap<ListarItensListaDto, ListarItensListaViewModel>()
            .ForCtorParam("ListaComprasNome", opt => opt.MapFrom(src => src.ListaComprasNoem));
        CreateMap<DetalhesItemListaDto, ExcluirItemListaViewModel>()
            .ForCtorParam("ListaComprasNome", opt => opt.MapFrom(src => src.ListaComprasNoem));
        CreateMap<OpcaoProdutoDto, OpcaoProdutoViewModel>();
        CreateMap<CadastrarItemListaViewModel, CadastrarItemListaDto>();
        CreateMap<DetalhesListaComprasDto, DetalhesListaItemListaViewModel>();
    }
}

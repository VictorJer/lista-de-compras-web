using AutoMapper;
using ListaDeComprasWeb.WebApp.Modulos.ModuloProduto.Aplicacao;

namespace ListaDeComprasWeb.WebApp.Modulos.ModuloProduto.Apresentacao;

public class ProdutoProfile : Profile
{
    public ProdutoProfile()
    {
        CreateMap<OpcaoCategoriaDto, OpcaoCategoriaViewModel>();
        CreateMap<ListarProdutosDto, ListarProdutosViewModel>();
        CreateMap<CadastrarProdutoViewModel, CadastrarProdutoDto>();
        CreateMap<EditarProdutoViewModel, EditarProdutoDto>();
        CreateMap<DetalhesProdutoDto, EditarProdutoViewModel>();
        CreateMap<DetalhesProdutoDto, ExcluirProdutoViewModel>();
    }
}
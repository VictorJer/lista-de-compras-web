using AutoMapper;
using ListaDeComprasWeb.WebApp.Modulos.ModuloProduto.Aplicacao;
using ListaDeComprasWeb.WebApp.Modulos.ModuloProduto.Apresentacao;
using ListaDeComprasWeb.WebApp.Modulos.ModuloProduto.Dominio;
using Microsoft.AspNetCore.Mvc;

public class ProdutoController(ServicoProduto servicoProduto, IMapper mapper) : Controller
{
    [HttpGet]
    public ActionResult Listar()
    {
        List<ListarProdutosDto> dtos = servicoProduto.SelecionarTodos();
        List<ListarProdutosViewModel> vm = mapper.Map<List<ListarProdutosViewModel>>(dtos);

        return View(vm);
    }

    [HttpGet]
    public ActionResult Cadastrar()
    {
        CadastrarProdutoViewModel cadastrarVm = new CadastrarProdutoViewModel(
            string.Empty,
            Guid.Empty,
            UnidadeMedida.Unidade,
            0,
            SelecionarCategorias()
        );

        return View(cadastrarVm);
    }

    private List<OpcaoCategoriaViewModel> SelecionarCategorias()
    {
        var categoriasDto = servicoProduto.SelecionarCategorias();

        return mapper.Map<List<OpcaoCategoriaViewModel>>(categoriasDto);
    }
}
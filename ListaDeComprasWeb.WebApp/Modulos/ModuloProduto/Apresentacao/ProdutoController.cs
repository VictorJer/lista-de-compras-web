using AutoMapper;
using ListaDeComprasWeb.WebApp.Modulos.ModuloProduto.Aplicacao;
using ListaDeComprasWeb.WebApp.Modulos.ModuloProduto.Apresentacao;
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
}
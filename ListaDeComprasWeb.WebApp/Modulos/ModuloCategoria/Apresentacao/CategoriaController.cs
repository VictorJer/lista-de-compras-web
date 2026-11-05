using AutoMapper;
using ListaDeComprasWeb.WebApp.ModuloCategoria.Apresentacao;
using ListaDeComprasWeb.WebApp.Modulos.ModuloCategoria.Aplicacao;
using Microsoft.AspNetCore.Mvc;

public class CategoriaController(ServicoCategoria servicoCategoria, IMapper maper) : Controller
{
    [HttpGet]
    public IActionResult Index()
    {
        var dto = servicoCategoria.SelecionarTodos();
        var vm = maper.Map<List<ListarCategoriasViewModel>>(dto);

        return View(vm);
    }
}
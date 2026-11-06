using AutoMapper;
using ListaDeComprasWeb.WebApp.Compartilhado.Apresentacao.Extensions;
using ListaDeComprasWeb.WebApp.ModuloCategoria.Apresentacao;
using ListaDeComprasWeb.WebApp.Modulos.ModuloCategoria.Aplicacao;
using Microsoft.AspNetCore.Mvc;

public class CategoriaController(ServicoCategoria servicoCategoria, IMapper maper) : Controller
{

    [HttpGet]
    public ActionResult Listar()
    {
        var dto = servicoCategoria.SelecionarTodos();
        var vm = maper.Map<List<ListarCategoriasViewModel>>(dto);

        return View(vm);
    }

    [HttpGet]
    public ActionResult Cadastrar()
    {
        CadastrarCategoriaViewModel vm = new CadastrarCategoriaViewModel(string.Empty, "#0d6efd");

        return View(vm);
    }

    [HttpPost]
    public ActionResult Cadastrar(CadastrarCategoriaViewModel vm)
    {
        if (!ModelState.IsValid)
            return View(vm);

        var dto = maper.Map<CadastrarCategoriaDto>(vm);
         
        var resultado = servicoCategoria.Cadastrar(dto);

        if (!resultado.IsFailed)
        {
            ModelState.AddModelError(resultado);

            return View(vm);
        }

        return RedirectToAction(nameof(Listar));
    }
    
}
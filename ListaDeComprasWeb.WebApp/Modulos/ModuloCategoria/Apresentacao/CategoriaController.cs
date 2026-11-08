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

        if (resultado.IsFailed)
        {
            ModelState.AddModelError(resultado);

            return View(vm);
        }

        return RedirectToAction(nameof(Listar));
    }
    
    [HttpGet]
    public ActionResult Editar(Guid id)
    {
        var resultado = servicoCategoria.SelecionarPorId(id);

        if (resultado.IsFailed)
            return NotFound(resultado.Errors.First().Message);

        var vm = maper.Map<EditarCategoriaViewModel>(resultado.Value);

        return View(vm);
    }

    [HttpPost]
    public ActionResult Editar(EditarCategoriaViewModel vm)
    {
        if (!ModelState.IsValid)
            return View(vm);

        var dto = maper.Map<EditarCategoriaDto>(vm);

        var resultado = servicoCategoria.Editar(dto);

        if (resultado.IsFailed)
        {
            ModelState.AddModelError(resultado);

            return View(vm);
        }

        return RedirectToAction(nameof(Listar));
    }

    [HttpGet]
    public ActionResult Excluir(Guid id)
    {
        var resultado = servicoCategoria.SelecionarPorId(id);

        if (resultado.IsFailed)
            return NotFound(resultado.Errors.First().Message);

        var vm = maper.Map<ExcluirCategoriaViewModel>(resultado.Value);

        return View(vm);
    }

    [HttpPost]
    public ActionResult Excluir(Guid? id)
    {
        if (!id.HasValue)
            return BadRequest("O identificador da categoria é obrigatório.");

        var resultado = servicoCategoria.Excluir(id.Value);

        if (resultado.IsFailed)
        {
            ModelState.AddModelError(resultado);

            var detalhes = servicoCategoria.SelecionarPorId(id.Value);

            if (detalhes.IsFailed)
                return NotFound(detalhes.Errors.First().Message);

            var vm = maper.Map<ExcluirCategoriaViewModel>(detalhes.Value);

            return View(vm);
        }

        return RedirectToAction(nameof(Listar));
    }
}
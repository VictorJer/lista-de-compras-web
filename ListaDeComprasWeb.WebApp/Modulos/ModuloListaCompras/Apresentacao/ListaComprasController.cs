using AutoMapper;
using FluentResults;
using ListaDeComprasWeb.WebApp.Compartilhado.Apresentacao.Extensions;
using ListaDeComprasWeb.WebApp.Modulos.ModuloListaCompras.Aplicacao;
using ListaDeComprasWeb.WebApp.Modulos.ModuloListaCompras.Dominio;
using Microsoft.AspNetCore.Mvc;

namespace ListaDeComprasWeb.WebApp.Modulos.ModuloListaCompras.Apresentacao;

public class ListaComprasController(ServicoListaCompras servicoListaCompras, IMapper mapper) : Controller
{
    [HttpGet]
    public ActionResult Listar()
    {
        List<ListarListasComprasDto> listasCompras = servicoListaCompras.SelecionarTodos();
        List<ListarListasComprasViewModel> viewModel = mapper.Map<List<ListarListasComprasViewModel>>(listasCompras);

        return View(viewModel);
    }

    [HttpGet]
    public ActionResult Cadastrar()
    {
        CadastrarListaComprasViewModel viewModel = new CadastrarListaComprasViewModel(string.Empty);

        return View(viewModel);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public ActionResult Cadastrar(CadastrarListaComprasViewModel viewModel)
    {
        if (!ModelState.IsValid)
            return View(viewModel);

        CadastrarListaComprasDto dto = mapper.Map<CadastrarListaComprasDto>(viewModel);
        var result = servicoListaCompras.Cadastrar(dto);

        if (result.IsFailed)
        {
            ModelState.AddModelError(result);

            return View(viewModel);
        }

        TempData["MensagemSucesso"] = "Lista criada! Sua próxima compra já tem um lugar.";
        return RedirectToAction(nameof(Listar));
    }

    [HttpGet]
    public ActionResult Editar(Guid id)
    {
        Result<DetalhesListaComprasDto> result = servicoListaCompras.SelecionarPorId(id);

        if (result.IsFailed)
        {
            TempData.AddErrorMessage(result);

            return RedirectToAction(nameof(Listar));
        }

        EditarListaComprasViewModel viewModel = mapper.Map<EditarListaComprasViewModel>(result.Value);

        return View(viewModel);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public ActionResult Editar(EditarListaComprasViewModel editarVm)
    {
        if (!ModelState.IsValid)
            return View(editarVm);
        
        EditarListaComprasDto dto = mapper.Map<EditarListaComprasDto>(editarVm);
        Result result = servicoListaCompras.Editar(dto);

        if (result.IsFailed)
        {
            ModelState.AddModelError(result);

            return View(editarVm);
        }

        TempData["MensagemSucesso"] = "Lista atualizada! Tudo pronto para continuar seu planejamento.";
        return RedirectToAction(nameof(Listar));
    }

    public ActionResult Concluir(Guid id)
    {
        Result<DetalhesListaComprasDto> result = servicoListaCompras.SelecionarPorId(id);

        if (result.IsFailed)
        {
            TempData.AddErrorMessage(result);

            return RedirectToAction(nameof(Listar));
        }

        DetalhesListaComprasDto detalhes = result.Value;

        Result resultEdicao = servicoListaCompras.Editar(new EditarListaComprasDto(
            detalhes.Id,
            detalhes.Nome,
            StatusListaCompras.Concluida
        ));

        if (resultEdicao.IsFailed)
        {
            TempData.AddErrorMessage(result);

            return RedirectToAction(nameof(Listar));
        }

        return RedirectToAction(nameof(Listar));
    }
}

using AutoMapper;
using FluentResults;
using ListaDeComprasWeb.WebApp.Compartilhado.Apresentacao.Extensions;
using ListaDeComprasWeb.WebApp.Modulos.ModuloItemLista.Aplicacao;
using ListaDeComprasWeb.WebApp.Modulos.ModuloListaCompras.Aplicacao;
using ListaDeComprasWeb.WebApp.Modulos.ModuloListaCompras.Apresentacao;
using Microsoft.AspNetCore.Mvc;

namespace ListaDeComprasWeb.WebApp.Modulos.ModuloItemLista.Apresentacao;

public class ItemListaController(ServicoItemLista servicoItemLista, IMapper mapper) : Controller
{
    [HttpGet]
    public ActionResult Listar(Guid listaComprasId)
    {
        Result<DetalhesListaComprasDto> result = servicoItemLista.SelecionarDetalhesLista(listaComprasId);
        if (result.IsFailed)
        {
            TempData.AddErrorMessage(result);
            return RedirectToAction("Listar", "ListaCompras");
        }

        DetalhesListaItemListaViewModel lista = mapper.Map<DetalhesListaItemListaViewModel>(result.Value);
        List<ListarItensListaViewModel> itens = mapper.Map<List<ListarItensListaViewModel>>(
            servicoItemLista.SelecionarTodosPorLista(listaComprasId));
        return View(new GerenciaItemListaViewModel(lista, itens));
    }

    [HttpGet]
    public ActionResult Cadastrar(Guid listaComprasId)
    {
        Result<DetalhesListaComprasDto> lista = servicoItemLista.SelecionarDetalhesLista(listaComprasId);
        if (lista.IsFailed)
        {
            TempData.AddErrorMessage(lista);
            return RedirectToAction("Listar", "ListaCompras");
        }

        ViewBag.Lista = mapper.Map<DetalhesListaItemListaViewModel>(lista.Value);
        return View(new CadastrarItemListaViewModel(listaComprasId, Guid.Empty, 1, SelecionarProdutos()));
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public ActionResult Cadastrar(CadastrarItemListaViewModel viewModel)
    {
        Result<DetalhesListaComprasDto> lista = servicoItemLista.SelecionarDetalhesLista(viewModel.ListaComprasId);
        if (lista.IsFailed)
        {
            TempData.AddErrorMessage(lista);
            return RedirectToAction("Listar", "ListaCompras");
        }
        ViewBag.Lista = mapper.Map<DetalhesListaItemListaViewModel>(lista.Value);

        if (viewModel.ProdutoId == Guid.Empty)
            ModelState.AddModelError(nameof(viewModel.ProdutoId), "Selecione um produto válido.");

        if (!ModelState.IsValid)
            return View(viewModel with { Produtos = SelecionarProdutos() });

        Result resultado = servicoItemLista.Cadastrar(mapper.Map<CadastrarItemListaDto>(viewModel));
        if (resultado.IsFailed)
        {
            ModelState.AddModelError(resultado);
            return View(viewModel with { Produtos = SelecionarProdutos() });
        }

        TempData["MensagemSucesso"] = "Item adicionado à lista.";
        return RedirectToAction(nameof(Listar), new { listaComprasId = viewModel.ListaComprasId });
    }

    [HttpGet]
    public ActionResult Excluir(Guid id, Guid? listaComprasId)
    {
        Result<DetalhesItemListaDto> result = servicoItemLista.SelecionarPorId(id);
        if (result.IsFailed)
        {
            TempData.AddErrorMessage(result);
            return RetornarParaListaValidada(listaComprasId);
        }

        if (listaComprasId.HasValue && listaComprasId.Value != result.Value.ListaComprasId)
        {
            TempData["MensagemErro"] = "O item não pertence à lista informada.";
            return RedirectToAction(nameof(Listar), new { listaComprasId = result.Value.ListaComprasId });
        }

        return View(mapper.Map<ExcluirItemListaViewModel>(result.Value));
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public ActionResult Excluir(ExcluirItemListaViewModel viewModel)
    {
        Result<DetalhesItemListaDto> atual = servicoItemLista.SelecionarPorId(viewModel.Id);
        if (atual.IsFailed)
        {
            TempData.AddErrorMessage(atual);
            return RetornarParaListaValidada(viewModel.ListaComprasId == Guid.Empty ? null : viewModel.ListaComprasId);
        }

        Guid listaRealId = atual.Value.ListaComprasId;
        if (viewModel.ListaComprasId != listaRealId)
        {
            TempData["MensagemErro"] = "A lista do item mudou. Confira os itens antes de tentar novamente.";
            return RedirectToAction(nameof(Listar), new { listaComprasId = listaRealId });
        }

        Result resultado = servicoItemLista.Excluir(viewModel.Id);
        if (resultado.IsFailed)
            TempData.AddErrorMessage(resultado);
        else
            TempData["MensagemSucesso"] = "Item removido da lista.";

        return RedirectToAction(nameof(Listar), new { listaComprasId = listaRealId });
    }

    private ActionResult RetornarParaListaValidada(Guid? listaComprasId)
    {
        if (listaComprasId.HasValue && servicoItemLista.SelecionarDetalhesLista(listaComprasId.Value).IsSuccess)
            return RedirectToAction(nameof(Listar), new { listaComprasId = listaComprasId.Value });
        return RedirectToAction("Listar", "ListaCompras");
    }

    private List<OpcaoProdutoViewModel> SelecionarProdutos() =>
        mapper.Map<List<OpcaoProdutoViewModel>>(servicoItemLista.SelecionarProdutos());
}

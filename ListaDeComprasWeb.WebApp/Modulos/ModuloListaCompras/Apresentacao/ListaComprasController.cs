using AutoMapper;
using ListaDeComprasWeb.WebApp.Modulos.ModuloListaCompras.Aplicacao;
using ListaDeComprasWeb.WebApp.Modulos.ModuloListaCompras.Apresentacao;
using Microsoft.AspNetCore.Mvc;

public class ListaComprasController(ServicoListaCompras servicoListaCompras, IMapper mapper) : Controller
{
    [HttpGet]
    public ActionResult Listar()
    {
        List<ListarListasComprasDto> listasCompras = servicoListaCompras.SelecionarTodos();
        List<ListarListasComprasViewModel> viewModel = mapper.Map<List<ListarListasComprasViewModel>>(listasCompras);

        return View(viewModel);
    }
}
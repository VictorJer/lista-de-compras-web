using AutoMapper;
using FluentResults;
using ListaDeComprasWeb.WebApp.Compartilhado.Apresentacao.Extensions;
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

    [HttpPost]
    public ActionResult Cadastrar(CadastrarProdutoViewModel vm)
    {
        if (!ModelState.IsValid)
            return View(vm with { Categorias = SelecionarCategorias() });

        CadastrarProdutoDto dto = mapper.Map<CadastrarProdutoDto>(vm);
        var result = servicoProduto.Cadastrar(dto);

        if (!result.IsFailed)
        {
            ModelState.AddModelError(result);

            return View(vm with { Categorias = SelecionarCategorias() });
        }

        return RedirectToAction("Listar");
    }

    [HttpGet]
    public ActionResult Editar(Guid id)
    {
        Result<DetalhesProdutoDto> dto = servicoProduto.SelecionarPorId(id);

        if (dto.IsFailed)
        {
            TempData.AddErrorMessage(dto);

            return RedirectToAction(nameof(Listar));
        }

        EditarProdutoViewModel vm = mapper.Map<EditarProdutoViewModel>(dto.Value) with { Categorias = SelecionarCategorias() };

        return View(vm);
    }


    private List<OpcaoCategoriaViewModel> SelecionarCategorias()
    {
        var categoriasDto = servicoProduto.SelecionarCategorias();

        return mapper.Map<List<OpcaoCategoriaViewModel>>(categoriasDto);
    }
}
using Microsoft.AspNetCore.Mvc;

namespace ListaDeComprasWeb.WebApp.Compartilhado.Apresentacao;

public class HomeController : Controller
{
    [HttpGet]
    public ActionResult Index()
    {
        return View();
    }
}

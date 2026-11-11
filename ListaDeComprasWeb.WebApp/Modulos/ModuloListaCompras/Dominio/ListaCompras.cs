using ListaDeComprasWeb.WebApp.Compartilhado.Dominio;

namespace ListaDeComprasWeb.WebApp.Modulos.ModuloListaCompras.Dominio;

public sealed class ListaCompras : EntidadeBase<ListaCompras>
{
    public string Nome { get; set; } = string.Empty;
    public DateTime DataCriacao { get; set; } = DateTime.Now;
    public StatusListaCompras Status { get; set; } = StatusListaCompras.Aberta;

    public override void Atualizar(ListaCompras entidadeAtualizada)
    {
        throw new NotImplementedException();
    }

    public override List<string> Validar()
    {
        throw new NotImplementedException();
    }
}
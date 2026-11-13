using ListaDeComprasWeb.WebApp.Compartilhado.Dominio;
using ListaDeComprasWeb.WebApp.Modulos.ModuloListaCompras.Dominio;
using ListaDeComprasWeb.WebApp.Modulos.ModuloProduto.Dominio;

namespace ListaDeComprasWeb.WebApp.Modulos.ModuloItemLista.Dominio;

public sealed class ItemLista : EntidadeBase<ItemLista>
{
    public ListaCompras ListaCompras { get; set; } = null!;
    public Produto Produto { get; set; } = null!;
    public decimal Quantidade { get; set; }

    public ItemLista() { }
    public ItemLista(ListaCompras listaCompras, Produto produto, decimal quantidade)
    {
        ListaCompras = listaCompras;
        Produto = produto;
        Quantidade = quantidade;
    }

    public decimal CalcularSubtotal()
    {
        return Produto.PrecoAproximado * Quantidade;
    }

    public override void Atualizar(ItemLista entidadeAtualizada)
    {
        ListaCompras = entidadeAtualizada.ListaCompras;
        Produto = entidadeAtualizada.Produto;
        Quantidade = entidadeAtualizada.Quantidade;
    }

    public override List<string> Validar()
    {
        List<string> erros = new List<string>();

        if (ListaCompras == null)
            erros.Add("O campo \"Lista de Compras\" é obrigatório.");
        if (Produto == null)

            erros.Add("O campo \"Produto\" é obrigatório.");
        if (Quantidade <= 0)

            erros.Add("O campo \"Quantidade\" deve ser um valor positivo.");

        return erros;
    }
}
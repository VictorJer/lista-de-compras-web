using ListaDeComprasWeb.WebApp.Compartilhado.Dominio;

namespace ListaDeComprasWeb.WebApp.Modulos.ModuloListaCompras.Dominio;

public sealed class ListaCompras : EntidadeBase<ListaCompras>
{
    public string Nome { get; set; } = string.Empty;
    public DateTime DataCriacao { get; set; } = DateTime.Now;
    public StatusListaCompras Status { get; set; } = StatusListaCompras.Aberta;

    public ListaCompras() {  }
    public ListaCompras(string nome, DateTime dataCriacao, StatusListaCompras status = StatusListaCompras.Aberta)
    {
        Nome = nome;
        DataCriacao = dataCriacao;
        Status = status;
    }
    public override void Atualizar(ListaCompras entidadeAtualizada)
    {
        Nome = entidadeAtualizada.Nome;
        Status = entidadeAtualizada.Status;
    }

    public override List<string> Validar()
    {
        List<string> erros = new List<string>();

        if (string.IsNullOrWhiteSpace(Nome))
            erros.Add("O nome da lista de compras não pode ser vazio.");
        
        else if (Nome.Length < 3 || Nome.Length > 100)
            erros.Add("O nome da lista de compras não pode ter mais de 100 caracteres.");

        return erros;
    }
}
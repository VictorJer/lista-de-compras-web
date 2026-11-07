using System.Text.Json.Serialization;
using ListaDeComprasWeb.WebApp.Compartilhado.Dominio;

namespace ListaDeComprasWeb.WebApp.Modulos.ModuloCategoria.Dominio;

public sealed class Categoria : EntidadeBase<Categoria>
{
    [JsonInclude]
    public string Nome { get; private set; } = string.Empty;

    [JsonInclude]
    public string Cor { get; private set; } = string.Empty;

    public Categoria() {  }

    public Categoria(string nome, string cor)
    {
        Nome = nome;
        Cor = cor;
    }

    public override List<string> Validar()
    {
        List<string> erros = new List<string>();

        if (string.IsNullOrWhiteSpace(Nome))
            erros.Add("O nome da categoria é obrigatório.");

        if (Nome.Length < 3)
            erros.Add("O nome da categoria deve ter pelo menos 3 caracteres.");
        
        else if (Nome.Length > 50)
            erros.Add("O nome da categoria deve ter no máximo 50 caracteres.");
        
        if (string.IsNullOrWhiteSpace(Cor))
            erros.Add("A cor da categoria é obrigatória.");
        
        return erros;
    }

    public override void Atualizar(Categoria entidadeAtualizada)
    {
        Nome = entidadeAtualizada.Nome;
        Cor = entidadeAtualizada.Cor;
    }
}
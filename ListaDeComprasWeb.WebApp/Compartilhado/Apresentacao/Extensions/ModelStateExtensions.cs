using FluentResults;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace ListaDeComprasWeb.WebApp.Compartilhado.Apresentacao.Extensions;

public static class ModelStateExtensions
{
    public static void AddModelError(this ModelStateDictionary modelState, ResultBase result)
    {
        foreach (IError erro in result.Errors)
        {
            string campo = erro.Metadata.TryGetValue("Campo", out var valor) && valor is string nomeCampo
                ? nomeCampo
                : string.Empty;

            modelState.AddModelError(campo, erro.Message);
        }
    }
}

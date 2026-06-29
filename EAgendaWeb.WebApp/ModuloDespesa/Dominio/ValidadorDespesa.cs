namespace EAgendaWeb.WebApp.ModuloDespesa.Dominio;

public class ValidadorDespesa
{
    public List<string> Validar(Despesa despesa)
    {
        List<string> erros = [];

        if (string.IsNullOrWhiteSpace(despesa.Descricao))
            erros.Add("Descrição obrigatória.");

        if (despesa.Descricao?.Length < 2 ||
            despesa.Descricao?.Length > 100)
        {
            erros.Add(
                "Descrição deve possuir entre 2 e 100 caracteres.");
        }

        if (despesa.Valor <= 0)
            erros.Add(
                "O valor deve ser maior que zero.");

        if (!despesa.Categorias.Any())
            erros.Add(
                "Selecione ao menos uma categoria.");

        return erros;
    }
}
namespace EAgendaWeb.WebApp.ModuloTarefa.Dominio;

public class ValidadorItemTarefa
{
    public List<string> Validar(ItemTarefa item)
    {
        List<string> erros = [];

        if (string.IsNullOrWhiteSpace(item.Titulo))
            erros.Add("Título obrigatório.");

        if (item.Titulo.Length < 2 ||
            item.Titulo.Length > 100)
        {
            erros.Add(
                "Título deve possuir entre 2 e 100 caracteres.");
        }

        return erros;
    }
}
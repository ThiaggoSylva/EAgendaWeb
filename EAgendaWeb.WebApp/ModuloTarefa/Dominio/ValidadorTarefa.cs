namespace EAgendaWeb.WebApp.ModuloTarefa.Dominio;

public class ValidadorTarefa
{
    public List<string> Validar(Tarefa tarefa)
    {
        List<string> erros = [];

        if (string.IsNullOrWhiteSpace(tarefa.Titulo))
            erros.Add("Título obrigatório.");

        if (tarefa.Titulo.Length < 2 ||
            tarefa.Titulo.Length > 100)
        {
            erros.Add(
                "Título deve possuir entre 2 e 100 caracteres.");
        }

        return erros;
    }
}
namespace EAgendaWeb.WebApp.ModuloCategoria.Dominio;

public class ValidadorCategoria
{
    public List<string> Validar(Categoria categoria)
    {
        List<string> erros = [];

        if (string.IsNullOrWhiteSpace(categoria.Titulo))
            erros.Add("O campo Título é obrigatório.");

        if (!string.IsNullOrWhiteSpace(categoria.Titulo))
        {
            if (categoria.Titulo.Length < 2 ||
                categoria.Titulo.Length > 100)
            {
                erros.Add("O Título deve possuir entre 2 e 100 caracteres.");
            }
        }

        return erros;
    }
}
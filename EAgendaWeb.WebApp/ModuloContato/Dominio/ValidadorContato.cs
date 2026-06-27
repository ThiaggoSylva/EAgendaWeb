using System.Text.RegularExpressions;

namespace EAgendaWeb.WebApp.ModuloContato.Dominio;

public class ValidadorContato
{
    public List<string> Validar(Contato contato)
    {
        List<string> erros = [];

        if (string.IsNullOrWhiteSpace(contato.Nome))
            erros.Add("O campo Nome é obrigatório.");

        if (!string.IsNullOrWhiteSpace(contato.Nome))
        {
            if (contato.Nome.Length < 2 || contato.Nome.Length > 100)
                erros.Add("O Nome deve possuir entre 2 e 100 caracteres.");
        }

        if (string.IsNullOrWhiteSpace(contato.Email))
            erros.Add("O campo Email é obrigatório.");

        if (!string.IsNullOrWhiteSpace(contato.Email))
        {
            if (!Regex.IsMatch(
                contato.Email,
                @"^[^@\s]+@[^@\s]+\.[^@\s]+$"))
            {
                erros.Add("O Email informado é inválido.");
            }
        }

        if (string.IsNullOrWhiteSpace(contato.Telefone))
            erros.Add("O campo Telefone é obrigatório.");

        if (!string.IsNullOrWhiteSpace(contato.Telefone))
        {
            bool telefoneValido = Regex.IsMatch(
                contato.Telefone,
                @"^\(\d{2}\)\s\d{4,5}-\d{4}$");

            if (!telefoneValido)
                erros.Add(
                    "O Telefone deve estar no formato (XX) XXXX-XXXX ou (XX) XXXXX-XXXX.");
        }

        return erros;
    }
}
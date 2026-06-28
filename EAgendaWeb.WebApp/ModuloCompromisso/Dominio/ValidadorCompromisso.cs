namespace EAgendaWeb.WebApp.ModuloCompromisso.Dominio;

public class ValidadorCompromisso
{
    public List<string> Validar(Compromisso compromisso)
    {
        List<string> erros = [];

        if (string.IsNullOrWhiteSpace(compromisso.Assunto))
            erros.Add("O campo Assunto é obrigatório.");

        if (!string.IsNullOrWhiteSpace(compromisso.Assunto))
        {
            if (compromisso.Assunto.Length < 2 ||
                compromisso.Assunto.Length > 100)
            {
                erros.Add("O Assunto deve possuir entre 2 e 100 caracteres.");
            }
        }

        if (compromisso.DataOcorrencia == default)
            erros.Add("A Data de Ocorrência é obrigatória.");

        if (compromisso.HoraInicio >= compromisso.HoraTermino)
            erros.Add("A Hora de Início deve ser menor que a Hora de Término.");

        if (compromisso.Tipo == TipoCompromissoEnum.Presencial)
        {
            if (string.IsNullOrWhiteSpace(compromisso.Local))
                erros.Add("O Local é obrigatório para compromissos presenciais.");
        }

        if (compromisso.Tipo == TipoCompromissoEnum.Remoto)
        {
            if (string.IsNullOrWhiteSpace(compromisso.Link))
                erros.Add("O Link é obrigatório para compromissos remotos.");
        }

        return erros;
    }
}
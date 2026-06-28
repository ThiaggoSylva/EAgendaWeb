using FluentResults;
using EAgendaWeb.WebApp.ModuloCompromisso.Dominio;

namespace EAgendaWeb.WebApp.ModuloCompromisso.Aplicacao;

public class ServicoCompromisso
{
    private readonly IRepositorioCompromisso repositorio;
    private readonly ValidadorCompromisso validador;

    public ServicoCompromisso(
        IRepositorioCompromisso repositorio)
    {
        this.repositorio = repositorio;

        validador = new ValidadorCompromisso();
    }

    public Result Cadastrar(InserirCompromissoDto dto)
    {
        var compromisso = new Compromisso
        {
            Assunto = dto.Assunto,
            DataOcorrencia = dto.DataOcorrencia,
            HoraInicio = dto.HoraInicio,
            HoraTermino = dto.HoraTermino,
            Tipo = dto.Tipo,
            Local = dto.Local,
            Link = dto.Link,
            ContatoId = dto.ContatoId
        };

        var erros = validador.Validar(compromisso);

        if (erros.Any())
            return Result.Fail(erros);

        bool possuiConflito =
            repositorio.ExisteConflitoHorario(
                null,
                compromisso.DataOcorrencia,
                compromisso.HoraInicio,
                compromisso.HoraTermino);

        if (possuiConflito)
            return Result.Fail(
                "Já existe um compromisso neste horário.");

        repositorio.Cadastrar(compromisso);

        return Result.Ok();
    }

    public Result Editar(EditarCompromissoDto dto)
    {
        var compromisso =
            repositorio.SelecionarPorId(dto.Id);

        if (compromisso is null)
            return Result.Fail("Compromisso não encontrado.");

        compromisso.Assunto = dto.Assunto;
        compromisso.DataOcorrencia = dto.DataOcorrencia;
        compromisso.HoraInicio = dto.HoraInicio;
        compromisso.HoraTermino = dto.HoraTermino;
        compromisso.Tipo = dto.Tipo;
        compromisso.Local = dto.Local;
        compromisso.Link = dto.Link;
        compromisso.ContatoId = dto.ContatoId;

        var erros = validador.Validar(compromisso);

        if (erros.Any())
            return Result.Fail(erros);

        bool possuiConflito =
            repositorio.ExisteConflitoHorario(
                compromisso.Id,
                compromisso.DataOcorrencia,
                compromisso.HoraInicio,
                compromisso.HoraTermino);

        if (possuiConflito)
            return Result.Fail(
                "Já existe um compromisso neste horário.");

        repositorio.Editar(compromisso);

        return Result.Ok();
    }

    public Result Excluir(Guid id)
    {
        var compromisso =
            repositorio.SelecionarPorId(id);

        if (compromisso is null)
            return Result.Fail(
                "Compromisso não encontrado.");

        repositorio.Excluir(id);

        return Result.Ok();
    }

    public DetalhesCompromissoDto? SelecionarPorId(Guid id)
    {
        var compromisso =
            repositorio.SelecionarPorId(id);

        if (compromisso is null)
            return null;

        return new DetalhesCompromissoDto(
            compromisso.Id,
            compromisso.Assunto,
            compromisso.DataOcorrencia,
            compromisso.HoraInicio,
            compromisso.HoraTermino,
            compromisso.Tipo,
            compromisso.Local,
            compromisso.Link,
            compromisso.ContatoId
        );
    }

    public List<ListarCompromissoDto> SelecionarTodos()
    {
        var compromissos =
            repositorio.SelecionarTodos();

        return compromissos
            .Select(c => new ListarCompromissoDto(
                c.Id,
                c.Assunto,
                c.DataOcorrencia,
                c.HoraInicio,
                c.HoraTermino,
                c.Tipo))
            .ToList();
    }
}
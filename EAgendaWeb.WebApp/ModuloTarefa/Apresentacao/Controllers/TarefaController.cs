using AutoMapper;

using Microsoft.AspNetCore.Mvc;

using EAgendaWeb.WebApp.ModuloTarefa.Aplicacao;
using EAgendaWeb.WebApp.ModuloTarefa.Apresentacao.Models;

namespace EAgendaWeb.WebApp.ModuloTarefa.Apresentacao.Controllers;

public class TarefaController : Controller
{
    private readonly ServicoTarefa servicoTarefa;
    private readonly ServicoItemTarefa servicoItem;
    private readonly IMapper mapper;

    public TarefaController(
        ServicoTarefa servicoTarefa,
        ServicoItemTarefa servicoItem,
        IMapper mapper)
    {
        this.servicoTarefa = servicoTarefa;
        this.servicoItem = servicoItem;
        this.mapper = mapper;
    }

    public IActionResult Index()
    {
        var tarefas = servicoTarefa.SelecionarTodos();

        return View(tarefas);
    }

    public IActionResult Pendentes()
    {
        var tarefas =
            servicoTarefa.SelecionarPendentes();

        return View("Index", tarefas);
    }

    public IActionResult Concluidas()
    {
        var tarefas =
            servicoTarefa.SelecionarConcluidas();

        return View("Index", tarefas);
    }

    [HttpGet]
    public IActionResult Cadastrar()
    {
        return View();
    }

    [HttpPost]
    public IActionResult Cadastrar(
        CadastrarTarefaViewModel viewModel)
    {
        if (!ModelState.IsValid)
            return View(viewModel);

        var dto =
            mapper.Map<InserirTarefaDto>(viewModel);

        var resultado =
            servicoTarefa.Cadastrar(dto);

        if (resultado.IsFailed)
        {
            foreach (var erro in resultado.Errors)
                ModelState.AddModelError(
                    string.Empty,
                    erro.Message);

            return View(viewModel);
        }

        return RedirectToAction(nameof(Index));
    }

    [HttpGet]
    public IActionResult Editar(Guid id)
    {
        var tarefa =
            servicoTarefa.SelecionarPorId(id);

        if (tarefa is null)
            return NotFound();

        var viewModel =
            mapper.Map<EditarTarefaViewModel>(tarefa);

        return View(viewModel);
    }

    [HttpPost]
    public IActionResult Editar(
        EditarTarefaViewModel viewModel)
    {
        if (!ModelState.IsValid)
            return View(viewModel);

        var dto =
            mapper.Map<EditarTarefaDto>(viewModel);

        var resultado =
            servicoTarefa.Editar(dto);

        if (resultado.IsFailed)
        {
            foreach (var erro in resultado.Errors)
                ModelState.AddModelError(
                    string.Empty,
                    erro.Message);

            return View(viewModel);
        }

        return RedirectToAction(nameof(Index));
    }

    [HttpGet]
    public IActionResult Visualizar(Guid id)
    {
        var tarefa =
            servicoTarefa.SelecionarPorId(id);

        if (tarefa is null)
            return NotFound();

        var viewModel =
            mapper.Map<VisualizarTarefaViewModel>(tarefa);

        return View(viewModel);
    }

    [HttpGet]
    public IActionResult Excluir(Guid id)
    {
        var tarefa =
            servicoTarefa.SelecionarPorId(id);

        if (tarefa is null)
            return NotFound();

        var viewModel =
            mapper.Map<ExcluirTarefaViewModel>(tarefa);

        return View(viewModel);
    }

    [HttpPost]
    public IActionResult Excluir(
        ExcluirTarefaViewModel viewModel)
    {
        servicoTarefa.Excluir(viewModel.Id);

        return RedirectToAction(nameof(Index));
    }

    [HttpGet]
    public IActionResult GerenciarItens(Guid id)
    {
        var tarefa = servicoTarefa.SelecionarPorId(id);

        if (tarefa is null)
            return NotFound();

        var itens =
        servicoItem.SelecionarItens(id);

        var viewModel =
        new GerenciarItensViewModel
        {
            TarefaId = id,
            TituloTarefa = tarefa.Titulo,
            PercentualConcluido = tarefa.PercentualConcluido,

            Itens = itens.Select(i =>
                new ItemTarefaViewModel
                {
                    Id = i.Id,
                    Titulo = i.Titulo,
                    Concluido = i.Concluido
                }).ToList()
        };

        return View(viewModel);
    }

    [HttpPost]
    public IActionResult AdicionarItem(
        Guid tarefaId,
        string titulo)
    {
        var dto =
        new InserirItemTarefaDto(
            tarefaId,
            titulo);

        servicoItem.AdicionarItem(dto);

        return RedirectToAction(
        nameof(GerenciarItens),
        new { id = tarefaId });
    }

    [HttpGet]
    public IActionResult ConcluirItem(
        Guid itemId,
        Guid tarefaId)
    {
        servicoItem.ConcluirItem(
        itemId,
        tarefaId);

        return RedirectToAction(
        nameof(GerenciarItens),
        new { id = tarefaId });
    }

    [HttpGet]
    public IActionResult RemoverItem(
        Guid itemId,
        Guid tarefaId)
    {
        servicoItem.RemoverItem(
        itemId,
        tarefaId);

        return RedirectToAction(
        nameof(GerenciarItens),
        new { id = tarefaId });
    }
}
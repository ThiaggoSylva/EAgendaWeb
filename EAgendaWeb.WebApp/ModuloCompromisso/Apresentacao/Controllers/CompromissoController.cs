using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

using EAgendaWeb.WebApp.ModuloCompromisso.Aplicacao;
using EAgendaWeb.WebApp.ModuloCompromisso.Apresentacao.Models;
using EAgendaWeb.WebApp.ModuloContato.Aplicacao;

namespace EAgendaWeb.WebApp.ModuloCompromisso.Apresentacao.Controllers;

public class CompromissoController : Controller
{
    private readonly ServicoCompromisso servicoCompromisso;
    private readonly ServicoContato servicoContato;
    private readonly IMapper mapper;

    public CompromissoController(
        ServicoCompromisso servicoCompromisso,
        ServicoContato servicoContato,
        IMapper mapper)
    {
        this.servicoCompromisso = servicoCompromisso;
        this.servicoContato = servicoContato;
        this.mapper = mapper;
    }

    public IActionResult Index()
    {
        var registros = servicoCompromisso.SelecionarTodos();

        return View(registros);
    }

    [HttpGet]
    public IActionResult Cadastrar()
    {
        var viewModel = new CadastrarCompromissoViewModel();

        CarregarContatos(viewModel);

        return View(viewModel);
    }

    [HttpPost]
    public IActionResult Cadastrar(
        CadastrarCompromissoViewModel viewModel)
    {
        if (!ModelState.IsValid)
        {
            CarregarContatos(viewModel);

            return View(viewModel);
        }

        var dto =
            mapper.Map<InserirCompromissoDto>(viewModel);

        var resultado =
            servicoCompromisso.Cadastrar(dto);

        if (resultado.IsFailed)
        {
            foreach (var erro in resultado.Errors)
                ModelState.AddModelError(
                    string.Empty,
                    erro.Message);

            CarregarContatos(viewModel);

            return View(viewModel);
        }

        return RedirectToAction(nameof(Index));
    }

    [HttpGet]
    public IActionResult Editar(Guid id)
    {
        var compromisso =
            servicoCompromisso.SelecionarPorId(id);

        if (compromisso is null)
            return NotFound();

        var viewModel =
            mapper.Map<EditarCompromissoViewModel>(compromisso);

        CarregarContatos(viewModel);

        return View(viewModel);
    }

    [HttpPost]
    public IActionResult Editar(
        EditarCompromissoViewModel viewModel)
    {
        if (!ModelState.IsValid)
        {
            CarregarContatos(viewModel);

            return View(viewModel);
        }

        var dto =
            mapper.Map<EditarCompromissoDto>(viewModel);

        var resultado =
            servicoCompromisso.Editar(dto);

        if (resultado.IsFailed)
        {
            foreach (var erro in resultado.Errors)
                ModelState.AddModelError(
                    string.Empty,
                    erro.Message);

            CarregarContatos(viewModel);

            return View(viewModel);
        }

        return RedirectToAction(nameof(Index));
    }

    [HttpGet]
    public IActionResult Visualizar(Guid id)
    {
        var compromisso =
            servicoCompromisso.SelecionarPorId(id);

        if (compromisso is null)
            return NotFound();

        var viewModel =
            mapper.Map<VisualizarCompromissoViewModel>(compromisso);

        return View(viewModel);
    }

    [HttpGet]
    public IActionResult Excluir(Guid id)
    {
        var compromisso =
            servicoCompromisso.SelecionarPorId(id);

        if (compromisso is null)
            return NotFound();

        var viewModel =
            mapper.Map<ExcluirCompromissoViewModel>(compromisso);

        return View(viewModel);
    }

    [HttpPost]
    public IActionResult Excluir(
        ExcluirCompromissoViewModel viewModel)
    {
        var resultado =
            servicoCompromisso.Excluir(viewModel.Id);

        if (resultado.IsFailed)
        {
            TempData["Erro"] =
                resultado.Errors.First().Message;
        }

        return RedirectToAction(nameof(Index));
    }

    private void CarregarContatos(
        CadastrarCompromissoViewModel viewModel)
    {
        var contatos =
            servicoContato.SelecionarTodos();

        viewModel.ContatosDisponiveis =
            contatos.Select(c =>
                new SelectListItem(
                    c.Nome,
                    c.Id.ToString()))
            .ToList();
    }

    private void CarregarContatos(
        EditarCompromissoViewModel viewModel)
    {
        var contatos =
            servicoContato.SelecionarTodos();

        viewModel.ContatosDisponiveis =
            contatos.Select(c =>
                new SelectListItem(
                    c.Nome,
                    c.Id.ToString()))
            .ToList();
    }
}
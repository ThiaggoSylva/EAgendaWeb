using AutoMapper;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

using EAgendaWeb.WebApp.ModuloCategoria.Aplicacao;
using EAgendaWeb.WebApp.ModuloDespesa.Aplicacao;
using EAgendaWeb.WebApp.ModuloDespesa.Apresentacao.Models;

namespace EAgendaWeb.WebApp.ModuloDespesa.Apresentacao.Controllers;

public class DespesaController : Controller
{
    private readonly ServicoDespesa servicoDespesa;
    private readonly ServicoCategoria servicoCategoria;
    private readonly IMapper mapper;

    public DespesaController(
        ServicoDespesa servicoDespesa,
        ServicoCategoria servicoCategoria,
        IMapper mapper)
    {
        this.servicoDespesa = servicoDespesa;
        this.servicoCategoria = servicoCategoria;
        this.mapper = mapper;
    }

    public IActionResult Index()
    {
        var despesas = servicoDespesa.SelecionarTodos();

        return View(despesas);
    }

    [HttpGet]
    public IActionResult Cadastrar()
    {
        var viewModel = new CadastrarDespesaViewModel();

        CarregarCategorias(viewModel);

        return View(viewModel);
    }

    [HttpPost]
    public IActionResult Cadastrar(
        CadastrarDespesaViewModel viewModel)
    {
        if (!ModelState.IsValid)
        {
            CarregarCategorias(viewModel);

            return View(viewModel);
        }

        var dto =
            mapper.Map<InserirDespesaDto>(viewModel);

        var resultado =
            servicoDespesa.Cadastrar(dto);

        if (resultado.IsFailed)
        {
            foreach (var erro in resultado.Errors)
                ModelState.AddModelError(
                    string.Empty,
                    erro.Message);

            CarregarCategorias(viewModel);

            return View(viewModel);
        }

        return RedirectToAction(nameof(Index));
    }

    [HttpGet]
    public IActionResult Editar(Guid id)
    {
        var despesa =
            servicoDespesa.SelecionarPorId(id);

        if (despesa is null)
            return NotFound();

        var viewModel =
            mapper.Map<EditarDespesaViewModel>(despesa);

        CarregarCategorias(viewModel);

        return View(viewModel);
    }

    [HttpPost]
    public IActionResult Editar(
        EditarDespesaViewModel viewModel)
    {
        if (!ModelState.IsValid)
        {
            CarregarCategorias(viewModel);

            return View(viewModel);
        }

        var dto =
            mapper.Map<EditarDespesaDto>(viewModel);

        var resultado =
            servicoDespesa.Editar(dto);

        if (resultado.IsFailed)
        {
            foreach (var erro in resultado.Errors)
                ModelState.AddModelError(
                    string.Empty,
                    erro.Message);

            CarregarCategorias(viewModel);

            return View(viewModel);
        }

        return RedirectToAction(nameof(Index));
    }

    [HttpGet]
    public IActionResult Visualizar(Guid id)
    {
        var despesa =
            servicoDespesa.SelecionarPorId(id);

        if (despesa is null)
            return NotFound();

        var viewModel =
            mapper.Map<VisualizarDespesaViewModel>(despesa);

        return View(viewModel);
    }

    [HttpGet]
    public IActionResult Excluir(Guid id)
    {
        var despesa =
            servicoDespesa.SelecionarPorId(id);

        if (despesa is null)
            return NotFound();

        var viewModel =
            mapper.Map<ExcluirDespesaViewModel>(despesa);

        return View(viewModel);
    }

    [HttpPost]
    public IActionResult Excluir(
        ExcluirDespesaViewModel viewModel)
    {
        servicoDespesa.Excluir(viewModel.Id);

        return RedirectToAction(nameof(Index));
    }

    private void CarregarCategorias(
        CadastrarDespesaViewModel viewModel)
    {
        var categorias =
            servicoCategoria.SelecionarTodos();

        viewModel.CategoriasDisponiveis =
            new MultiSelectList(
                categorias,
                "Id",
                "Titulo");
    }

    private void CarregarCategorias(
        EditarDespesaViewModel viewModel)
    {
        var categorias =
            servicoCategoria.SelecionarTodos();

        viewModel.CategoriasDisponiveis =
            new MultiSelectList(
                categorias,
                "Id",
                "Titulo",
                viewModel.CategoriasIds);
    }
}
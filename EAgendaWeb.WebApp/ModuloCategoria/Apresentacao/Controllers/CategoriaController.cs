using AutoMapper;
using Microsoft.AspNetCore.Mvc;

using EAgendaWeb.WebApp.ModuloCategoria.Aplicacao;
using EAgendaWeb.WebApp.ModuloCategoria.Apresentacao.Models;

namespace EAgendaWeb.WebApp.ModuloCategoria.Apresentacao.Controllers;

public class CategoriaController : Controller
{
    private readonly ServicoCategoria servicoCategoria;
    private readonly IMapper mapper;

    public CategoriaController(
        ServicoCategoria servicoCategoria,
        IMapper mapper)
    {
        this.servicoCategoria = servicoCategoria;
        this.mapper = mapper;
    }

    public IActionResult Index()
    {
        var categorias =
            servicoCategoria.SelecionarTodos();

        return View(categorias);
    }

    [HttpGet]
    public IActionResult Cadastrar()
    {
        return View();
    }

    [HttpPost]
    public IActionResult Cadastrar(
        CadastrarCategoriaViewModel viewModel)
    {
        if (!ModelState.IsValid)
            return View(viewModel);

        var dto =
            mapper.Map<InserirCategoriaDto>(viewModel);

        var resultado =
            servicoCategoria.Cadastrar(dto);

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
        var categoria =
            servicoCategoria.SelecionarPorId(id);

        if (categoria is null)
            return NotFound();

        var viewModel =
            mapper.Map<EditarCategoriaViewModel>(categoria);

        return View(viewModel);
    }

    [HttpPost]
    public IActionResult Editar(
        EditarCategoriaViewModel viewModel)
    {
        if (!ModelState.IsValid)
            return View(viewModel);

        var dto =
            mapper.Map<EditarCategoriaDto>(viewModel);

        var resultado =
            servicoCategoria.Editar(dto);

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
        var categoria =
            servicoCategoria.SelecionarPorId(id);

        if (categoria is null)
            return NotFound();

        var viewModel =
            mapper.Map<VisualizarCategoriaViewModel>(categoria);

        return View(viewModel);
    }

    [HttpGet]
    public IActionResult Excluir(Guid id)
    {
        var categoria =
            servicoCategoria.SelecionarPorId(id);

        if (categoria is null)
            return NotFound();

        var viewModel =
            mapper.Map<ExcluirCategoriaViewModel>(categoria);

        return View(viewModel);
    }

    [HttpPost]
    public IActionResult Excluir(
        ExcluirCategoriaViewModel viewModel)
    {
        var resultado =
            servicoCategoria.Excluir(viewModel.Id);

        if (resultado.IsFailed)
        {
            TempData["Erro"] =
                resultado.Errors.First().Message;
        }

        return RedirectToAction(nameof(Index));
    }
}
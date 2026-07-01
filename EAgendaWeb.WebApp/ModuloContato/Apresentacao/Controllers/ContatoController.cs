using AutoMapper;
using Microsoft.AspNetCore.Mvc;

using EAgendaWeb.WebApp.ModuloContato.Aplicacao;
using EAgendaWeb.WebApp.ModuloContato.Apresentacao.Models;

namespace EAgendaWeb.WebApp.ModuloContato.Apresentacao.Controllers;

public class ContatoController : Controller
{
    private readonly ServicoContato servicoContato;
    private readonly IMapper mapper;

    public ContatoController(
        ServicoContato servicoContato,
        IMapper mapper)
    {
        this.servicoContato = servicoContato;
        this.mapper = mapper;
    }

    public IActionResult Index()
    {
    try
    {
        var contatos = servicoContato.SelecionarTodos();

        return Content($"Encontrados {contatos.Count()} contatos");
    }
    catch (Exception ex)
    {
        return Content(ex.ToString());
    }
    }

    [HttpGet]
    public IActionResult Cadastrar()
    {
        return View();
    }

    [HttpPost]
    public IActionResult Cadastrar(
        CadastrarContatoViewModel viewModel)
    {
        if (!ModelState.IsValid)
            return View(viewModel);

        var dto =
            mapper.Map<InserirContatoDto>(viewModel);

        var resultado =
            servicoContato.Cadastrar(dto);

        if (resultado.IsFailed)
        {
            foreach (var erro in resultado.Errors)
                ModelState.AddModelError(
                    string.Empty,
                    erro.Message);

            return View(viewModel);
        }

        TempData["Sucesso"] =
            "Contato cadastrado com sucesso.";

        return RedirectToAction(nameof(Index));
    }

    [HttpGet]
    public IActionResult Editar(Guid id)
    {
        var contato =
            servicoContato.SelecionarPorId(id);

        if (contato is null)
            return NotFound();

        var viewModel =
            mapper.Map<EditarContatoViewModel>(contato);

        return View(viewModel);
    }

    [HttpPost]
    public IActionResult Editar(
        EditarContatoViewModel viewModel)
    {
        if (!ModelState.IsValid)
            return View(viewModel);

        var dto =
            mapper.Map<EditarContatoDto>(viewModel);

        var resultado =
            servicoContato.Editar(dto);

        if (resultado.IsFailed)
        {
            foreach (var erro in resultado.Errors)
                ModelState.AddModelError(
                    string.Empty,
                    erro.Message);

            return View(viewModel);
        }

        TempData["Sucesso"] =
            "Contato atualizado com sucesso.";

        return RedirectToAction(nameof(Index));
    }
    [HttpGet]
    public IActionResult Visualizar(Guid id)

    {
        var contato = servicoContato.SelecionarPorId(id);

        if (contato is null)
            return NotFound();

        var viewModel =
        mapper.Map<VisualizarContatoViewModel>(contato);

        return View(viewModel);
    }

    [HttpGet]
    public IActionResult Excluir(Guid id)
    {
        var contato = servicoContato.SelecionarPorId(id);

        if (contato is null)
            return NotFound();

        var viewModel =
            mapper.Map<ExcluirContatoViewModel>(contato);

        return View(viewModel);
    }
    
    [HttpPost]
    public IActionResult Excluir(ExcluirContatoViewModel viewModel)
    {
    var resultado =
        servicoContato.Excluir(viewModel.Id);

    if (resultado.IsFailed)
    {
        TempData["Erro"] =
            resultado.Errors.First().Message;

        return RedirectToAction(nameof(Index));
    }

    TempData["Sucesso"] =
        "Contato excluído com sucesso.";

    return RedirectToAction(nameof(Index));
    }
}
using AutoMapper;

using EAgendaWeb.WebApp.ModuloCategoria.Aplicacao;
using EAgendaWeb.WebApp.ModuloCategoria.Apresentacao.Models;

namespace EAgendaWeb.WebApp.ModuloCategoria.Apresentacao;

public class CategoriaProfile : Profile
{
    public CategoriaProfile()
    {
        CreateMap<CadastrarCategoriaViewModel,
            InserirCategoriaDto>();

        CreateMap<EditarCategoriaViewModel,
            EditarCategoriaDto>();

        CreateMap<DetalhesCategoriaDto,
            EditarCategoriaViewModel>();

        CreateMap<DetalhesCategoriaDto,
            ExcluirCategoriaViewModel>();

        CreateMap<DetalhesCategoriaDto,
            VisualizarCategoriaViewModel>();
    }
}
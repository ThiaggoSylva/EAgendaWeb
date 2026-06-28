using AutoMapper;

using EAgendaWeb.WebApp.ModuloCompromisso.Aplicacao;
using EAgendaWeb.WebApp.ModuloCompromisso.Apresentacao.Models;

namespace EAgendaWeb.WebApp.ModuloCompromisso.Apresentacao;

public class CompromissoProfile : Profile
{
    public CompromissoProfile()
    {
        CreateMap<CadastrarCompromissoViewModel,
            InserirCompromissoDto>();

        CreateMap<EditarCompromissoViewModel,
            EditarCompromissoDto>();

        CreateMap<DetalhesCompromissoDto,
            EditarCompromissoViewModel>();

        CreateMap<DetalhesCompromissoDto,
            ExcluirCompromissoViewModel>();

        CreateMap<DetalhesCompromissoDto,
            VisualizarCompromissoViewModel>();
    }
}
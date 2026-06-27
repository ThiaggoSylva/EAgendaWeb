using AutoMapper;
using EAgendaWeb.WebApp.ModuloContato.Aplicacao;
using EAgendaWeb.WebApp.ModuloContato.Apresentacao.Models;

namespace EAgendaWeb.WebApp.ModuloContato.Apresentacao;

public class ContatoProfile : Profile
{
    public ContatoProfile()
    {
        CreateMap<CadastrarContatoViewModel, InserirContatoDto>();

        CreateMap<EditarContatoViewModel, EditarContatoDto>();

        CreateMap<DetalhesContatoDto, EditarContatoViewModel>();

        CreateMap<DetalhesContatoDto, ExcluirContatoViewModel>();

        CreateMap<DetalhesContatoDto, VisualizarContatoViewModel>();
    }
}
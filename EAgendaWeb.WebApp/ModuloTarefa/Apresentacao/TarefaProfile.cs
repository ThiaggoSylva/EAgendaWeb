using AutoMapper;

using EAgendaWeb.WebApp.ModuloTarefa.Aplicacao;
using EAgendaWeb.WebApp.ModuloTarefa.Apresentacao.Models;

namespace EAgendaWeb.WebApp.ModuloTarefa.Apresentacao;

public class TarefaProfile : Profile
{
    public TarefaProfile()
    {
        CreateMap<CadastrarTarefaViewModel,
            InserirTarefaDto>();

        CreateMap<EditarTarefaViewModel,
            EditarTarefaDto>();

        CreateMap<DetalhesTarefaDto,
            EditarTarefaViewModel>();

        CreateMap<DetalhesTarefaDto,
            VisualizarTarefaViewModel>();

        CreateMap<DetalhesTarefaDto,
            ExcluirTarefaViewModel>();
    }
}
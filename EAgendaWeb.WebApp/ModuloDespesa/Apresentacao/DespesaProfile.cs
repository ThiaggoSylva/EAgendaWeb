using AutoMapper;

using EAgendaWeb.WebApp.ModuloDespesa.Aplicacao;
using EAgendaWeb.WebApp.ModuloDespesa.Apresentacao.Models;

namespace EAgendaWeb.WebApp.ModuloDespesa.Apresentacao;

public class DespesaProfile : Profile
{
    public DespesaProfile()
    {
        CreateMap<CadastrarDespesaViewModel,
            InserirDespesaDto>();

        CreateMap<EditarDespesaViewModel,
            EditarDespesaDto>();

        CreateMap<DetalhesDespesaDto,
            EditarDespesaViewModel>();

        CreateMap<DetalhesDespesaDto,
            ExcluirDespesaViewModel>();

        CreateMap<DetalhesDespesaDto,
            VisualizarDespesaViewModel>();
    }
}
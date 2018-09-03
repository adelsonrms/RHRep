using AutoMapper;
using RH.Domain.Entities;
using RH.ViewModel;

namespace ERP.Presentation.Api
{
    public static class AutoMapperConfig
    {

        public class MappingProfile: AutoMapper.Profile
        {
            /// <summary>
            /// Cria uma classe de perfil de mapeamento entre a classe de dominio e a classe de View Model
            /// </summary>
            public MappingProfile()
            {
                CreateMap<Funcionario, FuncionarioViewModel>()
                    .ForMember(viewModel => viewModel.Matricula, domainMap => domainMap.MapFrom(domain => domain.Matricula))
                    .ForMember(viewModel => viewModel.CPF, domainMap => domainMap.MapFrom(domain => domain.CPF))
                    .ForMember(viewModel => viewModel.PIS, domainMap => domainMap.MapFrom(domain => domain.PIS))
                    .ForMember(viewModel => viewModel.Telefone, domainMap => domainMap.MapFrom(domain => domain.Telefone))
                    .ForMember(viewModel => viewModel.Celular, domainMap => domainMap.MapFrom(domain => domain.Celular))
                    .ForMember(viewModel => viewModel.EmailProfissional, domainMap => domainMap.MapFrom(domain => domain.EmailProfissional))
                    .ForMember(viewModel => viewModel.EmailPessoal, domainMap => domainMap.MapFrom(domain => domain.EmailPessoal))
                    .ForMember(viewModel => viewModel.Endereco, domainMap => domainMap.MapFrom(domain => domain.Endereco))
                    .ForMember(viewModel => viewModel.CEP, domainMap => domainMap.MapFrom(domain => domain.CEP))
                    .ForMember(viewModel => viewModel.Bairro, domainMap => domainMap.MapFrom(domain => domain.Bairro))
                    .ForMember(viewModel => viewModel.Cidade, domainMap => domainMap.MapFrom(domain => domain.Cidade))
                    .ForMember(viewModel => viewModel.Estado, domainMap => domainMap.MapFrom(domain => domain.Estado))
                    .ForMember(viewModel => viewModel.DataNascimento, domainMap => domainMap.MapFrom(domain => domain.DataNascimento))
                    .ForMember(viewModel => viewModel.SexoId, domainMap => domainMap.MapFrom(domain => domain.SexoId))
                    .ForMember(viewModel => viewModel.Idade, domainMap => domainMap.MapFrom(domain => domain.Idade))
                    .ForMember(viewModel => viewModel.Ativo, domainMap => domainMap.MapFrom(domain => domain.Ativo))
                    .ReverseMap();
            }
        }

        public static void RegisterAutoMappings()
        {
            //Aplica o mapeamento baseado em uma classe de perfil.
            //Caso não haja a necessidade de mapear campos especificos, esse metodo ja é suficiente
            Mapper.Initialize(mp => mp.AddProfile<MappingProfile>());
        }
    }
}

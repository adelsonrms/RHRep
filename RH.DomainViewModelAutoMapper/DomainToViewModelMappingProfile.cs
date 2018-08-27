using AutoMapper;
using RH.Domain.Entities;
using RH.ViewModel;

namespace RH.DomainViewModelAutoMapper
{
    public class DomainToViewModelMappingProfile:Profile
    {
        public override string ProfileName
        {
            get { return "DomainToViewModelMappings"; }
        }

        protected void Configure()
        {
            
            
        }

    }
}

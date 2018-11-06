using AutoMapper;
using Onion.Model;
using Onion.RazorUI.ViewModels;

namespace OnionApp.Mappings
{
    public class DomainToViewModelMappingProfile : Profile
    {
        public override string ProfileName => "DomainToViewModelMappings";

        public DomainToViewModelMappingProfile()
        {
            CreateMap<Person, PersonViewModel>();
            CreateMap<Project, ProjectViewModel>();
            CreateMap<Entry, EntryViewModel>();
        }
    }
}

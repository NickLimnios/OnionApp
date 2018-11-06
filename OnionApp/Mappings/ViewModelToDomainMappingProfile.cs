using AutoMapper;
using Onion.Model;
using Onion.RazorUI.ViewModels;

namespace OnionApp.Mappings
{
    public class ViewModelToDomainMappingProfile : Profile
    {
        public override string ProfileName => "ViewModelToDomainMappings";

        public ViewModelToDomainMappingProfile()
        {
            CreateMap<PersonViewModel, Person>();
            CreateMap<ProjectViewModel, Project>();
            CreateMap<EntryViewModel, Entry>()
                .ForMember(m => m.id, map => map.MapFrom(vm => vm.id));
        }
    }
}

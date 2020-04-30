using AutoMapper;
using WebMotorsProject.App.Models;
using WebMotorsProject.Models.DML;

namespace WebMotorsProject.App.AutoMapper
{
    public class ModelsToViewModelMappingProfile : Profile
    {
        public override string ProfileName
        {
            get { return "ViewModelToModelsMappings"; }
        }

        protected override void Configure()
        {
            Mapper.CreateMap<AnuncioViewModel, Anuncio>();
        }
    }
}
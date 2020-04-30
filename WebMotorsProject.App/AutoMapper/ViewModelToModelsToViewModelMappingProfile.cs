using AutoMapper;
using WebMotorsProject.App.Models;
using WebMotorsProject.Models.DML;

namespace WebMotorsProject.App.AutoMapper
{
    public class ViewModelToModelsToViewModelMappingProfile : Profile
    {
        public override string ProfileName
        {
            get { return "ModelsToViewModelMappings"; }
        }

        protected override void Configure()
        {
            Mapper.CreateMap<Anuncio, AnuncioViewModel>();
        }
    }
}
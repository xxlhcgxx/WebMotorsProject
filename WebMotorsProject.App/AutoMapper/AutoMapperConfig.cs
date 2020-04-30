using AutoMapper;

namespace WebMotorsProject.App.AutoMapper
{
    public class AutoMapperConfig
    {
        public static void RegisterMappings()
        {
            Mapper.Initialize(x =>
            {
                x.AddProfile<ModelsToViewModelMappingProfile>();
                x.AddProfile<ViewModelToModelsToViewModelMappingProfile>();
            });
        }
    }
}
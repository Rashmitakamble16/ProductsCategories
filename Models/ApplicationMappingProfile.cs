using AutoMapper;

namespace ASP.NET_MVC_Project.Models
{
    public class ApplicationMappingProfile : Profile
    {
        public ApplicationMappingProfile()
        {
            CreateMap<CreateProductViewModel, Product>();
            // Add more mappings as needed
        }
    }
}

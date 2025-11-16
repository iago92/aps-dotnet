using Mapster;
using University.Application.ViewModels;
using University.Domain.Entities;

namespace University.Application.Mappings
{
    /// <summary>
    /// Configuração de mapeamento entre entidades e ViewModels usando Mapster
    /// </summary>
    public static class MappingConfig
    {
        public static void RegisterMappings()
        {
            // Category
            TypeAdapterConfig<Category, CategoryViewModel>.NewConfig()
                .PreserveReference(true)
                .IgnoreNullValues(true);

            TypeAdapterConfig<CategoryViewModel, Category>.NewConfig()
                .PreserveReference(true)
                .IgnoreNullValues(true);

            // Product
            TypeAdapterConfig<Product, ProductViewModel>.NewConfig()
                .PreserveReference(true)
                .IgnoreNullValues(true)
                .Map(dest => dest.CategoryName, src => src.Category != null ? src.Category.Name : null);

            TypeAdapterConfig<ProductViewModel, Product>.NewConfig()
                .PreserveReference(true)
                .IgnoreNullValues(true);
        }
    }
}

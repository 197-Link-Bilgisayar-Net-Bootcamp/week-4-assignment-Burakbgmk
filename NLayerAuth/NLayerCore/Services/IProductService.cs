using NLayerCore.Models;
using NLayerCore.Dtos;
using SharedLibrary.Response;

namespace NLayerCore.Services
{
    public interface IProductService
    {
        Task<Response<List<ProductDto>>> GetAll();
        Task<Response<ProductDto>> GetById(int id);
        Task<Response<string>> Create(ProductDto productDto);
        Task<Response<string>> CreateWithFeatures(ProductDto productDto, ProductFeatureDto productFeatureDto);
        Task<Response<string>> Update(int id, ProductDto productDto);
        Response<string> Delete(int id);
        Task<Response<List<ProductFullModel>>> GetFullModel();
        Task<Response<List<ProductFullModel>>> GetFullModelWithFunction();
        //Task<int> CreateProduct(ProductDto productDto);
        //Task CreateFeature(int id, ProductFeatureDto productFeatureDto);
    }
}
using NLayerCore.Dtos;
using SharedLibrary.Response;

namespace NLayerCore.Services
{
    public interface IProductFeatureService
    {
        Task<Response<ProductFeatureDto>> GetById(int id);
        Task<Response<string>> Create(int id, ProductFeatureDto productFeatureDto);
        Task<Response<string>> Update(int id, ProductFeatureDto productFeatureDto);
        Response<string> Delete(int id);
    }
}
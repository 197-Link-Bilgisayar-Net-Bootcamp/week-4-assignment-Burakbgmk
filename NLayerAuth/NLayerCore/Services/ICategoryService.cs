using NLayerCore.Dtos;
using SharedLibrary.Response;

namespace NLayerCore.Services
{
    public interface ICategoryService
    {
        Task<Response<List<CategoryDto>>> GetAll();
        Task<Response<CategoryDto>> GetById(int id);
        Task<Response<string>> Create(CategoryDto categoryDto);
        Task<Response<string>> Update(int id, CategoryDto categoryDto);
        Response<string> Delete(int id);
    }
}
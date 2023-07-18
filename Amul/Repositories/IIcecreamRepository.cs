using Amul.Models.Domain;
using Amul.Models.DTO;

namespace Amul.Repositories
{
    public interface IIcecreamRepository
    {
        Task<List<Icecream>> GetAllIcecreamsAsync(string? filterOn= null, string? filterQuery= null, string? sortBy =null, bool isAscending = true, int pageNumber = 1, int pageSize = 100);
        Task<Icecream?> GetIcecreamAsync(Guid id);
        Task<Icecream?> PostIcecreamAsync(Icecream icecream);
        Task<Icecream?> UpdateIcecreamAsync(Guid id, Icecream icecream);
        Task<Icecream?> DeleteIcecreamAsync(Guid id);
    }
}

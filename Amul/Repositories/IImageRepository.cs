using Amul.Models.Domain;

namespace Amul.Repositories
{
    public interface IImageRepository
    {
        Task<Image> Upload(Image image);
    }
}

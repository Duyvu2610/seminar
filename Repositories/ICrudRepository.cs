using seminar.Data;

namespace seminar.Repositories
{
    public interface ICrudRepository
    {
        Task<List<object>> GetAllAsync();
        Task<object> GetByIdAsync(int id);
        Task<object> CreateAsync(object item);
        Task<object> UpdateAsync(int id, object item);
        Task<bool> DeleteAsync(int id);
    }
}

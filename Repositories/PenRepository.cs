using Microsoft.EntityFrameworkCore;
using seminar.Data;

namespace seminar.Repositories
{
    public class PenRepository : ICrudRepository
    {
        private readonly BookStoreContext _context;
        public PenRepository(BookStoreContext context)
        {
            _context = context;
        }

        public async Task<object> CreateAsync(object item)
        {
            if (item is Pen pen)
            {
                _context.Pens.Add(pen);
                await _context.SaveChangesAsync();
                return pen;
            }
            throw new ArgumentException("Invalid type for item.");
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var pen = _context.Pens!.SingleOrDefault(x => x.Id == id);
            if (pen != null)
            {
                _context.Pens!.Remove(pen);
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }


        public async Task<List<object>> GetAllAsync()
        {
            var books = await _context.Pens!.Cast<object>().ToListAsync();
            return books;
        }

        public async Task<object> GetByIdAsync(int id)
        {
            var book = await _context.Pens!.SingleOrDefaultAsync(x => x.Id == id);
            return (object)book;
        }


        public async Task<object> UpdateAsync(int id, object item)
        {
            if (item is Pen pen)
            {
                var existingBook = _context.Books!.FirstOrDefault(b => b.Id == id);
                if (existingBook != null)
                {
                    _context.Pens!.Update(pen);
                    await _context.SaveChangesAsync();
                    return pen;
                }
            }

            throw new ArgumentException("Invalid type for item or book not found.");
        }
    }
}

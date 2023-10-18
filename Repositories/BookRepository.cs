using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using seminar.Data;
using static System.Reflection.Metadata.BlobBuilder;

namespace seminar.Repositories
{
    public class BookRepository : ICrudRepository
    {
        private readonly BookStoreContext _context ;
        public BookRepository(BookStoreContext context) { 
            _context = context;
        }

        public async Task<object> CreateAsync(object item)
        {
            if (item is Book book)
            {
                _context.Books.Add(book);
                await _context.SaveChangesAsync();
                return book;
            }
            throw new ArgumentException("Invalid type for item.");
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var book = _context.Books!.SingleOrDefault(x => x.Id == id);
            if (book != null)
            {
                _context.Books!.Remove(book);
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }


        public async Task<List<object>> GetAllAsync()
        {
            var books = await _context.Books!.Cast<object>().ToListAsync();
            return books;
        }

        public async Task<object> GetByIdAsync(int id)
        {
            var book = await _context.Books!.SingleOrDefaultAsync(x => x.Id == id);
            return (object) book;
        }


        public async Task<object> UpdateAsync(int id, object item)
        {
            if (item is Book book)
            {
                var existingBook = _context.Books!.FirstOrDefault(b => b.Id == id);
                if (existingBook != null)
                {
                    _context.Books!.Update(book);
                    await _context.SaveChangesAsync();
                    return book;
                }
            }

            throw new ArgumentException("Invalid type for item or book not found.");
        }
    }
}

using System.Data.Entity;
using System.Threading.Tasks;
using ProductManagerApp.Services;

namespace ProductManagerApp.Data
{
    public class ProductManagerUnitOfWork : IUnitOfWork
    {
        private readonly DbContext _context;
        private bool _cancelSaving;
        public ProductManagerUnitOfWork(DbContext context)
        {
            _context = context;
            _cancelSaving = false;
        }
        public void SaveChanges()
        {
            if (_cancelSaving)
                return;

            _context.SaveChanges();
        }

        public async Task SaveChangesAsync()
        {
            if (_cancelSaving)
                return;

            await _context.SaveChangesAsync();
        }

        public void CancelSaving()
        {
            _cancelSaving = true;
        }
    }
}

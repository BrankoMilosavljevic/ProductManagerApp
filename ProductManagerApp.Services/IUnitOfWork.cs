using System.Threading.Tasks;

namespace ProductManagerApp.Services
{
    public interface IUnitOfWork
    {
        void SaveChanges();
        Task SaveChangesAsync();
        void CancelSaving();
    }
}

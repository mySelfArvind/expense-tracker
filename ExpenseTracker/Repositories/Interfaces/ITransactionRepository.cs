using ExpenseTracker.Models;

namespace ExpenseTracker.Repositories.Interfaces
{
    public interface ITransactionRepository
    {
        Task<long> AddTransaction(TransactionRequest request);
    }
}

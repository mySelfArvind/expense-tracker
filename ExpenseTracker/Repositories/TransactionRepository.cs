using Dapper;
using ExpenseTracker.Helpers;
using ExpenseTracker.Models;
using ExpenseTracker.Repositories.Interfaces;
using Npgsql;
using System.Data;

namespace ExpenseTracker.Repositories
{
    public class TransactionRepository : ITransactionRepository
    {
        private readonly IConfiguration _config;

        private readonly IDbConnection _connection;
        public TransactionRepository(IDbConnection connection)
        {
            _connection = connection;
        }
        public async Task<long> AddTransaction(TransactionRequest request)
        {
            var transactionId = (await _connection?.QueryAsync <long>("""SELECT sp_InsertTransaction(@amount, @merchant);""", new { request.amount, request.merchant })!).First();

            if (transactionId == 0)
            {
                Console.WriteLine("transaction not inserted into db");
            }
            return transactionId;
        }
    }
}

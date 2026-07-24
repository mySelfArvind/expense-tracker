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
        public TransactionRepository(IConfiguration config)
        {
            _config = config;
        }
        public async Task<long> AddTransaction(TransactionRequest request)
        {
            SqlHelper helper = new SqlHelper(_config);
            var connection = helper.GetConnection();
            if (connection == null)
            {
                Console.WriteLine("Not connected to the Server");
            }
            if (connection?.State == System.Data.ConnectionState.Closed)
            {
                connection.Open();
            }
            //int transactionId = await connection?.QuerySingleAsync<int>("sp_InsertTransaction",request,commandType: CommandType.StoredProcedure)!;

            //var transactionId await connection?.ExecuteScalar<long>("""SELECT sp_InsertTransaction(@amount, @merchant);""", new{request.amount,request.merchant});

            var transactionId = (await connection?.QueryAsync <long>("""SELECT sp_InsertTransaction(@amount, @merchant);""", new { request.amount, request.merchant })!).First();

            if (transactionId == 0)
            {
                Console.WriteLine("transaction not inserted into db");
            }
            return transactionId;
        }
    }
}

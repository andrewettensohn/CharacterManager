using CharacterManager.DAC.Data.Contracts;
using CharacterManager.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CharacterManager.DAC.Data.Repositories
{
    public class TransactionRepository : ITransactionRepository
    {
        private readonly ApplicationDbContext _context;

        public TransactionRepository(IDbContextFactory<ApplicationDbContext> dbFactory)
        {
            _context = dbFactory.CreateDbContext();
        }

        public async Task AddNewTransaction(string sourceRepo, string methodName, Guid SourceId)
        {
            Transaction transaction = new Transaction
            {
                SourceRepository = sourceRepo,
                SourceMethod = methodName,
                SourceId = SourceId,
                DateTime = DateTime.Now
            };
            await _context.Transactions.AddAsync(transaction);
        }

        public async Task AddTransaction(Transaction transaction)
        {
            await _context.AddAsync(transaction);
            await _context.SaveChangesAsync();
        }

        public async Task AddTransactionList(List<Transaction> transactions)
        {
            await _context.AddRangeAsync(transactions);
            await _context.SaveChangesAsync();
        }
    }
}

using CharacterManager.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CharacterManager.DAC.Data.Contracts
{
    public interface ITransactionRepository
    {
        Task AddNewTransaction(string sourceRepo, string methodName, Guid SourceId);

        Task AddTransaction(Transaction transaction);

        Task AddTransactionList(List<Transaction> transactions);
    }
}

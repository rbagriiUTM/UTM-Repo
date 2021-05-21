using MolDavaBanking.Persistance;
using MolDavaBanking.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MolDavaBanking.Repositories.Repositories
{
    public class TransactionRepository : ITransactionRepository
    {
        MolDavaBankingContext dbContext = MolDavaBankingContext.GetInstance();

        public List<Transaction> GetTransactions()
        {
            var transactions = dbContext.Transactions.ToList();

            return transactions;
        }

        public Transaction GetTransactionByTransactionNumber(string transactionNumber)
        {
            var transaction = dbContext.Transactions.Single(t => t.TransactionNumber == transactionNumber);

            return transaction;
        }

        public List<Transaction> FilterDataByCurrency(List<int> currenciesID)
        {
            var filteredByCurrencies = dbContext.Transactions.Where(currency => currenciesID.Contains((int)currency.Currency)).ToList();

            return filteredByCurrencies;
        }

        public List<int> GetAllCurrenciesAvailable()
        {
            var allCurriencies = dbContext.Transactions.Select(transactions => (int)transactions.Currency).ToList();

            return allCurriencies;
        }

        public List<Transaction> FilterDataByStatuses(List<Transaction> dataFilteredByCurrency, List<string> statuses)
        {
            var filteredByStatuses = dataFilteredByCurrency.Where(stat => statuses.Contains(stat.Status.ToString())).ToList();

            return filteredByStatuses;
        }

        public List<string> GetAllStatusesAvailable()
        {
            var allStatuses = dbContext.Transactions.Select(stat => stat.Status.ToString()).Distinct().ToList();

            return allStatuses;
        }

        public List<Transaction> FilteredDataByAmount(List<Transaction> dataFilteredByStatus, double amountSelected)
        {
                var filteredByAmount = dataFilteredByStatus.Where(t => t.Amount <= amountSelected).ToList();

            return filteredByAmount;
        }

        public List<Transaction> FilteredDataByDate(List<Transaction> dataFilteredByAmount, DateTime dateFrom, DateTime dateTo)
        {
            var filteredByDateTime = dataFilteredByAmount.Where(date => date.CreatedDate >= dateFrom && date.CreatedDate <= dateTo).ToList();

            return filteredByDateTime;
        }

        public List<Transaction> FilteredDataDateTo(List<Transaction> dataFilteredByAmount, DateTime dateTo)
        {
            var filteredByDateTo = dataFilteredByAmount.Where(date => date.CreatedDate <= dateTo).ToList();

            return filteredByDateTo;
        }

        public List<Transaction> FilteredDataDateFrom(List<Transaction> dataFilteredByAmount, DateTime dateFrom)
        {
            var filteredByDateFrom = dataFilteredByAmount.Where(date => date.CreatedDate >= dateFrom).ToList();

            return filteredByDateFrom;
        }

        public List<Transaction> SortFieldByAscending(List<Transaction> filteredData, string fieldToSort)
        {
            var selecColumn = dbContext.Transactions.FirstOrDefault().GetType().GetProperty(fieldToSort);

            var sortedField = filteredData.OrderBy(field => selecColumn.GetValue(field)).ToList();

            return sortedField;
        }

        public List<Transaction> SortFieldByDescending(List<Transaction> filteredData, string fieldToSort)
        {
            var selecColumn = dbContext.Transactions.FirstOrDefault().GetType().GetProperty(fieldToSort);

            var sortedField = filteredData.OrderByDescending(field => selecColumn.GetValue(field)).ToList();

            return sortedField;
        }

        public List<Transaction> PaginateSortedTransactions(List<Transaction> totalFilteredTransactions, int page, int pageSize)
        {
            var filteredDataOnPages = totalFilteredTransactions.Skip((page - 1) * pageSize).Take(pageSize).ToList();

            return filteredDataOnPages;
        }

        public List<Transaction> GetInfoAboutTransaction(string info)
        {
            var totalInfoAboutTransaction = dbContext.Transactions.Where(transactionNumb => transactionNumb.TransactionNumber == info).ToList();

            return totalInfoAboutTransaction;
        }
        
        public double GetMaxAmount()
        {
            var maxAmount = dbContext.Transactions.Select(a => a.Amount).Max();

            return maxAmount;
        }

        public double GetMinAmount()
        {
            var minAmount = dbContext.Transactions.Select(a => a.Amount).Min();

            return minAmount;
        }

        public List<Transaction> GetTransactionByNumber (string transactionNumber)
        {
            var transactionByTransactionNumber = dbContext.Transactions.Where(t => t.TransactionNumber == transactionNumber).ToList();

            return transactionByTransactionNumber;
        }

        public List<Transaction> GetTransactionForUser(string userLogedEmail)
        {
            var usersTransactions = dbContext.Transactions.Where(e => e.User.Email == userLogedEmail).ToList();

            return usersTransactions;
        }

        public void UpdateTransactionStatus (Transaction transaction, Statuses status)
        {
            dbContext.Transactions.Attach(transaction);
            transaction.Status = status;
            dbContext.SaveChanges();
        }

        public List<double> GetSumByCurrency(Currencies currency , string userLogedEmail)
        {
            var selectAmountBycurrency = dbContext.Transactions.Where(t => t.Currency == currency && t.User.Email == userLogedEmail).Select(a => a.Amount).ToList();

            return selectAmountBycurrency;
        }

        public List<Transaction> GetTransactionsByUserEmail(string userEmail)
        {
            var transactions = dbContext.Transactions.Where(t => t.User.Email == userEmail).ToList();

            return transactions;
        }
    }
}

using MolDavaBanking.Persistance;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MolDavaBanking.Repositories.Interfaces
{
    public interface ITransactionRepository
    {
        List<Transaction> GetTransactions();
        Transaction GetTransactionByTransactionNumber(string transactionNumber);
        List<Transaction> FilterDataByCurrency(List<int> currenciesID);
        List<Transaction> FilterDataByStatuses(List<Transaction> dataFilteredByCurrency, List<string> statuses);
        List<Transaction> FilteredDataByAmount(List<Transaction> dataFilteredByStatus, double amountSelected);
        List<Transaction> FilteredDataByDate(List<Transaction> dataFilteredByAmount, DateTime dateFrom, DateTime dateTo);
        List<Transaction> FilteredDataDateTo(List<Transaction> dataFilteredByAmount, DateTime dateTo);
        List<Transaction> FilteredDataDateFrom(List<Transaction> dataFilteredByAmount, DateTime dateFrom);
        List<Transaction> SortFieldByAscending(List<Transaction> filteredData, string fieldToSort);
        List<Transaction> SortFieldByDescending(List<Transaction> filteredData, string fieldToSort);
        List<Transaction> PaginateSortedTransactions(List<Transaction> totalFilteredTransactions, int page, int pageSize);
        List<Transaction> GetInfoAboutTransaction(string info);
        List<int> GetAllCurrenciesAvailable();
        List<string> GetAllStatusesAvailable();
        double GetMaxAmount();
        double GetMinAmount();
        List<Transaction> GetTransactionByNumber(string transactionNumber);
        List<Transaction> GetTransactionForUser(string userLogedEmail);
        void UpdateTransactionStatus(Transaction transaction, Statuses status);
        List<double> GetSumByCurrency(Currencies currency, string userLogedEmail);
        List<Transaction> GetTransactionsByUserEmail(string userEmail);
    }
}

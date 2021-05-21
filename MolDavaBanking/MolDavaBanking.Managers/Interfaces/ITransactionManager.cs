using MolDavaBanking.Domain.TransactionViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MolDavaBanking.Managers.Interfaces
{
    public interface ITransactionManager
    {
        List<TransactionViewModel_GetTransactions> GetTransactions();

        TransactionViewModel_GetTransactions GetTransactionByTransactionNumber(string transactionNumber);

        List<TransactionViewModel_GetTransactions> GetFileteredTransactions(TransactionViewModel_FilterData filterData, string email = null);

        List<TransactionViewModel_GetTransactions> GetSortedTransactions(TransactionViewModel_SortData sortTransactions, TransactionViewModel_FilterData filteredData, string email = null);

        List<TransactionViewModel_GetTransactions> Pagination(TransactionViewModel_SortData sortTransactions, TransactionViewModel_FilterData filteredData, TransactionViewModel_Pagination pagination, string email = null);

        int ReturnTotalPages(TransactionViewModel_SortData sortTransactions, TransactionViewModel_FilterData filteredData, TransactionViewModel_Pagination pagination, string email = null);

        List<TransactionViewModel_GetTransactions> GetMainTransactionsPages(int page);

        List<TransactionViewModel_GetTransactionDetails> GetFullInformationAboutTransaction(string transactionNumber);

        double GetMaxValueOfAmount();

        double GetMinValueOfAmount();

        List<TransactionViewModel_GetTransactions> GetTransactionsByLoggedUser(string email);

        List<TransactionViewModel_GetTransactions> GetMainTransactionsPaged(int page, string email);

        bool UpdateTransactionStatusToNew(string tranNumber, string tranStatus);

        List<string> GetAverageOfTransactions(string userEmail);
        List<TransactionViewModel_GetTransactions> GetTransactionsByUserEmail(string userEmail);
        List<TransactionViewModel_GetTransactions> GetTransactionsByYearAndMonth(List<TransactionViewModel_GetTransactions> transactions, int year, int month);
        List<IGrouping<Statuses, TransactionViewModel_GetTransactions>> GroupTransactionsByStatuses(List<TransactionViewModel_GetTransactions> tranasactions);
    }
}

using AutoMapper;
using MolDavaBanking.Domain.TransactionViewModel;
using MolDavaBanking.Managers.Interfaces;
using MolDavaBanking.Persistance;
using MolDavaBanking.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Statuses = MolDavaBanking.Persistance.Statuses;

namespace MolDavaBanking.Managers.Managers
{
    public class TransactionManager : ITransactionManager
    {
        private ITransactionRepository _iTransactionRepository;
        private IUserRepository _iUserRepository;

        public TransactionManager(ITransactionRepository iTransactionRepository, IUserRepository iUserRepository)
        {
            _iTransactionRepository = iTransactionRepository;
            _iUserRepository = iUserRepository;
        }

        public List<TransactionViewModel_GetTransactions> GetTransactions()
        {
            var transactions = _iTransactionRepository.GetTransactions();

            List<TransactionViewModel_GetTransactions> transactionsDomain = new List<TransactionViewModel_GetTransactions>();

            foreach (var transaction in transactions)
            {
                var transactionDomain = Mapper.Map<TransactionViewModel_GetTransactions>(transaction);
                transactionsDomain.Add(transactionDomain);
            }

            return transactionsDomain;
        }

        public TransactionViewModel_GetTransactions GetTransactionByTransactionNumber(string transactionNumber)
        {
            var transactionPersistance = _iTransactionRepository.GetTransactionByTransactionNumber(transactionNumber);

            var transactionDomain = Mapper.Map<TransactionViewModel_GetTransactions>(transactionPersistance);

            return transactionDomain;
        }

        public List<TransactionViewModel_GetTransactions> GetFileteredTransactions(TransactionViewModel_FilterData filterTransactions, string email)
        {
            int? userId = null;

            var userStatus = _iUserRepository.GetUserByEmail(email).Roles.Select(y => y.Name).FirstOrDefault();

            if (!string.IsNullOrWhiteSpace(email))
            {
                userId = _iUserRepository.GetUserByEmail(email).UserId;
            }

            if (filterTransactions.TransactionNumber != null && filterTransactions.Currencies == null && filterTransactions.DateFrom == DateTime.MinValue && filterTransactions.DateTo == DateTime.MinValue && filterTransactions.Statuses == null)
            {
                List<TransactionViewModel_GetTransactions> domainTransaction = new List<TransactionViewModel_GetTransactions>();
                var filteredByTranNumber = _iTransactionRepository.GetTransactionByNumber(filterTransactions.TransactionNumber);

                if (userId.HasValue && userStatus == "Client")
                {
                    filteredByTranNumber = filteredByTranNumber.Where(t => t.UserId == userId.Value).ToList();
                }

                foreach (var transaction in filteredByTranNumber)
                {
                    var transactionFiltered = Mapper.Map<TransactionViewModel_GetTransactions>(transaction);
                    domainTransaction.Add(transactionFiltered);
                }

                return domainTransaction;
            }
            else
            {
                if (filterTransactions.Currencies == null)
                {
                    filterTransactions.Currencies = new List<int>();

                    var allAvailableCurrencies = _iTransactionRepository.GetAllCurrenciesAvailable();

                    filterTransactions.Currencies.AddRange(allAvailableCurrencies);
                }

                var filterByCurrencies = _iTransactionRepository.FilterDataByCurrency(filterTransactions.Currencies);

                if (userId.HasValue && userStatus != "Accountant")
                {
                    filterByCurrencies = filterByCurrencies.Where(t => t.UserId == userId.Value).ToList();
                }

                if (filterTransactions.Statuses == null)
                {
                    filterTransactions.Statuses = new List<string>();

                    var allAvailableStatuses = _iTransactionRepository.GetAllStatusesAvailable();

                    filterTransactions.Statuses.AddRange(allAvailableStatuses);
                }

                var filterByStatuses = _iTransactionRepository.FilterDataByStatuses(filterByCurrencies, filterTransactions.Statuses);

                var filteredByAmount = _iTransactionRepository.FilteredDataByAmount(filterByStatuses, filterTransactions.AmountSelected);

                List<TransactionViewModel_GetTransactions> dataWithAppliedFilter = new List<TransactionViewModel_GetTransactions>();

                if (filterTransactions.DateFrom == DateTime.MinValue && filterTransactions.DateTo == DateTime.MinValue)
                {
                    foreach (var transaction in filteredByAmount)
                    {
                        var transactionFiltered = Mapper.Map<TransactionViewModel_GetTransactions>(transaction);
                        dataWithAppliedFilter.Add(transactionFiltered);
                    }
                }
                else if (filterTransactions.DateFrom == DateTime.MinValue)
                {
                    var transactions = _iTransactionRepository.FilteredDataDateTo(filteredByAmount, filterTransactions.DateTo);
                    foreach (var transaction in transactions)
                    {
                        var transactionFiltered = Mapper.Map<TransactionViewModel_GetTransactions>(transaction);
                        dataWithAppliedFilter.Add(transactionFiltered);
                    }
                }
                else if (filterTransactions.DateTo == DateTime.MinValue)
                {
                    var transactions = _iTransactionRepository.FilteredDataDateFrom(filteredByAmount, filterTransactions.DateFrom);
                    foreach (var transaction in transactions)
                    {
                        var transactionFiltered = Mapper.Map<TransactionViewModel_GetTransactions>(transaction);
                        dataWithAppliedFilter.Add(transactionFiltered);
                    }
                }
                else
                {
                    var transactions = _iTransactionRepository.FilteredDataByDate(filteredByAmount, filterTransactions.DateFrom, filterTransactions.DateTo);
                    foreach (var transaction in transactions)
                    {
                        var transactionFiltered = Mapper.Map<TransactionViewModel_GetTransactions>(transaction);
                        dataWithAppliedFilter.Add(transactionFiltered);
                    }
                }
                return dataWithAppliedFilter;
            }
        }

        public List<TransactionViewModel_GetTransactions> GetSortedTransactions(TransactionViewModel_SortData sortTransactions, TransactionViewModel_FilterData filteredData, string email)
        {
            List<Transaction> transactionsPersistance = new List<Transaction>();
            List<TransactionViewModel_GetTransactions> sortedData = new List<TransactionViewModel_GetTransactions>();
            List<TransactionViewModel_GetTransactions> filteredTransactions = new List<TransactionViewModel_GetTransactions>();
            filteredTransactions = GetFileteredTransactions(filteredData, email);

            if (sortTransactions.SortField == null)
            {
                sortedData = GetFileteredTransactions(filteredData, email);
            }
            else
            {
                if (sortTransactions.Ascending)
                {
                    foreach (var transaction in filteredTransactions)
                    {
                        var persistenceTransition = Mapper.Map<Transaction>(transaction);
                        transactionsPersistance.Add(persistenceTransition);
                    }

                    var sortedTransactionsAsc = _iTransactionRepository.SortFieldByAscending(transactionsPersistance, sortTransactions.SortField).ToList();

                    foreach (var transaction in sortedTransactionsAsc)
                    {
                        var domainTransactions = Mapper.Map<TransactionViewModel_GetTransactions>(transaction);
                        sortedData.Add(domainTransactions);
                    }
                }
                else
                {
                    foreach (var transaction in filteredTransactions)
                    {
                        var persistenceTransition = Mapper.Map<Transaction>(transaction);
                        transactionsPersistance.Add(persistenceTransition);
                    }
                    var sortedTransactionsByDes = _iTransactionRepository.SortFieldByDescending(transactionsPersistance, sortTransactions.SortField).ToList();

                    foreach (var transaction in sortedTransactionsByDes)
                    {
                        var domainTransactions = Mapper.Map<TransactionViewModel_GetTransactions>(transaction);
                        sortedData.Add(domainTransactions);
                    }
                }
            }

            return sortedData;
        }

        public List<TransactionViewModel_GetTransactions> Pagination(TransactionViewModel_SortData sortTransactions, TransactionViewModel_FilterData filteredData, TransactionViewModel_Pagination pagination, string email)
        {
            List<Transaction> totalTransactions = new List<Transaction>();
            List<TransactionViewModel_GetTransactions> sortedTransactions = new List<TransactionViewModel_GetTransactions>();
            List<TransactionViewModel_GetTransactions> paginatedTransactions = new List<TransactionViewModel_GetTransactions>();

            sortedTransactions = GetSortedTransactions(sortTransactions, filteredData, email);

            if (pagination.Page < 0 || pagination.Page == 0)
            {
                pagination.Page = 1;
            }

            if (pagination.PageSize < 0 || pagination.PageSize == 0)
            {
                pagination.PageSize = 10;
            }

            int totalRecord = sortedTransactions.Count();
            int totalPages = (totalRecord / pagination.PageSize) + ((totalRecord % pagination.PageSize) > 0 ? 1 : 0);

            foreach (var transaction in sortedTransactions)
            {
                var persistanceTransactions = Mapper.Map<Transaction>(transaction);
                totalTransactions.Add(persistanceTransactions);
            }
            var filteredDataDividedOnPages = _iTransactionRepository.PaginateSortedTransactions(totalTransactions, pagination.Page, pagination.PageSize);

            foreach (var transaction in filteredDataDividedOnPages)
            {
                var domainTransactions = Mapper.Map<TransactionViewModel_GetTransactions>(transaction);
                paginatedTransactions.Add(domainTransactions);
            }

            return paginatedTransactions;
        }

        public int ReturnTotalPages(TransactionViewModel_SortData sortTransactions, TransactionViewModel_FilterData filteredData, TransactionViewModel_Pagination pagination, string email)
        {
            List<TransactionViewModel_GetTransactions> sortedTransactions = new List<TransactionViewModel_GetTransactions>();

            sortedTransactions = GetSortedTransactions(sortTransactions, filteredData, email);

            int totalRecord = sortedTransactions.Count();

            int totalPages = (totalRecord / pagination.PageSize) + ((totalRecord % pagination.PageSize) > 0 ? 1 : 0);

            return totalPages;
        }

        public List<TransactionViewModel_GetTransactionDetails> GetFullInformationAboutTransaction(string transactionNumber)
        {
            var transactionInfo = _iTransactionRepository.GetInfoAboutTransaction(transactionNumber);

            List<TransactionViewModel_GetTransactionDetails> transactionsDomain = new List<TransactionViewModel_GetTransactionDetails>();

            foreach (var transaction in transactionInfo)
            {
                var transactionDomain = Mapper.Map<TransactionViewModel_GetTransactionDetails>(transaction);
                transactionsDomain.Add(transactionDomain);
            }

            return transactionsDomain;
        }

        public List<TransactionViewModel_GetTransactions> GetMainTransactionsPages(int page)
        {
            var allTransactions = _iTransactionRepository.GetTransactions();
            var pageSize = 10;
            var totalRecord = _iTransactionRepository.GetTransactions().Count();
            var totalPage = (totalRecord / pageSize) + ((totalRecord % pageSize) > 0 ? 1 : 0);
            var query = _iTransactionRepository.PaginateSortedTransactions(allTransactions, page, pageSize);

            List<TransactionViewModel_GetTransactions> transactionsDomain = new List<TransactionViewModel_GetTransactions>();

            foreach (var transaction in query)
            {
                var transactionDomain = Mapper.Map<TransactionViewModel_GetTransactions>(transaction);
                transactionsDomain.Add(transactionDomain);
            }
            return transactionsDomain;
        }

        public List<TransactionViewModel_GetTransactions> GetMainTransactionsPaged(int page, string email)
        {
            var allTransactions = _iTransactionRepository.GetTransactionForUser(email);
            var pageSize = 10;
            var totalRecord = _iTransactionRepository.GetTransactionForUser(email).Count();
            var totalPage = (totalRecord / pageSize) + ((totalRecord % pageSize) > 0 ? 1 : 0);
            var query = _iTransactionRepository.PaginateSortedTransactions(allTransactions, page, pageSize);

            List<TransactionViewModel_GetTransactions> transactionsDomain = new List<TransactionViewModel_GetTransactions>();

            foreach (var transaction in query)
            {
                var transactionDomain = Mapper.Map<TransactionViewModel_GetTransactions>(transaction);
                transactionsDomain.Add(transactionDomain);
            }
            return transactionsDomain;
        }

        public double GetMaxValueOfAmount()
        {
            var maxVal = _iTransactionRepository.GetMaxAmount();

            return Math.Round(maxVal + 1, 0);
        }

        public double GetMinValueOfAmount()
        {
            var minVal = _iTransactionRepository.GetMinAmount();

            return minVal;
        }

        public List<TransactionViewModel_GetTransactions> GetTransactionsByLoggedUser(string email)
        {
            List<TransactionViewModel_GetTransactions> userTransactions = new List<TransactionViewModel_GetTransactions>();

            var persistanceTransactions = _iTransactionRepository.GetTransactionForUser(email);

            foreach (var transaction in persistanceTransactions)
            {
                var transactionsDomain = Mapper.Map<TransactionViewModel_GetTransactions>(transaction);
                userTransactions.Add(transactionsDomain);
            }

            return userTransactions;
        }

        public bool UpdateTransactionStatusToNew(string tranNumber, string tranStatus)
        {
            Statuses castedStatus = Statuses.Pending;
            bool isParsed = false;

            if (!string.IsNullOrWhiteSpace(tranStatus))
            {
                isParsed = Enum.TryParse<Statuses>(tranStatus, out castedStatus);
            }

            var transaction = _iTransactionRepository.GetTransactionByTransactionNumber(tranNumber);

            if (isParsed && (castedStatus == Statuses.Completed && transaction.Status != Statuses.Pending && transaction.Status != Statuses.Canceled || castedStatus == Statuses.Canceled && transaction.Status != Statuses.Completed))
            {
                _iTransactionRepository.UpdateTransactionStatus(transaction, castedStatus);
                return true;
            }
            else return false;
        }

        public List<string> GetAverageOfTransactions(string userEmail)
        {
            var sumByEUR = _iTransactionRepository.GetSumByCurrency(Persistance.Currencies.EUR, userEmail).Sum();
            var sumByUSD = _iTransactionRepository.GetSumByCurrency(Persistance.Currencies.USD, userEmail).Sum();
            var sumByMDL = _iTransactionRepository.GetSumByCurrency(Persistance.Currencies.MDL, userEmail).Sum();
            var sumByRUB = _iTransactionRepository.GetSumByCurrency(Persistance.Currencies.RUB, userEmail).Sum();

            var countByEUR = _iTransactionRepository.GetSumByCurrency(Persistance.Currencies.EUR, userEmail).Count();
            var countByUSD = _iTransactionRepository.GetSumByCurrency(Persistance.Currencies.USD, userEmail).Count();
            var countByMDL = _iTransactionRepository.GetSumByCurrency(Persistance.Currencies.MDL, userEmail).Count();
            var countByRUB = _iTransactionRepository.GetSumByCurrency(Persistance.Currencies.RUB, userEmail).Count();

            List<string> averageByCurrencies = new List<string>();
            var averageEUR = sumByEUR == 0 ? 0 : sumByEUR / countByEUR;
            var averageUSD = sumByUSD == 0 ? 0 : sumByUSD / countByUSD;
            var averageMDL = sumByMDL == 0 ? 0 : sumByMDL / countByMDL;
            var averageRUB = sumByRUB == 0 ? 0 : sumByRUB / countByRUB;

            averageByCurrencies.Add(Math.Round(averageEUR,2).ToString()+" EUR");
            averageByCurrencies.Add(Math.Round(averageUSD,2).ToString() + " USD");
            averageByCurrencies.Add(Math.Round(averageMDL,2).ToString() +  " MDL");
            averageByCurrencies.Add(Math.Round(averageRUB,2).ToString() + " RUB");

            return averageByCurrencies;
        }

        public List<TransactionViewModel_GetTransactions> GetTransactionsByUserEmail(string userEmail)
        {
            var transactionsPersistance = _iTransactionRepository.GetTransactionsByUserEmail(userEmail);

            List<TransactionViewModel_GetTransactions> transactionsDomain = new List<TransactionViewModel_GetTransactions>();

            foreach (var transactionPersistance in transactionsPersistance)
            {
                var transactionDomain = Mapper.Map<TransactionViewModel_GetTransactions>(transactionPersistance);

                transactionsDomain.Add(transactionDomain);
            }

            return transactionsDomain;

        }

        public List<TransactionViewModel_GetTransactions> GetTransactionsByYearAndMonth(List<TransactionViewModel_GetTransactions> transactions, int year, int month)
        {
            var transactionsBySpecifiedYearAndMonth = transactions.Where(t => t.CreatedDate.Year == year && t.CreatedDate.Month == month).ToList();

            return transactionsBySpecifiedYearAndMonth;
        }

        public List<IGrouping<Domain.TransactionViewModel.Statuses, TransactionViewModel_GetTransactions>> GroupTransactionsByStatuses(List<TransactionViewModel_GetTransactions> tranasactions)
        {
            var transactionsGroupedByStatuses = tranasactions.GroupBy(t => t.Status).ToList();

            return transactionsGroupedByStatuses;
        }
    }
}

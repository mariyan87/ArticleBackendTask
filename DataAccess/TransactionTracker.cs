using System;
using System.Threading.Tasks;

namespace DataAccess
{
    /// <summary>
    /// Used to track transactions - local vs inherited.
    /// </summary>
    public class TransactionTracker : IDisposable
    {
        private readonly IUnitOfWork unitOfWork;

        /// <summary>
        /// Creates a new transation tracker.
        /// </summary>
        /// <param name="unitOfWork">The related unit of work.</param>
        /// <param name="isLocal">Is transaction local or inherited.</param>
        public TransactionTracker(IUnitOfWork unitOfWork, bool isLocal)
        {
            this.unitOfWork = unitOfWork;
            IsLocal = isLocal;
        }

        /// <summary>
        /// Shows if transactions is local.
        /// </summary>
        public bool IsLocal { get; set; }

        /// <summary>
        /// Clears the transaction if error appears.
        /// </summary>
        public void Dispose()
        {
            Rollback();
        }

        /// <summary>
        /// Rollbacks the transaction if it is local;
        /// </summary>
        public void Rollback()
        {
            unitOfWork.RollbackTransaction(this);
        }

        /// <summary>
        /// Commits the transaction if it is local;
        /// </summary>
        public async Task CommitAsync()
        {
            await unitOfWork.CommitTransactionAsync(this);
        }
    }
}

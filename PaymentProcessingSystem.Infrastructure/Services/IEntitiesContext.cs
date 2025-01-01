using Microsoft.EntityFrameworkCore;
using PaymentProcessingSystem.SharedKernel.Domain;

namespace PaymentProcessingSystem.Infrastructure.Services
{
    public interface IEntitiesContext : IDisposable
    {
        DbSet<TEntity> Set<TEntity>() where TEntity : Entity;

        void SetAsAdded<TEntity>(TEntity entity) where TEntity : Entity;

        void SetAsModified<TEntity>(TEntity entity) where TEntity : Entity;

        void SetAsDeleted<TEntity>(TEntity entity) where TEntity : Entity;

        int SaveChanges();

        Task<bool> SaveEntitiesAsync(CancellationToken cancellationToken = default(CancellationToken));

        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default(CancellationToken));
    }
}

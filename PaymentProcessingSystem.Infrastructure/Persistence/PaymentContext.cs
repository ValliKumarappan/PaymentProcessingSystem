using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using PaymentProcessingSystem.Infrastructure.Persistence.EntityConfigurations;
using PaymentProcessingSystem.Infrastructure.Services;
using PaymentProcessingSystem.SharedKernel.Domain;

namespace PaymentProcessingSystem.Infrastructure.Persistence
{
    public class PaymentContext : DbContext, IEntitiesContext
    {
        public PaymentContext() { }
        static SemaphoreSlim semaphoreSlim = new SemaphoreSlim(1, 1);

        public DbSet<Payment> Payments { get; set; }

        public DbSet<Customer> Customer { get; set; }


        private static readonly object Lock = new object();

        private static bool _databaseInitialized;
        protected PaymentContext(DbContextOptions options) : base(options)
        {
        }
        public PaymentContext(DbContextOptions<PaymentContext> options) : base(options)
        {
            if (_databaseInitialized)
            {
                return;
            }
            lock (Lock)
            {
                if (!_databaseInitialized)
                {
                    // Set the database intializer which is run once during application start
                    _databaseInitialized = true;
                }
            }
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfiguration(new PaymentConfig());
            builder.ApplyConfiguration(new CustomerConfig());
        }

        public new DbSet<TEntity> Set<TEntity>() where TEntity : Entity
        {
            return base.Set<TEntity>();
        }

        public void SetAsAdded<TEntity>(TEntity entity) where TEntity : Entity
        {
            UpdateEntityState(entity, EntityState.Added);
        }

        public void SetAsModified<TEntity>(TEntity entity) where TEntity : Entity
        {
            UpdateEntityState(entity, EntityState.Modified);
        }

        public void SetAsDeleted<TEntity>(TEntity entity) where TEntity : Entity
        {
            UpdateEntityState(entity, EntityState.Deleted);
        }

        private void UpdateEntityState<TEntity>(TEntity entity, EntityState entityState) where TEntity : Entity
        {
            var dbEntityEntry = GetDbEntityEntrySafely(entity);
            dbEntityEntry.State = entityState;
        }

        private EntityEntry GetDbEntityEntrySafely<TEntity>(TEntity entity) where TEntity : Entity
        {
            var dbEntityEntry = Entry<TEntity>(entity);
            if (dbEntityEntry.State == EntityState.Detached)
            {
                Set<TEntity>().Attach(entity);
            }
            return dbEntityEntry;
        }

        //public async Task<int> SaveChangesAsync()
        //{
        //    return await base.SaveChangesAsync();
        //}

        public async Task<bool> SaveEntitiesAsync(CancellationToken cancellationToken = default(CancellationToken))
        {
            var result = await base.SaveChangesAsync();
            return true;
        }

        public override int SaveChanges()
        {
            semaphoreSlim.Wait();
            try
            {
                return base.SaveChanges();
            }
            finally
            {
                //When the task is ready, release the semaphore. It is vital to ALWAYS release the semaphore when we are ready, or else we will end up with a Semaphore that is forever locked.
                //This is why it is important to do the Release within a try...finally clause; program execution may crash or take a different path, this way you are guaranteed execution
                semaphoreSlim.Release();
            }
        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            await semaphoreSlim.WaitAsync(cancellationToken);
            try
            {
                return await base.SaveChangesAsync();
            }
            finally
            {
                //When the task is ready, release the semaphore. It is vital to ALWAYS release the semaphore when we are ready, or else we will end up with a Semaphore that is forever locked.
                //This is why it is important to do the Release within a try...finally clause; program execution may crash or take a different path, this way you are guaranteed execution
                semaphoreSlim.Release();
            }
        }
    }

    public class DataContextDesignFactory : IDesignTimeDbContextFactory<PaymentContext>
    {

        public DataContextDesignFactory()
        {
        }

        public PaymentContext CreateDbContext(string[] args)
        {
            var envName = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
            IConfigurationRoot configuration = new ConfigurationBuilder()
                 .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                 .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                 .AddJsonFile($"appsettings.{envName}.json", optional: true)
                 .Build();

            var connectionString =
               "Data Source=DESKTOP-IKICVPM\\SQLEXPRESS;Initial Catalog=PaymentDb;TrustServerCertificate=true;MultipleActiveResultSets=true;Integrated Security=True;";
              //"Server=10.0.2.51;Database=PaymentProcessingDb;User ID=travesys;Password=bnlgjrsHJHJ@123#;TrustServerCertificate=true";

            //var connectionString = configuration["ConnectionStrings:PaymentsDb"];
            var optionsBuilder = new DbContextOptionsBuilder<PaymentContext>()
                .UseSqlServer(connectionString);

            return new PaymentContext(optionsBuilder.Options);
        }
    }
}

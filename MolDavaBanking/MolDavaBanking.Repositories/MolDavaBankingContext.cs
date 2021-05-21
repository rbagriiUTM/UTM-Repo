using MolDavaBanking.Persistance;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Data.Entity.Infrastructure.Annotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MolDavaBanking.Repositories
{
    public class MolDavaBankingContext : DbContext
    {
        private static readonly MolDavaBankingContext instance = new MolDavaBankingContext();

        public MolDavaBankingContext() : base("name=db")
        {

        }

        public static MolDavaBankingContext GetInstance()
        {
            return instance;
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Transaction> Transactions { get; set; }
        public DbSet<Bank> Banks { get; set; }
        public DbSet<Card> Cards { get; set; }
        public DbSet<SupportMessage> SupportMessages { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Transaction>()
                .HasRequired<Bank>(x => x.Bank)
                .WithMany(b => b.Transactions)
                .HasForeignKey(x => x.BankId);

            modelBuilder.Entity<Transaction>()
                .HasRequired<User>(x => x.User)
                .WithMany(u => u.Transactions)
                .HasForeignKey(x => x.UserId);

            modelBuilder.Entity<User>()
                .HasMany<Role>(u => u.Roles)
                .WithMany(r => r.Users)
                .Map(ur =>
                {
                    ur.MapLeftKey("UserRefId");
                    ur.MapRightKey("RoleRefId");
                    ur.ToTable("UserRole");
                });

            modelBuilder.Entity<Card>()
                        .HasRequired<User>(c => c.User)
                        .WithMany(u => u.Cards)
                        .HasForeignKey<int>(c => c.UserId);

            modelBuilder.Entity<Card>()
                .HasRequired<Bank>(c => c.Bank)
                .WithMany(b => b.Cards)
                .HasForeignKey<int>(c => c.BankId);

            modelBuilder.Entity<SupportMessage>()
                .HasRequired<User>(sm => sm.User)
                .WithMany(u => u.SupportMessages)
                .HasForeignKey(sm => sm.UserId);
        }
    }
}
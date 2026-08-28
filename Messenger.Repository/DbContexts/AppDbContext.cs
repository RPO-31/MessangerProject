using Microsoft.EntityFrameworkCore;
using Messanger.DataAccess.Models;

namespace Messenger.Api.Repository
{
    public class AppDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Chat> Chats { get; set; }
        public DbSet<Message> Messages { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .HasMany(u => u.Chats)
                .WithMany(c => c.Users)
                .UsingEntity<Dictionary<string, object>>(
                    "UserChat",
                    j => j.HasOne<Chat>().WithMany().HasForeignKey("ChatId"),
                    j => j.HasOne<User>().WithMany().HasForeignKey("UserId")
                );
            
            modelBuilder.Entity<Message>()
                .HasOne(m => m.MainChat)
                .WithMany(c => c.Messages)
                .HasForeignKey(m => m.MainChatId)
                .OnDelete(DeleteBehavior.Cascade);
            
            modelBuilder.Entity<Chat>()
                .HasOne(c => c.Admin)
                .WithMany()
                .HasForeignKey("AdminId")
                .OnDelete(DeleteBehavior.Restrict);
             
            modelBuilder.Entity<Chat>()
                .Ignore(c => c.UsersId)
                .Ignore(c => c.MessagesId);

            modelBuilder.Entity<User>()
                .Ignore(u => u.ChatsId);
        }
    }
}
using Microsoft.EntityFrameworkCore;

namespace TicTacToe.Data;

public class TicTacToeDbContext : DbContext
{
    public TicTacToeDbContext(
        DbContextOptions<TicTacToeDbContext> options)
        : base(options)
    {
    }

    public DbSet<Turn> Turns => Set<Turn>();
}
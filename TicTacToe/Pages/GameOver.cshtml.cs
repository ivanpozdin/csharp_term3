using Microsoft.AspNetCore.Mvc.RazorPages;
using TicTacToe.Data;
using TicTacToe.Models;

namespace TicTacToe.Pages;

public class GameOver : PageModel
{
    private readonly TicTacToeDbContext _context;
    private readonly List<Turn> _turns;
    public Board Board;
    public string Winner;

    /// <summary>
    ///     Creates GameOver class which shows who won the game and deletes all moves from the DataBase.
    /// </summary>
    /// <param name="context"></param>
    public GameOver(TicTacToeDbContext context)
    {
        _context = context;
        _turns = context.Turns.OrderBy(p => p.TurnId).ToList();
        Board = new Board(_turns);
        if (Board.DidFirstWon)
            Winner = "Победил первый игрок!";
        else if (Board.DidSecondWon)
            Winner = "Победил второй игрок!";
        else
            Winner = "Ничья!";
    }

    /// <summary>
    ///     Deletes all turns in DataBase and redirects to the Home page.
    /// </summary>
    public async void OnPost()
    {
        foreach (var turn in _turns)
        {
            _context.Turns.Remove(turn);
            await _context.SaveChangesAsync();
        }

        Response.Redirect("/Index");
    }
}
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


    public void OnGet()
    {
    }

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
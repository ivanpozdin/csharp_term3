using Microsoft.AspNetCore.Mvc.RazorPages;
using TicTacToe.Data;
using TicTacToe.Models;

namespace TicTacToe.Pages;

public class GameOver : PageModel
{
    private readonly TicTacToeDbContext _context;
    private readonly List<Turn> Turns;
    public Board Board;
    public string Winner;

    public GameOver(TicTacToeDbContext context)
    {
        _context = context;
        Turns = context.Turns.OrderBy(p => p.TurnId).ToList();
        Board = new Board(Turns);
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
        foreach (var turn in Turns)
        {
            _context.Turns.Remove(turn);
            await _context.SaveChangesAsync();
        }

        Response.Redirect("/Index");
    }
}
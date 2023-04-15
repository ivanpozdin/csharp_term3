using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TicTacToe.Data;
using TicTacToe.Models;

namespace TicTacToe.Pages;

[BindProperties]
public class Game : PageModel
{
    private readonly TicTacToeDbContext _context;
    private readonly List<Turn> _turns;
    public Board Board;

    public Game(TicTacToeDbContext context)
    {
        _context = context;
        _turns = context.Turns.OrderBy(p => p.TurnId).ToList();
        Board = new Board(_turns);
    }

    public Turn Turn { get; set; } = new();


    public async void OnPostButton1()
    {
        var isTurnedAlready = false;
        foreach (var turn in _turns)
            if (Turn.Row == turn.Row && Turn.Column == turn.Column)
                isTurnedAlready = true;

        if (!isTurnedAlready)
        {
            _turns.Add(Turn);
            Board = new Board(_turns);
            _context.Turns.Add(Turn);
            await _context.SaveChangesAsync();
            if (Board.IsGameOver)
            {
                Console.WriteLine("Game Over!");
                Response.Redirect("/GameOver");
            }
        }
    }
    public async void OnPostButton2()
    {
        foreach (var turn in _turns)
        {
            _context.Turns.Remove(turn);
            await _context.SaveChangesAsync();
        }
    }
}
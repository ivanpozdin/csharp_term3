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

    /// <summary>
    ///     Class that stores moves and game board.
    /// </summary>
    public Board Board;

    /// <summary>
    ///     Create Game class that handles turns.
    /// </summary>
    /// <param name="context">DataBase context.</param>
    public Game(TicTacToeDbContext context)
    {
        _context = context;
        _turns = context.Turns.OrderBy(p => p.TurnId).ToList();
        Board = new Board(_turns);
    }

    public Turn Turn { get; set; } = new();

    /// <summary>
    ///     Receive new turn and redirect to GameOver page if game over.
    /// </summary>
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

    /// <summary>
    ///     Delete all turns from DataBase.
    /// </summary>
    public async void OnPostButton2()
    {
        foreach (var turn in _turns)
        {
            _context.Turns.Remove(turn);
            await _context.SaveChangesAsync();
        }

        Board = new Board(new List<Turn>());
    }
}
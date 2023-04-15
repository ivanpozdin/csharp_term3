using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TicTacToe.Data;
using TicTacToe.Models;

namespace TicTacToe.Pages;

[BindProperties]
public class Game : PageModel
{
    private readonly TicTacToeDbContext _context;

    public Game(TicTacToeDbContext context)
    {
        _context = context;
    }

    public Board Board { get; set; } = new();
    public Turn Turn { get; set; } = new();


    public async void OnPostAsync()
    {
        var turns = _context.Turns.OrderBy(p => p.TurnId).ToList();


        // foreach (var turn in turns)
        // {
        //     _context.Turns.Remove(turn);
        //     await _context.SaveChangesAsync();
        // }


        var isTurnedAlready = false;
        foreach (var turn in turns)
            if (Turn.Row == turn.Row && Turn.Column == turn.Column)
                isTurnedAlready = true;

        if (!isTurnedAlready)
        {
            turns.Add(Turn);
            Board = new Board(turns);
            _context.Turns.Add(Turn);
            await _context.SaveChangesAsync();
            if (Board.IsGameOver)
            {
                Console.WriteLine("Game Over!");
                Response.Redirect("/GameOver");
            }
        }
    }
}
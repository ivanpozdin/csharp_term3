namespace TicTacToe.Data;

public class Turn
{
    public int TurnId { get; set; }
    public int Row { get; set; }
    public int Column { get; set; }
    public bool IsFirst { get; set; }
}
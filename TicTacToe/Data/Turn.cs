namespace TicTacToe.Data;

public class Turn
{
    /// <summary>
    ///     Primary key for Database.
    /// </summary>
    public int TurnId { get; set; }

    /// <summary>
    ///     Row number in game board. Must be in range 0<=Row<=2
    /// </summary>
    public int Row { get; set; }

    /// <summary>
    ///     Column number in game board. Must be in range 0<=Column<=2
    /// </summary>
    public int Column { get; set; }
}
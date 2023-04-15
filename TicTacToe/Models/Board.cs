using TicTacToe.Data;

namespace TicTacToe.Models;

public class Board
{
    private const string FirstPlayerFigure = "❌";
    private const string SecondPlayerFigure = "⭕️";
    private const string EmptyFigure = "🆓";

    private readonly string[,] _board = new string[3, 3]
    {
        { EmptyFigure, EmptyFigure, EmptyFigure }, { EmptyFigure, EmptyFigure, EmptyFigure },
        { EmptyFigure, EmptyFigure, EmptyFigure }
    };

    private readonly List<Turn> _turns;

    /// <summary>
    ///     Create Board class which stores and analyzes turns.
    /// </summary>
    /// <param name="turns">List of player's turns.</param>
    public Board(List<Turn> turns)
    {
        _turns = turns;
        for (var i = 0; i < turns.Count; i++) MakeTurn(i % 2 == 0, turns[i].Row, turns[i].Column);
    }

    /// <summary>
    ///     Returns true if it's game over, and false otherwise.
    /// </summary>
    public bool IsGameOver => DidWon(FirstPlayerFigure) || DidWon(SecondPlayerFigure) || _turns.Count == 9;

    /// <summary>
    ///     Returns true if first player won, and false otherwise.
    /// </summary>
    public bool DidFirstWon => DidWon(FirstPlayerFigure);

    /// <summary>
    ///     Returns true if second player won, and false otherwise.
    /// </summary>
    public bool DidSecondWon => DidWon(SecondPlayerFigure);

    private bool MakeTurn(bool isFirstPlayer, int row, int column)
    {
        var turnChar = SecondPlayerFigure;
        if (isFirstPlayer) turnChar = FirstPlayerFigure;

        if (column is < 0 or > 2 || row is < 0 or > 2 ||
            _board[row, column] != EmptyFigure) return false;

        _board[row, column] = turnChar;
        return true;
    }

    /// <summary>
    ///     Returns string that represents a row of the game board.
    /// </summary>
    /// <param name="r">
    ///     Row number. 0<=r<=2
    /// </param>
    /// <returns></returns>
    public string GetRow(int r)
    {
        if (r < 0 || r > 2) throw new ArgumentOutOfRangeException();
        return _board[r, 0] + _board[r, 1] + _board[r, 2] + "\n";
    }


    private bool DidWon(string figure)
    {
        return DoHaveThreeInDiagonals(figure) || DoHaveThreeInInColumns(figure) || DoHaveThreeInRows(figure);
    }

    private bool DoHaveThreeInRows(string figure)
    {
        for (var row = 0; row < 3; row++)
            if (_board[row, 0] == _board[row, 1] && _board[row, 1] == _board[row, 2] && _board[row, 0] == figure)
                return true;

        return false;
    }

    private bool DoHaveThreeInDiagonals(string figure)
    {
        return (
                   _board[0, 0] == _board[1, 1] &&
                   _board[0, 0] == _board[2, 2] &&
                   _board[0, 0] == figure
               ) ||
               (
                   _board[0, 2] == _board[1, 1] &&
                   _board[0, 2] == _board[2, 0] &&
                   _board[0, 2] == figure
               );
    }

    private bool DoHaveThreeInInColumns(string figure)
    {
        for (var column = 0; column < 3; column++)
            if (_board[0, column] == _board[1, column] &&
                _board[0, column] == _board[2, column] &&
                _board[0, column] == figure
               )
                return true;
        return false;
    }
}
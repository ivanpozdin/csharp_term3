using TicTacToe.Data;

namespace TicTacToe.Models;

public class Board
{
    private readonly char[,] _board = new char[3, 3] { { '-', '-', '-' }, { '-', '-', '-' }, { '-', '-', '-' } };
    private readonly List<Turn> _turns;

    public Board()
    {
        _turns = new List<Turn>();
    }

    public Board(List<Turn> turns)
    {
        _turns = turns;
        for (var i = 0; i < turns.Count; i++) MakeTurn(i % 2 == 0, turns[i].Row, turns[i].Column);
    }


    public bool IsGameOver => DidWon('X') || DidWon('O') || _turns.Count == 9;
    public bool DidFirstWon => DidWon('X');
    public bool DidSecondWon => DidWon('O');

    private bool MakeTurn(bool isFirstPlayer, int row, int column)
    {
        var turnChar = Convert.ToChar("O");
        if (isFirstPlayer) turnChar = 'X';

        if (column is < 0 or > 2 || row is < 0 or > 2 ||
            _board[row, column] != '-') return false;

        _board[row, column] = turnChar;
        return true;
    }

    public string GetRow1()
    {
        return _board[0, 0] + _board[0, 1].ToString() + _board[0, 2] + "\n";
    }

    public string GetRow2()
    {
        return _board[1, 0] + _board[1, 1].ToString() + _board[1, 2] + "\n";
    }

    public string GetRow3()
    {
        return _board[2, 0] + _board[2, 1].ToString() + _board[2, 2] + "\n";
    }


    private bool DidWon(char figure)
    {
        return DoHaveThreeInDiagonals(figure) || DoHaveThreeInInColumns(figure) || DoHaveThreeInRows(figure);
    }

    private bool DoHaveThreeInRows(char figure)
    {
        for (var row = 0; row < 3; row++)
            if (_board[row, 0] == _board[row, 1] && _board[row, 1] == _board[row, 2] && _board[row, 0] == figure)
                return true;

        return false;
    }

    private bool DoHaveThreeInDiagonals(char figure)
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

    private bool DoHaveThreeInInColumns(char figure)
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
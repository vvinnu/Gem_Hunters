// Author : Vineeth Kanoor
// Date   : 24/02/2024

using System;

// Declaring all the classes required

// Class to store the position on the board
public class Position
{
    public int X;
    public int Y;

    public Position(int x, int y)
    {
        X = x;
        Y = y;
    }
}

// Class to represent the player in the game
public class Player
{
    public string Name;
    public Position Position;
    public int GemCount;

    public Player(string name, Position position)
    {
        Name = name;
        Position = position;
        GemCount = 0;
    }

    public void Move(char direction)
    {
        switch (direction)
        {
            case 'U':
                Position.Y--;
                break;
            case 'D':
                Position.Y++;
                break;
            case 'L':
                Position.X--;
                break;
            case 'R':
                Position.X++;
                break;
            default:
                break;
        }
    }
}

// Class to represent the Game Board
public class Board
{
    public Cell[,] Grid;

    public Board()
    {
        Grid = new Cell[6, 6];

    }

    public void Display()
    {

    }

    public bool IsValidMove(Player player, char direction)
    {

        return true;
    }

    public void CollectGem(Player player)
    {
        // Check if the player's new position contains a gem and update GemCount
    }
}

// Class to Represent a cell on the game board
public class Cell
{
    public string Occupant;
}

// Class to navigate the game
public class Game
{
    public Board Board;
    public Player Player1;
    public Player Player2;
    public Player CurrentTurn;
    public int TotalTurns;

    public Game()
    {

    }

    public void Start()
    {

    }

    public void SwitchTurn()
    {

    }

    public bool IsGameOver()
    {

        return true;
    }

    public void AnnounceWinner()
    {

    }
}

// Main class of the Game Board program to initialize
class Program
{
    static void Main(string[] args)
    {
        Game gemHunters = new Game();
        gemHunters.Start();
    }
}

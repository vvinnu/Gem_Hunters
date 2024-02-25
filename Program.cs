﻿// Author : Vineeth Kanoor
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

    public void board()
    {
        Grid = new Cell[6, 6];
        Random rand = new Random();

        int gemCount = 0;
        int obstacleCount = 0;

        for (int row = 0; row < 6; row++)
        {
            for (int col = 0; col < 6; col++)
            {
                Cell cell = new Cell();

                if (row == 0 && col == 0)
                {
                    cell.Occupant = "P1"; // Player 1 starts at top left corner
                }
                else if (row == 5 && col == 5)
                {
                    cell.Occupant = "P2"; // Player 2 starts at bottom right corner
                }
                else if (rand.Next(10) < 2 && gemCount < 7)
                {
                    cell.Occupant = "O"; 
                    gemCount++;
                }
                else if (rand.Next(10) < 2 && obstacleCount < 7)
                {
                    cell.Occupant = "G"; 
                    obstacleCount++;
                }
                else
                {
                    cell.Occupant = "-"; 
                }

                Grid[row, col] = cell;
            }
        }

    }

    public void Display()
    {
        for (int row = 0; row < 6; row++)
        {
            for (int col = 0; col < 6; col++)
            {
                string occupant = Grid[row, col].Occupant;

                Console.Write(occupant.PadRight(3));
            }
            Console.WriteLine();
        }
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

        Board init = new Board();
        init.board();
        init.Display();
    }
}

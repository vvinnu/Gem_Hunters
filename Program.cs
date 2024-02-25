﻿// Author : Vineeth Kanoor
// Date   : 24/02/2024

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

    public void Move(Board board, char direction)
    {
        switch (direction)
        {
            case 'U':
                Position = new Position(Position.X, Position.Y - 1);
                break;
            case 'D':
                Position = new Position(Position.X, Position.Y + 1);
                break;
            case 'L':
                Position = new Position(Position.X - 1, Position.Y);
                break;
            case 'R':
                Position = new Position(Position.X + 1, Position.Y);
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
                else if (rand.Next(10) < 2 && obstacleCount < 7)
                {
                    cell.Occupant = "O";
                    obstacleCount++;
                }
                else if (rand.Next(10) < 2 && gemCount < 7)
                {
                    cell.Occupant = "G";
                    gemCount++;

                }
                else
                {
                    cell.Occupant = "-";
                }

                Grid[row, col] = cell;

            }
        }

    }

    // Method to display the current state of the board
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
        // Get the current position of the player
        Position currentPosition = player.Position;

        // Calculate the new position based on the direction
        Position newPosition;


        switch (direction)
        {
            case 'U':
                newPosition = new Position(currentPosition.X, currentPosition.Y - 1);
                break;
            case 'D':
                newPosition = new Position(currentPosition.X, currentPosition.Y + 1);
                break;
            case 'L':
                newPosition = new Position(currentPosition.X - 1, currentPosition.Y);
                break;
            case 'R':
                newPosition = new Position(currentPosition.X + 1, currentPosition.Y);
                break;
            default:
                // Invalid direction
                return false;
        }
        if (newPosition.X < 0 || newPosition.X >= 6 || newPosition.Y < 0 || newPosition.Y >= 6)
        {
            return false;
        }
        if (Grid[newPosition.Y, newPosition.X].Occupant == "O")
        {
            return false;
        }
        {
            return true;
        }



    }



    public void CollectGem(Player player)
    {

        Position playerPosition = player.Position;

        player.GemCount++;

        // Print a message indicating that the gem has been collected
        Console.WriteLine($"Player {player.Name} collected a gem! Total gems: {player.GemCount}");


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
    public int TotalTurns = 0;

    public Game()
    {
        Board = new Board();
        Player1 = new Player("P1", new Position(0, 0));
        Player2 = new Player("P2", new Position(5, 5));
        CurrentTurn = Player1;

    }

    public void Start()
    {
        Console.WriteLine("!!!! WELCOME TO GEM HUNTERS GAMEE !!!!\n");
        while (TotalTurns < 30)
        {

            Console.WriteLine("\nCurrent Board:");
            Board.Display();
            // Reset the board by clearing all player positions

            // Set the new player positions on the board


            Console.WriteLine("\nTurn: " + (TotalTurns + 1));
            Console.WriteLine("\nCurrent Player: " + CurrentTurn.Name);
            Console.Write("\nEnter move (U/D/L/R): ");
            char move = char.ToUpper(Console.ReadKey().KeyChar);


            // Print Player positions
            Console.WriteLine($"\nPlayer {Player1.Name} Position: {Player1.Position.X},{Player1.Position.Y}");
            Console.WriteLine($"\nPlayer {Player2.Name} Position: {Player2.Position.X},{Player2.Position.Y}");
            //



            if (Board.IsValidMove(CurrentTurn, move))
            {
                Console.WriteLine("\nMove successful!");
                // Clear the current player's position on the board
                ClearPlayerPosition(CurrentTurn);
                CurrentTurn.Move(Board, move);
                if (Board.Grid[CurrentTurn.Position.Y, CurrentTurn.Position.X].Occupant == "G")
                {
                    Board.CollectGem(CurrentTurn);
                }
                // Set the new player position on the board
                SetPlayerPosition(CurrentTurn);


            }
            else
            {
                Console.WriteLine("\nInvalid move! Try again.");
                continue;
            }


            // Print Player positions
            Console.WriteLine($"\nPlayer {Player1.Name} Position: {Player1.Position.X},{Player1.Position.Y}");
            Console.WriteLine($"\nPlayer {Player2.Name} Position: {Player2.Position.X},{Player2.Position.Y}");
            //




            TotalTurns++;

            SwitchTurn();
            IsGameOver();
            AnnounceWinner();

        }

    }
    private void ClearPlayerPosition(Player player)
    {
        Board.Grid[player.Position.Y, player.Position.X].Occupant = "-";
    }

    private void SetPlayerPosition(Player player)
    {
        Board.Grid[player.Position.Y, player.Position.X].Occupant = player.Name;
    }

    public void SwitchTurn()
    {
        if (CurrentTurn == Player1)
        {
            CurrentTurn = Player2;
        }
        else
        {
            CurrentTurn = Player1;
        }
    }

    public void IsGameOver()
    {
        Console.Write("Game Over!");
    }

    public void AnnounceWinner()
    {
        {

            Console.WriteLine($"Player {Player1.Name} total gems: {Player1.GemCount}");
            Console.WriteLine($"Player {Player2.Name} total gems: {Player2.GemCount}");

            if (Player1.GemCount > Player2.GemCount)
            {
                Console.WriteLine($"Player {Player1.Name} wins!");
            }
            else if (Player1.GemCount < Player2.GemCount)
            {
                Console.WriteLine($"Player {Player2.Name} wins!");
            }
            else
            {
                Console.WriteLine("It's a tie!");
            }
        }
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
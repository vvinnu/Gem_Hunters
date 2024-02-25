// Author : Vineeth Kanoor
// Date   : 24/02/2024


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
    //Method to move the player position based on user input validation
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
        //Initializing the game board with random Gems and Obstacles
        Grid = new Cell[6, 6];
        Random rand = new Random();

        int gemCount = 0;
        int obstacleCount = 0;

        //Creating 6*6 Game Board
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
                    cell.Occupant = "O"; // Random number of obstacles placed on the board , maximum of 7 obstacles
                    obstacleCount++;
                }
                else if (rand.Next(10) < 2 && gemCount < 7)
                {
                    cell.Occupant = "G"; // Random number of gems placed on the board , maximum of 7 gems
                    gemCount++;
                }
                else
                {
                    cell.Occupant = "-"; // Remaining positions are displayed by -
                }

                Grid[row, col] = cell;

            }
        }

    }

    // Method to display the current state of the board for each turn
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

    // Method to check if the user move is valid or not
    public bool IsValidMove(Player player, char direction)
    {
        // Get the current position of the player
        Position currentPosition = player.Position;

        // Calculate the new position based on the direction
        Position newPosition;

        // Set the new position indexes based on user input
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
                return false;
        }
        // Checking if the new position goes out of the 6*6 Matrix
        if (newPosition.X < 0 || newPosition.X >= 6 || newPosition.Y < 0 || newPosition.Y >= 6)
        {
            return false;
        }
        // Checking if the new position has an obstacle
        if (Grid[newPosition.Y, newPosition.X].Occupant == "O")
        {
            return false;
        }
        // Checking if the new position is occupied by the other player
        if (Grid[newPosition.Y, newPosition.X].Occupant == "P1" || Grid[newPosition.Y, newPosition.X].Occupant == "P2")
        {
            return false;
        }
        // if all the above conditions failed , then it is a valid move 
        return true;
 
    }

    // Method to store the gems count collected by the user
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

    // Game starts here in this method
    public void Start()
    {
        Console.WriteLine("!!!! WELCOME TO GEM HUNTERS GAMEE !!!!\n");
        while (TotalTurns < 30)
        {
            // Display current board initially and updated board after each turn
            Console.WriteLine("\nCurrent Board:");
            Board.Display();

            Console.WriteLine("\nTurn: " + (TotalTurns + 1));
            Console.WriteLine("\nCurrent Player: " + CurrentTurn.Name);
            Console.Write("\nEnter move (U/D/L/R): ");
            char move = char.ToUpper(Console.ReadKey().KeyChar);

            // Calling IsValidMove method to verify the user input and update the position accordingly
            if (Board.IsValidMove(CurrentTurn, move))
            {
                Console.WriteLine("\n\nMove successful!");
                // Clear the current player's position on the board
                ClearPlayerPosition(CurrentTurn);
                // Calling Move method to update the player index
                CurrentTurn.Move(Board, move);

                // Calling the CollectGem method if there is a gem in the new position
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

            TotalTurns++;
            
            // Calling Switch method to turn between the players P1 and P2
            SwitchTurn();

        }

    }

    // Temporary method created to replace the previous player position with "-"
    private void ClearPlayerPosition(Player player)
    {
        Board.Grid[player.Position.Y, player.Position.X].Occupant = "-";
    }

    // Temporary method created to update the P1,P2 position to display on the output after each turn
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
        Console.WriteLine("Game Over!");
    }

    public void AnnounceWinner()
    {
        {

            Console.WriteLine($"\n\nPlayer {Player1.Name} total gems: {Player1.GemCount}");
            Console.WriteLine($"Player {Player2.Name} total gems: {Player2.GemCount}");

            if (Player1.GemCount > Player2.GemCount)
            {
                Console.WriteLine($"Player {Player1.Name} wins!");
            }
            else if (Player1.GemCount < Player2.GemCount)
            {
                Console.WriteLine($"Player {Player2.Name} wins!. Congratulations");
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

        // Check if the game is over and announce the winner
        if (gemHunters.TotalTurns == 30)
        {
            gemHunters.IsGameOver();
            gemHunters.AnnounceWinner();
        }

    }
}
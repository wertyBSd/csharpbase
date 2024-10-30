using System;
using System.Threading;

class Tetris
{
    static int[,] field = new int[20, 10];
    static int[,] currentPiece;
    static int currentPieceRow = 0;
    static int currentPieceCol = 3;
    static Random random = new Random();
    static bool gameOver = false;

    static void Main()
    {
        Console.CursorVisible = false;
        SpawnPiece();
        Thread inputThread = new Thread(InputHandler);
        inputThread.Start();
        while (!gameOver)
        {
            DrawField();
            Thread.Sleep(500);
            MovePieceDown();
        }
        Console.SetCursorPosition(0, 21);
        Console.WriteLine("Game Over!");
    }

    static void InputHandler()
    {
        while (!gameOver)
        {
            var key = Console.ReadKey(true).Key;
            switch (key)
            
{
                case ConsoleKey.LeftArrow:
                    MovePieceLeft();
                    break;
                case ConsoleKey.RightArrow:
                    MovePieceRight();
                    break;
                case ConsoleKey.Spacebar:
                    RotatePiece();
                    break;
            }
        }
    }

    static void SpawnPiece()
    {
        int pieceType = random.Next(0, 7);
        switch (pieceType)
        {
            case 0: // I
                currentPiece = new int[4, 4]
                {
{ 0, 1, 0, 0 },
{ 0, 1, 0, 0 },
{ 0, 1, 0, 0 },
{ 0, 1, 0, 0 }
                };
                break;
            case 1: // O
                currentPiece = new int[2, 2]
                {
{ 1, 1 },
{ 1, 1 }
                };
                break;
            case 2: // T
                currentPiece = new int[3, 3]
                {
{ 0, 1, 0 },
{ 1, 1, 1 },
{ 0, 0, 0 }
                };
                break;
            case 3: // S
                currentPiece = new int[3, 3]
                {
{ 0, 1, 1 },
{ 1, 1, 0 },
{ 0, 0, 0 }
                };
                break;
            case 4: // Z
                currentPiece = new int[3, 3]
                {
{ 1, 1, 0 },
{ 0, 1, 1 },
{ 0, 0, 0 }
                };
                break;
            case 5: // J
                currentPiece = new int[3, 3]
                {
{ 1, 0, 0 },
{ 1, 1, 1 },
{ 0, 0, 0 }
                };
                break;
            case 6: // L
                currentPiece = new int[3, 3]
                {
{ 0, 0, 1 },
{ 1, 1, 1 },
{ 0, 0, 0 }
                };
                break;
        }
        currentPieceRow = 0;
        currentPieceCol = 3;
    }

    static void DrawField()
    {
        Console.Clear();
        for (int row = 0; row < field.GetLength(0); row++)
        {
            for (int col = 0; col < field.GetLength(1); col++)
            {
                if (field[row, col] == 1)
                    Console.Write("█");
                else
                    Console.Write(" ");
            }
            Console.WriteLine();
        }
        DrawPiece();
    }

    static void DrawPiece()
    {
        for (int row = 0; row < currentPiece.GetLength(0); row++)
        {
            for (int col = 0; col < currentPiece.GetLength(1); col++)
            {
                if (currentPiece[row, col] == 1)
                {
                    Console.SetCursorPosition(currentPieceCol + col, currentPieceRow + row);
                    Console.Write("█");
                }
            }
        }
    }

    static void MovePieceDown()
    {
        if (CanMove(currentPieceRow + 1, currentPieceCol))
        {
            currentPieceRow++;
        }
        else
        {
            MergePiece();
            CheckForFullLines();
            SpawnPiece();
            if (!CanMove(currentPieceRow, currentPieceCol))
            {
                gameOver = true;
            }
        }
    }

    static void MovePieceLeft()
    {
        if (CanMove(currentPieceRow, currentPieceCol - 1))
        {
            currentPieceCol--;
            DrawField();
        }
    }

    static void MovePieceRight()
    {
        if (CanMove(currentPieceRow, currentPieceCol + 1))
        {
            currentPieceCol++;
            DrawField();
        }
    }

    static void RotatePiece()
    {
        int[,] rotatedPiece = new int[currentPiece.GetLength(1), currentPiece.GetLength(0)];
        for (int row = 0; row < currentPiece.GetLength(0); row++)
        {
            for (int col = 0; col < currentPiece.GetLength(1); col++)
            {
                rotatedPiece[col, currentPiece.GetLength(0) - 1 - row] = currentPiece[row, col];
            }
        }
        if (CanMove(currentPieceRow, currentPieceCol, rotatedPiece))
        {
            currentPiece = rotatedPiece;
            DrawField();
        }
    }

    static bool CanMove(int newRow, int newCol, int[,] piece = null)
    {
        piece = piece ?? currentPiece;
        for (int row = 0; row < piece.GetLength(0); row++)
        {
            for (int col = 0; col < piece.GetLength(1); col++)
            {
                if (piece[row, col] == 1)
                {
                    int newFieldRow = newRow + row;
                    int newFieldCol = newCol + col;
                    if (newFieldRow >= field.GetLength(0) || newFieldCol < 0 || newFieldCol >= field.GetLength(1) || field[newFieldRow, newFieldCol] == 1)
                    {
                        return false;
                    }
                }
            }
        }
        return true;
    }

    static void MergePiece()
    {
        for (int row = 0; row < currentPiece.GetLength(0); row++)
        {
            for (int col = 0; col < currentPiece.GetLength(1); col++)
            {
                if (currentPiece[row, col] == 1)
                {
                    field[currentPieceRow + row, currentPieceCol + col] = 1;
                }
            }
        }
    }

    static void CheckForFullLines()
    {
        for (int row = 0; row < field.GetLength(0); row++)
        {
            bool isFullLine = true;
            for (int col = 0; col < field.GetLength(1); col++)
            {
                if (field[row, col] == 0)
                {
                    isFullLine = false;
                    break;
                }
            }
            if (isFullLine)
            {
                for (int r = row; r > 0; r--)
                {
                    for (int c = 0; c < field.GetLength(1); c++)
                    {
                        field[r, c] = field[r - 1, c];
                    }
                }
                for (int c = 0; c < field.GetLength(1); c++)
                {
                    field[0, c] = 0;
                }
            }
        }
    }
}
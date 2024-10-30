using System;
using System.Threading;

class Program
{
    static int width = 30;
    static int height = 20;
    static int paddleWidth = 5;
    static int ballX, ballY, ballDX = 1, ballDY = 1;
    static int paddleX;
    static bool[,] blocks = new bool[width, height / 2];

    static void Main()
    {
        Console.CursorVisible = false;
        InitializeBlocks();
        paddleX = width / 2 - paddleWidth / 2;
        ballX = width / 2;
        ballY = height / 2;

        while (true)
        {
            Draw();
            Input();
            Logic();
            Thread.Sleep(200);
        }
    }

    static void InitializeBlocks()
    {
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height / 2; y++)
            {
                blocks[x, y] = true;
            }
        }
    }

    static void Draw()
    {
        Console.Clear();
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height / 2; y++)
            {
                if (blocks[x, y])
                {
                    Console.SetCursorPosition(x, y);
                    Console.Write("#");
                }
            }
        }

        Console.SetCursorPosition(ballX, ballY);
        Console.Write("O");

        for (int i = 0; i < paddleWidth; i++)
        {
            Console.SetCursorPosition(paddleX + i, height - 1);
            Console.Write("-");
        }
    }

    static void Input()
    {
        if (Console.KeyAvailable)
        {
            var key = Console.ReadKey(true).Key;
            if (key == ConsoleKey.LeftArrow && paddleX > 0)
                paddleX--;
            if (key == ConsoleKey.RightArrow && paddleX < width - paddleWidth)
                paddleX++;
        }
    }

    static void Logic()
    {
        ballX += ballDX;
        ballY += ballDY;

        if (ballX <= 0 || ballX >= width - 1)
            ballDX = -ballDX;
        if (ballY <= 0)
            ballDY = -ballDY;

        if (ballY == height - 1 && ballX >= paddleX && ballX < paddleX + paddleWidth)
            ballDY = -ballDY;

        if (ballY < height / 2 && blocks[ballX, ballY])
        {
            blocks[ballX, ballY] = false;
            ballDY = -ballDY;
        }

        if (ballY >= height)
        {
            Console.Clear();
            Console.SetCursorPosition(width / 2 - 5, height / 2);
            Console.WriteLine("Game Over");
            Thread.Sleep(2000);
            Environment.Exit(0);
        }
    }
}
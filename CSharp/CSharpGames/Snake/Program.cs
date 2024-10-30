using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

class Program
{
    static void Main()
    {
        Console.CursorVisible = false;
        SnakeGame game = new SnakeGame();
        game.Start();
    }
}

class SnakeGame
{
    private int width = 40;
    private int height = 20;
    private List<Point> snake = new List<Point>();
    private Point food;
    private Direction direction = Direction.Right;
    private Random random = new Random();

    public void Start()
    {
        InitializeGame();
        while (true)
        {
            if (Console.KeyAvailable)
            {
                var key = Console.ReadKey(true).Key;
                ChangeDirection(key);
            }
            MoveSnake();
            if (CheckCollision())
            {
                GameOver();
                break;
            }
            if (snake[0].Equals(food))
            {
                EatFood();
            }
            Draw();
            Thread.Sleep(100);
        }
    }

    private void InitializeGame()
    {
        snake.Clear();
        snake.Add(new Point(width / 2, height / 2));
        GenerateFood();
    }

    private void ChangeDirection(ConsoleKey key)
    {
        switch (key)
        
{
            case ConsoleKey.UpArrow:
                if (direction != Direction.Down) direction = Direction.Up;
                break;
            case ConsoleKey.DownArrow:
                if (direction != Direction.Up) direction = Direction.Down;
                break;
            case ConsoleKey.LeftArrow:
                if (direction != Direction.Right) direction = Direction.Left;
                break;
            case ConsoleKey.RightArrow:
                if (direction != Direction.Left) direction = Direction.Right;
                break;
        }
    }

    private void MoveSnake()
    {
        Point head = snake[0];
        Point newHead = head;

        switch (direction)
        {
            case Direction.Up:
                newHead = new Point(head.X, head.Y - 1);
                break;
            case Direction.Down:
                newHead = new Point(head.X, head.Y + 1);
                break;
            case Direction.Left:
                newHead = new Point(head.X - 1, head.Y);
                break;
            case Direction.Right:
                newHead = new Point(head.X + 1, head.Y);
                break;
        }

        snake.Insert(0, newHead);
        snake.RemoveAt(snake.Count - 1);
    }

    private bool CheckCollision()
    {
        Point head = snake[0];
        if (head.X < 0 || head.X >= width || head.Y < 0 || head.Y >= height)
        {
            return true;
        }
        for (int i = 1; i < snake.Count; i++)
        {
            if (head.Equals(snake[i]))
            {
                return true;
            }
        }
        return false;
    }

    private void EatFood()
    {
        snake.Add(snake[snake.Count - 1]);
        GenerateFood();
    }

    private void GenerateFood()
    {
        int x, y;
        do
        {
            x = random.Next(0, width);
            y = random.Next(0, height);
        } while (snake.Any(p => p.X == x && p.Y == y));
        food = new Point(x, y);
    }

    private void Draw()
    {
        Console.Clear();
        foreach (var point in snake)
        {
            Console.SetCursorPosition(point.X, point.Y);
            Console.Write("O");
        }
        Console.SetCursorPosition(food.X, food.Y);
        Console.Write("X");
    }

    private void GameOver()
    {
        Console.Clear();
        Console.SetCursorPosition(width / 2 - 5, height / 2);
        Console.WriteLine("Game Over");
    }
}

struct Point
{
    public int X { get; }
    public int Y { get; }

    public Point(int x, int y)
    {
        X = x;
        Y = y;
    }

    public override bool Equals(object obj)
    {
        if (obj is Point)
        {
            Point other = (Point)obj;
            return X == other.X && Y == other.Y;
        }
        return false;
    }

    public override int GetHashCode()
    {
        return X.GetHashCode() ^ Y.GetHashCode();
    }
}

enum Direction
{
    Up,
    Down,
    Left,
    Right
}
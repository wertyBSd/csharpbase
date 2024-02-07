using System.Text;

internal class Program
{
    private static void Main(string[] args)
    {
        // comment lesson 5
        Console.OutputEncoding = Encoding.UTF8;
        Random rnd = new Random();

        // тут мы создаём закрытое игровое поле
        string[,] board = { { "*", "*", "*", "*", "*" },
    { "*", "*", "*", "*", "*" },
    { "*", "*", "*", "*", "*" },
    { "*", "*", "*", "*", "*" },
    { "*", "*", "*", "*", "*" }};

        // создаём массив с короблями
        int[,] ship = new int[5, 5];
        // заполняем его
        for (int i = 0; i < 5; i++)
        {
            for (int j = 0; j < 5; j++)
            {
                // если 0 - пусто, если 1 - это корабль
                ship[j, i] = rnd.Next(2);
            }
        }
        // условие победы
        bool win = false;
        // если правда - значит победа
        while (win != true)
        {
            // очищаем консоль
            Console.Clear();
            // выводим игровое поле
            for (int i = 0; i < 5; i++)
            {
                for (int j = 0; j < 5; j++)
                {
                    Console.Write(board[j, i]);
                }
                Console.WriteLine();
            }
            Console.WriteLine("Введите координаты выстренла");
            int x = int.Parse(Console.ReadLine()) - 1;
            int y = int.Parse(Console.ReadLine()) - 1;
            // первая проверка, стреляли ли мы в эту координату, если *, значит ещё не стреляли
            if (board[x, y] == "*")
            {
                // проверяем корабль, если 1, то попали
                if (ship[x, y] == 1)
                {
                    // на игровом поле рисуем крестик
                    board[x, y] = "X";
                    // удаляем корабль, т.е. присваеваем 0
                    ship[x, y] = 0;
                    Console.WriteLine("Вы попали");
                }
                else
                {
                    // если не попали, то в данных координатах рисуем пусто
                    board[x, y] = " ";
                    Console.WriteLine("Вы промахнулись");
                }
            }
            else
            {
                // если уже стреляли, просим ещё раз ввести
                Console.WriteLine("Вы уже сюда стреляли");
            }
            int count = 0;
            // подсчитываем количество оставшихся кораблей
            foreach (int f in ship)
            {
                if (f == 1) count++;
            }
            // если кораблей не осталось, значит мы выйграли
            if (count == 0) win = true;
            Console.WriteLine("Продолжим");
            Console.ReadKey();
        }
        Console.WriteLine("Вы победили");
        Console.ReadLine();
    }
}
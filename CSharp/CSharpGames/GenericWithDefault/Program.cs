class BrickFactory<T> : IFactory<T>
{
    // Метод класса который возвращает значение по умолчанию
    public T CreateBrick()
    {
        return default(T);
    }
}

internal interface IFactory<T>
{
    public T CreateBrick();
}

internal class Brick
{
    string Name { get; set; }
}

internal class Program
{
    // Метод с дефолтными значениями
    static T[] InitializeArray<T>(int length)
    {
        if (length < 0)
        {
            throw new ArgumentOutOfRangeException(nameof(length), "Array length must be nonnegative.");
        }

        var array = new T[length];
        for (var i = 0; i < length; i++)
        {
            array[i] = default(T);
        }
        return array;
    }
    private static void Main(string[] args)
    {
        void Display<T>(T[] values) => Console.WriteLine($"[ {string.Join(", ", values)} ]");

        // Получаем значение по умолчанию для обычных типов
        Display(InitializeArray<int>(3));  // output: [ 0, 0, 0 ]
        Display(InitializeArray<bool>(4));  // output: [ False, False, False, False ]

        IFactory<Brick> factory = new BrickFactory<Brick>();
        Brick brick = factory.CreateBrick();
        // Значение по умолчанию для переменых ссылочного типа - null
        if (brick == null)
            Console.WriteLine("brick is null");
    }
}


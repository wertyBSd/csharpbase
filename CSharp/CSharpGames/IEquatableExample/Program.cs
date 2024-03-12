sealed class Student : IEquatable<Student>
{
    public readonly string Name;
    public readonly string Surname;

    public Student(string name, string surname)
    {
        Name = name;
        Surname = surname;
    }

    public override bool Equals(object other) => Equals(other as Student);
    public bool Equals(Student other)
    {
        if (this == other) return true;
        if (other == null) return false;
        if (Name != other.Name) return false;
        if (Surname != other.Surname) return false;
        return true;
    }
    public override int GetHashCode()
    {
        unchecked
        {
            // https://stackoverflow.com/a/263416/4340086
            int hash = 17;
            hash = (23 * hash) ^ (Name?.GetHashCode() ?? 0);
            hash = (23 * hash) ^ (Surname?.GetHashCode() ?? 0);
            return hash;
        }
    }

    public string Print()
    { return $"Name: {Name} Surname: {Surname}, HashCode: {GetHashCode()}"; }
}

internal class Program
{
    private static void Main(string[] args)
    {
        Student[] students = new Student[6] {
            new Student("", ""),
            new Student("Alex", ""),
            new Student("", "Fox"),
            new Student("Alex", "Fox"),
            new Student("Alex", "Fox"),
            new Student("William", "Smith") };
        for (int i = 0; i < students.Length; i++)
        {
            Student student = students[i];
            for (int j = 0; j < students.Length; j++)
            {
                if(i==j) continue;
                Student other = students[j];
                string isEqual = student.Equals(other) ? string.Empty : "not";
                Console.WriteLine($"Student: {student.Print()} is {isEqual} Equals {other.Print()}"); 
            }
        }
    }
}
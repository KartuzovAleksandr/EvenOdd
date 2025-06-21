// Дан массив целых чисел размерностью n > 0.
// Выделить четные и отсортировать по возрастанию, нечетные - по убыванию
// Результат записать обратно
using static System.Console;

int n = 0;
int max = 100;

begin:
Write("Введите размерность > 0 ");
try
{
    n = Convert.ToInt32(ReadLine());
    if (n <= 0)
        throw new FormatException();
}
catch (FormatException e)
{
    Beep();
    ForegroundColor = ConsoleColor.Red;
    WriteLine("Просили положительное число !");
    WriteLine(e.Message);
    ForegroundColor = ConsoleColor.White;
    goto begin;
}

WriteLine();
WriteLine("Массивы (только циклы)");
var m = EvenOddA();
WriteLine("Результат = " + String.Join(", ", m));
WriteLine();

WriteLine("Массивы (с помощью Array)");
m = EvenOddAA();
WriteLine("Результат = " + String.Join(", ", m));
WriteLine();

WriteLine("Коллекции");
var l = EvenOddC();
WriteLine("Результат = " + String.Join(", ", l));
WriteLine();

WriteLine("LINQ");
l = EvenOddL();
WriteLine("Результат = " + String.Join(", ", l));
WriteLine();

int[] EvenOddA()
{
    int[] m = new int[n];
    Random r = new();
    for (int i = 0; i < n; i++)
    {
        m[i] = r.Next(max);
    }
    // Array.Fill(m, RandomNumberGenerator.GetInt32(max));
    WriteLine("Исходный массив = " + String.Join(", ", m));
    // выделение четных и нечетных
    int[] m2 = new int[n];
    int[] m1 = new int[n];
    int c1 = 0, c2 = 0;
    for (int i = 0; i < n; i++)
    {
        if (m[i] % 2 == 0)
        {
            m2[c2++] = m[i];
        }
        else
        {
            m1[c1++] = m[i];
        }
    }
    WriteLine("Нечет = " + String.Join(", ", m1));
    WriteLine("Чет = " + String.Join(", ", m2));
    /*
    // пузырек по возрастанию для четных
    int temp;
    for (int i = 0; i < c2; i++)
    {
        for (int j = 0; j < c2 - i - 1; j++)
        {
            if (m2[j] > m2[j+1]) 
            {
                temp = m2[j];
                m2[j] = m2[j+1];
                m2[j + 1] = temp;
            }
        }
    }
    // пузырек по убыванию для нечетных
    for (int i = 0; i < c1; i++)
    {
        for (int j = 0; j < c1 - i - 1; j++)
        {
            if (m1[j] < m1[j + 1])
            {
                temp = m1[j];
                m1[j] = m1[j + 1];
                m1[j + 1] = temp;
            }
        }
    }
    */
    QuickSort(false, m2, 0, c2 - 1);
    QuickSort(true, m1, 0, c1 - 1);
    WriteLine("После сортировки");
    WriteLine("Нечет = " + String.Join(", ", m1));
    WriteLine("Чет = " + String.Join(", ", m2));
    // объединение массивов
    int k = 0;
    for (int i = 0; i < c2; i++)
    {
        m[k++] = m2[i];
    }
    for (int i = 0; i < c1; i++)
    {
        m[k++] = m1[i];
    }
    return m;
}

int[] QuickSort(bool desc, int[] array, int leftIndex, int rightIndex)
{
    var i = leftIndex;
    var j = rightIndex;
    var pivot = array[leftIndex];
    while (i <= j)
    {
        if (!desc)
        {
            while (array[i] < pivot)
            {
                i++;
            }
            while (array[j] > pivot)
            {
                j--;
            }
        }
        else
        {
            while (array[i] > pivot)
            {
                i++;
            }
            while (array[j] < pivot)
            {
                j--;
            }
        }
        if (i <= j)
        {
            int temp = array[i];
            array[i] = array[j];
            array[j] = temp;
            i++;
            j--;
        }
    }
    if (leftIndex < j)
        QuickSort(desc, array, leftIndex, j);
    if (i < rightIndex)
        QuickSort(desc, array, i, rightIndex);
    return array;
}
int[] EvenOddAA()
{
    int[] m = new int[n];
    Random r = new();
    for (int i = 0; i < n; i++)
    {
        m[i] = r.Next(max);
    }
    WriteLine("Исходный массив = " + String.Join(", ", m));
    var m2 = Array.FindAll(m, x => x % 2 == 0);
    var m1 = Array.FindAll(m, x => x % 2 != 0);
    WriteLine("Нечет = " + String.Join(", ", m1));
    WriteLine("Чет = " + String.Join(", ", m2));
    Array.Sort(m2);
    Array.Sort(m1, (x, y) => y.CompareTo(x));
    WriteLine("После сортировки");
    WriteLine("Нечет = " + String.Join(", ", m1));
    WriteLine("Чет = " + String.Join(", ", m2));
    // m = m1.Concat(m2).ToArray();
    m = [.. m2, .. m1];

    return m;
}

List<int> EvenOddC()
{
    List<int> l = new(n);
    Random r = new();
    for (int i = 0; i < n; i++)
    {
        l.Add(r.Next(max)); // l[i] = r.Next(max) - нельзя !!!
    }
    WriteLine("Исходный массив = " + String.Join(", ", l));
    var l2 = new List<int>(l); // l2 = l
    l2.RemoveAll(x => x % 2 != 0);
    var l1 = new List<int>(l);
    l1.RemoveAll(x => x % 2 == 0);
    WriteLine("Нечет = " + String.Join(", ", l1));
    WriteLine("Чет = " + String.Join(", ", l2));
    l2.Sort();
    l1.Sort((x, y) => y.CompareTo(x));
    WriteLine("После сортировки");
    WriteLine("Нечет = " + String.Join(", ", l1));
    WriteLine("Чет = " + String.Join(", ", l2));
    // l = l1.Concat(l2).ToList();
    l = [.. l2, .. l1];

    return l;
}
List<int> EvenOddL()
{
    Random r = new();
    var l = Enumerable.Range(0, n).Select(x => r.Next(max)).ToList();
    WriteLine("Исходный массив = " + String.Join(", ", l));
    var l2 = from x in l
             where x % 2 == 0
             orderby x
             select x;
    var l1 = from x in l
             where x % 2 != 0
             orderby x descending
             select x;
    WriteLine("Нечет = " + String.Join(", ", l1));
    WriteLine("Чет = " + String.Join(", ", l2));
    // l = l2.Concat(l1).ToList();
    l = [.. l2, .. l1];

    return l;
}
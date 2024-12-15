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
    
//    QuickSort(m2, 0, c2 - 1);
//    QuickSort(m1, 0, c1 - 1);
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

int[] QuickSort(int[] array, int leftIndex, int rightIndex)
{
    var i = leftIndex;
    var j = rightIndex;
    var pivot = array[leftIndex];
    while (i <= j)
    {
        while (array[i] < pivot)
        {
            i++;
        }

        while (array[j] > pivot)
        {
            j--;
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
        QuickSort(array, leftIndex, j);
    if (i < rightIndex)
        QuickSort(array, i, rightIndex);
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
using lab1;
using static System.Int32;

// Ввод размерности матрицы
Console.Write("Введите размерность матрицы: ");
int? matrixDimension = null;

// Ковертируем размерность в число
if (TryParse(Console.ReadLine(), out var convertedDimension))
{
    matrixDimension = convertedDimension;
}

// Если не получилось сконвертировать кидаем ошибку
ArgumentNullException.ThrowIfNull(matrixDimension);

// Если размерность матрицы меньше или равна нулю
if (matrixDimension <= 0)
{
    throw new Exception("Размерность матрицы должна быть положительным целым числом");
}

MatrixMethods.AddLine();

// Заполнение матрицы пользователем
var matrix = MatrixMethods.FillMatrix(matrixDimension.Value);

MatrixMethods.AddLine("Исходная матрица:");

// Вывод матрицы на экран
MatrixMethods.WriteMatrix(matrix);

Console.WriteLine();
Console.Write("Выходные элементы: ");
for (var i = 0; i < matrixDimension; i++)
{
    if (matrix[i].Sum() == 0)
    {
        Console.Write($"{i + 1} ");
    }
}

Console.WriteLine();
Console.Write("Входные элементы: ");
for (var i = 0; i < matrixDimension; i++)
{
    if (matrix.Select(list => list[i]).Sum() == 0)
    {
        Console.Write($"{i + 1} ");
    }
}
Console.WriteLine();
// Проверка на зацикленность
//MatrixMethods.CheckMatrix(matrix);

// Такты образования
var to = Enumerable.Repeat(-1, matrixDimension.Value).ToArray();

// Матрица прямых и косвенных связей
var matrixToSum = new List<List<int>>();

// Заполнение матрицы нулями
for (var i = 0; i < matrixDimension; i++)
{
    var list = new List<int>();
    matrixToSum.Add(FillWithZeros(list, matrixDimension.Value));
}

var something = 0;

// Умножение матрицы на саму себя + такты образования + матрица прямых и косвенных связей
MatrixMethods.MultiplyMatrix(matrix, matrix, ref to, 0, ref matrixToSum, ref something);

Console.WriteLine();
Console.WriteLine($"Порядок графа: {something - 1}");
Console.WriteLine();

// Такты гашения
var tg = Enumerable.Repeat(0, matrixDimension.Value).ToArray();

// Заполнение тактов гашения
for (var i = 0; i < matrixDimension; i++)
{
    var max = 0;
    for (var j = 0; j < matrixDimension; j++)
    {
        max = to[j] * matrix[i][j] > max
            ? to[j] * matrix[i][j]
            : max;
    }

    tg[i] = max;
    
    if (to[i] > tg[i])
    {
        tg[i] = to[i];
    }
}

// суммируем матрицу прямых и косвенных связей с исходной матрицей
for (var i = 0; i < matrixDimension; i++)
{
    for (var j = 0; j < matrixDimension; j++)
    {
        matrixToSum[i][j] += matrix[i][j];
    }
}

Console.WriteLine();
// Вывод тактов образования
Console.Write("To |");
foreach (var item in to)
{
    Console.Write($"{ item} |");
}

Console.WriteLine();
// Вывод тактов гашения
Console.Write("Tг |");
foreach (var item in tg)
{
    Console.Write($"{ item} |");
}

Console.WriteLine();
Console.Write("Tх |");
for (var i = 0; i < matrixDimension; i++)
{
    Console.Write($"{ tg[i] - to[i]} |");
}

MatrixMethods.AddLine();

// Вывод матрицы прмых и косвенных связей
MatrixMethods.WriteMatrix(matrixToSum);

List<int> FillWithZeros(List<int> list, int dimension)
{
    for (var i = 0; i < dimension; i++)
    {
        list.Add(0);
    }

    return list;
}

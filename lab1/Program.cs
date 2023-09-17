using lab1;
using static System.Int32;

Console.Write("Введите размерность матрицы: ");
int? matrixDimension = null;

if (TryParse(Console.ReadLine(), out var convertedDimension))
{
    matrixDimension = convertedDimension;
}

ArgumentNullException.ThrowIfNull(matrixDimension);

if (matrixDimension <= 0)
{
    throw new Exception("Размерность матрицы должна быть положительным целым числом");
}

Console.WriteLine();
/*var matrix = new List<List<int>>
{
    new() { 0, 1, 1, 0, 1, 0, 0 },
    new() { 0, 0, 1, 0, 1, 0, 1 },
    new() { 0, 0, 0, 0, 1, 0, 0 },
    new() { 0, 1, 0, 0, 0, 0, 0 },
    new() { 0, 0, 0, 0, 0, 0, 0 },
    new() { 0, 1, 1, 0, 0, 0, 0 },
    new() { 0, 0, 0, 0, 0, 0, 0 }
};*/

var matrix = MatrixMethods.FillMatrix(matrixDimension.Value, out var workTimes);
Console.WriteLine();

MatrixMethods.WriteMatrix(matrix);

var inElements = new List<int>();
var outElements = new List<int>();
Console.WriteLine();
Console.Write("Выходные элементы: ");
for (var i = 0; i < matrixDimension; i++)
{
    if (matrix[i].Sum() == 0)
    {
        outElements.Add(i + 1);
        Console.Write($"{i + 1} ");
    }
}
Console.WriteLine();

Console.WriteLine();
Console.Write("Входные элементы: ");
for (var i = 0; i < matrixDimension; i++)
{
    if (matrix.Select(list => list[i]).Sum() == 0)
    {
        inElements.Add(i + 1);
        Console.Write($"{i + 1} ");
    }
}

var to = Enumerable.Repeat(-1, matrixDimension.Value).ToArray();

MatrixMethods.MultiplyMatrix(matrix, matrix, ref to, 0);

Console.WriteLine();
Console.Write("To |");
foreach (var item in to)
{
    Console.Write($"{ item} |");
}
Console.WriteLine();

Console.WriteLine();
Console.WriteLine("|Работа|Время|РСНР|РСОР|ПСНР|ПСОР|Резерв|");
var table = new WorkTable(inElements, outElements, to.ToList(), workTimes);
foreach (var line in table.Lines)
{
    Console.WriteLine(line);
}
Console.WriteLine();

Console.WriteLine("|Событие|Ранний срок||Поздний срок|Резерв|");
var eventTable = new EventTable(table, to.ToList(), inElements, outElements);
foreach (var line in eventTable.EventTableLines)
{
    Console.WriteLine(line);
}
Console.WriteLine();

var criticalWay = table.Lines.Where(_ => _.PCHP == _.ПCHP).OrderBy(_ => _.PCHP).Select(_ => _.Work).ToList();
Console.Write("Критический путь: ");
for (var i = 0; i < criticalWay.Count; i++)
{
    Console.Write(i < criticalWay.Count - 1 
        ? $"{criticalWay[i]}->" 
        : $"{criticalWay[i]}");
}
namespace lab1;
using static Int32;

public static class MatrixMethods
{
    public static void MultiplyMatrix(List<List<int>> matrix, List<List<int>> anotherMatrix, ref int[] to, int count)
    {
        var matrixDimension = matrix.Count;
        
        var newMatrix = new List<List<int>>();

        for (var i = 0; i < matrixDimension; i++)
        {
            if (anotherMatrix.Sum(line => line[i]) == 0 && to[i] == -1)
            {
                to[i] = count;
            }
            var line = new List<int>();
            for (var j = 0; j < matrixDimension; j++)
            {
                var sum = 0;
                var firstElements = matrix[i];
                var secondElements = anotherMatrix.Select(l => l[j]).ToList();
                for (var k = 0; k < matrixDimension; k++)
                {
                    sum += firstElements[k] * secondElements[k];
                }
                line.Add(sum);
            }
            
            newMatrix.Add(line);
        }

        count++;

        if (newMatrix.Any(l => l.Any(value => value != 0)))
        {
            Console.WriteLine();
            MultiplyMatrix(matrix, newMatrix, ref to, count);
        }
        else
        {
            for (var i = 0; i < to.Length; i++)
            {
                if (to[i] == -1)
                {
                    to[i] = count;
                }
            }
        }
    }

    public static void WriteMatrix(List<List<int>> matrix)
    {
        var matrixDimension = matrix.Count;
        var sums = new List<int>();
        for (var i = 0; i < matrixDimension; i++)
        {
            sums.Add(matrix.Sum(line => line[i]));
            
            Console.Write("|");
            for (var j = 0; j < matrixDimension; j++)
            {
                Console.Write($" {matrix[i][j]} |");
            }

            Console.BackgroundColor = ConsoleColor.Yellow;
            var sum = matrix[i].Sum();
            Console.Write($" {sum}");
            Console.ResetColor();
            Console.WriteLine();
        }
        Console.BackgroundColor = ConsoleColor.Yellow;
        Console.Write("|");
        foreach (var sum in sums)
        {
            Console.Write($" {sum} |");
        }
        Console.ResetColor();
        Console.WriteLine();
    }

    public static List<List<int>> FillMatrix(int matrixDimension, out List<WorkTime> workTimes)
    {
        workTimes = new List<WorkTime>();
        
        var matrix = new List<List<int>>();
        for (var i = 0; i < matrixDimension; i++)
        {
            var line = new List<int>();
            int? value = null;
    
            for (var j = 0; j < matrixDimension; j++)
            {
                Console.Write($"Введите время работы [{i + 1};{j + 1}]: ");
                if (TryParse(Console.ReadLine(), out var convertedValue))
                {
                    value = convertedValue;
                }
        
                ArgumentNullException.ThrowIfNull(value);

                line.Add(value.Value != 0 ? 1 : 0);

                if (value.Value != 0)
                {
                    workTimes.Add(new WorkTime(i + 1, j + 1, value.Value));
                }
                
                Console.WriteLine();
            }
    
            matrix.Add(line);
        }

        return matrix;
    }
}
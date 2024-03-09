//Створити клас с атрибутами та конструктором.У методі main() ініціалізувати створення
//екземплярів класу та продемонструвати роботу його методів згідно умов завдання.

//Скласти опис класу для матриці. Зберігає кількість рядків і стовпців, елементи
//цілочисленної матриці. Методи: сума, різниця, добуток двох матриць, множення матриці на скаляр,
//транспонування матриці, чи рівні дві матриці, чи квадратна матриця,
//для квадратної матриці - перевірка симетричності відносно головної (побічної) діагоналі.
//Зробити властивості класу приватними, а для їх читання створити методи-геттери.

using System;
using System.Numerics;

public class Matrix
{
    private int[,] elements;
    private int countLine { get; set; }
    private int countColum { get; set; }

    public Matrix(int[,] elements)
    {
        this.elements = elements;
        this.countLine = elements.GetLength(0);
        this.countColum = elements.GetLength(1);
    }

    public static Matrix MatrixMultipByNumb(int n,Matrix matrix)
    {
        int[,] result = new int[matrix.GetLineCount(), matrix.GetColumnCount()];
       for (int i = 0; i < result.GetLength(0); i++)
       {
            for (int j = 0; j < result.GetLength(1); j++)
            {
                result[i, j] = n * matrix.GetElement(i,j);
            }
       }
       return new Matrix(result);
    }
    public static Matrix MultiplyTwoMatrix(Matrix matrix1, Matrix matrix2)
    {
        if (matrix1.GetColumnCount() != matrix2.GetLineCount())
        {
            throw new ArgumentException("Кількість стовпців першої матриці повинна бути рівною кількості рядків другої матриці.");
        }

        int[,] result = new int[matrix1.GetLineCount(), matrix2.GetColumnCount()];

        for (int i = 0; i < matrix1.GetLineCount(); i++)
        {
            for (int j = 0; j < matrix2.GetColumnCount(); j++)
            {
                int sum = 0;
                for (int k = 0; k < matrix1.GetColumnCount(); k++)
                {
                    sum += matrix1.GetElement(i, k) * matrix2.GetElement(k, j);
                }
                result[i, j] = sum;  
            }
        }
        return new Matrix(result);  
    }

public static Matrix Sum(Matrix matrix1,Matrix matrix2)
    {
        CheckSizeTwoMatrix(matrix1, matrix2);

        int[,] result = new int[matrix1.GetLineCount(),matrix1.GetColumnCount()];
        for (int i = 0; i < matrix1.GetLineCount(); i++)
        {
            for (int j = 0; j < matrix1.GetColumnCount(); j++)
            {
                result[i, j] = matrix1.GetElement(i, j) + matrix2.GetElement(i, j);
            }
        }
        return new Matrix(result);
    }
    public static Matrix Difference(Matrix matrix1, Matrix matrix2)
    {
        CheckSizeTwoMatrix(matrix1, matrix2);

        int[,] result = new int[matrix1.GetLineCount(), matrix1.GetColumnCount()];
        for (int i = 0; i < matrix1.GetLineCount(); i++)
        {
            for (int j = 0; j < matrix1.GetColumnCount(); j++)
            {
                result[i, j] = matrix1.GetElement(i, j) - matrix2.GetElement(i, j);
            }
        }
        return new Matrix(result);
    }
    public static void CheckSizeTwoMatrix(Matrix matrix1, Matrix matrix2)
    {
        if (matrix1.GetLineCount() != matrix2.GetLineCount() || matrix1.GetColumnCount() != matrix2.GetColumnCount())
        {
            throw new ArgumentException("Розміри матриць повинні бути однакові для виконання операції додавання.");
        }
    }
    public static void Print(Matrix matrix)
    {
        for (int i = 0; i < matrix.GetLineCount(); i++)
        {
            for (int j = 0; j < matrix.GetColumnCount(); j++)
            {
                Console.Write(matrix.GetElement(i, j) + " ");
            }
            Console.WriteLine();
        }
    }

    public int GetLineCount()
    {
        return countLine;
    }
    public int GetColumnCount()
    {
        return countColum;
    }
    public int GetElement(int line, int column)
    {
        return elements[line, column];
    }
}

class Programm
{
    static void Main(string[] args)
    {
        int[,] matrix1Elements = { { 1, 2, 3 }, { 4, 5, 6 }, { 7, 8, 9 } };

        int[,] matrix2Elements = { { 1, 2, 3 }, { 4, 5, 6 }, { 7, 8, 9 } };

        Matrix matrix1 = new Matrix(matrix1Elements);
        Matrix matrix2 = new Matrix(matrix2Elements);

        Console.WriteLine("Кiлькiсть рядкiв матрицi: " + matrix1.GetLineCount());
        Console.WriteLine("Кiлькiсть стовпцiв матрицi: " + matrix1.GetColumnCount());
        Console.WriteLine("Матрицi ");
        Console.WriteLine(" Matrix 1 ");
        Matrix.Print(matrix1);

        Console.WriteLine("Matrix 2 ");
        Matrix.Print(matrix2);



        Matrix sumMatrix = Matrix.Sum(matrix1, matrix2);

        Console.WriteLine("Сума матриць(1+2):");
        Matrix.Print(sumMatrix); 

        Matrix difMatrix = Matrix.Difference(matrix1, matrix2);

        Console.WriteLine("Рiзниця матриць(1-2): ");
        Matrix.Print(difMatrix);

        Matrix multiplyMatrix= Matrix.MultiplyTwoMatrix(matrix1, matrix2);

        Console.WriteLine("Множення матриць(1*2): ");
        Matrix.Print(multiplyMatrix);

        Matrix multiplyMatrixNumb = Matrix.MatrixMultipByNumb(4,matrix1);

        Console.WriteLine("Множення матрицi(matrix1*4): ");
        Matrix.Print(multiplyMatrixNumb);

    }
   
}
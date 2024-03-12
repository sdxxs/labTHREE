//Створити клас с атрибутами та конструктором.У методі main() ініціалізувати створення
//екземплярів класу та продемонструвати роботу його методів згідно умов завдання.

//Скласти опис класу для матриці. Зберігає кількість рядків і стовпців, елементи
//цілочисленної матриці. Методи: сума, різниця, добуток двох матриць, множення матриці на скаляр,
//транспонування матриці, чи рівні дві матриці, чи квадратна матриця,
//для квадратної матриці - перевірка симетричності відносно головної (побічної) діагоналі.
//Зробити властивості класу приватними, а для їх читання створити методи-геттери.

using System;
using System.Numerics;
using System.Reflection.Metadata;
using System.Text.Json;
using System.Text.Json.Serialization;

public class Matrix
{
    private int[,] elements;

    private int countLine;
    public int CountLine
    {
        get { return countLine; } 
    }

    private int countColum;
    public int CountColum
    {
        get { return countColum; }
    }

    public Matrix(int[,] elements)
    {
        this.elements = elements;
        this.countLine = elements.GetLength(0);
        this.countColum = elements.GetLength(1);
    }
    public int GetElement(int line, int column)
    {
        return elements[line, column];
    }

    public static void SaveToJsonFile(string fileName,Matrix matrix)
    {
        string json = JsonSerializer.Serialize(matrix);
        File.WriteAllText(fileName, json);
        Console.WriteLine($"Об'єкт класу збережено у файл {fileName}");
    }

    public static Matrix LoadFromJsonFile(string fileName)
    {
        string json = File.ReadAllText(fileName);
        Matrix jsonMatr= JsonSerializer.Deserialize<Matrix>(json);
        return jsonMatr;
    }

    public static bool Symmetry(Matrix matrix)
    {
        if (!isSquare(matrix))
        {
        throw new ArgumentException("Матриця не квадратна.");
        }
        for (int i = 0; i < matrix.CountLine; i++)
        {
            for (int j = 0; j < matrix.CountColum; j++)
            {
                if (matrix.GetElement(i, j) != matrix.GetElement(j, i))
                    return false;
            }
        }
        return true;    
    }

    public static Matrix Trasposicion(Matrix matrix)
    {
        int[,] result = new int[matrix.CountColum,matrix.CountLine];
        for (int i = 0; i < matrix.CountLine; i++)
        {
            for (int j = 0; j < matrix.CountColum; j++)
            {
                result[j,i]=matrix.GetElement(i,j);
            }
        }
        return new Matrix(result);
    }
    public static bool isSquare(Matrix matrix)
    {
        if(matrix.CountColum == matrix.CountLine) return true;
        return false;
    }
    public static bool areEqual(Matrix matrix1, Matrix matrix2)
    {      
        if (matrix1.CountColum != matrix2.CountColum|| matrix1.CountLine != matrix2.CountLine)
            return false;
        for (int i = 0;i< matrix1.CountLine; i++)
        {
            for (int j = 0;j< matrix1.CountColum; j++)
            {
                if (matrix1.GetElement(i, j) != matrix2.GetElement(i, j))  return false;

            }
        }
        return true;
    }
    public static Matrix MatrixMultipByNumb(int n,Matrix matrix)
    { 
        int[,] result = new int[matrix.CountLine, matrix.CountColum];
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
        if (matrix1.CountColum != matrix2.CountLine)
        {
            throw new ArgumentException("Кількість стовпців першої матриці повинна бути рівною кількості рядків другої матриці.");
        }

        int[,] result = new int[matrix1.CountLine, matrix2.CountColum];

        for (int i = 0; i < matrix1.CountLine; i++)
        {
            for (int j = 0; j < matrix2.CountColum; j++)
            {
                int sum = 0;
                for (int k = 0; k < matrix1.CountColum; k++)
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
        
        int[,] result = new int[matrix1.CountLine,matrix1.CountColum];
        for (int i = 0; i < matrix1.CountLine; i++)
        {
            for (int j = 0; j < matrix1.CountColum; j++)
            {
                result[i, j] = matrix1.GetElement(i, j) + matrix2.GetElement(i, j);
            }
        }
        return new Matrix(result);
    }
    public static Matrix Difference(Matrix matrix1, Matrix matrix2)
    {
        CheckSizeTwoMatrix(matrix1, matrix2);

        int[,] result = new int[matrix1.CountLine, matrix1.CountColum];
        for (int i = 0; i < matrix1.CountLine; i++)
        {
            for (int j = 0; j < matrix1.CountColum; j++)
            {
                result[i, j] = matrix1.GetElement(i, j) - matrix2.GetElement(i, j);
            }
        }
        return new Matrix(result);
    }
    private static void CheckSizeTwoMatrix(Matrix matrix1, Matrix matrix2)
    {
        if (matrix1.CountLine != matrix2.CountLine || matrix1.CountColum != matrix2.CountColum)
        {
            throw new ArgumentException("Розміри матриць повинні бути однакові для виконання операції.");
        }
    }
    public static void Print(Matrix matrix)
    {
        for (int i = 0; i < matrix.CountLine; i++)
        {
            for (int j = 0; j < matrix.CountColum; j++)
            {
                Console.Write(matrix.GetElement(i, j) + " ");
            }
            Console.WriteLine();
        }
    }
  
}

class Programm
{
    static void Main(string[] args)
    {
        int[,] matrix1Elements = { { 1, 2, 3 }, { 4, 5, 6 }, { 7, 8, 9 } };

        int[,] matrix2Elements = { { 1, 2, 3 }, { 4, 5, 6 }, { 7, 8, 9 } };

        int[,] matrix3Elements = { { 1, 2, 3 }, { 2, 2, 3 }, { 3, 3, 3 } };

        int[,] matrix4Elements = { { 1, 2, 3 }, { 1, 2, 3 }, { 1, 2, 3 }, { 1, 2, 3 } };

        Matrix matrix1 = new Matrix(matrix1Elements);
        Matrix matrix2 = new Matrix(matrix2Elements);
        Matrix matrix3 = new Matrix(matrix3Elements);
        Matrix matrix4 = new Matrix(matrix4Elements);

        Console.WriteLine("Кiлькiсть рядкiв matrix1: " + matrix1.CountLine);
        Console.WriteLine("Кiлькiсть стовпцiв matrix1: " + matrix1.CountColum);
        Console.WriteLine("Матрицi ");
        Console.WriteLine(" Matrix 1 ");
        Matrix.Print(matrix1);

        Console.WriteLine("Matrix 2 ");
        Matrix.Print(matrix2);

        Console.WriteLine("Matrix 3 ");
        Matrix.Print(matrix3);


        Console.WriteLine("Matrix 4 ");
        Matrix.Print(matrix4);

        Matrix sumMatrix12 = Matrix.Sum(matrix1, matrix2);
        Console.WriteLine("Сума матриць(1+2):");
        Matrix.Print(sumMatrix12);


        Matrix difMatrix12 = Matrix.Difference(matrix1, matrix2);
        Console.WriteLine("Рiзниця матриць(1-2): ");
        Matrix.Print(difMatrix12);


        Matrix multiplyMatrix12 = Matrix.MultiplyTwoMatrix(matrix1, matrix2);
        Console.WriteLine("Множення матриць(1*2): ");
        Matrix.Print(multiplyMatrix12);


        Matrix multiplyMatrixNumb = Matrix.MatrixMultipByNumb(4, matrix1);
        Console.WriteLine("Множення матрицi(matrix1*4): ");
        Matrix.Print(multiplyMatrixNumb);


        bool Equal12 = Matrix.areEqual(matrix1, matrix2);
        Console.WriteLine("Перевiрка рiвностi matrix1==matrix2 : " + Equal12);


        bool Square1 = Matrix.isSquare(matrix1);
        Console.WriteLine("Чи є матриця квадратною matrix1 : " + Square1);


        Matrix TranspMatrix1 = Matrix.Trasposicion(matrix1);
        Console.WriteLine("Транспонована матриця(matrix1): ");
        Matrix.Print(TranspMatrix1);

        Matrix TranspMatrix4 = Matrix.Trasposicion(matrix4);
        Console.WriteLine("Транспонована матриця(matrix4): ");
        Matrix.Print(TranspMatrix4);


        bool Symmetry1 = Matrix.Symmetry(matrix1);
        Console.WriteLine("Перевiрка cимметрiї matrix1 : " + Symmetry1);

        bool Symmetry3 = Matrix.Symmetry(matrix3);
        Console.WriteLine("Перевiрка cимметрiї matrix3 : " + Symmetry3);
        
    }
   
}
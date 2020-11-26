using System;
using System.IO;
using System.Linq;

namespace Lab06
{
    class Program
    {
        static void Main(string[] args)
        {
            int[,] mainArray = { { 1, 2, 3 }, { 3, 4, 5 }, { 7, 8, 9 } };
            /// проверка на вложенность осуществляется с этим массивом, не стала запихивать в файл,
            /// но я понимаю как это осуществить


            ///  Массивы для проверки записаны в текстовом файле "Arrays.txt". Записаны они по такому принципу:
            ///  1 строка: 1 число - количество подмассивов, 2 число - количество ячеек подмассива
            ///  Остальные строки являются подмассивами до тех пор, пока их количество равно 1 числу из 1-й строки, 
            ///  затем все остальные массивы повторяются аналогично (в файле только двухмерные массивы)
        
            int count = 0; //считает количество прочитанных строчек, чтобы потом не начинать читать файл сначала
            while (true) {
                Console.WriteLine(CheckSubArray(mainArray, ReadArray("Arrays.txt", ref count)));
            }
        }
        private static int[,] ReadArray(string filename, ref int count)
        {
            int[,] result = null;
            using (var reader = new StreamReader(filename))
            {
                for (var i = 0; i < count; i++)
                    reader.ReadLine();
                
                var line = reader.ReadLine();
                ++count;
                if (line == null) 
                    Environment.Exit(0);

                var values = line.Split(' ').Select(int.Parse).ToArray();
                int n = values[0], m = values[1];
                result = new int[n, m];

                for (int i = 0; i < n; i++){
                    line = reader.ReadLine();
                    ++count;
                    values = line.Split(' ').Select(int.Parse).ToArray();
                    
                    for (int j = 0; j < m; j++)
                        result[i, j] = values[j];
                }
            }
            return result;
        }
        private static bool CheckSubArray(int[] array, int[] subArray){
            int count = 0;
            for (int i = 0; i < array.Length; i++){
                if (array[i] == subArray[count])
                    count++;
                else
                    count = 0;

                if (count == subArray.Length)
                    return true;
            }
            return false;
        }
        private static bool CheckSubArray(int[,] mainArray, int[] array)
        {
            for (int i = 0; i < array.GetLength(0); i++)
            {
                int[] subArray = Get1D(mainArray, i);
                if (CheckSubArray(subArray, subArray)) 
                    return true;
            }
            return false;
        }
        private static bool CheckSubArray(int[,] mainArray, int[,] array)
        {
            int count = 0;
            for (int i = 0; i < mainArray.GetLength(0) - array.GetLength(0) + 1; i++)
            {
                for (int j = 0; j < array.GetLength(0); j++)
                {
                    int[] array1 = Get1D(mainArray, i + j);
                    int[] array2 = Get1D(array, j);
                    if (CheckSubArray(array1, array2)) count++;
                }
                if (count == array.GetLength(0)) return true;
            }
            return false;
        }
        private static int[] Get1D(int[,] mainArray, int index = 0)
        {
            int size = mainArray.GetLength(1);
            var array1D = new int[size];
            for (int i = 0; i < size; i++)
                array1D[i] = mainArray[index, i];
            
            return array1D;
        }
    }
}
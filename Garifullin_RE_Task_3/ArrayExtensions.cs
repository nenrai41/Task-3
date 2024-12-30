using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Garifullin_RE_Task_3
{
    public static class ArrayExtensions
    {


        public static void Sort<T>(ref T[] array, SortOrder order, SortAlgorithm algorithm)
        {
            Sort(ref array, order, algorithm, Comparer<T>.Default);
        }

        public static void Sort<T>(ref T[] array, SortOrder order, SortAlgorithm algorithm, IComparer<T> comparer)
        {
            if (algorithm == SortAlgorithm.Insertion)
                InsertionSort(ref array, order, comparer);
            else if (algorithm == SortAlgorithm.Selection)
                SelectionSort(ref array, order, comparer);
            else if (algorithm == SortAlgorithm.Heap)
                HeapSort(ref array, order, comparer);
            else if (algorithm == SortAlgorithm.Quick)
                QuickSort(ref array, order, comparer);
            else if (algorithm == SortAlgorithm.Merge)
                MergeSort(ref array, order, comparer);
            else
                throw new ArgumentException("Неподдерживаемый алгоритм сортировки");
        }

        public static void Sort<T>(ref T[] array, SortOrder order, SortAlgorithm algorithm, Comparison<T> comparison)
        {
            Sort(ref array, order, algorithm, Comparer<T>.Create(comparison));
        }

        private static void InsertionSort<T>(ref T[] array, SortOrder order, IComparer<T> comparer)
        {
            for (int i = 1; i < array.Length; i++)
            {
                T key = array[i];
                int j = i - 1;

                while (j >= 0 && (order == SortOrder.Ascending ? comparer.Compare(array[j], key) > 0 : comparer.Compare(array[j], key) < 0))
                {
                    array[j + 1] = array[j];
                    j--;
                }
                array[j + 1] = key;
            }
        }

        private static void SelectionSort<T>(ref T[] array, SortOrder order, IComparer<T> comparer)
        {
            for (int i = 0; i < array.Length - 1; i++)
            {
                int extremeIndex = i;
                for (int j = i + 1; j < array.Length; j++)
                {
                    if (order == SortOrder.Ascending ? comparer.Compare(array[j], array[extremeIndex]) < 0 : comparer.Compare(array[j], array[extremeIndex]) > 0)
                    {
                        extremeIndex = j;
                    }
                }
                T temp = array[extremeIndex];
                array[extremeIndex] = array[i];
                array[i] = temp;
            }
        }

        private static void HeapSort<T>(ref T[] array, SortOrder order, IComparer<T> comparer)
        {
            int n = array.Length;

            // Построение кучи
            for (int i = n / 2 - 1; i >= 0; i--)
                Heapify(array, n, i, order, comparer);

            // Один за другим извлекаем элементы из кучи
            for (int i = n - 1; i > 0; i--)
            {
                // Перемещаем текущий корень в конец
                T temp = array[i];
                array[i] = array[0];
                array[0] = temp;

                // Вызываем Heapify на уменьшенной куче
                Heapify(array, i, 0, order, comparer);
            }
        }

        private static void Heapify<T>(T[] array, int n, int i, SortOrder order, IComparer<T> comparer)
        {
            int largest = i; // Инициализируем корень как наибольший
            int left = 2 * i + 1; // левый = 2*i + 1
            int right = 2 * i + 2; // правый = 2*i + 2

            if (left < n && (order == SortOrder.Ascending ? comparer.Compare(array[left], array[largest]) > 0 : comparer.Compare(array[left], array[largest]) < 0))
                largest = left;

            if (right < n && (order == SortOrder.Ascending ? comparer.Compare(array[right], array[largest]) > 0 : comparer.Compare(array[right], array[largest]) < 0))
                largest = right;

            if (largest != i)
            {
                T swap = array[i];
                array[i] = array[largest];
                array[largest] = swap;

                Heapify(array, n, largest, order, comparer);
            }
        }

        private static void QuickSort<T>(ref T[] array, SortOrder order, IComparer<T> comparer)
        {
            QuickSortRecursive(ref array, 0, array.Length - 1, order, comparer);
        }

        private static void QuickSortRecursive<T>(ref T[] array, int low, int high, SortOrder order, IComparer<T> comparer)
        {
            if (low < high)
            {
                int pi = Partition(ref array, low, high, order, comparer);

                QuickSortRecursive(ref array, low, pi - 1, order, comparer);
                QuickSortRecursive(ref array, pi + 1, high, order, comparer);
            }
        }

        private static int Partition<T>(ref T[] array, int low, int high, SortOrder order, IComparer<T> comparer)
        {
            T pivot = array[high];
            int i = (low - 1);

            for (int j = low; j < high; j++)
            {
                if (order == SortOrder.Ascending ? comparer.Compare(array[j], pivot) <= 0 : comparer.Compare(array[j], pivot) >= 0)
                {
                    i++;
                    T temp = array[i];
                    array[i] = array[j];
                    array[j] = temp;
                }
            }

            T temp1 = array[i + 1];
            array[i + 1] = array[high];
            array[high] = temp1;

            return i + 1;
        }

        private static void MergeSort<T>(ref T[] array, SortOrder order, IComparer<T> comparer)
        {
            if (array.Length < 2)
                return;

            int mid = array.Length / 2;

            T[] left = new T[mid];
            T[] right = new T[array.Length - mid];

            Array.Copy(array, 0, left, 0, mid);
            Array.Copy(array, mid, right, 0, array.Length - mid);

            MergeSort(ref left, order, comparer);
            MergeSort(ref right, order, comparer);

            Merge(ref array, left, right, order, comparer);
        }

        private static void Merge<T>(ref T[] array, T[] left, T[] right, SortOrder order, IComparer<T> comparer)
        {
            int i = 0, j = 0, k = 0;

            while (i < left.Length && j < right.Length)
            {
                if (order == SortOrder.Ascending ? comparer.Compare(left[i], right[j]) <= 0 : comparer.Compare(left[i], right[j]) >= 0)
                {
                    array[k++] = left[i++];
                }
                else
                {
                    array[k++] = right[j++];
                }
            }

            while (i < left.Length)
                array[k++] = left[i++];

            while (j < right.Length)
                array[k++] = right[j++];
        }
    }


    public enum SortOrder
    {
        Ascending,
        Descending
    }

    public enum SortAlgorithm
    {
        Insertion,
        Selection,
        Heap,
        Quick,
        Merge
    }
}


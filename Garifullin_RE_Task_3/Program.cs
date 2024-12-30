using Garifullin_RE_Task_3;

int[] numbers = { 5, 2, 9, 1, 5, 6 };

ArrayExtensions.Sort(ref numbers, SortOrder.Ascending, SortAlgorithm.Insertion);
Console.WriteLine(string.Join(", ", numbers));

ArrayExtensions.Sort(ref numbers, SortOrder.Descending, SortAlgorithm.Quick, Comparer<int>.Create((x, y) => y.CompareTo(x)));
Console.WriteLine(string.Join(", ", numbers));
namespace SaveTube.Extentions;

public static class ListExtentions
{
    public static void AddRange<T>(this ObservableCollection<T> collection, IEnumerable<T> newItems, bool clearFirst = false)
    {
        if (clearFirst)
            collection.Clear();

        newItems.ForEach(newItems => collection.Add(newItems));
    }

    public static void ForEach<T>(this IEnumerable<T> enumeration, Action<T> action)
    {
        foreach (var item in enumeration)
        {
            action(item);
        }
    }

    public static IEnumerable<T> Randamize<T>(this IEnumerable<T> source)
    {
        Random rnd = new Random();
        return source.OrderBy<T, int>((item) => rnd.Next());
    }
}

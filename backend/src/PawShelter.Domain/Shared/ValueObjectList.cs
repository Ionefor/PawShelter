using System.Collections;

namespace PawShelter.Domain.Shared;

public class ValueObjectList<T> : IReadOnlyList<T>
{
    private ValueObjectList()
    {
    }

    public ValueObjectList(IEnumerable<T> list)
    {
        Values = new List<T>(list).AsReadOnly();
    }

    public IReadOnlyList<T> Values { get; } = null!;
    public T this[int index] => Values[index];

    public int Count => Values.Count;

    public IEnumerator<T> GetEnumerator()
    {
        return Values.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return Values.GetEnumerator();
    }

    public static implicit operator ValueObjectList<T>(List<T> list)
    {
        return new ValueObjectList<T>(list);
    }

    public static implicit operator List<T>(ValueObjectList<T> list)
    {
        return list.Values.ToList();
    }
}
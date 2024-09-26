using System;
using System.Collections.Generic;

[Serializable]
public class ExpansionList<T> : List<T>
{
    public event EventHandler ListChanged;
    public event EventHandler ListAdded;
    public event EventHandler ListRemoved;

    public new void Add(T item)
    {
        base.Add(item);
        ListChanged?.Invoke(this, EventArgs.Empty);
        ListAdded?.Invoke(this, EventArgs.Empty);
    }

    public new void Remove(T item) 
    { 
        base.Remove(item);
        ListChanged?.Invoke(this, EventArgs.Empty);
        ListRemoved?.Invoke(this, EventArgs.Empty);
    }
}

public static class ExtensionList
{
    private static Random _random = new Random();

        public static T[] Shuffle<T>(this T[] toShuffleArr)
        {
            for (int i = toShuffleArr.Length - 1; i > 0; i--)
            {
                int r = _random.Next(i + 1);

                T value = toShuffleArr[r];
                toShuffleArr[r] = toShuffleArr[i];
                toShuffleArr[i] = value;
            }

            return toShuffleArr;
        }

        public static List<T> Shuffle<T>(this List<T> toShuffleArr)
        {
            for (int i = toShuffleArr.Count - 1; i > 0; i--)
            {
                int r = _random.Next(i + 1);

                T value = toShuffleArr[r];
                toShuffleArr[r] = toShuffleArr[i];
                toShuffleArr[i] = value;
            }

            return toShuffleArr;
        }
}

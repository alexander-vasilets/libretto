namespace Libretto.WPF.Utils;

using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;

public class PagedObservableCollection<T> : ICollection<T>, INotifyCollectionChanged
{
    private List<T> items;

    private int startIndex;
    private int endIndex;

    public readonly int ItemsPerPage;

    public int PageCount
    {
        get
        {
            int count = items.Count / ItemsPerPage;
            return count + 1;
        }
    }   

    private int currentPage;
    public int CurrentPage
    {
        get => currentPage;
        set
        {
            if (value < 0 || value > PageCount)
            {
                throw new ArgumentException("Page number is out of range!");
            }
            currentPage = value;
            startIndex = (value - 1) * ItemsPerPage;
            endIndex = value * ItemsPerPage;
            Items = items.Skip(startIndex).Take(ItemsPerPage).ToList();
            CollectionChanged?.Invoke(this, new(NotifyCollectionChangedAction.Reset));
        }
    }

    public int Count => items.Count;

    public bool IsReadOnly => false;

    public List<T> Items;

    public event NotifyCollectionChangedEventHandler? CollectionChanged;

    public PagedObservableCollection(int itemsPerPage)
    {
        items = new();
        Items = new();
        ItemsPerPage = itemsPerPage;
        CurrentPage = 1;
    }

    private bool OnCurrentPage(int index)
        => index >= startIndex && index < endIndex;

    public void Add(T item)
    {
        int index = items.Count;
        items.Add(item);
        if (OnCurrentPage(index))
        {
            Items.Add(item);
            CollectionChanged?.Invoke(this, new(NotifyCollectionChangedAction.Add, item, index - startIndex));
        }
    }

    public void Clear()
    {
        items.Clear();
        Items.Clear();
        CollectionChanged?.Invoke(this, new(NotifyCollectionChangedAction.Reset));
    }

    public bool Contains(T item)
        => Items.Contains(item);

    public void CopyTo(T[] array, int arrayIndex)
    {
        int i = arrayIndex;
        foreach(var item in items)
        {
            array[i++] = item;
        }
    }

    public bool Remove(T item)
    {
        int index = items.IndexOf(item);
        if (index == -1)
        {
            return false;
        }
        items.RemoveAt(index);
        if (OnCurrentPage(index))
        {
            Items.Remove(item);
            CollectionChanged?.Invoke(this, new(NotifyCollectionChangedAction.Remove, item, index - startIndex));
            if (items.Count > endIndex)
            {
                Items.Add(items[endIndex]);
                CollectionChanged?.Invoke(this, new(NotifyCollectionChangedAction.Add, item, endIndex - startIndex));
            }
        }
        return true;
    }

    public IEnumerator<T> GetEnumerator()
        => Items.GetEnumerator();

    IEnumerator IEnumerable.GetEnumerator()
        => Items.GetEnumerator();
}

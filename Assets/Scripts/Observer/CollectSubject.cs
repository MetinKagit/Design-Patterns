using System.Collections.Generic;
using UnityEngine;

public class CollectSubject : MonoBehaviour, ISubject<ItemCollected>
{
    private readonly List<IObserver<ItemCollected>> _observers = new();

    private int _carrot;
    private int _cauliflower;

    public int GetCount(ItemType type) =>
        type == ItemType.Carrot ? _carrot : _cauliflower;

    public void ReportCollect(ItemType type)
    {
        if (type == ItemType.Carrot)
            _carrot++;
        else
            _cauliflower++;

        Notify(new ItemCollected(type, GetCount(type)));
    }

    public void Subscribe(IObserver<ItemCollected> observer)
    {
        if (!_observers.Contains(observer)) _observers.Add(observer);

        observer.OnNotify(new ItemCollected(ItemType.Carrot, _carrot));
        observer.OnNotify(new ItemCollected(ItemType.Cauliflower, _cauliflower));
    }

    public void Unsubscribe(IObserver<ItemCollected> observer) => _observers.Remove(observer);

    public void Notify(ItemCollected data)
    {
        for (int i = _observers.Count - 1; i >= 0; i--)
            _observers[i].OnNotify(data);
    }
}

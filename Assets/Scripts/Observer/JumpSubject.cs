using System.Collections.Generic;
using UnityEngine;

public class JumpSubject : MonoBehaviour, ISubject<Jumped>
{
    private readonly List<IObserver<Jumped>> _observers = new();
    private int _totalJumps;
    public int TotalJumps => _totalJumps;

    public void ReportJump()
    {
        _totalJumps++;
        Notify(new Jumped(_totalJumps));
    }

    public void Subscribe(IObserver<Jumped> observer)
    {
        if (!_observers.Contains(observer)) _observers.Add(observer);
        observer.OnNotify(new Jumped(_totalJumps));
    }

    public void Unsubscribe(IObserver<Jumped> observer) => _observers.Remove(observer);

    public void Notify(Jumped data)
    {
        for (int i = _observers.Count - 1; i >= 0; i--)
            _observers[i].OnNotify(data);
    }
}

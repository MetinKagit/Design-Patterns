using System.Collections.Generic;
using UnityEngine;

public class MailboxSubject : MonoBehaviour, ISubject<MailboxChecked>
{
    private readonly List<IObserver<MailboxChecked>> _observers = new();
    [SerializeField] private bool done; // inspector’dan da bakılabilsin

    public bool Done => done;

    public void ReportMailboxChecked()
    {
        if (done) return;            
        done = true;
        Notify(new MailboxChecked(true));
    }

    public void Subscribe(IObserver<MailboxChecked> observer)
    {
        if (!_observers.Contains(observer)) _observers.Add(observer);// REPLAY: yeni aboneye mevcut durumu anında bildir
        observer.OnNotify(new MailboxChecked(done));
    }

    public void Unsubscribe(IObserver<MailboxChecked> observer) => _observers.Remove(observer);

    public void Notify(MailboxChecked data)
    {
        for (int i = _observers.Count - 1; i >= 0; i--)
            _observers[i].OnNotify(data);
    }
}
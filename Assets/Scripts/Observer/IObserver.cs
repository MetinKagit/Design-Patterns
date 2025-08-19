using System.Collections.Generic;

public interface IObserver<T>
{
    void OnNotify(T data);
}

public interface ISubject<T>
{
    void Subscribe(IObserver<T> observer);
    void Unsubscribe(IObserver<T> observer);
    void Notify(T data);
}

// --- Payload type ---
public enum ItemType { Carrot, Cauliflower }

public struct ItemCollected
{
    public ItemType Type;
    public int Total;
    public ItemCollected(ItemType type, int total)
    { Type = type; Total = total; }
}

public struct Jumped
{
    public int Total;
    public Jumped(int total) { Total = total; }
}

public struct MailboxChecked
{
    public bool Done;
    public MailboxChecked(bool done) { Done = done; }
}

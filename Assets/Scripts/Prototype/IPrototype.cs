using UnityEngine;

namespace PrototypeDemo
{
    public interface IPrototype<T>
    {
        T Clone();
    }

}
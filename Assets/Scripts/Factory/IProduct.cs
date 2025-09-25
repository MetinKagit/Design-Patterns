using UnityEngine;

public interface IProduct
{
    string ProductName { get; set; }

    void Initialize();
    void SetColor(Color color);
}

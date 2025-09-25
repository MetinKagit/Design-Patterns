using UnityEngine;

[RequireComponent(typeof(Renderer))]
public class Product : MonoBehaviour, IProduct
{
    [SerializeField] private string productName = "Product";
    public string ProductName { get => productName; set => productName = value; }

    [Header("Render Binding")]
    [SerializeField] private Renderer targetRenderer;

    
    private static MaterialPropertyBlock _mpb;

    private void Reset()
    {
        targetRenderer = GetComponent<Renderer>();
    }

    private void OnValidate()
    {
        if (!targetRenderer)
            targetRenderer = GetComponent<Renderer>();
    }

    public void Initialize()
    {
        gameObject.name = productName;
    }

    public void SetColor(Color color)
    {
        if (!targetRenderer) targetRenderer = GetComponent<Renderer>();
        if (targetRenderer == null) return;

        _mpb ??= new MaterialPropertyBlock();
        _mpb.Clear();

        _mpb.SetColor("_BaseColor", color);
        _mpb.SetColor("_Color",     color);

        targetRenderer.SetPropertyBlock(_mpb);
    }
}

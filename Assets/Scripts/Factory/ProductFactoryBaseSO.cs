using UnityEngine;

public abstract class ProductFactoryBaseSO : ScriptableObject
{
    [SerializeField] protected Product productPrefab; 

    public IProduct Create(Vector3 position, Quaternion rotation)
    {
        if (!productPrefab) throw new System.MissingFieldException("Prefab missing.");

        var go = Instantiate(productPrefab.gameObject, position, rotation);
        var product = go.GetComponent<IProduct>();
        if (product == null) throw new MissingComponentException("Prefab not contain IProduct.");

        product.Initialize();
        ApplyVariant(product); 
        return product;
    }

    protected abstract void ApplyVariant(IProduct product);
}

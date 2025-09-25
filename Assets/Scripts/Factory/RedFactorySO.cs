using UnityEngine;


[CreateAssetMenu(menuName="Factory Demo/Red Factory", fileName="RedFactory")]
public class RedFactorySO : ProductFactoryBaseSO
{
    [SerializeField] private ColorVariantSO variant; // Red
    protected override void ApplyVariant(IProduct product)
    {
        if (variant) product.SetColor(variant.Color);
    }
}
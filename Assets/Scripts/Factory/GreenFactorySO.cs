using UnityEngine;

[CreateAssetMenu(menuName="Factory Demo/Green Factory", fileName="GreenFactory")]
public class GreenFactorySO : ProductFactoryBaseSO
{
    [SerializeField] private ColorVariantSO variant; // Green
    protected override void ApplyVariant(IProduct product)
    {
        if (variant) product.SetColor(variant.Color);
    }
}

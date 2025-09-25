using UnityEngine;

[CreateAssetMenu(menuName = "Factory Demo/Color Variant", fileName = "ColorVariant")]
public class ColorVariantSO : ScriptableObject
{
    [SerializeField] private string displayName = "Blue";
    [SerializeField] private Color color = Color.cyan;

    public string DisplayName => displayName;
    public Color Color => color;
}

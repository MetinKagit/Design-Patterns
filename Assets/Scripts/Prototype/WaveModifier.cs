using UnityEngine;

[System.Serializable]
public class WaveModifier
{
    public string name;

    [Header("Stat Düzeltmeleri")]
    public int addHealth = 0;     
    public float mulSpeed = 1f;  

    [Header("Renk (opsiyonel override)")]
    public bool overrideColor = false;
    public Color color = Color.white;

    
    public void Apply(EnemyData data)
    {
        data.health += addHealth;
        data.speed *= Mathf.Max(0.1f, mulSpeed); 
        if (overrideColor) data.color = color;
    }
}

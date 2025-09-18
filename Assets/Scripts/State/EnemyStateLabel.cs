using UnityEngine;
using TMPro;

public class EnemyStateLabel : MonoBehaviour
{
    [SerializeField] private Game.AI.EnemyController enemy;
    [SerializeField] private TMP_Text tmpLabel;

    [Header("Colors")]
    [SerializeField] private Color wanderColor = Color.green;
    [SerializeField] private Color chaseColor = Color.red;
    [SerializeField] private Color frightColor = Color.cyan;

    void Awake()
    {
        if (enemy == null)
            enemy = FindFirstObjectByType<Game.AI.EnemyController>();

        if (tmpLabel == null)
            tmpLabel = GetComponent<TMP_Text>();
    }

    void OnEnable()
    {
        if (enemy == null) return;
        enemy.OnStateChanged += HandleStateChanged;
        // Initial update
        HandleStateChanged(enemy.CurrentStateName);
    }

    void OnDisable()
    {
        if (enemy != null)
            enemy.OnStateChanged -= HandleStateChanged;
    }

    private void HandleStateChanged(string stateName)
    {
        string txt = $"State: {stateName}";
        SetText(txt);
        SetColorFor(stateName);
    }

    private void SetText(string text)
    {
        if (tmpLabel != null)
            tmpLabel.text = text;
    }

    private void SetColorFor(string stateName)
    {
        Debug.Log(stateName);

        Color color;

        if (stateName.Contains("Wander"))
            color = wanderColor;       
        else if (stateName.Contains("Chase"))        
            color = chaseColor;        
        else if (stateName.Contains("Frightened"))
            color = frightColor;      
        else
            color = Color.white;       
        
        if (tmpLabel != null)
            tmpLabel.color = color;

    }
}

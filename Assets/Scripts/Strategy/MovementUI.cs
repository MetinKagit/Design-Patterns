using UnityEngine;
using UnityEngine.UI;
public class MovementUI : MonoBehaviour
{
    [SerializeField] private Mover mover;
    [SerializeField] private Button walkButton;
    [SerializeField] private Button runButton;
    [SerializeField] private Button zigzagButton;

    void Awake()
    {
        walkButton.onClick.AddListener(mover.BeginWalk);
        runButton.onClick.AddListener(mover.BeginRun);
        zigzagButton.onClick.AddListener(mover.BeginZigZag);
    }
}

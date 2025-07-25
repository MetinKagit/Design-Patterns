using UnityEngine;

public class InputHandler : MonoBehaviour
{
    public CommandManager cmdManager;
    public Player player;
    void Update()
    {
        if (player.IsMoving) return;
        
        if (Input.GetKeyDown(KeyCode.W))
            TryMove(Vector3.forward);
        if (Input.GetKeyDown(KeyCode.S))
            TryMove(Vector3.back);
        if (Input.GetKeyDown(KeyCode.A))
            TryMove(Vector3.left);
        if (Input.GetKeyDown(KeyCode.D))
            TryMove(Vector3.right);

        // Undo
        if (Input.GetKeyDown(KeyCode.Q))
            cmdManager.Undo();

        // Redo
        if (Input.GetKeyDown(KeyCode.E))
            cmdManager.Redo();
    }

    void TryMove(Vector3 dir)
    {

        float step = player.step;
        Vector3 targetPos = player.transform.position + dir * step;

        Ray ray = new Ray(targetPos + Vector3.up * 2f, Vector3.down);
        if (Physics.Raycast(ray, out RaycastHit hit, 5f))
        {
            if (hit.collider.CompareTag("GreenTile") || hit.collider.CompareTag("Star"))
            {
                var cmd = new MoveCommand(player, dir * step);
                cmdManager.ExecuteCommand(cmd);
            }
            else
            {
                Debug.Log("⛔ Sadece yeşil karolardan ilerleyebilirsin!");
            }
        }
    }
}

using System.Collections.Generic;
using UnityEngine;

public class CommandManager : MonoBehaviour
{
    private List<ICommand> history = new List<ICommand>();

    private int current = 0;

    public void ExecuteCommand(ICommand cmd)
    {
        if (current < history.Count)
            // If ececute new command after undo, remove all commands after the current position
            history.RemoveRange(current, history.Count - current);

        cmd.Execute();
        history.Add(cmd);
        current++;
    }

    public void Undo()
    {
        if (current > 0)
        {
            current--;
            history[current].Undo();
        }
    }

    public void Redo()
    {
        if (current >= history.Count) return;
        history[current].Execute();
        current++;
    }
    
     public List<ICommand> GetHistory()
    {
        return history;
    }
}

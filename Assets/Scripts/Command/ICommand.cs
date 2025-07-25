using UnityEngine;

public interface ICommand
{
    /// <summary>Triggers the action performed by the command.</summary>
    void Execute();

    /// <summary>Undoes the action performed by the command.</summary>
    void Undo();

}

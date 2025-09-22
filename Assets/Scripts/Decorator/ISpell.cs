public interface ISpell
{
    int Damage { get; }       
    int ManaCost { get; }    
    string Label { get; }

    void Cast();  
}
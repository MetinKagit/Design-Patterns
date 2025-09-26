using System;

public sealed class HealthModel
{
    public int Max { get; private set; }
    public int Current { get; private set; }
    public bool IsDead => Current <= 0;
    public bool IsFull => Current >= Max;

    public event Action<int, int> OnChanged;


    public HealthModel(int max, int start)
    {
        if (max < 1)
            throw new ArgumentOutOfRangeException(nameof(max), "Max must >= 1.");
        Max = max;
        Current = Clamp(start, 0, Max);
        OnChanged?.Invoke(Current, Max);
    }

    public void Damage(int amount)
    {
        if (amount <= 0 || IsDead)
            return;

        SetCurrent(Current - amount);
    }

    public void Heal(int amount)
    {
        if (amount <= 0 || IsFull)
            return;

        SetCurrent(Current + amount);
    }

    public void SetMax(int newMax, bool keepRatio = false)
    {
        if (newMax < 1)
            throw new ArgumentOutOfRangeException(nameof(newMax));

        if (newMax == Max)
            return;

        if (keepRatio && Max > 0)
        {
            float ratio = (float)Current / Max;
            Max = newMax;
            SetCurrent((int)MathF.Round(newMax * ratio), invokeIfUnchanged: true);
        }
        else
        {
            Max = newMax;
            SetCurrent(Current, invokeIfUnchanged: true);
        }
    }

    private void SetCurrent(int value, bool invokeIfUnchanged = false)
    {
        int clamped = Clamp(value, 0, Max);
        if (!invokeIfUnchanged && clamped == Current)
            return;

        Current = clamped;
        OnChanged?.Invoke(Current, Max);
    }

    private static int Clamp(int v, int min, int max)
    {
        if (v < min)
            return min;

        if (v > max)
            return max;
            
        return v;
    }
}

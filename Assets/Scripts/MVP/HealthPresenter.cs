using System;

public sealed class HealthPresenter : IDisposable
{
    private readonly HealthModel model;
    private readonly IHealthView view;
    private readonly int step;
    private bool disposed;

    public HealthPresenter(HealthModel model, IHealthView view, int step = 10)
    {
        this.model = model ?? throw new ArgumentNullException(nameof(model));
        this.view  = view  ?? throw new ArgumentNullException(nameof(view));

        if (step <= 0)
            throw new ArgumentOutOfRangeException(nameof(step), "step must> 0.");

        this.step = step;

        view.OnDamageClicked += HandleDamageClicked;
        view.OnHealClicked   += HandleHealClicked;
        
        model.OnChanged += UpdateView;

        UpdateView(model.Current, model.Max);
    }

    private void HandleDamageClicked()
    {
        if (disposed)
            return;

        model.Damage(step);
    }

    private void HandleHealClicked()
    {
        if (disposed)
            return;

        model.Heal(step);
    }

    private void UpdateView(int current, int max)
    {
        if (disposed)
            return;

        float n = max <= 0 ? 0f : (float)current / max;
        view.SetBar(n);
        view.SetText($"{current} / {max}");
    }

    public void Dispose()
    {
        if (disposed)
            return;
            
        disposed = true;

        // Unsubscribe 
        view.OnDamageClicked -= HandleDamageClicked;
        view.OnHealClicked   -= HandleHealClicked;
        model.OnChanged      -= UpdateView;
    }
}

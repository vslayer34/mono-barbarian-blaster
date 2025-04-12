using Godot;
using System;

public partial class HomeBase : Node3D
{
    [Export]
    private int _maxHitPoints;

    private int _currentHitPoints;

    private Label3D _hitPointsLabel;


    // Game Loop Methods---------------------------------------------------------------------------

    public override void _Ready()
    {
        _hitPointsLabel = GetNode<Label3D>("HitPointsLabel");
        CurrentHitPoints = _maxHitPoints;
    }

    // Member Methods------------------------------------------------------------------------------
    public void TakeDamage()
    {
        GD.Print("Oh no the tower is hit");
        CurrentHitPoints--;
    }

    private void SetHitPointsLabel<T>(T hitPoints) => _hitPointsLabel.Text = $"{hitPoints}";

    // Getters & Setters---------------------------------------------------------------------------

    public int CurrentHitPoints
    {
        get => _currentHitPoints;
        set
        {
            _currentHitPoints = Mathf.Clamp(value, 0, _maxHitPoints);
            SetHitPointsLabel(_currentHitPoints);

            if (_currentHitPoints < 1)
            {
                GetTree().ReloadCurrentScene();
            }
        }
    }
}

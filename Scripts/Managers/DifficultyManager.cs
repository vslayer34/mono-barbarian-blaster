using Godot;
using System;

public partial class DifficultyManager : Node
{
    [Export]
    private float _gameLength = 30.0f;

    [Export]
    private Timer _gameDuratonTimer;

    [Export]
    private Curve _spawnRatioCurve;



    // Game Loop Methods---------------------------------------------------------------------------

    public override void _Ready()
    {
        _gameDuratonTimer.Start(_gameLength);
    }

    public override void _Process(double delta)
    {
        GD.Print(SpawnTime);
    }

    // Member Methods------------------------------------------------------------------------------

    // Getters & Setters---------------------------------------------------------------------------

    public float GameProgressRatio
    {
        get
        {
            return 1.0f - (float) (_gameDuratonTimer.TimeLeft / _gameDuratonTimer.WaitTime);
        }
    }

    public float SpawnTime
    {
        get => _spawnRatioCurve.Sample(GameProgressRatio);
    }
}

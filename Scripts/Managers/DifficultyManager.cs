using Godot;
using System;

public partial class DifficultyManager : Node
{
    [Signal]
    public delegate void RoundTimePassedEventHandler();

    [Export]
    private float _gameLength = 30.0f;

    [Export]
    private Timer _gameDuratonTimer;

    [Export]
    private Curve _spawnRatioCurve;
    
    [Export]
    private Curve _enemyHealthCurve;



    // Game Loop Methods---------------------------------------------------------------------------

    public override void _Ready()
    {
        _gameDuratonTimer.Start(_gameLength);
        _gameDuratonTimer.Timeout += StopSpawningEnemies;
    }

    // Signal Methods------------------------------------------------------------------------------

    private void StopSpawningEnemies()
    {
        EmitSignal(SignalName.RoundTimePassed);
    }

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

    public float EnemyHealth
    {
        get => _enemyHealthCurve.Sample(GameProgressRatio);
    }
}

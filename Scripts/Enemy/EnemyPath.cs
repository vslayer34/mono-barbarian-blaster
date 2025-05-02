using Godot;
using System;

public partial class EnemyPath : Path3D
{
    [Export]
    private PackedScene _enemyScene;

    [Export]
    private Timer _spawnTimer;

    [Export]
    private DifficultyManager _difficulityManager;



    // Game Loop Methods---------------------------------------------------------------------------

    public override void _Ready()
    {
        _spawnTimer.Timeout += SpawnEnemy;
        _difficulityManager.RoundTimePassed += StopSpawning;
    }

    public override void _ExitTree()
    {
        _spawnTimer.Timeout -= SpawnEnemy;
        _difficulityManager.RoundTimePassed -= StopSpawning;
    }



    // Member Methods------------------------------------------------------------------------------

    private void SpawnEnemy()
    {
        Enemy enemy = _enemyScene.Instantiate<Enemy>();
        enemy.MaxHealth = _difficulityManager.EnemyHealth;
        AddChild(enemy);
        _spawnTimer.WaitTime = _difficulityManager.SpawnTime;

        GD.Print(_difficulityManager.EnemyHealth);
    }

    // Signal Methods------------------------------------------------------------------------------

    private void StopSpawning()
    {
        _spawnTimer.Stop();
    }
}

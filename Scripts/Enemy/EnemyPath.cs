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
    }

    public override void _ExitTree()
    {
        _spawnTimer.Timeout -= SpawnEnemy;
    }



    // Member Methods------------------------------------------------------------------------------

    private void SpawnEnemy()
    {
        Enemy enemy = _enemyScene.Instantiate<Enemy>();
        AddChild(enemy);
        _spawnTimer.WaitTime = _difficulityManager.SpawnTime;
    }
}

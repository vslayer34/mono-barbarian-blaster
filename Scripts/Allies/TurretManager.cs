using BarbarianBlaster.Managers;
using Godot;
using System;
using System.Threading.Tasks;

public partial class TurretManager : Node3D
{
    [Export]
    private PackedScene _defenceTower;

    public Path3D LevelPath { get; private set; }



    // Game Loop Methods---------------------------------------------------------------------------

    public override async void _Ready()
    {
        await ToSignal(Owner, SignalName.Ready);
        LevelPath = GetOwner<LevelManager>().LevelPath;
    }


    // Member Methods------------------------------------------------------------------------------

    public void SpawnTurretAtPosition(Vector3 spawnPosition)
    {
        var createdTower = _defenceTower.Instantiate<Tower>();
        AddChild(createdTower);

        createdTower.GlobalPosition = spawnPosition;
    }
}

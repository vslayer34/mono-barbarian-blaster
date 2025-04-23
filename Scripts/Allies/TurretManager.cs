using Godot;
using System;

public partial class TurretManager : Node3D
{
    [Export]
    private PackedScene _defenceTower;



    // Game Loop Methods---------------------------------------------------------------------------

    // Member Methods------------------------------------------------------------------------------

    public void SpawnTurretAtPosition(Vector3 spawnPosition)
    {
        var createdTower = _defenceTower.Instantiate<Node3D>();
        AddChild(createdTower);
        createdTower.GlobalPosition = spawnPosition;
    }
}

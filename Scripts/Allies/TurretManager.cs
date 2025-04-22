using Godot;
using System;

public partial class TurretManager : Node3D
{
    [Export]
    private PackedScene _defenceTower;



    // Game Loop Methods---------------------------------------------------------------------------

    public override void _Ready()
    {
        var createdTower = _defenceTower.Instantiate();
        AddChild(createdTower);
    }

}

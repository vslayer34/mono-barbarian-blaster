using Godot;
using System;

public partial class Tower : Node3D
{
    [Export]
    private PackedScene _projectileScene;

    [Export]
    private Marker3D _projectileSpawnPosition;

    private Timer _fireRateTimer;



    // Game Loop Methods---------------------------------------------------------------------------

    public override void _Ready()
    {
        _fireRateTimer = GetNode<Timer>("FireRateTimer");
        _fireRateTimer.Timeout += ShootProjectile;
    }

    public override void _ExitTree()
    {
        _fireRateTimer.Timeout -= ShootProjectile;
    }


    // Signal Methods------------------------------------------------------------------------------

    private void ShootProjectile()
    {
        var projectile = _projectileScene.Instantiate() as Node3D;
        AddChild(projectile);
        projectile.GlobalPosition = _projectileSpawnPosition.GlobalPosition;
    }

}

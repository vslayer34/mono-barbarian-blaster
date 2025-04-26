using Godot;
using System;

public partial class Tower : Node3D
{
    [Export]
    private PackedScene _projectileScene;

    [Export]
    private Marker3D _projectileSpawnPosition;

    private Timer _fireRateTimer;

    private Path3D _levelPath;



    // Game Loop Methods---------------------------------------------------------------------------

    public override void _Ready()
    {
        _fireRateTimer = GetNode<Timer>("FireRateTimer");
        _fireRateTimer.Timeout += ShootProjectile;

        _levelPath = GetParent<TurretManager>().LevelPath;
    }

    public override void _PhysicsProcess(double delta)
    {

        LookAt((_levelPath?.GetChildren()[_levelPath.GetChildren().Count - 1] as Node3D).GlobalPosition, Vector3.Up, true);
    }


    public override void _ExitTree()
    {
        _fireRateTimer.Timeout -= ShootProjectile;
    }


    // Signal Methods------------------------------------------------------------------------------

    private void ShootProjectile()
    {
        var projectile = _projectileScene.Instantiate() as Projectile;
        AddChild(projectile);

        projectile.GlobalPosition = _projectileSpawnPosition.GlobalPosition;
        projectile.ForwardDirection = GlobalTransform.Basis.Z;
        // projectile.Basis = Basis;
        projectile.Rotation = GlobalRotation;
    }

}

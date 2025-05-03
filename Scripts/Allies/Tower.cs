using BarbarianBlaster.Helper.Constants;
using Godot;
using System;

public partial class Tower : Node3D
{
    [Export]
    private float _towerRadius = 10.0f;

    [Export]
    private PackedScene _projectileScene;

    [Export]
    private Node3D _cannon;

    [Export]
    private Marker3D _projectileSpawnPosition;

    [Export]
    private AnimationPlayer _animator;

    private Timer _fireRateTimer;

    private Path3D _levelPath;

    private PathFollow3D currentTarget;



    // Game Loop Methods---------------------------------------------------------------------------

    public override void _Ready()
    {
        _fireRateTimer = GetNode<Timer>("FireRateTimer");
        _fireRateTimer.Timeout += ShootProjectile;

        _levelPath = GetParent<TurretManager>().LevelPath;
    }

    public override void _PhysicsProcess(double delta)
    {
        currentTarget = GetNearestTarget();

        if (currentTarget != null)
        {
            _cannon.LookAt(currentTarget.GlobalPosition, Vector3.Up, true);
        }
    }


    public override void _ExitTree()
    {
        _fireRateTimer.Timeout -= ShootProjectile;
    }

    // Member Methods------------------------------------------------------------------------------

    private PathFollow3D GetNearestTarget()
    {
        PathFollow3D nearestEnemy = null;
        float higherProgress = 0;

        foreach (var target in _levelPath?.GetChildren())
        {
            if (target is Enemy enemy)
            {
                if ((enemy.Progress > higherProgress) && (GlobalPosition.DistanceTo(enemy.GlobalPosition) < _towerRadius))
                {
                    higherProgress = enemy.Progress;
                    nearestEnemy = enemy;
                }
            }
        }
        return nearestEnemy;
    }

    // Signal Methods------------------------------------------------------------------------------

    private void ShootProjectile()
    {
        if (currentTarget == null)
        {
            return;
        }

        var projectile = _projectileScene.Instantiate() as Projectile;
        AddChild(projectile);

        projectile.GlobalPosition = _projectileSpawnPosition.GlobalPosition;
        projectile.ForwardDirection = _cannon.GlobalTransform.Basis.Z;
        // projectile.Basis = Basis;
        projectile.Rotation = _cannon.GlobalRotation;
        
        _animator.Play(AnimationConsts.Turret.SHOOT_SHELL);
    }

}

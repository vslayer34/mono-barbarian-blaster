using Godot;
using System;

public partial class Projectile : Area3D
{
    private Vector3 _forwardDirection = Vector3.Forward;

    [Export]
    private float _speed = 30.0f;

    private Timer _lifeSpanTimer;



    // Game Loop Methods---------------------------------------------------------------------------

    public override void _Ready()
    {
        _lifeSpanTimer = GetNode<Timer>("LifeSpanTimer");
        _lifeSpanTimer.Timeout += SelfDestruct;
    }

    public override void _ExitTree()
    {
        _lifeSpanTimer.Timeout -= SelfDestruct;
    }


    public override void _PhysicsProcess(double delta)
    {
        Position += _forwardDirection * _speed * (float)delta;
    }

    // Signal Methods------------------------------------------------------------------------------

    private void SelfDestruct()
    {
        QueueFree();
    }
}

using Godot;
using System;

public partial class Projectile : Area3D
{
    public Vector3 ForwardDirection { get; set; } = Vector3.Forward;

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
        Position += ForwardDirection * _speed * (float)delta;
    }

    // Signal Methods------------------------------------------------------------------------------

    private void SelfDestruct()
    {
        QueueFree();
    }
}

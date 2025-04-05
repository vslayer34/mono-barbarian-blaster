using Godot;
using System;

public partial class Enemy : PathFollow3D
{
    [Export]
    private float _speed = 2.5f;



    // Game Loop Methods---------------------------------------------------------------------------

    public override void _Process(double delta)
    {
        Progress += (float)delta * _speed;
    }

}

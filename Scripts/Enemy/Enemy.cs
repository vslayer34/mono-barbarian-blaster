using BarbarianBlaster.Helper.Groups;
using Godot;
using System;

public partial class Enemy : PathFollow3D
{
    [Export]
    private float _speed = 2.5f;

    private HomeBase _homeBase;



    // Game Loop Methods---------------------------------------------------------------------------

    public override void _Ready()
    {
        _homeBase = GetTree().GetFirstNodeInGroup(SC_Groups.HOME_BASE) as HomeBase;
    }


    public override void _Process(double delta)
    {
        Progress += (float)delta * _speed;

        if (ProgressRatio >= 1.0f)
        {
            _homeBase.TakeDamage();
        }
    }

}

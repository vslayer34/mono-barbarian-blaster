using Godot;
using System;

public partial class RayPickerCamera : Camera3D
{
    [Export]
    private float _rayDistance = 100.0f;

    private RayCast3D _rayCaster;
    private Vector2 _mousePosition;



    // Game Loop Methods---------------------------------------------------------------------------

    public override void _Ready()
    {
        _rayCaster = GetNode<RayCast3D>("RayCast3D");
    }

    public override void _Process(double delta)
    {
        _mousePosition = GetViewport().GetMousePosition();

        _rayCaster.TargetPosition = ProjectLocalRayNormal(_mousePosition) *_rayDistance;
        _rayCaster.ForceRaycastUpdate();

        GD.PrintT(_rayCaster.GetCollider(), _rayCaster.GetCollisionPoint());

        GD.Print(_rayCaster.GetCollider());
        GD.Print(_rayCaster.GetCollisionPoint());
    }


    // Member Methods------------------------------------------------------------------------------
}

using BarbarianBlaster.Helper.Constants;
using Godot;
using System;

public partial class RayPickerCamera : Camera3D
{
    [Export]
    private float _rayDistance = 100.0f;

    private RayCast3D _rayCaster;
    private Vector2 _mousePosition;

    private GridMap _gridMap;



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

        if (_rayCaster.IsColliding())
        {
            Input.SetDefaultCursorShape(Input.CursorShape.PointingHand);
            
            if (_rayCaster.GetCollider() is GridMap)
            {
                if (Input.IsActionJustPressed(InputConst.CLICK))
                {
                    LevelGridMap = _rayCaster.GetCollider() as GridMap;
                    var cellPosition = LevelGridMap.LocalToMap(_rayCaster.GetCollisionPoint());
                    GD.Print(cellPosition);

                    if (LevelGridMap.GetCellItem(cellPosition) == 0)
                    {
                        LevelGridMap.SetCellItem(cellPosition, 1);
                    }
                }
            }   
        }
        else
        {
            Input.SetDefaultCursorShape(Input.CursorShape.Arrow);
        }
    }


    // Member Methods------------------------------------------------------------------------------

    // Getters & Setters---------------------------------------------------------------------------

    public GridMap LevelGridMap
    {
        get => _gridMap; 
        set
        {
            _gridMap ??= value;
        }
    }
}

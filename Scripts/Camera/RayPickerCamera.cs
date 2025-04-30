using BarbarianBlaster.Helper.Constants;
using BarbarianBlaster.Helper.Groups;
using BarbarianBlaster.Managers;
using Godot;
using System;
using System.Threading.Tasks;

public partial class RayPickerCamera : Camera3D
{
    [Export]
    private float _rayDistance = 100.0f;

    [Export]
    private int _turretPirce = 100;

    private RayCast3D _rayCaster;
    private Vector2 _mousePosition;

    private GridMap _gridMap;

    private LevelManager _levelManager;

    private Bank _bank;



    // Game Loop Methods---------------------------------------------------------------------------

    public override async void _Ready()
    {
        _rayCaster = GetNode<RayCast3D>("RayCast3D");

        await ToSignal(Owner, SignalName.Ready);
        _levelManager = GetOwner<LevelManager>();
        _bank = GetTree().GetFirstNodeInGroup(SC_Groups.BANK) as Bank;
    }

    public override void _Process(double delta)
    {
        _mousePosition = GetViewport().GetMousePosition();

        _rayCaster.TargetPosition = ProjectLocalRayNormal(_mousePosition) *_rayDistance;
        _rayCaster.ForceRaycastUpdate();

        if (_rayCaster.IsColliding())
        {
            if (_bank.CanPurchase(_turretPirce))
            {
                Input.SetDefaultCursorShape(Input.CursorShape.PointingHand);
            
                if (_rayCaster.GetCollider() is GridMap)
                {
                    if (Input.IsActionJustPressed(InputConsts.CLICK))
                    {
                        LevelGridMap = _rayCaster.GetCollider() as GridMap;
                        var cellPosition = LevelGridMap.LocalToMap(_rayCaster.GetCollisionPoint());
                        GD.Print(cellPosition);

                        if (LevelGridMap.GetCellItem(cellPosition) == 0)
                        {
                            LevelGridMap.SetCellItem(cellPosition, 1);
                            _levelManager.BuildTurret(LevelGridMap.MapToLocal(cellPosition));
                        }

                        _bank.RemoveGoldFromBank(_turretPirce);
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

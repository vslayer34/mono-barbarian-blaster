using Godot;
using System;


namespace BarbarianBlaster.Managers;
public partial class LevelManager : Node3D
{
    [Export]
    private TurretManager _turretManager;



    // Game Loop Methods---------------------------------------------------------------------------

    // Memeber Methods-----------------------------------------------------------------------------

    public void BuildTurret(Vector3 position)
    {
        _turretManager.SpawnTurretAtPosition(position);
    }
}

using Godot;
using System;


namespace BarbarianBlaster.UI;
public partial class VictoryLayer : CanvasLayer
{
    [Export]
    private Button _retryBtn;
    
    [Export]
    private Button _quitBtn;



    // Game Loop Methods---------------------------------------------------------------------------

    public override void _Ready()
    {
        _retryBtn.Pressed += () =>
        {
            GetTree().ReloadCurrentScene();
        };

        _quitBtn.Pressed += () =>
        {
            GetTree().Quit();
        };
    }

}

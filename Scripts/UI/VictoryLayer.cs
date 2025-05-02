using BarbarianBlaster.Helper.Groups;
using Godot;
using Godot.Collections;
using System;


namespace BarbarianBlaster.UI;
public partial class VictoryLayer : CanvasLayer
{
    [ExportCategory("Required Nodes")]
    
    [Export]
    private TextureRect[] _winStars = new TextureRect[3];

    [Export]
    private Label[] _winConditionLabels = new Label[3];

    [ExportCategory("")]
    [Export]
    private Button _retryBtn;
    
    [Export]
    private Button _quitBtn;



    // Game Loop Methods---------------------------------------------------------------------------

    public override void _Ready()
    {
        Visible = false;

        _winStars[1].Modulate = Colors.Black;
        _winStars[2].Modulate = Colors.Black;

        _winConditionLabels[1].Visible = false;
        _winConditionLabels[2].Visible = false;

        _retryBtn.Pressed += () =>
        {
            GetTree().ReloadCurrentScene();
        };

        _quitBtn.Pressed += () =>
        {
            GetTree().Quit();
        };
    }

    // Member Methods------------------------------------------------------------------------------

    public void DisplayVictoryScreen()
    {
        Visible = true;
        ShowStarAndCondition();

        if (GetTree().GetFirstNodeInGroup(SC_Groups.HOME_BASE) is HomeBase homeBase && homeBase.IsGameFinishedWithFullHealth)
        {
            ShowStarAndCondition(2);
        }

        if (GetTree().GetFirstNodeInGroup(SC_Groups.BANK) is Bank bank && bank.IsGoldPlenty)
        {
            ShowStarAndCondition(3);
        }
    }

    private void ShowStarAndCondition(int starNumber = 1)
    {
        _winStars[starNumber - 1].Modulate = Colors.White;
        _winConditionLabels[starNumber - 1].Visible = true;
    }
}

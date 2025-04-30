using Godot;
using System;

public partial class Bank : MarginContainer
{
    [ExportCategory("Required Nodes")]

    [Export]
    private Label _goldAmountLabel;
    [ExportCategory("")]
    
    private int _goldAmount = 150;
    
    
    [Export(PropertyHint.Range, "0,999999,")]
    public int GoldAmount
    {
        get => _goldAmount;
        set
        {
            _goldAmount = value <= 0 ? 0 : value;

            if (_goldAmountLabel != null)
            {
                _goldAmountLabel.Text = $"Gold: {_goldAmount}";
            }
        }
    }



    // Game Loop Methods---------------------------------------------------------------------------

    public override void _Ready()
    {
        _goldAmountLabel.Text = $"Gold: {_goldAmount}";
    }

    // Member Methods------------------------------------------------------------------------------

    public void AddGoldToBank(int amount)
    {
        GoldAmount += amount;
    }

    public void RemoveGoldFromBank(int amount)
    {
        GoldAmount -= amount;
    }

    public bool CanPurchase(int price)
    {
        if (price <= GoldAmount)
        {
            return true;
        }

        return false;
    }
}

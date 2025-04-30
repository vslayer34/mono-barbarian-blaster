using BarbarianBlaster.Helper.Constants;
using BarbarianBlaster.Helper.Groups;
using Godot;
using System;

public partial class Enemy : PathFollow3D
{
    [ExportCategory("Required Nodes")]
    [Export]
    private Area3D _enemyArea;

    [ExportCategory("")]

    [Export]
    private float _speed = 2.5f;

    [Export]
    private int _rewardGoldAmount = 15;

    [Export]
    private float _maxHealth = 50.0f;

    [Export]
    private AnimationPlayer _animator;


    private float _currentHealth;
    private HomeBase _homeBase;
    private Bank _bank;



    // Game Loop Methods---------------------------------------------------------------------------

    public override void _Ready()
    {
        _currentHealth = _maxHealth;
        _homeBase = GetTree().GetFirstNodeInGroup(SC_Groups.HOME_BASE) as HomeBase;
        _bank = GetTree().GetFirstNodeInGroup(SC_Groups.BANK) as Bank;
    }

    public override void _ExitTree()
    {
        base._ExitTree();
    }

    public override void _Process(double delta)
    {
        Progress += (float)delta * _speed;

        if (ProgressRatio >= 1.0f)
        {
            _homeBase.TakeDamage();
            SetProcess(false);
        }
    }

    // Member Methods------------------------------------------------------------------------------

    public void TakeDamage(float damageAmount)
    {
        _currentHealth -= damageAmount;
        _animator.Play(AnimationConsts.Enemy.DAMAGE_PLAYER);
        
        if (_currentHealth < 1.0f)
        {
            _bank.AddGoldToBank(_rewardGoldAmount);
            QueueFree();
        } 
    }

    // Setters & Getters---------------------------------------------------------------------------
}

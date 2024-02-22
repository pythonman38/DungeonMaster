using System;
using System.Linq;
using Godot;

public partial class EnemyChaseState : EnemyState
{
    [Export] private Timer chaseTimer;

    private CharacterBody3D target;

    protected override void EnterState()
    {
        character.AnimationPlayer.Play(GameConstants.ANIM_MOVE);
        target = character.ChaseAreaNode.GetOverlappingBodies().First() as CharacterBody3D;

        chaseTimer.Timeout += HandleTimeout;
        character.AttackAreaNode.BodyEntered += HandleAttackAreaBodyEntered;
        character.ChaseAreaNode.BodyExited += HandleAttackAreaBodyExited;
    }

    protected override void ExitState()
    {
        chaseTimer.Timeout -= HandleTimeout;
        character.AttackAreaNode.BodyEntered -= HandleAttackAreaBodyEntered;
        character.ChaseAreaNode.BodyExited -= HandleAttackAreaBodyExited;
    }

    public override void _PhysicsProcess(double delta)
    {
        Move();
    }

    private void HandleTimeout()
    {
        destination = target.GlobalPosition;
        character.AgentNode.TargetPosition = destination;
    }

    private void HandleAttackAreaBodyEntered(Node3D body)
    {
        character.StateMachine.SwitchState<EnemyAttackState>();
    }

    private void HandleAttackAreaBodyExited(Node3D body)
    {
        character.StateMachine.SwitchState<EnemyReturnState>();
    }
}

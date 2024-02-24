using System;
using Godot;

public abstract partial class EnemyState : CharacterState
{
    protected Vector3 destination;

    public override void _Ready()
    {
        base._Ready();

        character.GetStatResource(Stat.Health).OnZero += HandleZeroHealth;
    }

    protected Vector3 GetPointsGlobalPosition(int index)
    {
        Vector3 localPos = character.PathNode.Curve.GetPointPosition(index);
        Vector3 globalPos = character.PathNode.GlobalPosition;
        return localPos + globalPos;
    }

    protected void Move()
    {
        character.AgentNode.GetNextPathPosition();
        character.Velocity = character.GlobalPosition.DirectionTo(destination);
        character.MoveAndSlide();
        character.Flip();
    }

    protected void HandleChaseAreaBodyEntered(Node3D body)
    {
        character.StateMachine.SwitchState<EnemyChaseState>();
    }

    private void HandleZeroHealth()
    {
        character.StateMachine.SwitchState<EnemyDeathState>();
    }
}
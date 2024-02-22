using Godot;

public partial class EnemyReturnState : EnemyState
{
    public override void _Ready()
    {
        base._Ready();

        destination = GetPointsGlobalPosition(0);
    }

    public override void _PhysicsProcess(double delta)
    {
        if (character.AgentNode.IsNavigationFinished())
        {
            character.StateMachine.SwitchState<EnemyPatrolState>();
        }

        Move();
    }

    protected override void EnterState()
    {
        character.AnimationPlayer.Play(GameConstants.ANIM_MOVE);
        character.AgentNode.TargetPosition = destination;
        character.ChaseAreaNode.BodyEntered += HandleChaseAreaBodyEntered;
    }

    protected override void ExitState()
    {
        character.ChaseAreaNode.BodyEntered -= HandleChaseAreaBodyEntered;
    }
}

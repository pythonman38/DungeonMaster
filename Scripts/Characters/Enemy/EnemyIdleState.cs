public partial class EnemyIdleState : EnemyState
{
    protected override void EnterState()
    {
        character.AnimationPlayer.Play(GameConstants.ANIM_IDLE);
        character.ChaseAreaNode.BodyEntered += HandleChaseAreaBodyEntered;
    }

    protected override void ExitState()
    {
        character.ChaseAreaNode.BodyEntered -= HandleChaseAreaBodyEntered;
    }

    public override void _PhysicsProcess(double delta)
    {
        character.StateMachine.SwitchState<EnemyReturnState>();
    }
}

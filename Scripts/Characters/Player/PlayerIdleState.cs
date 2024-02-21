using Godot;

public partial class PlayerIdleState : PlayerState
{
    public override void _PhysicsProcess(double delta)
    {
        if (player.direction != Vector2.Zero)
        {
            player.StateMachine.SwitchState<PlayerMoveState>();
        }
    }

    public override void _Input(InputEvent @event)
    {
        if (Input.IsActionJustPressed(GameConstants.INPUT_DASH))
        {
            player.StateMachine.SwitchState<PlayerDashState>();
        }
    }

    protected override void EnterState()
    {
        player.AnimationPlayer.Play(GameConstants.ANIM_IDLE);
    }
}

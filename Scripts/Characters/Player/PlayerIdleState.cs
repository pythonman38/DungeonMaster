using Godot;

public partial class PlayerIdleState : PlayerState
{
    public override void _PhysicsProcess(double delta)
    {
        if (character.direction != Vector2.Zero)
        {
            character.StateMachine.SwitchState<PlayerMoveState>();
        }
    }

    public override void _Input(InputEvent @event)
    {
        if (Input.IsActionJustPressed(GameConstants.INPUT_DASH))
        {
            character.StateMachine.SwitchState<PlayerDashState>();
        }
    }

    protected override void EnterState()
    {
        character.AnimationPlayer.Play(GameConstants.ANIM_IDLE);
    }
}

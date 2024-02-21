using Godot;

public partial class PlayerMoveState : PlayerState
{
    [Export(PropertyHint.Range, "0, 20, 0.1")] private float walkSpeed = 5;

    public override void _PhysicsProcess(double delta)
    {
        if (player.direction == Vector2.Zero)
        {
            player.StateMachine.SwitchState<PlayerIdleState>();
        }

        SetPlayerWalkSpeed();

        player.MoveAndSlide();

        player.Flip();
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
        player.AnimationPlayer.Play(GameConstants.ANIM_MOVE);
    }

    public void SetPlayerWalkSpeed()
    {
        player.Velocity = new(player.direction.X, 0, player.direction.Y);
        player.Velocity *= walkSpeed;
    }
}

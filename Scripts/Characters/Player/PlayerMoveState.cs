using Godot;

public partial class PlayerMoveState : PlayerState
{
    [Export(PropertyHint.Range, "0, 20, 0.1")] private float walkSpeed = 5;

    public override void _PhysicsProcess(double delta)
    {
        if (character.direction == Vector2.Zero)
        {
            character.StateMachine.SwitchState<PlayerIdleState>();
        }

        SetPlayerWalkSpeed();

        character.MoveAndSlide();

        character.Flip();
    }

    public override void _Input(InputEvent @event)
    {
        CheckForAttackInput();

        if (Input.IsActionJustPressed(GameConstants.INPUT_DASH))
        {
            character.StateMachine.SwitchState<PlayerDashState>();
        }
    }

    protected override void EnterState()
    {
        character.AnimationPlayer.Play(GameConstants.ANIM_MOVE);
    }

    public void SetPlayerWalkSpeed()
    {
        character.Velocity = new(character.direction.X, 0, character.direction.Y);
        character.Velocity *= walkSpeed;
    }
}

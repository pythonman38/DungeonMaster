using Godot;

public partial class PlayerDashState : PlayerState
{
    [Export] private Timer dashTimer;
    [Export(PropertyHint.Range, "0, 20, 0.1")] private float dashSpeed = 10;

    public override void _Ready()
    {
        base._Ready();

        dashTimer.Timeout += HandleDashTimeout;
    }

    public override void _PhysicsProcess(double delta)
    {
        character.MoveAndSlide();
        character.Flip();
    }

    private void HandleDashTimeout()
    {
        character.Velocity = Vector3.Zero;
        character.StateMachine.SwitchState<PlayerIdleState>();
    }

    protected override void EnterState()
    {
        character.AnimationPlayer.Play(GameConstants.ANIM_DASH);
        character.Velocity = new(character.direction.X, 0, character.direction.Y);
        if (character.Velocity == Vector3.Zero) character.Velocity = character.CharacterSprite.FlipH ? Vector3.Left : Vector3.Right;
        character.Velocity *= dashSpeed;
        dashTimer.Start();
    }
}

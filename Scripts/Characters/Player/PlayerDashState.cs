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
        player.MoveAndSlide();
        player.Flip();
    }

    private void HandleDashTimeout()
    {
        player.Velocity = Vector3.Zero;
        player.StateMachine.SwitchState<PlayerIdleState>();
    }

    protected override void EnterState()
    {
        player.AnimationPlayer.Play(GameConstants.ANIM_DASH);
        player.Velocity = new(player.direction.X, 0, player.direction.Y);
        if (player.Velocity == Vector3.Zero) player.Velocity = player.PlayerSprite.FlipH ? Vector3.Left : Vector3.Right;
        player.Velocity *= dashSpeed;
        dashTimer.Start();
    }
}

using Godot;

public partial class Player : CharacterBody3D
{
    [ExportGroup("Required Nodes")]
    [Export] private AnimationPlayer animationPlayer;
    [Export] private Sprite3D playerSprite;

    private Vector2 direction = new();

    public override void _Ready()
    {
        animationPlayer.Play(GameConstants.ANIM_IDLE);
    }

    public override void _PhysicsProcess(double delta)
    {
        SetPlayerSpeed();

        MoveAndSlide();

        Flip();
    }

    public override void _Input(InputEvent @event)
    {
        direction = Input.GetVector
        (
            GameConstants.INPUT_MOVE_LEFT,
            GameConstants.INPUT_MOVE_RIGHT,
            GameConstants.INPUT_MOVE_FORWARD,
            GameConstants.INPUT_MOVE_BACKWARD
        );

        if (direction == Vector2.Zero) animationPlayer.Play(GameConstants.ANIM_IDLE);
        else animationPlayer.Play(GameConstants.ANIM_MOVE);
    }

    private void SetPlayerSpeed()
    {
        Velocity = new(direction.X, 0, direction.Y);
        Velocity *= 5;
    }

    private void Flip()
    {
        if (Velocity.X == 0) return;
        playerSprite.FlipH = Velocity.X < 0;
    }
}

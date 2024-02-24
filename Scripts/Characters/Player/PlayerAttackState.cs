using Godot;

public partial class PlayerAttackState : PlayerState
{
    [Export] private Timer attackTimer;

    private int comboCounter = 1, maxComboCount = 2;

    public override void _Ready()
    {
        base._Ready();

        attackTimer.Timeout += () => comboCounter = 1;
    }

    protected override void EnterState()
    {
        character.AnimationPlayer.Play(GameConstants.ANIM_ATTACK + comboCounter, -1, 1.5f);

        character.AnimationPlayer.AnimationFinished += HandleAnimationFinished;
    }

    protected override void ExitState()
    {
        character.AnimationPlayer.AnimationFinished -= HandleAnimationFinished;
        attackTimer.Start();
    }

    private void HandleAnimationFinished(StringName animName)
    {
        comboCounter++;
        comboCounter = Mathf.Wrap(comboCounter, 1, maxComboCount + 1);
        character.ToggleHitBox(true);

        if (character.direction != Vector2.Zero)
        {
            character.StateMachine.SwitchState<PlayerMoveState>();
        }
        else character.StateMachine.SwitchState<PlayerIdleState>();
    }

    private void PerformHit()
    {
        Vector3 newPosition = character.CharacterSprite.FlipH ? Vector3.Left : Vector3.Right;
        float distanceMultiplier = 0.75f;
        character.MakeDamageArea.Position = newPosition * distanceMultiplier;
        character.ToggleHitBox(false);
    }
}

using Godot;

public partial class EnemyAttackState : EnemyState
{
    protected override void EnterState()
    {
        character.AnimationPlayer.Play(GameConstants.ANIM_ATTACK);
    }
}

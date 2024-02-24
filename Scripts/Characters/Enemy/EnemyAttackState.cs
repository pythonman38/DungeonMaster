using System.Linq;
using Godot;

public partial class EnemyAttackState : EnemyState
{
    private Vector3 targetPosition;

    protected override void EnterState()
    {
        character.AnimationPlayer.Play(GameConstants.ANIM_ATTACK);

        Node3D target = character.AttackAreaNode.GetOverlappingBodies().First();
        targetPosition = target.GlobalPosition;

        character.AnimationPlayer.AnimationFinished += HandleAnimationFinished;
    }

    protected override void ExitState()
    {
        character.AnimationPlayer.AnimationFinished -= HandleAnimationFinished;
    }

    private void HandleAnimationFinished(StringName animName)
    {
        character.ToggleHitBox(true);

        Node3D target = character.AttackAreaNode.GetOverlappingBodies().FirstOrDefault();
        if (target == null)
        {
            Node3D chaseTarget = character.ChaseAreaNode.GetOverlappingBodies().FirstOrDefault();
            if (chaseTarget == null) character.StateMachine.SwitchState<EnemyReturnState>();
            else character.StateMachine.SwitchState<EnemyChaseState>();
        }
        else 
        {
            character.AnimationPlayer.Play(GameConstants.ANIM_ATTACK);
            targetPosition = target.GlobalPosition;
            Vector3 direction = character.GlobalPosition.DirectionTo(targetPosition);
            character.CharacterSprite.FlipH = direction.X < 0;
        }
    }

    private void PerformHit()
    {
        character.ToggleHitBox(false);
        character.AreaHitBox.GlobalPosition = targetPosition;
    }
}

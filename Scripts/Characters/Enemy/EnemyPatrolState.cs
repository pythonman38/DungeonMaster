using Godot;

public partial class EnemyPatrolState : EnemyState
{
    [Export] private Timer idleTimer;
    [Export(PropertyHint.Range, "0, 20, 0.1")] private float maxIdleTime = 4;

    private int pointIndex = 0;


    protected override void EnterState()
    {
        character.AnimationPlayer.Play(GameConstants.ANIM_MOVE);

        pointIndex = 1;
        destination = GetPointsGlobalPosition(pointIndex);
        character.AgentNode.TargetPosition = destination;

        character.AgentNode.NavigationFinished += HandleNavigationFinished;
        idleTimer.Timeout += HandleTimeout;
        character.ChaseAreaNode.BodyEntered += HandleChaseAreaBodyEntered;
    }

    protected override void ExitState()
    {
        character.AgentNode.NavigationFinished -= HandleNavigationFinished;
        idleTimer.Timeout -= HandleTimeout;
        character.ChaseAreaNode.BodyEntered -= HandleChaseAreaBodyEntered;
    }

    public override void _PhysicsProcess(double delta)
    {
        if (idleTimer.IsStopped()) Move();
    }

    private void HandleNavigationFinished()
    {
        character.AnimationPlayer.Play(GameConstants.ANIM_IDLE);

        RandomNumberGenerator rng = new();
        idleTimer.WaitTime = rng.RandfRange(0, maxIdleTime);
        idleTimer.Start();
    }

    private void HandleTimeout()
    {
        character.AnimationPlayer.Play(GameConstants.ANIM_MOVE);

        pointIndex = Mathf.Wrap(pointIndex + 1, 0, character.PathNode.Curve.PointCount);
        destination = GetPointsGlobalPosition(pointIndex);
        character.AgentNode.TargetPosition = destination;
    }
}

using Godot;

public partial class Character : CharacterBody3D
{
    [ExportGroup("Required Nodes")]
    [Export] public AnimationPlayer AnimationPlayer { get; private set; }
    [Export] public Sprite3D CharacterSprite { get; private set; }
    [Export] public StateMachine StateMachine { get; private set; }

    [ExportGroup("AI Nodes")]
    [Export] public Path3D PathNode { get; private set; }
    [Export] public NavigationAgent3D AgentNode { get; private set; }
    [Export] public Area3D ChaseAreaNode { get; private set; }
    [Export] public Area3D AttackAreaNode { get; private set; }

    public Vector2 direction = new();

    public void Flip()
    {
        if (Velocity.X == 0) return;
        CharacterSprite.FlipH = Velocity.X < 0;
    }
}
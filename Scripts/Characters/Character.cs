using System.Linq;
using Godot;

public partial class Character : CharacterBody3D
{
    [Export] private StatResource[] stats;

    [ExportGroup("Required Nodes")]
    [Export] public AnimationPlayer AnimationPlayer { get; private set; }
    [Export] public Sprite3D CharacterSprite { get; private set; }
    [Export] public StateMachine StateMachine { get; private set; }
    [Export] public Area3D TakeDamageArea { get; private set; }
    [Export] public Area3D MakeDamageArea { get; private set; }
    [Export] public CollisionShape3D AreaHitBox { get; private set; }

    [ExportGroup("AI Nodes")]
    [Export] public Path3D PathNode { get; private set; }
    [Export] public NavigationAgent3D AgentNode { get; private set; }
    [Export] public Area3D ChaseAreaNode { get; private set; }
    [Export] public Area3D AttackAreaNode { get; private set; }

    public Vector2 direction = new();

    public override void _Ready()
    {
        TakeDamageArea.AreaEntered += HandleTakeDamageEntered;
    }

    private void HandleTakeDamageEntered(Area3D area)
    {
        StatResource health = GetStatResource(Stat.Health);

        Character player = area.GetOwner<Character>();

        health.StatValue -= player.GetStatResource(Stat.Strength).StatValue;

        GD.Print(health.StatValue);
    }

    public StatResource GetStatResource(Stat stat)
    {
        return stats.Where((element) => element.StatType == stat).FirstOrDefault();
    }

    public void Flip()
    {
        if (Velocity.X == 0) return;
        CharacterSprite.FlipH = Velocity.X < 0;
    }

    public void ToggleHitBox(bool flag)
    {
        AreaHitBox.Disabled = flag;
    }
}
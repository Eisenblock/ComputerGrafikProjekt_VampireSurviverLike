using OpenTK.Mathematics;

public abstract class Entity
{
    public Vector2 Position { get; set; }
    public abstract bool IsPlayer { get; }

    public Vector2 health { get; set; }
    public DateTime LastCollision { get; set; } = DateTime.MinValue;
}
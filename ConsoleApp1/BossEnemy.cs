using OpenTK.Mathematics;
using OpenTK.Graphics.OpenGL;
internal class BossEnemy : Enemy
{
    public Color4 Color { get; set; } = Color4.Red;

    public float speed = 0.000005f;
    public BossEnemy(Vector2 pos, bool dead, int dmg) : base(pos, dead, dmg)
    {
        health = 5;
        max_Health = health;
        size = 0.5f;
        // Konstruktor der Unterklasse. Ruft den Konstruktor der Basisklasse auf.
    }

    public override void Draw(float scale)
    {
        base.Draw(scale); 
    }

    public void Update(Vector2 targetPosition)
    {
        MoveTowards(targetPosition, speed); 
    }
}
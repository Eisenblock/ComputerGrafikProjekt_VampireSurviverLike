using OpenTK.Mathematics;
using OpenTK.Graphics.OpenGL;
internal class BossEnemy : Enemy
{
    public Color4 Color { get; set; } = Color4.Red;

    public float speed = 0.000005f;
    public BossEnemy(Vector2 pos, bool dead, int dmg) : base(pos, dead, dmg)
    {
        health = 5;
        size = 0.2f;
        // Konstruktor der Unterklasse. Ruft den Konstruktor der Basisklasse auf.
    }

    public override void Draw(Color4 color)
    {
        base.Draw(Color); 
    }

    public void Update(Vector2 targetPosition)
    {
        MoveTowards(targetPosition, speed); 
    }
}
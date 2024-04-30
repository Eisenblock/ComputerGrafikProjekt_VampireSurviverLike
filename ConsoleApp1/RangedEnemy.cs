using OpenTK.Mathematics;
using OpenTK.Graphics.OpenGL;
internal class RangedEnemy : Enemy
{
    public Color4 Color { get; set; } = Color4.Orange;

    public float speed = 0.00015f;
    public RangedEnemy(Vector2 pos, bool dead,int dmg) : base(pos, dead,dmg)
    {
        // Konstruktor der Unterklasse. Ruft den Konstruktor der Basisklasse auf.
    }

    public override void Draw(Color4 color)
    {
        base.Draw(Color); 
    }

    public void Update(Vector2 targetPosition)
    {
        MoveAway(targetPosition, speed); 
    }
}
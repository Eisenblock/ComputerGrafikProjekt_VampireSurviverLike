using OpenTK.Mathematics;
using OpenTK.Graphics.OpenGL;
internal class BigEnemy : Enemy
{
    public Color4 Color { get; set; } = Color4.Red;

    public float speed = 0.00005f;
    public BigEnemy(Vector2 pos, bool dead) : base(pos, dead)
    {
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
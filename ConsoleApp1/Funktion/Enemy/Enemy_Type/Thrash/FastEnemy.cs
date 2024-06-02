using OpenTK.Mathematics;
using OpenTK.Graphics.OpenGL;
internal class FastEnemy : Enemy
{
    public Color4 Color { get; set; } = Color4.Purple;

    public float speed = 0.0001f;
    public FastEnemy(Vector2 pos, bool dead, int dmg, Vector2 _range) : base(pos, dead, dmg, _range)
    {
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
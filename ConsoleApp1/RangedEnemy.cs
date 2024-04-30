using OpenTK.Mathematics;
using OpenTK.Graphics.OpenGL;
 
 internal class RangedEnemy : Enemy
{
    public Color4 Color { get; set; } = Color4.Pink;

    public float speed = 0.00015f;
    public RangedEnemy(Vector2 pos, bool dead,int dmg) : base(pos, dead,dmg)
    {
        // Konstruktor der Unterklasse. Ruft den Konstruktor der Basisklasse auf.
    }

    public void Update(Vector2 targetPosition)
    {
        MoveAway(targetPosition, speed); 
    }
}
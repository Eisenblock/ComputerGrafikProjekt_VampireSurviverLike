using OpenTK.Mathematics;
internal class FastEnemy : Enemy
{
    public Color4 Color { get; set; } = Color4.Purple;

    public float speed = 0.0001f;
    public FastEnemy(Vector2 pos, bool dead, int dmg, Vector2 _range, List<int> particlesList) : base(pos, dead, dmg, _range, particlesList)
    {
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
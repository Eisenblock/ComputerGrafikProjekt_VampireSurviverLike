using OpenTK.Mathematics;
internal class BigEnemy : Enemy
{
    public Color4 Color { get; set; } = Color4.Green;

    public float speed = 0.00005f;
    public BigEnemy(Vector2 pos, bool dead, int dmg, Vector2 _range, List<int> particlesList) : base(pos, dead, dmg, _range, particlesList)
    {
        health = 2;
        size = 0.15f;
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
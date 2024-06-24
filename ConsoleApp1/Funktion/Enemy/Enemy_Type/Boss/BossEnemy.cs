using OpenTK.Mathematics;
internal class BossEnemy : Enemy
{
    public float speed = 0.000005f;
    public BossEnemy(Vector2 pos, bool dead, int dmg, Vector2 _range, List<int>particlesList) : base(pos, dead, dmg, _range, particlesList)
    {
        health = 6;
        max_Health = health;
        size = 0.4f;
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
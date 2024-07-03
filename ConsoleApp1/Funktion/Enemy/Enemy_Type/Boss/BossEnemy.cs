using OpenTK.Mathematics;
internal class BossEnemy : Enemy
{
    public float speed = 1f;
    public BossEnemy(Vector2 pos, bool dead, int dmg, Vector2 _range, int max_Health, List<int>particlesList) : base(pos, dead, dmg, _range, particlesList)
    {
        this.max_Health = max_Health;
        health = max_Health;
        size = 0.4f;
    }

    public override void Draw(float scale)
    {
        base.Draw(scale); 
    }

    public void Update(Vector2 targetPosition)
    {
        MoveTowards(targetPosition, 0.5f); 
    }
}
using OpenTK.Mathematics;
internal class RangedEnemy : Enemy
{
    public Color4 Color { get; set; } = Color4.Pink;

    public float speed = 0.00015f;
    Gun gun = new Gun();
    public RangedEnemy(Vector2 pos, bool dead, int dmg, Vector2 _range,  List<int> particlesList) : base(pos, dead, dmg, _range, particlesList)
    {

    }

    public void Update(Vector2 targetPosition)
    {
        MoveAway(targetPosition, speed);
        gun.Update(this, targetPosition);
    }
    public void DrawGun()
    {
        gun.Draw();
    }


}
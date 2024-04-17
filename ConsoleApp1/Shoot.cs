using OpenTK.Mathematics;
using OpenTK.Graphics.OpenGL;

internal class Shoot
{
    public bool shootBool = false;
    private Player player;
    private Vector2 shootPos;
    private Vector2 targetPos;
    private double lifetime;
    private double lastPrintedTime = 0;   
    private float scale;
    private double lastShootTime = 0; 
    Circle boundShoot = new Circle(Vector2.Zero,0.1f);


    public Shoot(Player player)
    {
        this.player = player;
        shootPos = this.player.Position;
        boundShoot = new Circle(shootPos, 0.1f);
    }

    float SetScale()
    {
        return GlobalSettings.AspectRatio;
    }


    void Circle(Vector2 pos, float radius, int segments)
    {
        scale = SetScale();
        
        GL.Begin(PrimitiveType.TriangleFan);
        GL.Color4(Color4.Aqua);
        GL.Vertex2(pos.X, pos.Y); // Mitte des Kreises

        for (int i = 0; i <= segments; i++)
        {
            double theta = 2.0 * Math.PI * i / segments;
            float dx = (float)(radius * Math.Cos(theta) / scale);
            float dy = (float)(radius * Math.Sin(theta));
            GL.Vertex2(pos.X + dx, pos.Y + dy);
        }

        GL.End();
    }

    public void Draw()
    {
        if(shootBool == true)
        { 
            Circle(shootPos, 0.1f, 32); // Zeichnet einen Kreis mit Radius 0.1 und 32 Segmenten
            ShootDirection(targetPos, 0.0001f);
            DrawCircle(shootPos, 0.1f, 32);
        }
    }


    public void ShootDirection(Vector2 targetPosition, float speed)
    {
        if(shootBool == true)
        {
            Vector2 direction = targetPosition - shootPos;
            direction = Vector2.Normalize(direction);
            shootPos += direction * speed;
        }
        else
        {
            
        }      
    }

    public void PlayerShoots(Vector2 target,double currentTime )
    {        
        if(shootBool != true)
        {
            targetPos = target;
            lastShootTime = currentTime;
        }
              
        shootBool = true;
       
    }

    public void UpdateTimerShoot(double timer)
    {

        if (timer - lastShootTime >= 0.25)
        {
            shootBool = false; // Setzen Sie shootBool auf false, da die Lebensdauer abgelaufen ist
            shootPos = player.Position;
        }
    }

    private void DrawCircle(Vector2 center, float radius, int segments)
    {
        GL.Begin(PrimitiveType.LineLoop);
        GL.Color4(Color4.Green);
        for (int i = 0; i < segments; i++)
        {
            float angle = i / (float)segments * 2.0f * MathF.PI;
            float x = center.X + radius * MathF.Cos(angle);
            float y = center.Y + radius * MathF.Sin(angle);
            GL.Vertex2(x, y);
        }
        GL.End();
    }

    public bool CheckCollision(Enemy enemy)
    {
        if (enemy == null){
            Console.WriteLine("Enemy is null");
            return false;
        }

        float distanceSquared = 1;
        float radiusSumSquared = 0;
              
        if (enemy != null && shootBool == true )
        {
            Console.WriteLine(shootPos);
            Console.WriteLine(enemy.Position);
            distanceSquared = (shootPos - enemy.Position).LengthSquared;
            radiusSumSquared = (boundShoot.Radius + enemy.boundEnemy.Radius) * (boundShoot.Radius + enemy.boundEnemy.Radius);
        }
        return distanceSquared < radiusSumSquared;
    }
}
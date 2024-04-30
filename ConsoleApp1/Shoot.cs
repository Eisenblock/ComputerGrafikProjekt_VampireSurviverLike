using OpenTK.Mathematics;
using OpenTK.Graphics.OpenGL;

internal class Shoot
{
    public bool shootBool = false;
    public Entity entity;
    public bool isLive = false;
    public Vector2 shootPos;
    public Vector2 targetPos;
    public double lifetime;
    public double lastPrintedTime ;   
    private float scale;
    private double lastShootTime = 0;
    public Circle boundShoot;
    public bool shotbyPlayer;


    public Shoot(Entity entity,Vector2 target, double time,double timeStart,bool shootBool,bool isLive)
    {
        this.entity = entity;
        this.shootBool = shootBool;
        this.lifetime = time;
        this.lastPrintedTime = timeStart;
        shootPos = entity.Position;
        targetPos = target;
        this.isLive = isLive;
        boundShoot = new Circle(shootPos, 0.1f);
        if(entity.IsPlayer)
            this.shotbyPlayer = true;
        else
            this.shotbyPlayer = false;
    }

    float SetScale()
    {
        return GlobalSettings.AspectRatio;
    }


    void Circle(Vector2 pos, float radius, int segments)
    {
        if(shotbyPlayer == true)
            GL.Color4(Color4.Aqua);
        else
            GL.Color4(Color4.Red);

        scale = SetScale();
        GL.Begin(PrimitiveType.TriangleFan);
        GL.Vertex2(pos.X, pos.Y); // Mitte des Kreises

        for (int i = 0; i <= segments; i++)
        {
            double theta = 2.0 * Math.PI * i / segments;
            float dx = (float)(radius * Math.Cos(theta) / scale / 1.8);
            float dy = (float)(radius * Math.Sin(theta) / 1.8);
            GL.Vertex2(pos.X + dx, pos.Y + dy);
        }

        GL.End();
    }

    public void Draw()
    {
        Circle(shootPos, 0.1f, 32); // Zeichnet einen Kreis mit Radius 0.1 und 32 Segmenten         
        DrawCircle(shootPos, 0.1f, 32);
        
    }


    public void ShootDirection(float speed)
    {
            Vector2 direction = targetPos - shootPos;
            direction = Vector2.Normalize(direction);
            shootPos += direction * speed;
            targetPos += direction * speed;
            boundShoot.Center = shootPos;
    }

    

    private void DrawCircle(Vector2 center, float radius, int segments)
    {
        scale = SetScale();
        GL.Begin(PrimitiveType.LineLoop);
        GL.Color4(Color4.Green);
        for (int i = 0; i < segments; i++)
        {
            float angle = i / (float)segments * 2.0f * MathF.PI;
            float x = center.X + radius * MathF.Cos(angle) / scale / 1.8f;
            float y = center.Y + radius * MathF.Sin(angle) / 1.8f;
            GL.Vertex2(x, y);
        }
        GL.End();
    }

}
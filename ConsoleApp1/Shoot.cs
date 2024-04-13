using OpenTK.Mathematics;
using OpenTK.Graphics.OpenGL;
using static System.Formats.Asn1.AsnWriter;

internal class Shoot
{

    Player player = new Player();
 
    Vector2 shootPos;
    Vector2 targetPos;

    public bool shootBool = false;

    private double lifetime;
    private double lastPrintedTime = 0;   
    private float scale;
    private double lastShootTime = 0;


    public Shoot()
    {
        shootPos = player.Position;
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
            Console.WriteLine("Kannst net schießen");
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
            Console.WriteLine("TimerUpdate" + timer);
            shootBool = false; // Setzen Sie shootBool auf false, da die Lebensdauer abgelaufen ist
            shootPos = player.Position;
        }
    }
}
using OpenTK.Mathematics;
using OpenTK.Graphics.OpenGL;
using System;
using System.Drawing;
using NAudio.Wave;

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
    public double lastShootTime;
    public Circle boundShoot;
    public bool shotbyPlayer;
    public int TextureID { get; private set; } // Hier speichern wir die Textur-ID
    public string Texture { get; private set; }
    Texturer texturer = new Texturer(); // Create an instance of the Texturer class
    SoundsPlayer soundsPlayer = new SoundsPlayer(); // Create an instance of the SoundsPlayer class


    public Shoot(Entity entity, Vector2 target, double time, double timeStart, bool shootBool, bool isLive)
    {
        Texture = "assets/sBullet.png";
        TextureID = texturer.LoadTexture(Texture, 1)[0]; // Call the LoadTexture method on the instance

        this.entity = entity;
        this.shootBool = shootBool;
        this.lifetime = time;
        this.lastPrintedTime = timeStart;
        shootPos = entity.Position;
        targetPos = target;
        this.isLive = isLive;
        boundShoot = new Circle(shootPos, 0.1f);
        if (entity.IsPlayer)
        {
            this.shotbyPlayer = true;
            var shoot_sound = "assets/Shoot.wav";
            soundsPlayer.PlaySoundAsync(shoot_sound, false);
        }      
        else
        {
            this.shotbyPlayer = false;
        }
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
        if(shotbyPlayer == true)
            GL.Color4(Color4.Green);
        else
            GL.Color4(Color4.Red);

        var rect = new RectangleF(shootPos.X-0.05f, shootPos.Y-0.05f, 0.1f, 0.1f);
        var tex_rect = new RectangleF(0f, 0f, 1f, 1f);

        if (isLive == true)
        {
            texturer.Draw(TextureID, rect, tex_rect); // Hintergrundbild zeichnen   
            DrawCircle(shootPos, 0.1f, 32);
        }
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
            float x = center.X + radius * MathF.Cos(angle) / scale / 2.8f;
            float y = center.Y + radius * MathF.Sin(angle) / 2.8f;
            GL.Vertex2(x, y);
        }
        GL.End();
    }

}
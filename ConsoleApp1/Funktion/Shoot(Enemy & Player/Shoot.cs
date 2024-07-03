using OpenTK.Mathematics;
using OpenTK.Graphics.OpenGL;
using System.Drawing;

internal class Shoot
{
    //instances of other classes
    Texturer texturer = new Texturer(); // Create an instance of the Texturer class
    SoundsPlayer soundsPlayer = new SoundsPlayer(); // Create an instance of the SoundsPlayer class

    //variables
    public bool shootBool = false;
    public Entity entity;
    public bool isLive = false;
    public Vector2 shootPos;
    public Vector2 targetPos;
    public double lifetime;
    public double lastPrintedTime;   
    private float scale;
    public Circle boundShoot;
    public bool shotbyPlayer;
    float time = 0f;

    //variables for the animation
    public int TextureID { get; private set; } 
    public string Texture { get; private set; }

    public Shoot(Entity entity, Vector2 target, double time, double timeStart, bool shootBool, bool isLive)
    {
        //load the textures
        Texture = "assets/sBullet.png";
        TextureID = texturer.LoadTexture(Texture, 1,1)[0];

        this.entity = entity;
        this.shootBool = shootBool;
        this.lifetime = time;
        this.lastPrintedTime = timeStart;
        shootPos = entity.Position;
        targetPos = target;
        this.isLive = isLive;
        boundShoot = new Circle(shootPos, 0.1f);
        scale = SetScale();
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
    
    public void ShootDirection(float speed)
    {
        Vector2 direction = targetPos - shootPos;
        direction = Vector2.Normalize(direction);
        speed *= time;
        shootPos += direction * speed;
        targetPos += direction * speed;
        boundShoot.Center = shootPos;
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
            //DrawCircle(shootPos, 0.1f, 32);
        }
    }

    private void DrawCircle(Vector2 center, float radius, int segments)
    {
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

    public void GetTimer(float timer)
    {
        timer = timer - time + 1 ;
        time += (float)timer;
    }
}
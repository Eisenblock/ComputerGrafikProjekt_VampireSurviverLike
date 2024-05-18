using OpenTK.Mathematics;
using OpenTK.Graphics.OpenGL;
using System.Drawing.Drawing2D;
using System.Drawing.Printing;
using System.Drawing;
using System.Drawing.Imaging;
using ImageMagick;
using Image = System.Drawing.Image;


internal class Player : Entity
{
    public static Vector2 WindowSize => Program.WindowSize;
    public List<int> TextureID_Idle { get; private set; } // Hier speichern wir die Textur-ID/ Hier speichern wir die Textur-ID
    public override bool IsPlayer => true;
    public float radius_col;
    public float PositionX; 
    public float PositionY;
    public float scale;
    
    public float speed = 0.00015f;
    public Circle bounds = new Circle(Vector2.Zero,0);
    public bool playerDead;
    public Color4 color = Color4.Blue;
    bool ismoving = false;
    public List<int> TextureID_Run { get; private set; } // Hier speichern wir die Textur-ID
    public List<int> current_TextureID; // Hier speichern wir die Textur-ID

    Texturer Texturer = new Texturer(); // Create an instance of the Texturer class


    public Player()
    {
        string Texture_Idle = "assets/topdown_shooter_assets/sPlayer_Idle.png";
        TextureID_Idle = Texturer.LoadTexture(Texture_Idle,4); // Call the LoadTexture method on the instance
        string Texture_Run = "assets/topdown_shooter_assets/sPlayer_Run.png";
        TextureID_Run = Texturer.LoadTexture(Texture_Run,7); // Call the LoadTexture method on the instance
        current_TextureID = TextureID_Idle;

        Position = new Vector2(0.0f, 0.0f);
        bounds = new Circle(Position, 0.065f);
        PositionX = Position.X;
        PositionY = Position.Y;
        max_Health = 6;
        health = 6;
        playerDead = false;
    }
    public void ClearAll()
    {
        Position = Vector2.Zero;
        playerDead = false;
        health = 5;
    }

    public Vector2 getPlayerPosition()
    {
        return Position;
    }

    internal void Stop()
    {
        ismoving = false;
    }

    internal void Left()
    {
        ismoving = true;
        float newX = Position.X - speed;
        if (newX >= -0.85)
        {
            Position = new Vector2(newX, Position.Y);
        }
    }

    internal void Right()
    {
        ismoving = true;
        float newX = Position.X + speed;
        if (newX <= 0.85)
        {
            Position = new Vector2(newX, Position.Y);
        }
    }

    internal void Up()
    {
        ismoving = true;
        float newY = Position.Y + speed;
        if (newY <= 0.85)
        {
            Position = new Vector2(Position.X, newY);
        }
    }

    internal void Down()
    {
        ismoving = true;
        float newY = Position.Y - speed;
        if (newY >= -0.85)
        {
            Position = new Vector2(Position.X, newY);
        }
    }

    float SetScale()
    {
        return GlobalSettings.AspectRatio;
    }

    void Circle(Vector2 pos, float radius, int segments, Color4 color)
    {
        scale = SetScale();
            
        GL.Begin(PrimitiveType.TriangleFan);
        GL.Color4(color);
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


    public void Draw(double time, Vector2 mouse)
    {
        var isFacingRight = true;
        if (!playerDead)
        {
            GL.Color4(Color4.White);
            var hittime = DateTime.Now - LastCollision;
            if(hittime.TotalSeconds <= 0.3){
                GL.Color4(Color4.Red);
            }
            var rect = new RectangleF(Position.X-0.05f, Position.Y-0.05f, 0.15f, 0.15f);
            var tex_rect = new RectangleF(0, 0, 1, 1);
            
            // Flipped texture coordinates
            var flipped_rect = new Rectangle( 1, 0, -1, 1);

            // Use the normal or flipped texture coordinates based on the direction the player is facing
            if (mouse.X < Position.X){
                isFacingRight = false;
            }
            var currentTexCoords = isFacingRight ? tex_rect : flipped_rect;

            // Check if enough time has passed since the last frame change
            if (time - lastFrameTime >= frameDuration)
            {
                // Determine the new texture ID
                List<int> new_TextureID = ismoving ? TextureID_Run : TextureID_Idle;

                // If the texture ID has changed, reset the current frame
                if (new_TextureID != current_TextureID)
                {
                    currentFrame = 0;
                }
                // Update the current texture ID
                current_TextureID = new_TextureID;

                // Update the current frame
                currentFrame = (currentFrame + 1) % current_TextureID.Count;

                // Update the time of the last frame change
                lastFrameTime = time;
            }
            Texturer.Draw(current_TextureID[currentFrame], rect, currentTexCoords);
            DrawCircle(Position, bounds.Radius, 32);    
        }
    }

    private void DrawCircle(Vector2 center, float radius, int segments)
    {
        bounds.Center = Position;
        scale = SetScale();
        GL.Begin(PrimitiveType.LineLoop);
        GL.Color4(Color4.Green);
        for (int i = 0; i < segments; i++)
        {
            float angle = i / (float)segments * 2.0f * MathF.PI;
            float x = center.X+0.025f + radius * MathF.Cos(angle) / scale;
            float y = center.Y+0.02f+ + radius * MathF.Sin(angle);
            GL.Vertex2(x, y);
        }
        GL.End();
    }

    public void IncreaseHealth()
    {
        health++;
        Console.WriteLine("Life" +  health);
    }

    public void DecreaseHealth(int dmg)
    {
        health -= dmg;
        Console.WriteLine("Life: " + health);
        if(health <= 0) 
        {
            playerDead = true;
            Console.WriteLine("Player Dead");
        }
    }
}
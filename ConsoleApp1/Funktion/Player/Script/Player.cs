using OpenTK.Mathematics;
using OpenTK.Graphics.OpenGL;
using System.Drawing;

internal class Player : Entity
{
    //instances of other classes
    Texturer Texturer = new Texturer();
    SoundsPlayer SoundsPlayer = new SoundsPlayer(); 

    //variables    
    public override bool IsPlayer => true;
    public float scale;
    public float speed = 0.00015f;
    public Circle bounds;
    public bool playerDead = false;
    bool ismoving = false;

    //variables for the animation
    public List<int> TextureID_Run { get; private set; } 
    public List<int> TextureID_Idle { get; private set; } 
    public List<int> current_TextureID; 

    //variables for the sound
    string damage_sound;
    string death_sound;

    public Player()
    {
        // Load the textures
        string Texture_Idle = "assets/sPlayer_Idle.png";
        TextureID_Idle = Texturer.LoadTexture(Texture_Idle,4,1); 
        string Texture_Run = "assets/sPlayer_Run.png";
        TextureID_Run = Texturer.LoadTexture(Texture_Run,7,1);
        current_TextureID = TextureID_Idle;
        // Load the sounds
        damage_sound = "assets/Damage.wav";
        death_sound = "assets/GameOver.wav";

        bounds = new Circle(Position, 0.065f);
        max_Health = 6;
        health = 6;
        scale = SetScale();
    }
    public void ClearAll()
    {
        Position = Vector2.Zero;
        playerDead = false;
        health = max_Health;
        lastShoottime = 0;
    }

    internal void Stop()
    {
        ismoving = false;
    }

    public async Task DecreaseHealth(int dmg)
    {
        health -= dmg;
        await SoundsPlayer.PlaySoundAsync(damage_sound, false);
        if(health <= 0) 
        {
            playerDead = true;
            await SoundsPlayer.PlaySoundAsync(death_sound, false);
        }
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
    private void DrawCircle(Vector2 center, float radius, int segments)
    {
        bounds.Center = Position;
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
    public void Draw(double time, Vector2 mouse)
    {
        bounds.Center = Position;
        var isFacingRight = true;
        if (!playerDead)
        {
            GL.Color4(Color4.White);
            var hittime = DateTime.Now - LastCollision;
            // Change the color of the player if it was hit recently
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
            //DrawCircle(Position, bounds.Radius, 32);    
        }
    }
}
using OpenTK.Mathematics;
using OpenTK.Graphics.OpenGL;
using System.Drawing;

internal class Enemy : Entity
{
    //instances of other classes
    Texturer texturer = new Texturer(); 
    Player player = new Player();
    Particles particles;

    //variables
    public float speed = 0.00015f;
    public override bool IsPlayer => false;
    public int i = 0;
    public float scale;
    public float size { get; set; } = 0.1f;
    public double time;
    public Vector2 range;
    public Vector2 range2;
    public Circle boundEnemy;
    public bool enemyDead;
    public bool isActive;
    
    //variables for the animation
    public List<int> TextureID_Run;
    public List<int> TextureID_Dead;
    public List<int> current_TextureID = new List<int>();
    bool particleDrawn = false;

    public Enemy(Vector2 pos, bool dead , int dmg, Vector2 _range, List<int>particlesList) 
    {
        string Texture_Run = "assets/sEnemy_Run.png";
        TextureID_Run = texturer.LoadTexture(Texture_Run, 7,1); 
        string Texture_Dead = "assets/sEnemy_Dead.png";
        TextureID_Dead = texturer.LoadTexture(Texture_Dead, 1,1);
        enemyDead = dead;
        Position = pos;
        isActive = true;
        health = 1;
        range = _range;
        boundEnemy = new Circle(Position,0.1f);
        this.Dmg = dmg;
        particles = new Particles(particlesList);
    }

    float SetScale()
    {
        return GlobalSettings.AspectRatio;
    }
    public void DecreaseHealth()
    {
        particleDrawn = true;
        health--;
        if(health <= 0){
            enemyDead = true;
        }
    }

    public void setTargetPosition(Vector2 targetPosition)
    {
        player.Position = targetPosition;
    }

    public void setTime(double time)
    {
        this.time = time;
    }

    public virtual void Draw(float scale)
    {
        if(particleDrawn)
        {
            Vector2 direction = player.Position - Position;
            direction = Vector2.Normalize(direction);
            bool temp = particles.Draw(Position, size, direction);
            particleDrawn = temp;
        }
        
        var isFacingRight = true;
        GL.Color4(Color4.White);
        var hittime = DateTime.Now - LastCollision;
        if(hittime.TotalSeconds <= 0.3 && !enemyDead){
            GL.Color4(Color4.Red);
        }

        var rect = new RectangleF(Position.X - size /2, Position.Y  - size/2, size, size);
        var tex_rect = new RectangleF(0, 0, 1, 1);
        
        // Flipped texture coordinates
        var flipped_rect = new Rectangle( 1, 0, -1, 1);

        // Use the normal or flipped texture coordinates based on the direction the player is facing
        if (player.Position.X < Position.X && !enemyDead){
            isFacingRight = false;
        }
        var currentTexCoords = isFacingRight ? tex_rect : flipped_rect;

        List<int> new_TextureID = enemyDead ? TextureID_Dead : TextureID_Run;
        if (new_TextureID != current_TextureID)
        {
            currentFrame = 0;
        }
        current_TextureID = new_TextureID;

        // Check if enough time has passed since the last frame change
        if (time - lastFrameTime >= frameDuration)
        {
            // Update the current frame
            currentFrame = (currentFrame + 1) % current_TextureID.Count;

            // Update the time of the last frame change
            lastFrameTime = time; 
        }
        texturer.Draw(current_TextureID[currentFrame], rect, currentTexCoords);
        //DrawCircle(Position, boundEnemy.Radius, 32);  
    }

    public void MoveTowards(Vector2 targetPosition, float speed)
    {

        float damping = 0.90f;
        if (isActive)
        {
            MoveTowardsTarget();
        }
        else
        {
            MoveAwayFromTarget();
            i++;
            if (i >= 2000)
            {
                isActive = true;
                i = 0;
            }
        }

        void MoveTowardsTarget()
        {
            Vector2 direction = targetPosition - Position;
            direction = Vector2.Normalize(direction);
            speed *= damping; // speed dampen
            Position += direction * speed;
            boundEnemy.Center = Position;
        }

        void MoveAwayFromTarget()
        {
            Vector2 direction = Position - targetPosition; // Change direction
            direction = Vector2.Normalize(direction);
            speed *= damping; // speed dampen
            Position += direction * speed;
            boundEnemy.Center = Position;
        }

    }

    public void MoveAway(Vector2 targetPosition, float speed)
    {
        // Keeping distance from the player
        range = targetPosition - Position;
        range2 = targetPosition - Position;
        float distance = range.Length;
        if (distance > 0.5f)
        {
            range2 = Vector2.Normalize(range);
            Position += range2 * speed;
        }
        else
        {
            range2 = Vector2.Normalize(range) * -1;
            Position += range2 * speed;
        }
        boundEnemy.Center = Position;
        Position = Vector2.Clamp(Position, new Vector2(-0.95f, -0.95f), new Vector2(0.95f, 0.95f));
    }

    private void DrawCircle(Vector2 center, float radius, int segments)
    {
        scale = SetScale();
        GL.Begin(PrimitiveType.LineLoop);
        GL.Color4(Color4.Green);
        for (int i = 0; i < segments; i++)
        {
            float angle = i / (float)segments * 2.0f * MathF.PI;
            float x = center.X + size/2 * MathF.Cos(angle) / scale;
            float y = center.Y + size/2 * MathF.Sin(angle);
            GL.Vertex2(x, y);
        }
        GL.End();
    }
}
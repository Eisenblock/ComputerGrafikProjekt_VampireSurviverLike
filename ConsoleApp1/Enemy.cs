using OpenTK.Mathematics;
using OpenTK.Graphics.OpenGL;
using System.Drawing.Printing;
using System.Drawing;

internal class Enemy : Entity
{
    public override bool IsPlayer => false;
    public float PositionX;
    public float PositionY;
    public int i = 0;
    public float scale;
    public float size { get; set; } = 0.1f;
    public double time;

    Player player = new Player();
    //EnemyList enemyList1 = new EnemyList();
    public Circle boundEnemy;

    public bool enemyDead;
    public bool isActive;
    public int Dmg;
    Texturer texturer = new Texturer(); // Create an instance of the Texturer class
    public List<int> TextureID_Run;
    public List<int> TextureID_Dead;
    public List<int> current_TextureID;


    public Color4 Color { get; set; }
    public Enemy(Vector2 pos, bool dead , int dmg) 
    {

        string Texture_Run = "assets/topdown_shooter_assets/sEnemy_Run.png";
        TextureID_Run = texturer.LoadTexture(Texture_Run, 7); // Call the LoadTexture method on the instance and assign the first element of the returned list to TextureID

        string Texture_Dead = "assets/topdown_shooter_assets/sEnemy_Dead.png";
        TextureID_Dead = texturer.LoadTexture(Texture_Dead, 1); // Call the LoadTexture method on the instance and assign the first element of the returned list to TextureID
        enemyDead = dead;
        Position = pos;
        isActive = true;
        health = 1;
        boundEnemy = new Circle(Position,0.1f);
        this.Dmg = dmg;

    }

    float SetScale()
    {
        return GlobalSettings.AspectRatio;
    }
    public void DecreaseHealth()
    {
        health--;
        if(health <= 0){
            enemyDead = true;
        }
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
        DrawCircle(Position, boundEnemy.Radius, 32);  
    }

    public void MoveTowards(Vector2 targetPosition, float speed)
    {       
        if(isActive == true)
        {
            Vector2 direction = targetPosition - Position;  
            direction = Vector2.Normalize(direction);
            Position += direction * speed;
            boundEnemy.Center = Position;
        }
        else
        {
            Vector2 direction =( targetPosition - Position)*-1;
            direction = Vector2.Normalize(direction);
            Position += direction * speed;
            boundEnemy.Center = Position;
            i++;
            if(i >= 2000)
            {
                isActive = true;
                i = 0;
            }
        }
        
    }

    public void MoveAway(Vector2 targetPosition, float speed)
    {
        //Bewegung für Abstand halten vom Gegner (ranged Enemy)
        Vector2 direction = targetPosition - Position;
        float distance = direction.Length;
        if (distance > 1f)
        {
            direction = Vector2.Normalize(direction);
            Position += direction * speed;
        }
        else
        {
            direction = Vector2.Normalize(direction) * -1;
            Position += direction * speed;
        }
        boundEnemy.Center = Position;
    } 
    

    private void DrawCircle(Vector2 center, float radius, int segments)
    {
        scale = SetScale();
        GL.Begin(PrimitiveType.LineLoop);
        GL.Color4(Color4.Green);
        for (int i = 0; i < segments; i++)
        {
            float angle = i / (float)segments * 2.0f * MathF.PI;
            float x = center.X + size/2 * MathF.Cos(angle);
            float y = center.Y + size/2 * MathF.Sin(angle);
            GL.Vertex2(x, y);
        }
        GL.End();
    }


}
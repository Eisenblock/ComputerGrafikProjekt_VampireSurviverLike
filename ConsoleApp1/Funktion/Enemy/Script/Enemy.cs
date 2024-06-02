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
    public Vector2 range;
    public Vector2 range2;

    Player player = new Player();
    //EnemyList enemyList1 = new EnemyList();
    public Circle boundEnemy;
    Shootlist shootlist;

    public bool enemyDead;
    public bool isActive;
    public int Dmg;
    Texturer texturer = new Texturer(); // Create an instance of the Texturer class
    public List<int> TextureID_Run;
    public List<int> TextureID_Dead;
    public List<int> current_TextureID;


    public Color4 Color { get; set; }
    public Enemy(Vector2 pos, bool dead , int dmg, Vector2 _range) 
    {

        string Texture_Run = "assets/sEnemy_Run.png";
        TextureID_Run = texturer.LoadTexture(Texture_Run, 7); // Call the LoadTexture method on the instance and assign the first element of the returned list to TextureID

        string Texture_Dead = "assets/sEnemy_Dead.png";
        TextureID_Dead = texturer.LoadTexture(Texture_Dead, 1); // Call the LoadTexture method on the instance and assign the first element of the returned list to TextureID
        enemyDead = dead;
        Position = pos;
        isActive = true;
        health = 1;
        range = _range;
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
            speed *= damping; // Geschwindigkeit dämpfen
            Position += direction * speed;
            boundEnemy.Center = Position;
        }

        void MoveAwayFromTarget()
        {
            Vector2 direction = Position - targetPosition; // Korrigiere die Richtung für das Wegbewegen
            direction = Vector2.Normalize(direction);
            speed *= damping; // Geschwindigkeit dämpfen
            Position += direction * speed;
            boundEnemy.Center = Position;
        }

    }

    public void MoveAway(Vector2 targetPosition, float speed)
    {
        //Bewegung für Abstand halten vom Gegner (ranged Enemy)
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
            float x = center.X + size/2 * MathF.Cos(angle);
            float y = center.Y + size/2 * MathF.Sin(angle);
            GL.Vertex2(x, y);
        }
        GL.End();
    }


}
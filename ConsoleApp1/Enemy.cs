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
    public int health = 1;
    public float size { get; set; } = 0.1f;

    Player player = new Player();
    //EnemyList enemyList1 = new EnemyList();
    public Circle boundEnemy;

    public bool enemyDead;
    public bool isActive;
    public int Dmg;
    Texturer texturer = new Texturer(); // Create an instance of the Texturer class
    public string Texture { get; private set; }
    public int TextureID { get; private set; }


    public Color4 Color { get; set; }
    public Enemy(Vector2 pos, bool dead , int dmg) 
    {
        Texture = "assets/topdown_shooter_assets/sEnemy_Dead.png";
        TextureID = texturer.LoadTexture(Texture); // Call the LoadTexture method on the instance

        enemyDead = dead;
        Position = pos;
        isActive = true;
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

    public virtual void Draw(Color4 color)
    {
        GL.Color4(Color4.White);
        var rect = new RectangleF(Position.X-0.1f, Position.Y-0.1f, 0.2f, 0.2f);
        var tex_rect = new RectangleF(0, 0, 1, 1);
        texturer.Draw(TextureID, rect, tex_rect);  
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
            float x = center.X + size * MathF.Cos(angle) / scale;
            float y = center.Y + size * MathF.Sin(angle);
            GL.Vertex2(x, y);
        }
        GL.End();
    }


}
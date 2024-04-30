using OpenTK.Mathematics;
using OpenTK.Graphics.OpenGL;

internal class Enemy
{
    public float PositionX;
    public float PositionY;
    public int i = 0;
    public float scale;
    public Vector2 Position { get; internal set; }

    Player player = new Player();
    //EnemyList enemyList1 = new EnemyList();
    public Circle boundEnemy;

    public bool enemyDead;

    public Color4 Color { get; set; }
    public Enemy(Vector2 pos, bool dead ) 
    {
        enemyDead = dead;
        Position = pos;
        boundEnemy = new Circle(Position,0.1f);
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

    public virtual void Draw(Color4 color)
    {
        Circle(Position, 0.1f, 32, color); // Zeichnet einen Kreis mit Radius 0.1 und 32 Segmenten
        DrawCircle(Position, boundEnemy.Radius, 32);
    }

    public void MoveTowards(Vector2 targetPosition, float speed)
    {       
        Vector2 direction = targetPosition - Position;
        direction = Vector2.Normalize(direction);
        Position += direction * speed;
        boundEnemy.Center = Position;
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
    } 
    

    private void DrawCircle(Vector2 center, float radius, int segments)
    {
        scale = SetScale();
        GL.Begin(PrimitiveType.LineLoop);
        GL.Color4(Color4.Green);
        for (int i = 0; i < segments; i++)
        {
            float angle = i / (float)segments * 2.0f * MathF.PI;
            float x = center.X + radius * MathF.Cos(angle) / scale;
            float y = center.Y + radius * MathF.Sin(angle);
            GL.Vertex2(x, y);
        }
        GL.End();
    }


}
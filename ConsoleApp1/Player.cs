using OpenTK.Mathematics;
using OpenTK.Graphics.OpenGL;
using System.Drawing.Drawing2D;
using System.Drawing.Printing;

internal class Player : Entity
{
    public override bool IsPlayer => true;
    public float radius_col;
    public float PositionX; 
    public float PositionY;
    public float scale;
    
    public float speed = 0.00015f;
    public Circle bounds = new Circle(Vector2.Zero,0);
    public Color4 color = Color4.Blue;
    public int health = 3;
    public Player()
    {
        Position = new Vector2(0.0f, 0.0f);
        bounds = new Circle(Position, 0.1f);
        PositionX = Position.X;
        PositionY = Position.Y;
    }
    public void ClearAll()
    {
        Position = Vector2.Zero;
        health = 3;
        ResetColor();
    }

    public Vector2 getPlayerPosition()
    {
        return Position;
    }

internal void Left()
{
    float newX = Position.X - speed;
    if (newX >= -0.99) // Berücksichtigen Sie den Radius des Spielers
    {
        Position = new Vector2(newX, Position.Y);
    }
}

internal void Right()
{
    float newX = Position.X + speed;
    if (newX <= 0.99) // Berücksichtigen Sie den Radius des Spielers
    {
        Position = new Vector2(newX, Position.Y);
    }
}

internal void Up()
{
    float newY = Position.Y + speed;
    if (newY <= 0.99) // Berücksichtigen Sie den Radius des Spielers
    {
        Position = new Vector2(Position.X, newY);
    }
}

internal void Down()
{
    float newY = Position.Y - speed;
    if (newY >= -0.99) // Berücksichtigen Sie den Radius des Spielers
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

    public void Draw()
    {
        Circle(Position, 0.1f, 32, color); // Zeichnet einen Kreis mit Radius 0.1 und 32 Segmenten
        DrawCircle(Position,bounds.Radius,32);
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
            float x = center.X + radius * MathF.Cos(angle) / scale;
            float y = center.Y + radius * MathF.Sin(angle);
            GL.Vertex2(x, y);
        }
        GL.End();
    }

    public bool CheckCollision(Enemy enemy)
    {
        float distanceSquared = 1;
        float radiusSumSquared = 0;

        if (enemy != null)
        {
            distanceSquared = (Position - enemy.Position).LengthSquared;
            radiusSumSquared = (bounds.Radius + enemy.boundEnemy.Radius) * (bounds.Radius + enemy.boundEnemy.Radius);
            
        }

        if(distanceSquared < radiusSumSquared)
        {
           DecreaseHealth();
           //Console.WriteLine("Player Health: " + Health);
        }
        return distanceSquared < radiusSumSquared;
    }

    public void IncreaseHealth()
    {
        health++;
    }

    public void DecreaseHealth()
    {
        health--;
        Console.WriteLine("Player Health: " + health);
        color = Color4.Red; // Ändern Sie die Farbe auf Rot, wenn der Spieler Schaden nimmt
    }   

    public void ResetColor()
    {
        color = Color4.Blue; // Setzen Sie die Farbe auf Blau zurück
    }
}
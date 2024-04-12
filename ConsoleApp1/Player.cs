using OpenTK.Mathematics;
using OpenTK.Graphics.OpenGL;

internal class Player
{
    public Vector2 Position { get; internal set; }

    public float PositionX; 
    public float PositionY;
    public float scale;


    public Player()
    {
        Position = new Vector2(0.0f,0.0f);
        
    }

   
    internal void Left()
    {
        Position = new Vector2(Position.X - 0.1f, Position.Y);
    }

    internal void Right()
    {
        Position = new Vector2(Position.X + 0.1f, Position.Y);
    }

    internal void Up()
    {
        Position = new Vector2(Position.X, Position.Y + 0.1f);
    }

    internal void Down()
    {
        Position = new Vector2(Position.X, Position.Y - 0.1f);
    }

    float SetScale()
    {
        return GlobalSettings.AspectRatio;
    }

    void Circle(Vector2 pos, float radius, int segments)
    {
        scale = SetScale();

        GL.Begin(PrimitiveType.TriangleFan);
        GL.Color4(Color4.Blue);
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
        Circle(Position, 0.1f, 32); // Zeichnet einen Kreis mit Radius 0.1 und 32 Segmenten
    }
}
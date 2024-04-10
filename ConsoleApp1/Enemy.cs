using OpenTK.Mathematics;
using OpenTK.Graphics.OpenGL;

internal class Enemy
{
    public float PositionX;
    public float PositionY;
    public Vector2 Position { get; internal set; }

    public Enemy() 
    {
        Position = new Vector2(0.8f,0.8f);
    }

    void Quad()
    {
        GL.Begin(PrimitiveType.Quads);
        GL.Color4(Color4.Aqua);
        GL.Vertex2( Position.X + 0.1f, Position.Y + 0.1f);
        GL.Vertex2( Position.X + 0.1f, Position.Y);
        GL.Vertex2( Position.X, Position.Y);
        GL.Vertex2( Position.X, Position.Y + 0.1f);
        GL.End();
    }

    public void Draw()
    {
        Quad();
    }

    public void MoveTowards(Vector2 targetPosition, float speed)
    {       
        Vector2 direction = targetPosition - Position;
        direction = Vector2.Normalize(direction);
        Position += direction * speed;
    }
}
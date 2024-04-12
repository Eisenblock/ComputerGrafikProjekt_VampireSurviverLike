using OpenTK.Mathematics;
using OpenTK.Graphics.OpenGL;

internal class Enemy
{
    public float PositionX;
    public float PositionY;
    public int i = 0;



    public Vector2 Position { get; internal set; }

  

    public Enemy(Vector2 pos ) 
    {
        Position = pos;
    }

    void Quad(Vector2 pos)
    {
        GL.Begin(PrimitiveType.Quads);
        GL.Color4(Color4.Blue);
        GL.Vertex2( pos.X + 0.1f, pos.Y + 0.1f);
        GL.Vertex2( pos.X + 0.1f, pos.Y);
        GL.Vertex2( pos.X, pos.Y);
        GL.Vertex2( pos.X, pos.Y + 0.1f);
        GL.End();
    }

    public void Draw()
    {

        Quad(Position);
        
    }

    public void MoveTowards(Vector2 targetPosition, float speed)
    {       
        Vector2 direction = targetPosition - Position;
        direction = Vector2.Normalize(direction);
        Position += direction * speed;
    }

}
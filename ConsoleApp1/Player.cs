using OpenTK.Mathematics;
using OpenTK.Graphics.OpenGL;

internal class Player
{
    public Vector2 Position { get; internal set; }

    public float PositionX; 
    public float PositionY;


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

    void Quad()
    {
        GL.ClearColor(Color4.LightGray);
        GL.Begin(PrimitiveType.Quads); 
        GL.Color4(Color4.IndianRed);
        GL.Vertex2(Position.X + 0.1f, Position.Y + 0.1f);
        GL.Vertex2(Position.X + 0.1f, Position.Y - 0.1f);
        GL.Vertex2(Position.X - 0.1f, Position.Y - 0.1f);
        GL.Vertex2(Position.X - 0.1f, Position.Y + 0.1f);
        GL.End();
    }

    public void Draw()
    {
        Quad();
    }
}
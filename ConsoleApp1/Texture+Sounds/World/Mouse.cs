using OpenTK.Mathematics;
using OpenTK.Graphics.OpenGL;
using System.Drawing;

public class Mouse
{
    //instances of other classes
    Texturer texturer = new Texturer();

    //variables
    public Vector2 Position { get; set; }

    //variables for the animation
    public string Texture { get; set; }
    public int TextureID { get; private set; }

    public Mouse()
    {
        // Load the textures
        Texture = "assets/crosshair.png";
        TextureID = texturer.LoadTexture(Texture,1,1)[0];
    }

    public void Update(Vector2 pos)
    {
        Position = pos;
    }

    public void Draw()
    {
        GL.Color4(Color4.White);
        var rect = new RectangleF(Position.X-0.065f, Position.Y-0.065f, 0.15f, 0.15f);
        var tex_rect = new RectangleF(0, 0, 1, 1);
        texturer.Draw(TextureID, rect, tex_rect);
    }
}
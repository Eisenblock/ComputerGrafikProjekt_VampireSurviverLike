using OpenTK.Mathematics;
using OpenTK.Graphics.OpenGL;
using System.Drawing.Printing;
using System.Drawing;

public class Mouse{

    public Vector2 Position { get; set; }
    public string Texture { get; set; }
    Texturer texturer = new Texturer(); // Create an instance of the Texturer class
    public int TextureID { get; private set; } // Hier speichern wir die Textur-ID

    public Mouse()
    {
        Texture = "assets/topdown_shooter_assets/crosshair.png";
        TextureID = texturer.LoadTexture(Texture,1)[0]; // Call the LoadTexture method on the instance
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
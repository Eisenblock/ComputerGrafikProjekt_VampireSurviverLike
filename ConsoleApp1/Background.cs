using OpenTK.Mathematics;
using OpenTK.Graphics.OpenGL;
using System.Drawing.Drawing2D;
using System.Drawing.Printing;
using System.Drawing;
using System.Drawing.Imaging;
using ImageMagick;
using Image = System.Drawing.Image;


internal class Background
{
    public static Vector2 WindowSize => Program.WindowSize;
    public int TextureID { get; private set; } // Hier speichern wir die Textur-ID
    public string Texture { get; private set; }
    Texturer texturer = new Texturer(); // Create an instance of the Texturer class

    public Background()
    {
        Texture = "assets/topdown_shooter_assets/sMap.png";
        TextureID = texturer.LoadTexture(Texture,1)[0]; // Call the LoadTexture method on the instance
    }

    float SetScale()
    {
        return GlobalSettings.AspectRatio;
    }

    public void Draw()
    {
        GL.Color4(Color4.White);
        var rect = new RectangleF(-1f,-1f, 2f, 2f);
        var tex_rect = new RectangleF(0f, 0f, 1f, 1f);
        texturer.Draw(TextureID, rect, tex_rect); // Hintergrundbild zeichnen
    }
}
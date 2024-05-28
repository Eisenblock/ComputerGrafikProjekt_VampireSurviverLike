using OpenTK.Mathematics;
using OpenTK.Graphics.OpenGL;
using System.Drawing.Drawing2D;
using System.Drawing.Printing;
using System.Drawing;
using System.Drawing.Imaging;
using ImageMagick;
using Image = System.Drawing.Image;
using ImageMagick.Formats;
using System.Numerics;


internal class Map
{
    public static OpenTK.Mathematics.Vector2 WindowSize => Program.WindowSize;
    public string Texture_Map;
    public int TextureID_Map;
    public string Texture_Wall;
    public int TextureID_Wall;
    public List<int> TextureID_Hearts;

    Texturer texturer = new Texturer(); // Create an instance of the Texturer class
    Player player = new Player();

    public Map()
    {
        this.player = player;
        Texture_Map = "assets/topdown_shooter_assets/sMap.png";
        TextureID_Map = texturer.LoadTexture(Texture_Map,1)[0]; // Call the LoadTexture method on the instance

        Texture_Wall = "assets/topdown_shooter_assets/sWall.png";
        TextureID_Wall = texturer.LoadTexture(Texture_Wall,1)[0]; // Call the LoadTexture method on the instance
    }

    float SetScale()
    {
        return GlobalSettings.AspectRatio;
    }

    public void Draw()
    {
        GL.Color4(Color4.White);
        var rect_map = new RectangleF(-1f,-1f, 2f, 2f);
        var rect_wall = new RectangleF(rect_map.Left - 0.1f, rect_map.Top - 0.1f, rect_map.Width + 0.2f, rect_map.Height + 0.2f);
        var tex_rect = new RectangleF(0f, 0f, 1f, 1f);


        texturer.Draw(TextureID_Map, rect_map, tex_rect); // Hintergrundbild zeichnen
        texturer.Draw(TextureID_Wall, rect_wall, tex_rect); // Hintergrundbild zeichnen
    }
}
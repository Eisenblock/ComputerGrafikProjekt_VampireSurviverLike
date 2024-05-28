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
using System.Security.Cryptography.X509Certificates;


internal class GUI
{
    public static OpenTK.Mathematics.Vector2 WindowSize => Program.WindowSize;
    public string Texture_Map;
    public int TextureID_Map;
    public string Texture_Wall;
    public int TextureID_Wall;
    public List<int> TextureID_Hearts;

    Texturer texturer = new Texturer(); // Create an instance of the Texturer class
    Entity entity;

    public GUI(Entity entity)
    {
        this.entity = entity;

        Texture_Wall = "assets/topdown_shooter_assets/Hearts.png";
        TextureID_Hearts = texturer.LoadTexture(Texture_Wall,3); // Call the LoadTexture method on the instance
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

        if (entity.IsPlayer)
        {
            DrawHeartsPlayer();
        }
        else
        {
            Console.WriteLine("DrawHeartsEnemy");
            DrawHeartsEnemy();
        }
    }

    public void DrawHeartsPlayer()
    {
        var current_health = entity.health;
        OpenTK.Mathematics.Vector2 pos = new OpenTK.Mathematics.Vector2(-1f, -1f);
        for (int i = 0; i < entity.max_Health/2; i++)
        {
            if (current_health >= 2)
            {
                // Draw full heart
                //texturer.Draw(TextureID_Hearts[0], new RectangleF(-1f + i * 0.15f, -1f, 0.15f, 0.15f), new RectangleF(0f, 0f, 1f, 1f));
                current_health -= 2;
            }
            else if (current_health == 1)
            {
                // Draw half heart
                //texturer.Draw(TextureID_Hearts[1], new RectangleF(-1f + i * 0.15f, -1f, 0.15f, 0.15f), new RectangleF(0f, 0f, 1f, 1f));
                current_health -= 1;
            }
            else
            {
                // Draw empty heart
                //texturer.Draw(TextureID_Hearts[2], new RectangleF(-1f + i * 0.15f, -1f, 0.15f, 0.15f), new RectangleF(0f, 0f, 1f, 1f));
            }
        }
    }
    public void DrawHeartsEnemy()
    {
        var current_health = entity.health;
        OpenTK.Mathematics.Vector2 pos = new OpenTK.Mathematics.Vector2(-1f, -1f);
        Console.WriteLine(entity.max_Health);
        for (int i = 0; i < entity.max_Health/2; i++)
        {
            if (current_health >= 2)
            {
                Console.WriteLine("Draw full heart");
                // Draw full heart
                texturer.Draw(TextureID_Hearts[0], new RectangleF(entity.Position.X+0.4f, entity.Position.Y, 0.05f, 0.05f), new RectangleF(0f, 0f, 1f, 1f));
                current_health -= 2;
            }
            else if (current_health == 1)
            {
                // Draw half heart
                texturer.Draw(TextureID_Hearts[1], new RectangleF(entity.Position.X, entity.Position.Y, 0.05f, 0.05f), new RectangleF(0f, 0f, 1f, 1f));
                current_health -= 1;
            }
            else
            {
                // Draw empty heart
                texturer.Draw(TextureID_Hearts[2], new RectangleF(entity.Position.X, entity.Position.Y, 0.05f, 0.05f), new RectangleF(0f, 0f, 1f, 1f));
            }
        }
    }
}
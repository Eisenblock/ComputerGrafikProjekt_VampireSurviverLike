using OpenTK.Mathematics;
using OpenTK.Graphics.OpenGL;
using System.Drawing.Drawing2D;
using System.Drawing.Printing;
using System.Drawing;
using System.Drawing.Imaging;
using ImageMagick;
using Image = System.Drawing.Image;


internal class Gun
{
    public int TextureID { get; private set; } // Hier speichern wir die Textur-ID
    public string Texture { get; private set; }
    public Vector2 Position; 
    public float scale;
    public Circle bounds = new Circle(Vector2.Zero,0);
    Texturer texturer = new Texturer(); // Create an instance of the Texturer class
    Vector2 MousePosition;

    public Gun(Player player)
    {
        Texture = "assets/topdown_shooter_assets/sGun.png";
        TextureID = texturer.LoadTexture(Texture,1)[0]; // Call the LoadTexture method on the instance
    }

    public void Update(Player player, Vector2 mousePosition)
    {
        Position = player.Position;
        MousePosition = mousePosition;
    } 

    public void Draw()
    {
        GL.Color4(Color4.White);
         var rect = new RectangleF(-0.02f, -0.04f, 0.1f, 0.1f);
        var tex_rect = new RectangleF(0, 0, 1, 1);

        // Calculate the angle between the player and the mouse
        float angle = (float)Math.Atan2(MousePosition.Y - Position.Y, MousePosition.X - Position.X);

        // Convert the angle to degrees
        angle = angle * (180f / (float)Math.PI);

        // Translate to the player's position
        GL.Translate(Position.X, Position.Y, 0);
        Console.WriteLine(MousePosition.X- Position.X);

        // If the mouse is to the left of the player, flip the sprite
        if (MousePosition.X < Position.X)
        {
            GL.Scale(1.0f, -1.0f, 1.0f);
            angle = -angle;
        }

        // Create a rotation matrix
        Matrix4 rotation = Matrix4.CreateRotationZ(MathHelper.DegreesToRadians(angle));

        // Apply the rotation matrix
        GL.MultMatrix(ref rotation);

        texturer.Draw(TextureID, rect, tex_rect);

        // Undo the translation
        GL.Translate(-Position.X, -Position.Y, 0);

        // Reset the transformation matrix
        GL.LoadIdentity();
    }
}
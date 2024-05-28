using OpenTK.Mathematics;
using OpenTK.Graphics.OpenGL;
using System.Drawing.Drawing2D;
using System.Drawing.Printing;
using System.Drawing;
using System.Drawing.Imaging;
using ImageMagick;
using System.IO;
using Image = System.Drawing.Image;
using OpenTK.Windowing.GraphicsLibraryFramework;

public class Animation
{
    // Liste der Texturen (Frames) der Animation
    public List<int> Textures { get; } = new List<int>();

    // Dauer jeder Frame in Sekunden
    public float FrameDuration { get; set; }

    // Aktueller Index der Frame, die gezeichnet wird
    private int currentFrameIndex = 0;

    // Zeit, die seit der letzten Frame-Änderung vergangen ist
    private float timeSinceLastFrame = 0;

    // Methode zum Hinzufügen einer Textur zur Animation
    public void AddTexture(int texture)
    {
        Textures.Add(texture);
    }

    // Methode zum Aktualisieren der Animation
    public void Update(float deltaTime)
    {
        timeSinceLastFrame += deltaTime;

        if (timeSinceLastFrame >= FrameDuration)
        {
            currentFrameIndex = (currentFrameIndex + 1) % Textures.Count;
            timeSinceLastFrame -= FrameDuration;
        }
    }

    // Methode zum Abrufen der aktuellen Frame
    public int GetCurrentFrame()
    {
        return Textures[currentFrameIndex];
    }
}
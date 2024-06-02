using System.Drawing; 
using OpenTK.Graphics.OpenGL;
using OpenTK.Input;
using OpenTK.Mathematics;
using System.Threading;
using OpenTK.Windowing.Desktop;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.GraphicsLibraryFramework;
using System.Runtime.Intrinsics;

internal class MenuHelper
{
    Texturer texturer = new Texturer(); // Creates an instance of the Texturer class
    public float button_length = 0.5f;
    public float button_height = 0.2f;
    public float button_spacing = 0.1f;
    public float button_y = -0.2f;
    public float moved_button_y;
    public void Draw(GameWindow myWindow, List<int> textures, bool background)
    {
        int j = 0;
        int currentID = textures[j];
        var rect = new RectangleF(-1f,-1f, 2f, 2f);
        var tex_rect = new RectangleF(0f, 0f, 1f, 1f);

        if (background)
        {
            texturer.Draw(currentID, rect, tex_rect); // Hintergrundbild zeichnen
            j = 1;
        }
        for (int i = j; i < textures.Count; i++)
        {
            currentID = textures[i];
            rect = new RectangleF(-button_length/2, moved_button_y, button_length, button_height);
            moved_button_y -= button_height + button_spacing;
            texturer.Draw(currentID, rect, tex_rect);
            currentID++;
        }
        moved_button_y = button_y;
        myWindow.SwapBuffers();
    }

    private bool IsMouseOver(Vector2 mouseposition, float minX, float maxX, float minY, float maxY)
    {
        return mouseposition.X > minX && mouseposition.X < maxX && mouseposition.Y > minY && mouseposition.Y < maxY;
    }

    public int Hovering(Vector2 mouseposition)
    {
        var length = button_length/2;
        var pos_Y = button_y;
        if (IsMouseOver(mouseposition, -length, length, pos_Y, pos_Y+button_height))
        {
            return 1;
        }
        pos_Y -= button_height + button_spacing;

        if (IsMouseOver(mouseposition, -length, length, pos_Y, pos_Y+button_height))
        {
            return 2;
        }
        pos_Y -= button_height + button_spacing;

        if (IsMouseOver(mouseposition, -length, length, pos_Y, pos_Y+button_height))
        {
            return 3;
        }
        return 0;
    }
}
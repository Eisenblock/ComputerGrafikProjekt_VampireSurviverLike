using System.Drawing; 
using OpenTK.Graphics.OpenGL;
using OpenTK.Input;
using OpenTK.Mathematics;
using System.Threading;
using OpenTK.Windowing.Desktop;
using OpenTK.Windowing.Common;
using OpenTK.Graphics.OpenGL;
using OpenTK.Windowing.GraphicsLibraryFramework;
using System.Runtime.Intrinsics;

internal class GameOver
{
    private Game game;
    private Player player;
    private bool isPaused;
    private GameWindow myWindow;
    private float size;
    private Action restart;

    public GameOver(GameWindow window, Game game, Player player, Action restart)
    {
        this.player = player;
        this.game = game;
        myWindow = window;
        isPaused = false;
        this.restart = restart;
    }

    public void show()
    {
        isPaused = true;
        Draw(myWindow);
    }

    public void Draw(GameWindow myWindow)
    {
        size = 0.2f;
        GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);
        GL.Begin(PrimitiveType.Quads);
        GL.Color3(Color.Black);
        GL.Vertex2(-1, -1);
        GL.Vertex2(1, -1);
        GL.Vertex2(1, 1);
        GL.Vertex2(-1, 1);
        GL.End();

        // Zeichnen Sie ein weißes Quadrat als Knopf
        GL.Begin(PrimitiveType.Quads);
        GL.Color3(Color.White);
        GL.Vertex2(-size, -size);
        GL.Vertex2(-size, size);
        GL.Vertex2(size, size);
        GL.Vertex2(size, -size);
        GL.End();

        myWindow.SwapBuffers();
    }

    // public void OnMouseClick(Vector2 mouseposition)
    // {
    //     Console.WriteLine("Mouse Clicked at: " + mouseposition.X + ", " + mouseposition.Y);
    //     // Überprüfen Sie, ob der Mausklick innerhalb des weißen Quadrats liegt
    //     if (mouseposition.X >= -size && mouseposition.X <= size && mouseposition.Y >= -size && mouseposition.Y <= size)
    //     {
    //         OnButtonClicked();
    //     }
    // }

    public void Restart()
    {
        if(player.health <= 0)
        {
            isPaused = false;
            restart();
            game.Running();
        }
    }
}
using System.Drawing; 
using OpenTK.Graphics.OpenGL;
using OpenTK.Input;
using OpenTK.Mathematics;
using System.Threading;
using OpenTK.Windowing.Desktop;
using OpenTK.Windowing.Common;
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
    SoundsPlayer soundsPlayer;
    Texturer texturer = new Texturer(); // Create an instance of the Texturer class
    int TextureID;

    public GameOver(GameWindow window, Game game, Player player, Action restart, SoundsPlayer soundsPlayer)
    {
        string Texture = "assets/GameOver.jpg";
        TextureID = texturer.LoadTexture(Texture, 1)[0]; // Call the LoadTexture method on the instance
        this.soundsPlayer = soundsPlayer;
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
        var rect = new RectangleF(-1f,-1f, 2f, 2f);
        var tex_rect = new RectangleF(0f, 0f, 1f, 1f);
        texturer.Draw(TextureID, rect, tex_rect); // Hintergrundbild zeichnen
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
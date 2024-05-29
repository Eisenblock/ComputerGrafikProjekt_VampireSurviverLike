using System.Drawing; 
using OpenTK.Graphics.OpenGL;
using OpenTK.Input;
using OpenTK.Mathematics;
using System.Threading;
using OpenTK.Windowing.Desktop;
using OpenTK.Windowing.Common;
using OpenTK.Graphics.OpenGL;
using OpenTK.Windowing.GraphicsLibraryFramework;


internal class UpgradeScreen
{
    private Game game;
    private Player player;
    private bool isPaused;
    private Thread inputThread;
    private GameWindow myWindow;
    private float size;

    public UpgradeScreen(GameWindow window, Game game, Player player)
   {
        this.player = player;
        this.game = game;
        myWindow = window;
        isPaused = false;
    }

    public void show()
    {
        isPaused = true;

        // Zeichnen Sie den Upgrade-Bildschirm
        Draw(myWindow);
    }

    public void update(Vector2 mouse)
    {
        if(isPaused == true)
        {

        }
    }
    public void Draw(GameWindow myWindow)
    {
        size = 0.2f;
        GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);
        GL.Begin(PrimitiveType.Quads);
        GL.Color3(Color.White);
        GL.Vertex2(-0.8, -0.8);
        GL.Vertex2(0.8, -0.8);
        GL.Vertex2(0.8, 0.8);
        GL.Vertex2(-0.8, 0.8);
        GL.End();

        // Zeichnen Sie ein rotes Quadrat
        GL.Begin(PrimitiveType.Quads);
        GL.Color3(Color.Red);
        GL.Vertex2(-size-0.5f, -size);
        GL.Vertex2(-size-0.5f, size);
        GL.Vertex2(size-0.5f, size);
        GL.Vertex2(size-0.5f, -size);
        GL.End();
        
        // Erstellen Sie einen TextRenderer und rendern Sie den Text
        // TextRenderer textRenderer = new TextRenderer();
        // Font font = new Font(FontFamily.GenericSansSerif, 16);
        // Color textColor = Color.Black;
        // Color backgroundColor = Color.White;
        // Bitmap renderedText = textRenderer.RenderText("cumlord", font, textColor, backgroundColor, size, size);
        
        // Zeichnen Sie ein blaues Quadrat
        GL.Begin(PrimitiveType.Quads);
        GL.Color3(Color.Blue);
        GL.Vertex2(-size, -size);
        GL.Vertex2(-size, size);
        GL.Vertex2(size, size);
        GL.Vertex2(size, -size);
        GL.End();
        
        // Zeichnen Sie ein grünes Quadrat
        GL.Begin(PrimitiveType.Quads);
        GL.Color3(Color.Green);
        GL.Vertex2(-size+0.5f, -size);
        GL.Vertex2(-size+0.5f, size);
        GL.Vertex2(size+0.5f, size);
        GL.Vertex2(size+0.5f, -size);
        GL.End();

        myWindow.SwapBuffers();
    }

    public void OnMouseClick(float mouseX, float mouseY)
    {
        Console.WriteLine("Mouse Clicked at: " + mouseX + ", " + mouseY);
        // Überprüfen Sie, ob der Mausklick innerhalb des roten Quadrats liegt
        if (mouseX >= -0.7f && mouseX <= -0.3f && mouseY >= -0.2f && mouseY <= 0.2f)
        {
            OnRedSquareClick();
            isPaused = false;
            game.Resume();
        }

        // Überprüfen Sie, ob der Mausklick innerhalb des blauen Quadrats liegt
        if (mouseX >= -0.2f && mouseX <= 0.2f && mouseY >= -0.2f && mouseY <= 0.2f)
        {
            OnBlueSquareClick();
            isPaused = false;
            game.Resume();
        }

        // Überprüfen Sie, ob der Mausklick innerhalb des grünen Quadrats liegt
        if (mouseX >= 0.3f && mouseX <= 0.7f && mouseY >= -0.2f && mouseY <= 0.2f)
        {
            OnGreenSquareClick();
            isPaused = false;
            game.Resume();
        }
    }

    private void OnRedSquareClick()
    {
        // Führen Sie den Code aus, der ausgeführt werden soll, wenn auf das rote Quadrat geklickt wird
        Console.WriteLine("Red Square Clicked");
    }

    private void OnBlueSquareClick()
    {
        // Führen Sie den Code aus, der ausgeführt werden soll, wenn auf das blaue Quadrat geklickt wird
        Console.WriteLine("Blue Square Clicked");
    }

    private void OnGreenSquareClick()
    {
        // Führen Sie den Code aus, der ausgeführt werden soll, wenn auf das grüne Quadrat geklickt wird
        Console.WriteLine("Green Square Clicked");
        player.IncreaseHealth();   
    }
}
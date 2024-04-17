using System.Drawing; 
using OpenTK.Graphics.OpenGL;
using OpenTK.Input;
using OpenTK.Mathematics;
using System.Threading;
using OpenTK.Windowing.Desktop;
using OpenTK.Windowing.Common;
using OpenTK.Graphics.OpenGL;
using OpenTK.Windowing.GraphicsLibraryFramework;
public enum GameState
{
    Running,
    Paused,
    UpgradeScreen
}

internal class Game
{
    private Player player;
    public GameState state;
    GameWindow myWindow;

    public Game(GameWindow window, Player player)
    {
        this.player = player;
        myWindow = window;
        state = GameState.Running;
    }

    public void Run()
    {
        state = GameState.Running;

        while (true) // Hauptspiel-Schleife
        {
            switch (state)
            {
                case GameState.Running:
                    // Führen Sie die Spiellogik aus
                    break;

                case GameState.Paused:
                    // Nichts tun, das Spiel ist pausiert
                    break;

                case GameState.UpgradeScreen:
                    var upgradeScreen = new UpgradeScreen(myWindow, this, player);
                    upgradeScreen.show();
                    break;
            }

            Thread.Sleep(100); // Verhindern Sie, dass die Schleife zu schnell läuft
        }
    }

    public void Pause()
    {
        state = GameState.Paused;
    }

    public void Resume()
    {
        state = GameState.Running;
    }

    public void ShowUpgradeScreen()
    {
        state = GameState.UpgradeScreen;
    }
}
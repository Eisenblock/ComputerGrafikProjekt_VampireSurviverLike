using System.Drawing; 
using OpenTK.Graphics.OpenGL;
using OpenTK.Input;
using OpenTK.Mathematics;
using System.Threading;
using OpenTK.Windowing.Desktop;
using OpenTK.Windowing.Common;
using OpenTK.Graphics.OpenGL;
using OpenTK.Windowing.GraphicsLibraryFramework;
using System.Drawing.Printing;
public enum GameState
{
    Running,
    Paused,
    UpgradeScreen,
    GameOver
}

internal class Game
{
    private Player player;
    public GameState state;
    GameWindow myWindow;
    Action restart;

    public Game(GameWindow window, Player player, Action restart)
    {
        this.player = player;
        myWindow = window;
        state = GameState.Running;
        this.restart = restart;
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
                case GameState.GameOver:
                    var gameOver = new GameOver(myWindow, this, player, restart);
                    gameOver.show();
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
    public void GameOver()
    {
        state = GameState.GameOver;
    }
    public void Running()
    {
        state = GameState.Running;
    }
}
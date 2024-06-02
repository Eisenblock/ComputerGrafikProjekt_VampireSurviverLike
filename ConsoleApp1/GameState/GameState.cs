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
    GameOver,
    MainMenu,
    Controls,
}

internal class Game
{
    private Player player;
    public GameState state;
    public GameState previousState;
    GameWindow myWindow;
    Action restart;
    SoundsPlayer soundsPlayer;
    public MainMenu mainMenu;
    public PauseMenu pauseMenu;
    public GameOver gameOver;
    public Running running;
    public Controls controls;

    public Game(GameWindow window, Player player, Action restart, SoundsPlayer soundsPlayer)
    {
        this.soundsPlayer = soundsPlayer;
        this.player = player;
        myWindow = window;
        state = GameState.MainMenu;
        this.restart = restart;
        mainMenu = new MainMenu(myWindow, this);
        running = new Running(soundsPlayer);
        pauseMenu = new PauseMenu(myWindow, this);
        gameOver = new GameOver(myWindow, this, restart);
        controls = new Controls(myWindow, this);
    }

    public void Run()
    {
        while (true) 
        {
            switch (state)
            {
                case GameState.MainMenu:
                    break;

                case GameState.Running:
                    break;

                case GameState.Paused:
                    break;

                case GameState.Controls:
                    break;

                case GameState.GameOver:
                    break;

                case GameState.UpgradeScreen:
                    var upgradeScreen = new UpgradeScreen(myWindow, this, player);
                    upgradeScreen.show();
                    break;
            }

            Thread.Sleep(100); // Verhindern Sie, dass die Schleife zu schnell läuft
        }
    }

    public void setPreviousState()
    {
        previousState = state;
    } 
    public void MainMenu()
    {
        setPreviousState();
        state = GameState.MainMenu;
    }

    public void Pause()
    {
        setPreviousState();
        state = GameState.Paused;
    }

    public void Resume()
    {
        setPreviousState();
        state = GameState.Running;
    }

    public void ShowUpgradeScreen()
    {
        setPreviousState();
        state = GameState.UpgradeScreen;
    }
    public void GameOver()
    {
        setPreviousState();
        state = GameState.GameOver;
    }
    public void Running()
    {
        setPreviousState();
        state = GameState.Running;
    }
    public void Controls()
    {
        setPreviousState();
        state = GameState.Controls;
    }
}
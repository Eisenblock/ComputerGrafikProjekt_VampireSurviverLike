using OpenTK.Windowing.Desktop;
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
    //instances of other classes
    private Player player;
    public GameState state;
    public GameState previousState;
    GameWindow myWindow;
    Action restart;
    Score score = new Score();
    SoundsPlayer soundsPlayer;
    
    //instances of the State classes
    public MainMenu mainMenu;
    public PauseMenu pauseMenu;
    public GameOver gameOver;
    public Running running;
    public Controls controls;
    
    public Game(GameWindow window, Player player, Action restart, SoundsPlayer soundsPlayer, Score score)
    {
        this.score = score;
        this.soundsPlayer = soundsPlayer;
        this.player = player;
        myWindow = window;
        state = GameState.MainMenu;
        this.restart = restart;
        mainMenu = new MainMenu(myWindow, this);
        running = new Running(myWindow, soundsPlayer,score);
        pauseMenu = new PauseMenu(myWindow, this);
        gameOver = new GameOver(myWindow, this, restart,score);
        controls = new Controls(myWindow, this);
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
        score.SaveHighscore();
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
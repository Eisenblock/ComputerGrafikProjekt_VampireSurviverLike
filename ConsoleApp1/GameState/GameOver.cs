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
    int BackgroundID;
    int PlayID;
    int MenuID;
    int ExitID;
    int col_PlayID;
    int col_MenuID;
    int col_ExitID;
    int current_Play;
    int current_Menu;
    int current_Exit;
    MenuHelper menuHelper = new MenuHelper();

    public GameOver(GameWindow window, Game game, Action restart)
    {
        
        string Texture = "assets/GameOver.png";
        BackgroundID = texturer.LoadTexture(Texture, 1)[0]; // Call the LoadTexture method on the instance
        string PlayTexture = "assets/NewGame Button.png";
        PlayID = texturer.LoadTexture(PlayTexture, 1)[0];
        current_Play = PlayID;
        string ControlsTexture = "assets/Menu Button.png";
        MenuID = texturer.LoadTexture(ControlsTexture, 1)[0];
        current_Menu = MenuID;
        string ExitTexture = "assets/Exit Button.png";
        ExitID = texturer.LoadTexture(ExitTexture, 1)[0];
        current_Exit = ExitID;


        string col_PlayTexture = "assets/col_NewGame Button.png";
        col_PlayID = texturer.LoadTexture(col_PlayTexture, 1)[0];
        string col_MenuTexture = "assets/col_Menu Button.png";
        col_MenuID = texturer.LoadTexture(col_MenuTexture, 1)[0];
        string col_ExitTexture = "assets/col_Exit Button.png";
        col_ExitID = texturer.LoadTexture(col_ExitTexture, 1)[0];

        this.game = game;
        this.restart = restart;
        myWindow = window;
        isPaused = false;
    }

    public void Draw(GameWindow myWindow)
    {
        menuHelper.Draw(myWindow, new List<int> {BackgroundID, current_Play, current_Menu, current_Exit},true);
    }
    public void ResetButtons()
    {
        current_Play = PlayID;
        current_Menu = MenuID;
        current_Exit = ExitID;
    }

    public void Hovering(Vector2 mouseposition, bool clicked)
    {
        ResetButtons();
        int button = menuHelper.Hovering(mouseposition);
        switch (button)
        {
            case 1:
                current_Play = col_PlayID;
                if(clicked)
                {
                    Restart();
                    game.Running();
                }
                break;
            case 2:
                current_Menu = col_MenuID;
                if(clicked)
                {
                    Restart();
                    game.MainMenu();
                }
                break;
            case 3:
                current_Exit = col_ExitID;
                if(clicked)
                {
                    myWindow.Close();
                }
                break;
        }
    }

    public void Update(Vector2 mouseposition)
    {
        Hovering(mouseposition, false);
    }

    public void OnMouseClick(Vector2 mouseposition)
    {
        // Überprüfen Sie, ob der Mausklick innerhalb der Buttons liegt
        Hovering(mouseposition, true);
    }

    public void Restart()
    {
        isPaused = false;
        restart();
    }
}
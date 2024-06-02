using OpenTK.Mathematics;
using OpenTK.Windowing.Desktop;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.GraphicsLibraryFramework;
using OpenTK.Input; // Add the missing import statement

internal class MainMenu
{
    Game game;
    private GameWindow myWindow;
    Texturer texturer = new Texturer(); // Create an instance of the Texturer class
    MenuHelper menuHelper = new MenuHelper();
    int BackgroundID;
    int PlayID;
    int ControlsID;
    int ExitID;
    int col_PlayID;
    int col_ControlsID;
    int col_ExitID;
    int current_Play;
    int current_Control;
    int current_Exit;
    Vector2 mouseposition;

    public MainMenu(GameWindow window, Game game)
    {
        string Texture = "assets/MainMenu.png";
        BackgroundID = texturer.LoadTexture(Texture, 1)[0];
        string PlayTexture = "assets/Play Button.png";
        PlayID = texturer.LoadTexture(PlayTexture, 1)[0];
        current_Play = PlayID;
        string ControlsTexture = "assets/Controls Button.png";
        ControlsID = texturer.LoadTexture(ControlsTexture, 1)[0];
        current_Control = ControlsID;
        string ExitTexture = "assets/Exit Button.png";
        ExitID = texturer.LoadTexture(ExitTexture, 1)[0];
        current_Exit = ExitID;


        string col_PlayTexture = "assets/col_Play Button.png";
        col_PlayID = texturer.LoadTexture(col_PlayTexture, 1)[0];
        string col_ControlsTexture = "assets/col_Controls Button.png";
        col_ControlsID = texturer.LoadTexture(col_ControlsTexture, 1)[0];
        string col_ExitTexture = "assets/col_Exit Button.png";
        col_ExitID = texturer.LoadTexture(col_ExitTexture, 1)[0];

        this.game = game;
        myWindow = window;
    }
    public void Draw(GameWindow myWindow)
    {
        menuHelper.Draw(myWindow, new List<int> {BackgroundID, current_Play, current_Control, current_Exit},true);
    }

    public void ResetButtons()
    {
        current_Play = PlayID;
        current_Control = ControlsID;
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
                    game.Running();
                }
                break;
            case 2:
                current_Control = col_ControlsID;
                if(clicked)
                {
                    game.Controls();
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

}
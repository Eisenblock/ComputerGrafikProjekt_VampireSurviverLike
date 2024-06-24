using OpenTK.Mathematics;
using OpenTK.Windowing.Desktop;


internal class MainMenu
{
    //instances of other classes
    Texturer texturer = new Texturer(); // Create an instance of the Texturer class
    MenuHelper menuHelper = new MenuHelper();
    Game game;
    private GameWindow myWindow;
   
    //variables for the animation
    int BackgroundID;
    int TitleID;
    int PlayID;
    int ControlsID;
    int ExitID;
    int col_PlayID;
    int col_ControlsID;
    int col_ExitID;
    int current_Play;
    int current_Control;
    int current_Exit;

    public MainMenu(GameWindow window, Game game)
    {
        // Load the textures
        string Texture = "assets/MenuBackground.png";
        BackgroundID = texturer.LoadTexture(Texture, 1,1)[0];
        string Title = "assets/GUNTATO.png";
        TitleID = texturer.LoadTexture(Title, 1,1)[0];

        string PlayTexture = "assets/Play Button.png";
        PlayID = texturer.LoadTexture(PlayTexture, 1,1)[0];
        current_Play = PlayID;
        string ControlsTexture = "assets/Controls Button.png";
        ControlsID = texturer.LoadTexture(ControlsTexture, 1,1)[0];
        current_Control = ControlsID;
        string ExitTexture = "assets/Exit Button.png";
        ExitID = texturer.LoadTexture(ExitTexture, 1,1)[0];
        current_Exit = ExitID;

        string col_PlayTexture = "assets/col_Play Button.png";
        col_PlayID = texturer.LoadTexture(col_PlayTexture, 1,1)[0];
        string col_ControlsTexture = "assets/col_Controls Button.png";
        col_ControlsID = texturer.LoadTexture(col_ControlsTexture, 1,1)[0];
        string col_ExitTexture = "assets/col_Exit Button.png";
        col_ExitID = texturer.LoadTexture(col_ExitTexture, 1,1)[0];

        this.game = game;
        myWindow = window;
    }
    public void Draw(GameWindow myWindow)
    {
        // Draw the screen
        menuHelper.DrawBackground(myWindow,BackgroundID);
        menuHelper.DrawTitle(TitleID, -5f);
        menuHelper.DrawButtons(myWindow,new List<int>{current_Play, current_Control, current_Exit});
        myWindow.SwapBuffers();
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
        Hovering(mouseposition, true);
    }

}
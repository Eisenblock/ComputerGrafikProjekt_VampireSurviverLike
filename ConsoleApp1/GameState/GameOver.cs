using OpenTK.Mathematics;
using OpenTK.Windowing.Desktop;

internal class GameOver
{
    //instances of other classes
    private Game game;
    private GameWindow myWindow;
    private Action restart;
    Texturer texturer = new Texturer();
    MenuHelper menuHelper = new MenuHelper();
    Score score;

    //variables
    private bool isPaused;  

    //variables for the animation
    int BackgroundID;
    int TitleID;
    int PlayID;
    int MenuID;
    int ExitID;
    int ScoreTextID;
    int HighscoreTextID;
    List<int> NumbersID;
    int col_PlayID;
    int col_MenuID;
    int col_ExitID;
    int current_Play;
    int current_Menu;
    int current_Exit;

    public GameOver(GameWindow window, Game game, Action restart, Score score)
    {
        // Load the textures
        string Texture = "assets/MenuBackground.png";
        BackgroundID = texturer.LoadTexture(Texture, 1,1)[0];
        string Title = "assets/GAME_OVER.png";
        TitleID = texturer.LoadTexture(Title, 1,1)[0];
        string ScoreTexture = "assets/YOURSCORE.png";
        ScoreTextID = texturer.LoadTexture(ScoreTexture, 1,1)[0];
        string HighScoreTexture = "assets/HIGHSCORE.png";
        HighscoreTextID = texturer.LoadTexture(HighScoreTexture, 1,1)[0];
        string NumbersTexture = "assets/numbers.png";
        NumbersID = texturer.LoadTexture(NumbersTexture, 10,1);

        string PlayTexture = "assets/NewGame Button.png";
        PlayID = texturer.LoadTexture(PlayTexture, 1,1)[0];
        current_Play = PlayID;
        string ControlsTexture = "assets/Menu Button.png";
        MenuID = texturer.LoadTexture(ControlsTexture, 1,1)[0];
        current_Menu = MenuID;
        string ExitTexture = "assets/Exit Button.png";
        ExitID = texturer.LoadTexture(ExitTexture, 1,1)[0];
        current_Exit = ExitID;

        string col_PlayTexture = "assets/col_NewGame Button.png";
        col_PlayID = texturer.LoadTexture(col_PlayTexture, 1,1)[0];
        string col_MenuTexture = "assets/col_Menu Button.png";
        col_MenuID = texturer.LoadTexture(col_MenuTexture, 1,1)[0];
        string col_ExitTexture = "assets/col_Exit Button.png";
        col_ExitID = texturer.LoadTexture(col_ExitTexture, 1,1)[0];

        this.score = score;
        this.game = game;
        this.restart = restart;
        myWindow = window;
        isPaused = false;
    }

    public void Restart()
    {
        restart();
    }

    public void Draw(GameWindow myWindow)
    {
        //Load the Score and Highscore
        List<int> scoreID = score.ScoreToTexture();
        List<int> highscoreID = score.HighscoreToTexture();

        //Draw the Screen
        menuHelper.DrawBackground(myWindow,BackgroundID);
        menuHelper.DrawTitle(TitleID, -5f);
        menuHelper.DrawScoresAndHighscores(myWindow, new List<int>{ScoreTextID, HighscoreTextID}, scoreID, highscoreID);
        menuHelper.DrawButtons(myWindow, new List<int> {current_Play, current_Menu, current_Exit});
        myWindow.SwapBuffers();
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
        Hovering(mouseposition, true);
    }
}
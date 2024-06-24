using OpenTK.Mathematics;
using OpenTK.Windowing.Desktop;

internal class Controls
{
    //instances of other classes
    Texturer texturer = new Texturer();
    MenuHelper menuHelper = new MenuHelper();
    Game game;

    //variables
    private GameWindow myWindow;
    int BackgroundID;
    int PictureID;
    int BackTextureID;
    int col_BackTextureID;
    int current_Back;

    public Controls(GameWindow window, Game game)
    {
        //load the textures
        string Texture = "assets/MenuBackground.png";
        BackgroundID = texturer.LoadTexture(Texture, 1,1)[0];
        string Picture = "assets/Controls.png";
        PictureID = texturer.LoadTexture(Picture, 1,1)[0];

        string BackTexture = "assets/Back Button.png";
        BackTextureID = texturer.LoadTexture(BackTexture, 1,1)[0];
        current_Back = BackTextureID;

        string col_BackTexture = "assets/col_Back Button.png";
        col_BackTextureID = texturer.LoadTexture(col_BackTexture, 1,1)[0];

        this.game = game;
        myWindow = window;
    }
    public void Draw(GameWindow myWindow)
    {
        //Middle Button for aesthetics
        menuHelper.moved_button_y -= menuHelper.button_height + menuHelper.button_spacing;
        //Draw the Background
        menuHelper.DrawBackground(myWindow,BackgroundID);
        menuHelper.DrawPicture(myWindow,PictureID);
        menuHelper.DrawButtons(myWindow,new List<int>{current_Back}); 
        myWindow.SwapBuffers();
    }

    public void ResetButtons()
    {
        current_Back = BackTextureID;
    }

    public void Hovering(Vector2 mouseposition, bool clicked)
    {
        ResetButtons();
        //Check if the mouse is hovering over the buttons
        int button = menuHelper.Hovering(mouseposition);
        switch (button)
        {
            case 2:
                current_Back = col_BackTextureID;
                if(clicked)
                {
                    Console.WriteLine("Back Button Clicked");
                    game.state = game.previousState;
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
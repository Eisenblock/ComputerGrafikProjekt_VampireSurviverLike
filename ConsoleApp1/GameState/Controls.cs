using OpenTK.Mathematics;
using OpenTK.Windowing.Desktop;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.GraphicsLibraryFramework;
using OpenTK.Input; // Add the missing import statement

internal class Controls
{
    Game game;
    private GameWindow myWindow;
    Texturer texturer = new Texturer(); // Create an instance of the Texturer class
    MenuHelper menuHelper = new MenuHelper();
    int BackgroundID;
    int BackTextureID;
    int col_BackTextureID;
    int current_Back;
    Vector2 mouseposition;

    public Controls(GameWindow window, Game game)
    {
        string Texture = "assets/ControlsBackground.png";
        BackgroundID = texturer.LoadTexture(Texture, 1)[0];
        string BackTexture = "assets/Back Button.png";
        BackTextureID = texturer.LoadTexture(BackTexture, 1)[0];
        current_Back = BackTextureID;

        string col_BackTexture = "assets/col_Back Button.png";
        col_BackTextureID = texturer.LoadTexture(col_BackTexture, 1)[0];

        this.game = game;
        myWindow = window;
    }
    public void Draw(GameWindow myWindow)
    {
        menuHelper.moved_button_y -= menuHelper.button_height + menuHelper.button_spacing;
        menuHelper.Draw(myWindow, new List<int> {BackgroundID, current_Back}, true);
    }

    public void ResetButtons()
    {
        current_Back = BackTextureID;
    }
    public void Hovering(Vector2 mouseposition, bool clicked)
    {
        ResetButtons();
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
        // Überprüfen Sie, ob der Mausklick innerhalb der Buttons liegt
        Console.WriteLine("Mouse Clicked");
        Hovering(mouseposition, true);
    }

}
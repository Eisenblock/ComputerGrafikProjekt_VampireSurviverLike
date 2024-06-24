using System.Drawing; 
using OpenTK.Mathematics;
using OpenTK.Windowing.Desktop;


internal class MenuHelper
{
    // instances of other classes
    Texturer texturer = new Texturer(); // Creates an instance of the Texturer class

    // variables
    public float button_length = 0.5f;
    public float button_height = 0.2f;
    public float button_spacing = 0.075f;
    public float button_y = -0.35f;
    public float moved_button_y;
    float startY = 0.6f; // Start Y position set to 0.6f
    float textHeight = 0.1f; // Height of each text
    float textWidth = 0.05f; // Width of each text
    float textSpacing = 0.05f; // Spacing between texts
    float textLength = 0.8f; // Length of the text area

    public void DrawBackground(GameWindow myWindow, int textures)
    {
        var rect = new RectangleF(-1f, -1f, 2f, 2f);
        var tex_rect = new RectangleF(0f, 0f, 1f, 1f);
        texturer.Draw(textures, rect, tex_rect); // Hintergrundbild zeichnen
    }

    public void DrawPicture(GameWindow myWindow, int textures)
    {
        var rect = new RectangleF(-0.65f, -0.15f, 1.3f, 0.65f);
        var tex_rect = new RectangleF(0f, 0f, 1f, 1f);
        texturer.Draw(textures, rect, tex_rect);
    }

    public void DrawScoresAndHighscores(GameWindow myWindow, List<int> texts, List<int> score, List<int> highscore)
    {
        float currentY = startY - textHeight - textSpacing - 0.05f; // Beginnt unter dem Titel
    
        // Draw "Scores" title and scores
        currentY = DrawSubTitle(texts[0], currentY);
        currentY = DrawScores(score, currentY, true);
    
        // Draw "Highscore" title and highscores
        currentY = DrawSubTitle(texts[1], currentY);
        DrawScores(highscore, currentY, false);
    }

    public float DrawTitle(int textId, float currentY)
    {
        currentY = startY;
        // Increase the height of the title
        float increasedTextHeight = textHeight * 1.5f; 
        float increasedTextLength = textLength * 1.5f;
    
        var titleRect = new RectangleF(-increasedTextLength / 2, currentY, increasedTextLength, increasedTextHeight);
        var titleTexRect = new RectangleF(0f, 0f, 1f, 1f);
        texturer.Draw(textId, titleRect, titleTexRect);
        return currentY - increasedTextHeight - textSpacing;
    }
    
    public float DrawSubTitle(int textId, float currentY)
    {
        // Draw the subtitle
        var titleRect = new RectangleF(-textLength / 2, currentY, textLength, textHeight);
        var titleTexRect = new RectangleF(0f, 0f, 1f, 1f);
        texturer.Draw(textId, titleRect, titleTexRect);
        return currentY - textHeight - textSpacing;
    }
    private float DrawScores(List<int> scores, float currentY, bool isScore)
    {
        //scoreWidth is the width of the score area
        float scoreWidth = 4 * (textLength / 5) + (3 * textSpacing); 
        float currentX = -scoreWidth / 2; 
        // Draw the scores
        foreach (int score in scores)
        {
            var rect = new RectangleF(currentX, currentY, textLength / 5, textHeight * 0.9f);
            var tex_rect = new RectangleF(0f, 0f, 1f, 1f);
            texturer.Draw(score, rect, tex_rect);
            currentX += textLength / 5 + textSpacing;
        }
        return currentY - (isScore ? textHeight + textSpacing : 0); 
    }

    public void DrawButtons(GameWindow myWindow, List<int> textures)
    {
        float moved_button_y = button_y;
        var tex_rect = new RectangleF(0f, 0f, 1f, 1f);
        // if only one button, move it in the middle spot
        if (textures.Count == 1)
        {
            moved_button_y -= button_height + button_spacing;
        }

        for (int i = 0; i < textures.Count; i++)
        {
            int currentID = textures[i];
            var rect = new RectangleF(-button_length / 2, moved_button_y, button_length, button_height);
            moved_button_y -= button_height + button_spacing;
            texturer.Draw(currentID, rect, tex_rect);
        }
    }

    private bool IsMouseOver(Vector2 mouseposition, float minX, float maxX, float minY, float maxY)
    {
        return mouseposition.X > minX && mouseposition.X < maxX && mouseposition.Y > minY && mouseposition.Y < maxY;
    }

    public int Hovering(Vector2 mouseposition)
    {
        var length = button_length/2;
        var pos_Y = button_y;
        if (IsMouseOver(mouseposition, -length, length, pos_Y, pos_Y+button_height))
        {
            return 1;
        }
        pos_Y -= button_height + button_spacing;

        if (IsMouseOver(mouseposition, -length, length, pos_Y, pos_Y+button_height))
        {
            return 2;
        }
        pos_Y -= button_height + button_spacing;

        if (IsMouseOver(mouseposition, -length, length, pos_Y, pos_Y+button_height))
        {
            return 3;
        }
        return 0;
    }
}
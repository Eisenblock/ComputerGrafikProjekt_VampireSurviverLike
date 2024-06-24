using OpenTK.Windowing.Desktop;
using OpenTK.Graphics.OpenGL;
using System.Drawing;

public class Running
{
    //instances of other classes
    GameWindow myWindow;
    SoundsPlayer soundsPlayer;
    Score score;
    Texturer texturer = new Texturer();

    //variables for the music
    string music = "assets/Music.wav";
    public Running(GameWindow myWindow, SoundsPlayer soundsPlayer, Score score)
    {
        this.myWindow = myWindow;
        this.score = score;
        this.soundsPlayer = soundsPlayer;
        soundsPlayer.PlaySoundAsync(music, true);
    }
    
    public void Draw()
    {
        float startX = -0.98f;
        float startY = 0.98f;
        float width = 0.05f;
        float height = 0.1f; 
        float spacing = 0.005f;

        // Draw the game
        List<int> scoreList = score.ScoreToTexture();
        float currentX = startX;
        GL.Color4(Color.White);
        for (int i = 0; i < scoreList.Count; i++)
        {
            var rect = new RectangleF(currentX, startY - height, width, height);
            var tex_rect = new RectangleF(0f, 0f, 1f, 1f);
            texturer.Draw(scoreList[i], rect, tex_rect);
            currentX += width + spacing;
        }
    }
}

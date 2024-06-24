public class Score
{
    private int score;
    private int highscore;
    List<int> numbersID;
    Texturer texturer = new Texturer();
    string path = "assets/highscore.txt";

    public Score()
    {
        string numbers = "assets/numbers1.png";
        numbersID = texturer.LoadTexture(numbers, 10,1);
    }

    public void AddScore(int points)
    {
        score += points;

        if (score > highscore)
        {
            highscore = score;
        }
    }

    public void ResetScore()
    {
        score = 0;
    }

    public int GetScore()
    {
        return score;
    }

    public int GetHighscore()
    {
        return highscore;
    }

    public List<int> ScoreToTexture()
    {
        // fill with zeros
        List<int> scoreTexture = new List<int> { numbersID[0], numbersID[0], numbersID[0], numbersID[0] };
        int scoreCopy = score;
        int digit = 0;
        int index = 3; // Start from the end of the list
        while (scoreCopy > 0 && index >= 0)
        {
            digit = scoreCopy % 10;
            scoreTexture[index] = numbersID[digit];
            scoreCopy /= 10;
            index--;
        }
        return scoreTexture;
    }
    
    public List<int> HighscoreToTexture()
    {
        // fill with zeros
        List<int> highscoreTexture = new List<int> { numbersID[0], numbersID[0], numbersID[0], numbersID[0] };
        int highscoreCopy = highscore;
        int digit = 0;
        int index = 3; // Start from the end of the list
        while (highscoreCopy > 0 && index >= 0)
        {
            digit = highscoreCopy % 10;
            highscoreTexture[index] = numbersID[digit];
            highscoreCopy /= 10;
            index--;
        }
        return highscoreTexture;
    }

    public void SaveHighscore()
    {
        // Save the highscore to a file
        if (!File.Exists(path))
        {
            File.Create(path).Close();
        }
        string[] lines = File.ReadAllLines(path);
        // If the file is empty, write the highscore
        if (lines.Length == 0)
        {
            File.WriteAllText(path, highscore.ToString());
        }
        // If the file is not empty, compare the highscore with the old highscore
        else
        {
            int oldHighscore = int.Parse(lines[0]);
            if (highscore > oldHighscore)
            {
                File.WriteAllText(path, highscore.ToString());
            }
        }
    }

    public void LoadHighscore()
    {
        // Load the highscore from a file
        if (File.Exists(path))
        {
            string[] lines = File.ReadAllLines(path);
            if (lines.Length > 0)
            {
                highscore = int.Parse(lines[0]);
            }
        }
    }
}
using OpenTK.Mathematics;

class EnemyList
{    

    public Enemy[] enemies { get; set; }
    Enemy[] array = new Enemy[10];
    private double spawnTimer;
    private double lastPrintedTime = 0;
    private int count;

    public EnemyList()
    {
        enemies = new Enemy[10];
        InitializeEnemies();
    }

    private void InitializeEnemies()
    {
        // Initialize enemies array with Enemy objects
        for (int i = 0; i < enemies.Length; i++)
        {
            Random random = new Random();
            float x = (float)random.NextDouble() * 2 - 1; // Zufällige x-Position zwischen -1 und 1
            float y = (float)random.NextDouble() * 2 - 1; // Zufällige y-Position zwischen -1 und 1

            enemies[i] = new Enemy(new Vector2(x,y));
        }
    }

    public void DrawArray()
    {
        for (int i = 0; i < count; i++)
        {
            enemies[i].Draw();
        }
    }

    public void UpdateTimer(double timer)
    {
        spawnTimer = timer;
        double flooredTimer = Math.Floor(spawnTimer);
        if(Math.Floor(flooredTimer) % 3 == 0 && flooredTimer != lastPrintedTime){
            Console.WriteLine("Spawned enemy at time: " + flooredTimer);
            DrawArray();
            count++;
            lastPrintedTime = flooredTimer;
        }
    }
}
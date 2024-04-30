using OpenTK.Mathematics;
using System;


class EnemyList
{    

    Player player;
    Enemy enemy;
    public List<Enemy> enemies { get; set; }
    private List<Enemy> enemyList_Remove;
    private double spawnTimer = 3; //wie schnell sollen gegner spawnen
    private double lastPrintedTime = 0;
    private int count;

    public EnemyList(Player player,Enemy enemy)
    {
        this.player = player;
        this.enemy = enemy;
        enemies = new List<Enemy>();
    }

    private void InitializeEnemy()
    {
        // Initialize enemy array with Enemy objects
        Random random = new Random();
        float x, y;

        do
        {
            x = (float)random.NextDouble() * 2.6f - 1.3f; // Zufällige x-Position zwischen -1.3 und 1.3
            y = (float)random.NextDouble() * 2.6f - 1.3f; // Zufällige y-Position zwischen -1.3 und 1.3
        }
        while (Math.Abs(x) < 1 && Math.Abs(y) < 1); // Wiederhole, bis die Position außerhalb des Bereichs -1 bis 1 liegt

        // random Gegner spawnen
        random = new Random();
        int randomNumber = random.Next(1, 4); // Generiert eine Zufallszahl zwischen 1 und 3 einschließlich
        if (randomNumber == 1)
        {
            Enemy enemy = new BigEnemy(new Vector2(x, y), false,1);
            enemies.Add(enemy);
            Console.WriteLine("qqq");
        }
        else if (randomNumber == 2)
        {
            Enemy enemy = new FastEnemy(new Vector2(x, y), false,1);
            enemies.Add(enemy);
            Console.WriteLine("qqq");
        }
        else if (randomNumber == 3)
        {
            Enemy enemy = new RangedEnemy(new Vector2(x, y), false,1);
            enemies.Add(enemy);
            Console.WriteLine("qqq");
        }
        UpdateList();
    }

    public void DrawArray()
    {
        foreach (Enemy enemy in enemies)
        {
            if(enemy.enemyDead != true)
            {
                enemy.Draw(enemy.Color);
            }
            else
            {
                enemyList_Remove = enemies;
            }
        }
       
    }

    private void UpdateList()
    {
        enemies.RemoveAll(enemy => enemy.enemyDead == true);
    }

    public void UpdateTimer(double timer)
    {
        double flooredTimer = Math.Floor(timer);
        if(Math.Floor(flooredTimer) % spawnTimer == 0 && flooredTimer != lastPrintedTime){
            Console.WriteLine("Spawned enemy at time: " + flooredTimer);

            InitializeEnemy();

            DrawArray();
            count++;
            lastPrintedTime = flooredTimer;
        }
    }
}
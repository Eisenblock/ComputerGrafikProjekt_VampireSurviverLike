using OpenTK.Mathematics;
using System;


class EnemyList
{    

    Player player;
    Shoot shoot;
    Enemy enemy;
    public Enemy[] enemies { get; set; }
    private double spawnTimer = 3; //wie schnell sollen gegner spawnen
    private double lastPrintedTime = 0;
    private int count;

    public EnemyList(Player player, Shoot shoot, Enemy enemy)
    {
        this.player = player;
        this.shoot = shoot;
        this.enemy = enemy;
        enemies = new Enemy[10];
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
            enemies[count] = new BigEnemy(new Vector2(x, y), false);
        }
        else if (randomNumber == 2)
        {
            enemies[count] = new FastEnemy(new Vector2(x, y), false);
        }
        else if (randomNumber == 3)
        {
            enemies[count] = new RangedEnemy(new Vector2(x, y), false);
        }
    }

    public void DrawArray()
    {
        for (int i = 0; i < count; i++)
        {
            if (enemies[i].enemyDead != true)
            {
                //draw enemy mit seiner speziellen Farbe
                enemies[i].Draw(enemies[i].Color);
            }
        }
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

    public void CheckCollisionPlayer()
    {
        //Console.WriteLine(player.Position);
        foreach (Enemy enemy in enemies)
        {
            if (player.CheckCollision(enemy) == true)
            {
                //Console.WriteLine("Treffer Player - Enemy");
                // Hier fügst du den Code für die Behandlung der Kollision hinzu
            }
        }
    }

    public void CheckCollisionShoot()
     {
        int i = 0;
        foreach (Enemy enemy in enemies)
         {
            
            if(enemy != null && shoot != null){
                if (shoot.CheckCollision(enemy) == true )
                {   
                    Vector2 pos = enemy.Position;
                    Console.WriteLine("Treffer Shoot - Enemy");
                    enemies[i] = new Enemy(pos, true);
                    // Hier fügst du den Code für die Behandlung der Kollision hinzu
                }
            }
            i++;
          }
    }
}
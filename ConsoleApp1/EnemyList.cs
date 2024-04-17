using OpenTK.Mathematics;

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
            float x = (float)random.NextDouble() * 2 - 1; // Zufällige x-Position zwischen -1 und 1
            float y = (float)random.NextDouble() * 2 - 1; // Zufällige y-Position zwischen -1 und 1

            enemies[count] = new Enemy(new Vector2(x,y),false);
    }

    public void DrawArray()
    {
        for (int i = 0; i < count; i++)
        {
            if (enemies[i].enemyDead != true)
            {
                enemies[i].Draw();
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
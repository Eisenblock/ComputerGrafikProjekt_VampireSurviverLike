using OpenTK.Mathematics;

class EnemyList
{
    //instances of other classes
    Entity player;
    GUI gui;
    Particles particles = new Particles(new List<int>());
    List<int> particlesList;
    Score score = new Score();

    //variables
    public List<Enemy> enemies { get; set; }
    private double spawnTimer = 2; //Timer for the spawn of the enemies
    private double lastPrintedTime = 0; //Last time the enemies were spawned
    private int deadEnemiesCount = 0;
    private int neededKills = 5; //Number of kills needed to spawn the boss
    private bool bossAlive = false;
    int bossHealth = 6;
    int deadBosses = 0;
    float t = 0f;

    public EnemyList(Entity player, GUI gui, Score score)
    {
        this.score = score;
        this.gui = gui;
        this.player = player;
        enemies = new List<Enemy>();
        particlesList = particles.InitParticles();
    }

    public void ClearAll()
    {
        lastPrintedTime = 0;
        spawnTimer = 2;
        deadEnemiesCount = 0;
        neededKills = 10;
        for (int i = 0; i < enemies.Count; i++)
        {
            if (enemies[i] is BossEnemy)
            {
                gui.RemoveBoss(enemies[i]);
            }
        }
        bossAlive = false;
        enemies.Clear();
    }
    private void InitializeEnemy()
    {
        // Initialize random enemy position
        Random random = new Random();
        float x, y;
        do
        {
            x = (float)random.NextDouble() * 2.6f - 1.3f;
            y = (float)random.NextDouble() * 2.6f - 1.3f;
        }
        while (Math.Abs(x) < 1 && Math.Abs(y) < 1); // Repeat until the enemy is not spawned in the arena

        // Enemies spawnen
        Console.WriteLine("Enemy spawned: " + (deadEnemiesCount % neededKills));
        Console.WriteLine("Enemy dead: " + deadEnemiesCount);
        Console.WriteLine("Boss killed: " + bossAlive);
        if (deadEnemiesCount % neededKills == 0 && deadEnemiesCount > 0 && bossAlive == false)
        {
            Enemy enemy = new BossEnemy(new Vector2(x, y), false, 2, Vector2.Zero, bossHealth + deadBosses, particlesList);
            enemies.Add(enemy);
            bossAlive = true;
            Console.WriteLine("Boss spawned");
            gui.AddBoss(enemy);
        }
        // random Gegner spawnen
        else
        {
            random = new Random();
            int randomNumber = random.Next(1, 4); // Random number between 1 and 3
            if (randomNumber == 1)
            {
                Enemy enemy = new BigEnemy(new Vector2(x, y), false, 1, Vector2.Zero, particlesList);
                enemies.Add(enemy);
            }
            else if (randomNumber == 2)
            {
                Enemy enemy = new FastEnemy(new Vector2(x, y), false, 1, Vector2.Zero, particlesList);
                enemies.Add(enemy);
            }
            else if (randomNumber == 3)
            {
                Enemy enemy = new RangedEnemy(new Vector2(x, y), false, 1, Vector2.Zero, particlesList);
                enemies.Add(enemy);
            }
        }

        UpdateList();
    }

    public void DrawArray(double timer, float time)
    {
        // Draws all enemies
        foreach (Enemy enemy in enemies)
        {
            t = time;
            enemy.setTargetPosition(player.Position);
            enemy.setTime(timer);
            //enemy.GetTimer(time);
            enemy.Draw(enemy.scale);
        }
    }

    public void UpdateTimerSpeed(float time)
    {
        foreach(Enemy enemy in enemies)
        {
            enemy.GetTimer(time);
            t = time;
        }
    }

    private void UpdateList()
    {
        int beforeRemovalCount = enemies.Count;
        // Checking if the enemy is dead
        for (int i = enemies.Count - 1; i >= 0; i--)
        {
            if (enemies[i].enemyDead == true && enemies[i] is BossEnemy && bossAlive == true)
            {
                if ((DateTime.Now - enemies[i].LastCollision).TotalSeconds >= 1)
                {
                    bossAlive = false;
                    deadBosses++;
                    gui.RemoveBoss(enemies[i]);
                    score.AddScore(5);
                    NextWave();
                    enemies.RemoveAt(i); // Deletes the boss
                    break;
                }

            }
            if (enemies[i].enemyDead == true)
            {
                // Delete the enemy after 1 second
                if ((DateTime.Now - enemies[i].LastCollision).TotalSeconds >= 1)
                {
                    enemies.RemoveAt(i);
                    score.AddScore(1); // add score for each killed enemy
                }
            }
        }

        int afterRemovalCount = enemies.Count;

        deadEnemiesCount += beforeRemovalCount - afterRemovalCount;
    }
    public void NextWave()
    {
        // Increase the difficulty
        neededKills -= 1;
        spawnTimer -= 0.25;
        if (spawnTimer < 0.5)
        {
            spawnTimer = 0.5;
        }
        deadEnemiesCount = 0;
    }

    public void UpdateTimer(double timer,float time)
    {
        UpdateList();
        double roundedTimer = Math.Round(timer, 1);
        if (roundedTimer % spawnTimer == 0 && roundedTimer != lastPrintedTime)
        {

            InitializeEnemy();

            DrawArray(timer,time);
            lastPrintedTime = roundedTimer;
        }
    }
}
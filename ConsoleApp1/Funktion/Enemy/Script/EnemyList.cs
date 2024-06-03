using OpenTK.Mathematics;
using System;
using System.Drawing.Printing;


class EnemyList
{    

    Entity player;
    Enemy enemy;
    public List<Enemy> enemies { get; set; }
    private List<Enemy> enemyList_Remove;
    private double spawnTimer = 2; //wie schnell sollen gegner spawnen
    private double lastPrintedTime = 0;
    private int count;
    private int deadEnemiesCount = 0;
    private int neededKills = 5;
    private bool bossAlive = false;
    GUI gui;

    public EnemyList(Entity player,Enemy enemy, GUI gui)
    {
        this.gui = gui;
        this.player = player;
        this.enemy = enemy;
        enemies = new List<Enemy>();
    }

    public void ClearAll()
    {
        enemies.Clear();
        lastPrintedTime = 0;
        count = 0;
        deadEnemiesCount = 0;
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

        // Enemies Spawnen
        if (deadEnemiesCount % neededKills == 0 && deadEnemiesCount > 0 && bossAlive == false)
        {
            Enemy enemy = new BossEnemy(new Vector2(x, y), false, 2, Vector2.Zero);
            enemies.Add(enemy);
            bossAlive = true;
            Console.WriteLine("Boss spawned");
            gui.AddBoss(enemy);
        }
        // random Gegner spawnen
        else
        {
            random = new Random();
            int randomNumber = random.Next(1, 4); // Generiert eine Zufallszahl zwischen 1 und 3 einschließlich
            if (randomNumber == 1)
            {
                Enemy enemy = new BigEnemy(new Vector2(x, y), false,1 ,Vector2.Zero);
                enemies.Add(enemy);
            }
            else if (randomNumber == 2)
            {
                Enemy enemy = new FastEnemy(new Vector2(x, y), false, 1, Vector2.Zero);
                enemies.Add(enemy);
            }
            else if (randomNumber == 3)
            {
                Enemy enemy = new RangedEnemy(new Vector2(x, y), false,1 ,Vector2.Zero);
                enemies.Add(enemy);
            }
        }
        
        UpdateList();
    }

    public void DrawArray(double timer)
    {
        foreach (Enemy enemy in enemies)
        {
            enemy.setTargetPosition(player.Position);
            enemy.setTime(timer);
            enemy.Draw(enemy.scale);
        }
       
    }

    private void UpdateList()
    {
        int beforeRemovalCount = enemies.Count;
        // Überprüfen Sie, ob ein Boss-Feind entfernt wird
        for (int i = 0; i < enemies.Count; i++)
        {
            if (enemies[i].enemyDead == true && enemies[i] is BossEnemy && bossAlive == true)
            {
                bossAlive = false;
                gui.RemoveBoss(enemies[i]);
                NextWave();
                break;
            }
        }

        enemies.RemoveAll(enemy => enemy.enemyDead == true && (DateTime.Now - enemy.LastCollision).TotalSeconds >= 1);
        int afterRemovalCount = enemies.Count;

        // Erhöhe die Zählvariable für jeden getöteten Feind
        deadEnemiesCount += beforeRemovalCount - afterRemovalCount;
    }
    public void NextWave()
    {
        neededKills += 5;
        spawnTimer -= 0.25;
        if (spawnTimer < 0.5)
        {
            spawnTimer = 0.5;
        }
    }

    public void UpdateTimer(double timer)
    {
        UpdateList();
        double roundedTimer = Math.Round(timer, 1);
        Console.WriteLine("Timer: " + roundedTimer);
        Console.WriteLine("spawntimer: " + spawnTimer);
        if(roundedTimer % spawnTimer == 0 && roundedTimer != lastPrintedTime){

            InitializeEnemy();

            DrawArray(timer);
            count++;
            lastPrintedTime = roundedTimer;
        }
    }
}
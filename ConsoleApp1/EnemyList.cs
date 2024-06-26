﻿using OpenTK.Mathematics;
using System;
using System.Drawing.Printing;


class EnemyList
{    

    Player player;
    Enemy enemy;
    public List<Enemy> enemies { get; set; }
    private List<Enemy> enemyList_Remove;
    private double spawnTimer = 3; //wie schnell sollen gegner spawnen
    private double lastPrintedTime = 0;
    private int count;
    private int deadEnemiesCount = 0;
    private bool bossAlive = false;

    public EnemyList(Player player,Enemy enemy)
    {
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
        if (deadEnemiesCount % 10 == 0 && deadEnemiesCount > 0 && bossAlive == false)
        {
            Enemy enemy = new BossEnemy(new Vector2(x, y), false, 2);
            enemies.Add(enemy);
            bossAlive = true;
            Console.WriteLine("Boss spawned");
        }
        // random Gegner spawnen
        else
        {
            random = new Random();
            int randomNumber = random.Next(1, 4); // Generiert eine Zufallszahl zwischen 1 und 3 einschließlich
            if (randomNumber == 1)
            {
                Enemy enemy = new BigEnemy(new Vector2(x, y), false,1 );
                enemies.Add(enemy);
            }
            else if (randomNumber == 2)
            {
                Enemy enemy = new FastEnemy(new Vector2(x, y), false, 1);
                enemies.Add(enemy);
            }
            else if (randomNumber == 3)
            {
                Enemy enemy = new RangedEnemy(new Vector2(x, y), false,1 );
                enemies.Add(enemy);
            }
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
        int beforeRemovalCount = enemies.Count;
        // Überprüfen Sie, ob ein Boss-Feind entfernt wird
        foreach (var enemy in enemies)
        {
            if (enemy.enemyDead == true && enemy is BossEnemy)
            {
                bossAlive = false;
            }
        }


        enemies.RemoveAll(enemy => enemy.enemyDead == true);
        int afterRemovalCount = enemies.Count;

        // Erhöhe die Zählvariable für jeden getöteten Feind
        deadEnemiesCount += beforeRemovalCount - afterRemovalCount;
    }

    public void UpdateTimer(double timer)
    {
        UpdateList();
        double flooredTimer = Math.Floor(timer);
        if(Math.Floor(flooredTimer) % spawnTimer == 0 && flooredTimer != lastPrintedTime){

            InitializeEnemy();

            DrawArray();
            count++;
            lastPrintedTime = flooredTimer;
        }
    }
}
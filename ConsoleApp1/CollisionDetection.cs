using System;
using System.Collections.Generic;
using System.Drawing.Printing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK.Mathematics;

class CollisionDetection
{
 

    private bool CheckCollision(Circle circle1, Circle circle2)
    {
        float distanceSquared = (circle1.Center - circle2.Center).LengthSquared;
        float radiusSumSquared = (circle1.Radius + circle2.Radius) * (circle1.Radius + circle2.Radius);
        
        //Console.WriteLine("<" + circle1.Center);
        //Console.WriteLine( "<" + circle2.Center);
        return distanceSquared < radiusSumSquared;
    }

     public void CheckCollision(Player player, List<Enemy> enemies, List<Shoot> shoots)
    {

        if (enemies != null)
        {
            foreach (Enemy enemy in enemies)
            {
                if (enemy != null)
                {
                    if(CheckCollision(player.bounds,enemy.boundEnemy) == true)
                    {
                        Console.WriteLine("Hit");
                        enemy.isActive = false;
                        if(enemy.enemyDead == true)
                        {
                            enemy.enemyDmg = 0;
                        }
                                           
                        player.DecreaseHealth(enemy.enemyDmg);
                    }                  
                }
            }
          
        }

       if(shoots != null)
        {
            foreach(Shoot shoot in shoots)
            {
                if (shoot.isLive == true && shoot.shotbyPlayer == true)
                {
                    foreach (Enemy enemy in enemies)
                    {
                        if (enemy != null){
                            if (CheckCollision(shoot.boundShoot, enemy.boundEnemy) == true)
                        {
                            // Überprüfen, ob seit der letzten Kollision mindestens eine Sekunde vergangen ist
                            if ((DateTime.Now - enemy.LastCollision).TotalSeconds >= 1)
                            {
                                // Console.WriteLine("Hit Bullet");
                                enemy.DecreaseHealth();
                                enemy.LastCollision = DateTime.Now; // Aktualisieren des Zeitstempels der letzten Kollision
                            }
                        }
                        }
                        
                    }
                }
                if(shoot.isLive == true && shoot.shotbyPlayer == false){
                    if(CheckCollision(shoot.boundShoot,player.bounds) == true)
                    {
                        if ((DateTime.Now - player.LastCollision).TotalSeconds >= 1){
                            player.DecreaseHealth(1);
                            Console.WriteLine("Hit Player");
                            player.LastCollision = DateTime.Now;
                            shoot.isLive = false;
                        }
                        else
                        {
                            player.ResetColor(); // Setzen Sie die Farbe des Spielers zurück, wenn die letzte Kollisionszeit größer oder gleich 1 ist
                        }
                    }
                }
            }
        }
        
    }
}

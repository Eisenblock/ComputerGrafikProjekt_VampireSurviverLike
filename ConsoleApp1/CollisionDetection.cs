using System;
using System.Collections.Generic;
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
                if (shoot.isLive == true)
                {
                    foreach (Enemy enemy in enemies)
                    {
                        if (enemy != null)
                        {

                            if (CheckCollision(shoot.boundShoot, enemy.boundEnemy) == true)
                            {
                                //Console.WriteLine("Hit Bullet");
                                enemy.enemyDead = true;
                            }
                        }
                    }
                }
            }
        }
        
    }
}

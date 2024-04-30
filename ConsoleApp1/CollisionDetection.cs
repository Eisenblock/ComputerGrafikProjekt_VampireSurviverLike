using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

class CollisionDetection
{
    int i = 0;

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
        int collisionCount = 0;
        int collisionCount_Shoot = 0;// Variable zur Verfolgung der Anzahl von Kollisionen

        if (enemies != null)
        {
            
                foreach (Enemy enemy in enemies)
                {
                if (enemy != null)
                {
                    if(CheckCollision(player.bounds,enemy.boundEnemy) == true)
                    {
                        //Console.WriteLine("Hit");
                    }
                    collisionCount++;
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
                        
                        if (CheckCollision(shoot.boundShoot, enemy.boundEnemy) == true)
                        {
                            //Console.WriteLine("Hit Bullet");
                            enemy.enemyDead = true;
                        }
                        collisionCount_Shoot++;
                    }
                }
            }
        }
        
    }
}

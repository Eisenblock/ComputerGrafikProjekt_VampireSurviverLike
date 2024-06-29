class CollisionDetection
{
    float shakeduratio = 0;
    private bool CheckCollision(Circle circle1, Circle circle2)
    {
        float distanceSquared = (circle1.Center - circle2.Center).LengthSquared;
        float radiusSumSquared = (circle1.Radius + circle2.Radius) * (circle1.Radius + circle2.Radius);
        
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
                    if(CheckCollision(player.bounds,enemy.boundEnemy) == true && enemy.enemyDead == false)
                    { 
                        enemy.isActive = false;
                        if ((DateTime.Now - player.LastCollision).TotalSeconds >= 1)
                        {                           
                            player.DecreaseHealth(enemy.Dmg);
                            Console.WriteLine(player.health);
                            if(player.health <= 0 )
                            {
                                shakeduratio = 0;
                            }
                            else
                            {
                                shakeduratio = 0.25f;
                            }
                            
                            player.LastCollision = DateTime.Now; // Update the last collision time
                        }
                       
                    }
                    
                    if ((DateTime.Now - player.LastCollision).TotalSeconds >= 0.25f)  {
                        shakeduratio = 0;
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
                        if (enemy != null)
                        {
                            if (CheckCollision(shoot.boundShoot, enemy.boundEnemy) == true && enemy.enemyDead == false)
                            {
                                shoot.isLive = false;
                                enemy.DecreaseHealth();
                                enemy.LastCollision = DateTime.Now;
                                enemy.isActive = false;
                                /*if ((DateTime.Now - enemy.LastCollision).TotalSeconds >= 1){
                                    enemy.DecreaseHealth();
                                    enemy.LastCollision = DateTime.Now;
                                    enemy.isActive=false;
                                }*/

                            }
                        } 
                    }
                } 

                if(shoot.isLive == true && shoot.shotbyPlayer == false){
                    if(CheckCollision(shoot.boundShoot,player.bounds) == true)
                    {
                            player.DecreaseHealth(1);
                            player.LastCollision = DateTime.Now;
                            if (player.health <= 0)
                            {
                                shakeduratio = 0;
                            }
                            else
                            {
                                shakeduratio = 0.25f;
                            }
                            shoot.isLive = false;
                    }

                    if ((DateTime.Now - player.LastCollision).TotalSeconds >= 0.25f)
                    {
                        shakeduratio = 0;
                    }
                }
            }
        }
        
    }

    public float UpdateShakeDuration()
    {
        

        return shakeduratio;
    }
}

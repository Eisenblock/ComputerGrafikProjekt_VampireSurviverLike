using OpenTK.Mathematics;

internal class Shootlist
{
    //variables
    bool shootTrueE = true;
    bool shootTrueP = true;
    double playerSpeed = -0.5;
    double enemySpeed = -0.75;
    public List<Shoot> shootList;
    public Shootlist(Entity entity)
    {      
        shootList = new List<Shoot>();
    }
    public void ClearAll()
    {
        // Clear the shootList
        shootList.Clear();
        shootTrueE = true;
        shootTrueP = true;
    }

    public void InitializeShoot(Vector2 target, Entity entity, double time)
    {
        if (entity.shootTrue)
        {
            int speed = entity is Enemy ? 2 : entity is Player ? 4 : 0; //Speed of the shoot depends on the entity
            if (speed > 0)
            {
                Shoot shoot = new Shoot(entity, target, speed, time, true, true);
                shoot.targetPos = target;
                shoot.lastPrintedTime = time;
                entity.lastShoottime = time;
                shoot.shootPos = entity.Position;
                this.shootList.Add(shoot);
                entity.shootTrue = false;
            }
        }
    }

    public void ShootDirectionList(double timer)
    {
        List<Shoot> shootsToRemove = new List<Shoot>();

        // Update the position of the shoots
        foreach (Shoot shoot in shootList.ToList())
        {
            double timeDifference = shoot.lastPrintedTime - timer;
            // Remove the shoot if the lifetime is over
            if (timeDifference <= -shoot.lifetime)
            {
                shootsToRemove.Add(shoot);
                shoot.isLive = false;
            }
            else
            {
                shoot.ShootDirection(0.0003f);
            }

            //attackspeed
            double timeDifference2 = shoot.entity.lastShoottime - timer;
            if (timeDifference2 <= playerSpeed)
            {
                if (shoot.shotbyPlayer == true)
                {
                    shoot.entity.shootTrue = true;
                }
            }
            double timeDifference3 = shoot.entity.lastShoottime - timer;
            if (timeDifference3 <= enemySpeed)
            {
                if (shoot.shotbyPlayer == false)
                {
                    shoot.entity.shootTrue = true;
                }
            }
        }

        // Remove the shoots from the shootList
        foreach (Shoot shootToRemove in shootsToRemove)
        {
            this.shootList.Remove(shootToRemove);
        }
    }

    public void DrawShoots()
    {
        foreach(Shoot shoot in shootList)
        {
            shoot.Draw();
        }
    }
}
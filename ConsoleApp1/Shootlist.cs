using OpenTK.Mathematics;

internal class Shootlist
{

    Shoot shoot;

    private int lifetime;
    private double lastShoottime;
    bool shootTrueE = true;
    bool shootTrueP = true;

    public List<Shoot> shootList;
    public Shootlist(Entity entity)
    {      
        shootList = new List<Shoot>();
    }
    public void ClearAll()
    {
        shootList.Clear();
        lastShoottime = 0;
        shootTrueE = true;
        shootTrueP = true;
    }

    public void InitializeShoot(Vector2 target,Entity entity, double time)
    {
        if (entity is Enemy && shootTrueE == true)
        {
            Shoot shoot = new Shoot(entity, target, 4, time, true, true);
            shoot.targetPos = target;
            shoot.lastPrintedTime = time;
            lastShoottime = time;
            shoot.shootPos = entity.Position;
            this.shootList.Add(shoot);
            shootTrueE = false;
        }
        else if (entity is Player && shootTrueP == true)
        {
            Shoot shoot = new Shoot(entity, target, 4, time, true, true);
            shoot.targetPos = target;
            shoot.lastPrintedTime = time;
            lastShoottime = time;
            shoot.shootPos = entity.Position;
            this.shootList.Add(shoot);
            shootTrueP = false;
        }
    }

    public void ShootDirectionList(double timer)
    {
        List<Shoot> shootsToRemove = new List<Shoot>();

        //lifetime : wird aus Array gelösht
        foreach (Shoot shoot in shootList.ToList())
        {
            double timeDifference = shoot.lastPrintedTime - timer;
            if (timeDifference <= -shoot.lifetime)
            {
                shootsToRemove.Add(shoot);
                shoot.isLive = false;
            }
            else
            {
                shoot.ShootDirection(0.0001f);
            }

            //attachspeed
            double timeDifference2 = lastShoottime - timer;
            if (timeDifference2 <= -1)
            {
                if (shoot.shotbyPlayer == true)
                {
                    shootTrueP = true;
                }
                else
                {
                    shootTrueE = true;
                }
            }
        }

        // Entferne die zu entfernenden Shoot-Objekte aus der Liste
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
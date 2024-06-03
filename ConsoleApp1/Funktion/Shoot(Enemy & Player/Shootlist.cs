using OpenTK.Mathematics;
using System;

internal class Shootlist
{
    Shoot shoot;
    Enemy enemy;    
    
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
        shootTrueE = true;
        shootTrueP = true;
    }

    public void InitializeShoot(Vector2 target,Entity entity, double time)
    {
        if (entity is Enemy && entity.shootTrue == true)
        {
            Shoot shoot = new Shoot(entity, target, 2, time, true, true);
            shoot.targetPos = target;
            shoot.lastPrintedTime = time;
            entity.lastShoottime = time;
            shoot.shootPos = entity.Position;
            this.shootList.Add(shoot);
            entity.shootTrue = false;
        }
        else if (entity is Player && entity.shootTrue == true)
        {
            Shoot shoot = new Shoot(entity, target, 4, time, true, true);
            shoot.targetPos = target;
            shoot.lastPrintedTime = time;
            entity.lastShoottime = time;
            shoot.shootPos = entity.Position;
            this.shootList.Add(shoot);
            entity.shootTrue = false;
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
                shoot.ShootDirection(0.0003f);
            }

            //attackspeed
            double timeDifference2 = shoot.entity.lastShoottime - timer;
            if (timeDifference2 <= -0.5)
            {
                if (shoot.shotbyPlayer == true)
                {
                    shoot.entity.shootTrue = true;
                }
            }
            double timeDifference3 = shoot.entity.lastShoottime - timer;
            if (timeDifference3 <= -0.75)
            {
                if (shoot.shotbyPlayer == false)
                {
                    shoot.entity.shootTrue = true;
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
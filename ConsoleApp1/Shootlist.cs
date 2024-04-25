﻿using OpenTK.Mathematics;

internal class Shootlist
{
   
    private int lifetime;
    private double lastShoottime;
    bool shootTrue = true;

    public List<Shoot> shootList;
    public Shootlist(Player player)
    {
        shootList = new List<Shoot>();
        
    }

    public void InitializeShoot(Vector2 target,Player player, double time)
    {
        if(shootTrue) 
        { 
            Shoot shoot = new Shoot(player,target,2,time);
            shoot.targetPos = target;
            shoot.lastPrintedTime = time;
            lastShoottime = time;
            shoot.shootPos = player.Position;
            
            this.shootList.Add(shoot);
            shootTrue = false;
        }
        

        Console.WriteLine("pp");
    }

    public void ShootDirectionList(double timer)
    {
        List<Shoot> shootsToRemove = new List<Shoot>();

        foreach (Shoot shoot in shootList.ToList())
        {
            double timeDifference = shoot.lastPrintedTime - timer;
            if (timeDifference <= -shoot.lifetime)
            {
                shootsToRemove.Add(shoot);
            }
            else
            {
                shoot.ShootDirection(0.0001f);
            }

            double timeDifference2 = lastShoottime - timer;
            if (timeDifference2 <= -1)
            {
                shootTrue = true;
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
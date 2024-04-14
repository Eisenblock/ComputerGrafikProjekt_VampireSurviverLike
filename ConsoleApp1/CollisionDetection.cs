using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

class CollisionDetection
{
    public bool CheckCollision(Circle circle1, Circle circle2)
    {
        float distanceSquared = (circle1.Center - circle2.Center).LengthSquared;
        float radiusSumSquared = (circle1.Radius + circle2.Radius) * (circle1.Radius + circle2.Radius);
        return distanceSquared < radiusSumSquared;
    }

    public bool CheckCollision(Player player, List<Circle> circles)
    {
        foreach (Circle circle in circles)
        {
            if (CheckCollision(player.bounds, circle))
            {
                return true; // Kollision gefunden
            }
        }
        return false; // Keine Kollision gefunden
    }
}

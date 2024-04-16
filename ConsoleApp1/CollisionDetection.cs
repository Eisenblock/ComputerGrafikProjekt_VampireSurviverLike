using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

class CollisionDetection
{
    EnemyList enemyList1 = new EnemyList();
    int i = 0;

    private bool CheckCollision(Circle circle1, Circle circle2)
    {
        float distanceSquared = (circle1.Center - circle2.Center).LengthSquared;
        float radiusSumSquared = (circle1.Radius + circle2.Radius) * (circle1.Radius + circle2.Radius);
        return distanceSquared < radiusSumSquared;
    }

     bool CheckCollision(Player player, Enemy[] enemies)
    {
        int collisionCount = 0; // Variable zur Verfolgung der Anzahl von Kollisionen

        if (enemies != null)
        {
            foreach (Enemy enemy in enemies)
            {
            }           
        }
        return collisionCount > 0; // Rückgabe, ob mindestens eine Kollision gefunden wurde
    }
}

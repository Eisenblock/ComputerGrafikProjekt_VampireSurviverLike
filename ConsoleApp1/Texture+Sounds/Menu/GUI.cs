using OpenTK.Mathematics;
using OpenTK.Graphics.OpenGL;
using System.Drawing;

internal class GUI
{
    //instances of other classes
    Texturer texturer = new Texturer(); // Create an instance of the Texturer class

    //variables
    List<Entity> entitylist = new List<Entity>(); // Initialize the entitylist

    //variables for the animation
    public string Texture_Hearts;
    public List<int> TextureID_Hearts;

    public GUI(Entity entity)
    {
        entitylist.Add(entity);

        Texture_Hearts = "assets/Hearts.png";
        TextureID_Hearts = texturer.LoadTexture(Texture_Hearts,3,1); // Call the LoadTexture method on the instance
    }
    public void AddBoss(Entity entity)
    {
        entitylist.Add(entity);
    }

    public void RemoveBoss(Entity entity)
    {
        entitylist.Remove(entity);
    }

    public void Draw()
    {
        GL.Color4(Color4.White);
        var rect_map = new RectangleF(-1f,-1f, 2f, 2f);
        var rect_wall = new RectangleF(rect_map.Left - 0.1f, rect_map.Top - 0.1f, rect_map.Width + 0.2f, rect_map.Height + 0.2f);
        var tex_rect = new RectangleF(0f, 0f, 1f, 1f);

        foreach (Entity entity in entitylist)
        {
            if (entity.IsPlayer)
            {
                DrawHeartsPlayer();
            }
            else
            {
                DrawHeartsEnemy();
            }
        }
    }

    public void DrawHeartsPlayer()
    {
        var current_health = entitylist[0].health;
        OpenTK.Mathematics.Vector2 pos = new OpenTK.Mathematics.Vector2(-1f, -1f);
        for (int i = 0; i < entitylist[0].max_Health/2; i++)
        {
            if (current_health >= 2)
            {
                // Draw full heart
                texturer.Draw(TextureID_Hearts[0], new RectangleF(-1f + i * 0.15f, -1f, 0.15f, 0.15f), new RectangleF(0f, 0f, 1f, 1f));
                current_health -= 2;
            }
            else if (current_health == 1)
            {
                // Draw half heart
                texturer.Draw(TextureID_Hearts[1], new RectangleF(-1f + i * 0.15f, -1f, 0.15f, 0.15f), new RectangleF(0f, 0f, 1f, 1f));
                current_health -= 1;
            }
            else
            {
                // Draw empty heart
                texturer.Draw(TextureID_Hearts[2], new RectangleF(-1f + i * 0.15f, -1f, 0.15f, 0.15f), new RectangleF(0f, 0f, 1f, 1f));
            }
        }
    }
    public void DrawHeartsEnemy()
    {
        // Draw the hearts for the Bosses
        for(int i = 1; i < entitylist.Count; i++)
        {
            var current_health = entitylist[i].health;
            float totalHearts = entitylist[i].max_Health / 2;
            float heartWidth = 0.05f; // Breite eines Herzens
            float totalWidth = totalHearts * heartWidth; // Gesamtbreite aller Herzen
            float startX = -totalWidth / 2; // Startposition, um Herzen zu zentrieren
            float offset_X = startX;
            float offset_Y = 0.15f;
    
            for (int j = 0; j < totalHearts; j++)
            {
                if (current_health >= 2)
                {
                    // Draw full heart
                    texturer.Draw(TextureID_Hearts[0], new RectangleF(entitylist[i].Position.X+offset_X, entitylist[i].Position.Y+offset_Y, heartWidth, heartWidth), new RectangleF(0f, 0f, 1f, 1f));
                    current_health -= 2;
                }
                else if (current_health == 1)
                {
                    // Draw half heart
                    texturer.Draw(TextureID_Hearts[1], new RectangleF(entitylist[i].Position.X+offset_X, entitylist[i].Position.Y+offset_Y, heartWidth, heartWidth), new RectangleF(0f, 0f, 1f, 1f));
                    current_health -= 1;
                }
                else
                {
                    // Draw empty heart
                    texturer.Draw(TextureID_Hearts[2], new RectangleF(entitylist[i].Position.X+offset_X, entitylist[i].Position.Y+offset_Y, heartWidth, heartWidth), new RectangleF(0f, 0f, 1f, 1f));
                }
                offset_X += heartWidth; // Nächstes Herz rechts vom vorherigen zeichnen
            }
        }
    }
}
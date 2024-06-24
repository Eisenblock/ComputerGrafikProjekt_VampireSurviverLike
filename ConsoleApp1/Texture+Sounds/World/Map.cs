using OpenTK.Mathematics;
using OpenTK.Graphics.OpenGL;
using System.Drawing;


internal class Map
{
    //instances of other classes
    Texturer texturer = new Texturer(); 

    //variables for the map
    public string Texture_Map;
    public int TextureID_Map;
    public string Texture_Wall;
    public int TextureID_Wall;
    public List<int> TextureID_Hearts;

    public Map()
    {
        // Load the textures
        Texture_Map = "assets/sMap.png";
        TextureID_Map = texturer.LoadTexture(Texture_Map,1,1)[0]; // Call the LoadTexture method on the instance

        Texture_Wall = "assets/sWall.png";
        TextureID_Wall = texturer.LoadTexture(Texture_Wall,1,1)[0]; // Call the LoadTexture method on the instance
    }

    public void Draw()
    {
        GL.Color4(Color4.White);
        var rect_map = new RectangleF(-1f,-1f, 2f, 2f);
        var rect_wall = new RectangleF(rect_map.Left - 0.1f, rect_map.Top - 0.1f, rect_map.Width + 0.2f, rect_map.Height + 0.2f);
        var tex_rect = new RectangleF(0f, 0f, 1f, 1f);


        texturer.Draw(TextureID_Map, rect_map, tex_rect); //Draw the map
        texturer.Draw(TextureID_Wall, rect_wall, tex_rect); //Draw the wall
    }
}
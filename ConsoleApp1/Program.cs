using OpenTK.Windowing.Desktop;
using OpenTK.Windowing.Common;
using OpenTK.Graphics.OpenGL;
using OpenTK.Windowing.GraphicsLibraryFramework;
using System.Drawing;
using System.Reflection;
using OpenTK.Mathematics;

var window = new GameWindow(GameWindowSettings.Default, new NativeWindowSettings { Profile = ContextProfile.Compatability });

Player player = new ();
Enemy enemy = new (new Vector2(0.6f,0.6f));
EnemyList enemyList = new EnemyList();

float aspectRatio = 1f;
double timer = 3;
double interval = 3;


Vector2 playerpos = new Vector2(0,0);


window.UpdateFrame += Update;
window.RenderFrame += Render;
window.Resize += Resize;
window.KeyDown += args =>
{
    switch (args.Key)
    {
        case Keys.Escape: window.Close(); break;
        case Keys.Left: player.Left(); break;
        case Keys.Right: player.Right(); break;
        case Keys.Up: player.Up(); break;
        case Keys.Down: player.Down(); break;
    }
};


window.Run();



void Render(FrameEventArgs e)
{
    GL.ClearColor(0.2f, 0.2f, 0.2f, 1.0f); // Setzt die Hintergrundfarbe auf dunkelgrau
    GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);
    
    player.Draw();
    enemyList.DrawArray();
  
    window.SwapBuffers();
}


void Update(FrameEventArgs e)
{
    playerpos = player.Position;

    // Durchläuft das Array der Feinde und lässt jeden Feind den Spieler verfolgen
    foreach (var enemy in enemyList.enemies)
    {
        if (enemy != null) // Überprüft, ob der Feind existiert
        {
            enemy.MoveTowards(player.Position, 0.0001f);
        }
    }

    timer += e.Time;  
    enemyList.UpdateTimer(timer);
}

void Resize(ResizeEventArgs e)
{
    GL.Viewport(0, 0, e.Width, e.Height);
    GlobalSettings.AspectRatio = e.Width / (float)e.Height;
}
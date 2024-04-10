using OpenTK.Windowing.Desktop;
using OpenTK.Windowing.Common;
using OpenTK.Graphics.OpenGL;
using OpenTK.Windowing.GraphicsLibraryFramework;
using System.Drawing;
using System.Reflection;
using OpenTK.Mathematics;

var window = new GameWindow(GameWindowSettings.Default, new NativeWindowSettings { Profile = ContextProfile.Compatability });

Player player = new ();
Enemy enemy = new ();

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
    GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);

    player.Draw();
    enemy.Draw();

    timer += e.Time;
    //Console.WriteLine(timer);
    if (timer >= interval)
    {
        Console.WriteLine(timer + "sdsad");
        enemy.Draw(); // Zeichne den Feind
        timer = 0; // Timer zurücksetzen
    }


    window.SwapBuffers();
}


void Update(FrameEventArgs e)
{
    
    playerpos = player.Position;
    enemy.MoveTowards(player.Position,0.0001f);
}

void Resize(ResizeEventArgs e)
{
    GL.Viewport(0, 0, e.Width, e.Height);
}


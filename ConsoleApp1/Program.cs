using OpenTK.Windowing.Desktop;
using OpenTK.Windowing.Common;
using OpenTK.Graphics.OpenGL;
using OpenTK.Windowing.GraphicsLibraryFramework;
using System.Drawing;
using OpenTK.Mathematics;

var window = new GameWindow(
    new GameWindowSettings(),
    new NativeWindowSettings()
    {
        Profile = ContextProfile.Compatability,
        ClientSize = new Vector2i(800, 600) // Setzen Sie die Fenstergröße auf 800x600 Pixel
    }
);

Player player = new Player();
Enemy enemy = new (new Vector2(0.6f,0.6f),false);
Shoot shoot = new Shoot(player);
EnemyList enemyList = new EnemyList(player, shoot,enemy);
Circle circle = new Circle(Vector2.Zero,0);
CollisionDetection collisionDetection = new CollisionDetection();

Game gamestate  = new Game(window, player);
UpgradeScreen upgradeScreen = new UpgradeScreen(window, gamestate, player);
TextRenderer textRenderer = new TextRenderer();

float aspectRatio = 1f;
double timer = 3;
double timerShoot = 0;
double interval = 3;
Vector2 mousePosition = Vector2.Zero;
Vector2 playerpos = new Vector2(0,0);
Vector2 pos = new Vector2(1,1); 

window.UpdateFrame += Update;
window.RenderFrame += Render;
window.Resize += Resize;

window.MouseMove +=  args =>
{
    mousePosition = new Vector2((float)args.X / window.ClientSize.X * 2 - 1, 1 - (float)args.Y / window.ClientSize.Y * 2);
};

window.KeyDown += args =>
{
    switch (args.Key)
    {
        case Keys.Escape: window.Close(); break;
        case Keys.Left: player.Left(); break;
        case Keys.Right: player.Right(); break;
        case Keys.Up: player.Up(); break;
        case Keys.Down: player.Down(); break;
        case Keys.Space: shoot.PlayerShoots(mousePosition, timer); break;
        case Keys.L: gamestate.ShowUpgradeScreen(); break;
    }
};

window.MouseDown += args =>
{
    if (args.Button == MouseButton.Left)
    {
        upgradeScreen.OnMouseClick(mousePosition.X, mousePosition.Y);
    }
};


window.Run();



void Render(FrameEventArgs e)
{
    if(gamestate.state == GameState.UpgradeScreen)
    {
        upgradeScreen.show();
    }
    else{
        GL.ClearColor(0.2f, 0.2f, 0.2f, 1.0f); // Setzt die Hintergrundfarbe auf dunkelgrau
        GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);
        
        player.Draw();
        enemyList.DrawArray();
        shoot.Draw();
    
    
        window.SwapBuffers();
    }

}


void Update(FrameEventArgs e)
{
    if (gamestate.state == GameState.UpgradeScreen)
    {
        // Führen Sie die Upgrade-Logik aus
        upgradeScreen.update(mousePosition);
    }
    else
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
        timerShoot += e.Time;
        enemyList.UpdateTimer(timer);
        shoot.UpdateTimerShoot(timerShoot);
        enemyList.CheckCollisionPlayer();
        enemyList.CheckCollisionShoot();
    }   
    
}

void Resize(ResizeEventArgs e)
{
    GL.Viewport(0, 0, e.Width, e.Height);
    GlobalSettings.AspectRatio = e.Width / (float)e.Height;
}
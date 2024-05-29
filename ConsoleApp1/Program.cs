using OpenTK.Windowing.Desktop;
using OpenTK.Windowing.Common;
using OpenTK.Graphics.OpenGL;
using OpenTK.Windowing.GraphicsLibraryFramework;
using OpenTK.Mathematics;
using System.Drawing;
using System.Windows.Forms;

public static class Program
{
    public static Vector2i WindowSize {get; private set;}

    public static void Main(string[] args)
    {
        var window = new GameWindow(
            new GameWindowSettings(),
            new NativeWindowSettings()
            {
                Profile = ContextProfile.Compatability,
                ClientSize = new Vector2i(1200, 1000) // Setzen Sie die Fenstergröße auf 800x600 Pixel
            }
        );
        WindowSize = window.ClientSize;
        window.CursorState = CursorState.Hidden;
        window.CursorState = CursorState.Grabbed;      

        Player player = new Player();
        Map map = new Map();
        GUI gui = new GUI(player);
        Gun gun = new Gun();
        Enemy enemy = new (new Vector2(0.6f,0.6f),false,1);
        EnemyList enemyList = new EnemyList(player,enemy, gui);
        CollisionDetection collisionDetection = new CollisionDetection();
        SoundsPlayer soundsPlayer = new SoundsPlayer();
        Shootlist shootlist = new Shootlist(player);
        Texturer texturer = new Texturer();
        Mouse mouse = new Mouse();

        float aspectRatio = 1f;
        double timer = 3;
        double timerShoot = 0;
        double interval = 3;
        Vector2 mousePosition = Vector2.Zero;
        Vector2 playerpos = new Vector2(0,0);
        Vector2 pos = new Vector2(1,1);

        bool moveLeft = false;
        bool moveRight = false;
        bool moveUp = false;
        bool moveDown = false;

        Action Restart = () =>
        {
            timerShoot = 0;
            enemyList.ClearAll();
            shootlist.ClearAll();
            player.ClearAll();
        };
        Game gamestate  = new Game(window, player, Restart, soundsPlayer);
        GameOver gameover = new GameOver(window, gamestate, player, Restart, soundsPlayer);
        Running running = new Running(soundsPlayer);


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
                case Keys.A: moveLeft = true; break;
                case Keys.D: moveRight = true; break;
                case Keys.W: moveUp = true; break;
                case Keys.S: moveDown = true; break;
                // case Keys.L: gamestate.state = GameState.UpgradeScreen; break;
                case Keys.R: gameover.Restart(); break;
            }
        };

        window.KeyUp += args =>
        {
            switch (args.Key)
            {
                case Keys.A: moveLeft = false; break;
                case Keys.D: moveRight = false; break;
                case Keys.W: moveUp = false; break;
                case Keys.S: moveDown = false; break;
            }
        };

        window.MouseDown += args =>
        {
            if (args.Button == MouseButton.Left)
            {
                shootlist.InitializeShoot(mousePosition,player,timer);
            }
        };



        window.Run();

        void Render(FrameEventArgs e)
        {
            if(gamestate.state == GameState.GameOver)
            {
                gameover.Draw(window);
            }
            else{
                GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);
                map.Draw();
                gui.Draw();
                gun.Draw();
                foreach (var enemies in enemyList.enemies)
                {
                    if (enemies is RangedEnemy rangedEnemy)
                    {
                        rangedEnemy.DrawGun();
                    }
                }
                enemyList.DrawArray(timer);
                player.Draw(timer, mousePosition);
                shootlist.DrawShoots();
                mouse.Draw();

                window.SwapBuffers();
            }

        }


        void Update(FrameEventArgs e)
        {
            if (player.health <= 0) 
            { 
                gamestate.state = GameState.GameOver;
            }
            else{
                if (moveLeft) player.Left();
                if (moveRight) player.Right();
                if (moveUp) player.Up();
                if (moveDown) player.Down();
                if (moveLeft == false && moveRight  == false && moveUp == false && moveDown == false) player.Stop();
                if (gamestate.state == GameState.UpgradeScreen)
                {
                    // Führen Sie die Upgrade-Logik aus
                    gameover.Restart();
                }
                else
                {
                    playerpos = player.Position;
                    // Durchläuft das Array der Feinde und lässt jeden Feind den Spieler verfolgen
                    foreach (var enemy in enemyList.enemies)
                    {
                        if (enemy != null && enemy.enemyDead == false) // Überprüft, ob der Feind existiert
                        {
                            //check welcher Enemy der derzeitige ist
                            if(enemy is BigEnemy bigEnemy)
                            {
                                bigEnemy.Update(playerpos);
                            }
                            if(enemy is FastEnemy fastEnemy)
                            {
                                fastEnemy.Update(playerpos);
                            }
                            if(enemy is RangedEnemy rangedEnemy)
                            {
                                rangedEnemy.Update(playerpos);
                                if (enemy.range.Length < 1)
                                {
                                    shootlist.InitializeShoot(playerpos, rangedEnemy, timer);
                                }
                            }
                            else
                            {
                                enemy.MoveTowards(playerpos, 0.0001f); // Bewegt den Feind in Richtung des Spielers
                            }
                        }
                    }
                    mouse.Update(mousePosition);
                    gun.Update(player, mousePosition);
                    timer += e.Time;
                    timerShoot += e.Time;
                    enemyList.UpdateTimer(timer);
                    shootlist.ShootDirectionList(timer);
                    collisionDetection.CheckCollision(player,enemyList.enemies,shootlist.shootList);
                }
            }
            

        }

        void Resize(ResizeEventArgs e)
        {
            GL.Viewport(0, 0, e.Width, e.Height);
            GlobalSettings.AspectRatio = e.Width / (float)e.Height;
        }
            }
}

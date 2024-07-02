using OpenTK.Windowing.Desktop;
using OpenTK.Windowing.Common;
using OpenTK.Graphics.OpenGL;
using OpenTK.Windowing.GraphicsLibraryFramework;
using OpenTK.Mathematics;
using System.Security.Cryptography;

public  class Program
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

        void Resize(ResizeEventArgs e)
        {
            GL.Viewport(0, 0, e.Width, e.Height);
            GlobalSettings.AspectRatio = e.Width / (float)e.Height;
            Matrix4 modelView = Matrix4.CreateOrthographic(e.Width, e.Height, -1.0f, 1.0f);
            /*GL.MatrixMode(MatrixMode.Projection);
            GL.LoadMatrix(ref projection);
            GL.MatrixMode(MatrixMode.Modelview);*/
           
        }

        // Set mouse to normal state
        void setMouseInMenu()
        {
            window.CursorState = CursorState.Normal;
            window.CursorState = CursorState.Normal;
        }
        // Set mouse to hidden state
        void setMouseInGame()
        {
            window.CursorState = CursorState.Grabbed;
            window.CursorState = CursorState.Hidden;
        } 

        //instances of other classes
        Score score = new Score();
        score.LoadHighscore();

        Player player = new Player();
        Map map = new Map();
        GUI gui = new GUI(player);
        Gun gun = new Gun();
        Enemy enemy = new (new Vector2(0.6f,0.6f),false,1,Vector2.Zero, new List<int>());
        EnemyList enemyList = new EnemyList(player, gui, score);
        CollisionDetection collisionDetection = new CollisionDetection();
        SoundsPlayer soundsPlayer = new SoundsPlayer();
        Shootlist shootlist = new Shootlist(player);
        Mouse mouse = new Mouse();
        
        //variables
        double timer = 3;
        Vector2 mousePosition = Vector2.Zero;
        bool moveLeft = false;
        bool moveRight = false;
        bool moveUp = false;
        bool moveDown = false;
        float shakeDuration = 0.0f;
        float ShakeMagnitude = 0.01f;
        Random random = new Random();
        Matrix4 modelView = new Matrix4();

        //functions
        Action Restart = () =>
        {
            enemyList.ClearAll();
            shootlist.ClearAll();
            player.ClearAll();
            score.ResetScore();
        };

        //Load gamestates
        Game gamestate  = new Game(window, player, Restart, soundsPlayer, score);
        window.UpdateFrame += Update;
        window.RenderFrame += Render;
        window.Resize += Resize;

        //Eventhandler button pressed
        window.KeyDown += args =>
        {
            switch (args.Key)
            {
                case Keys.Escape: gamestate.Pause(); break;
                case Keys.A: moveLeft = true; break;
                case Keys.D: moveRight = true; break;
                case Keys.W: moveUp = true; break;
                case Keys.S: moveDown = true; break;
                //case Keys.X: shakeDuration = 0.5f; break;
                // case Keys.L: gamestate.state = GameState.UpgradeScreen; break;
                case Keys.R: gamestate.gameOver.Restart(); break;
            }
        };

        //Eventhandler button released
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

        //Eventhandler mouse
        window.MouseMove +=  args =>
        {
            mousePosition = new Vector2((float)args.X / window.ClientSize.X * 2 - 1, 1 - (float)args.Y / window.ClientSize.Y * 2);
        };

        //Eventhandler mouse click
        window.MouseDown += args =>
        {
            if (args.Button == MouseButton.Left)
            {
                if(gamestate.state == GameState.Running){
                    shootlist.InitializeShoot(mousePosition,player,timer);
                }
                else if(gamestate.state == GameState.MainMenu)
                {
                    gamestate.mainMenu.OnMouseClick(mousePosition);
                }
                else if(gamestate.state == GameState.Paused)
                {
                    gamestate.pauseMenu.OnMouseClick(mousePosition);
                }  
                else if(gamestate.state == GameState.GameOver)
                {
                    gamestate.gameOver.OnMouseClick(mousePosition);
                } 
                else if(gamestate.state == GameState.Controls)
                {
                    gamestate.controls.OnMouseClick(mousePosition);
                } 
            }
        };

        //Run the game
        window.Run();

        //Render the game
        void Render(FrameEventArgs e)
        {
            //Draw the current gamestate
            if(gamestate.state == GameState.Paused)
            {
                gamestate.pauseMenu.Draw(window);
            }
            else if(gamestate.state == GameState.GameOver)
            {
                gamestate.gameOver.Draw(window);
            }
            else if(gamestate.state == GameState.MainMenu){
                gamestate.mainMenu.Draw(window);
            }
            else if(gamestate.state == GameState.Controls){
                gamestate.controls.Draw(window);
            }
            else{
                GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);
                Matrix4 modelView = Matrix4.Identity;
                shakeDuration = collisionDetection.UpdateShakeDuration();

                if (shakeDuration > 0)
                {
                    float offsetX = (float)(random.NextDouble() * 2 - 1) * ShakeMagnitude;
                    float offsetY = (float)(random.NextDouble() * 2 - 1) * ShakeMagnitude;
                    modelView = Matrix4.CreateTranslation(offsetX, offsetY, 0);
                }
           
                GL.MatrixMode(MatrixMode.Modelview);
                GL.LoadMatrix(ref modelView);
                // Draw everything else if the game is running
                map.Draw();
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
                gamestate.running.Draw();
                gui.Draw();
                mouse.Draw();
                window.SwapBuffers();
            }
        }


        void Update(FrameEventArgs e)
        {

            // Update the current gamestate
            if (player.health <= 0)
            {
                gamestate.GameOver();
            }
            if(gamestate.state != GameState.Running)
            {
                if(CursorState.Normal != window.CursorState)
                {
                    setMouseInMenu();
                }
                if (gamestate.state == GameState.GameOver) 
                {
                    gamestate.gameOver.Update(mousePosition);
                }
                else if(gamestate.state == GameState.MainMenu)
                {
                    gamestate.mainMenu.Update(mousePosition);
                }
                else if(gamestate.state == GameState.Paused)
                {
                    gamestate.pauseMenu.Update(mousePosition);
                }
                else if(gamestate.state == GameState.Controls)
                {
                    gamestate.controls.Update(mousePosition);
                }
            }
            else
            {
                // Update the game if the game is running
                if(CursorState.Normal == window.CursorState)
                {
                    setMouseInGame();
                }

                // Move the player
                if (moveLeft) player.Left();
                if (moveRight) player.Right();
                if (moveUp) player.Up();
                if (moveDown) player.Down();
                if (moveLeft == false && moveRight  == false && moveUp == false && moveDown == false) player.Stop();

                // Moves the enemies
                foreach (var enemy in enemyList.enemies)
                {
                    if (enemy != null && enemy.enemyDead == false) // Check if the enemy is alive and e
                    {
                        //check which type of enemy it is
                        if(enemy is BigEnemy bigEnemy)
                        {
                            bigEnemy.Update(player.Position);
                        }
                        if(enemy is FastEnemy fastEnemy)
                        {
                            fastEnemy.Update(player.Position);
                        }
                        if(enemy is RangedEnemy rangedEnemy)
                        {
                            rangedEnemy.Update(player.Position);
                            
                            if (enemy.range.Length < 0.9f)
                            {                                
                                shootlist.InitializeShoot(player.Position, rangedEnemy, timer);
                            }
                        }
                        else
                        {
                            enemy.MoveTowards(player.Position, 0.0001f); // move the enemy towards the player
                        }
                    }
                }
                // update everything else
                mouse.Update(mousePosition);
                gun.Update(player, mousePosition);
                timer += e.Time;
                enemyList.UpdateTimer(timer);
                shootlist.ShootDirectionList(timer);
                collisionDetection.CheckCollision(player,enemyList.enemies,shootlist.shootList);
                shakeDuration = collisionDetection.UpdateShakeDuration();
               
            }
        }

    }


}

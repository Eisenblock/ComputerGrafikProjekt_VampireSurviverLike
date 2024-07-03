using OpenTK.Graphics.OpenGL;
using OpenTK.Mathematics;
using System;
using System.Collections.Generic;


namespace ConsoleApp1
{
    internal class View
    {
        //variablen
        Camera camera_view = new Camera();

        public View(Camera camera)
        {
            camera.Scale = 9f;
            camera.Center = new Vector2(10f, 7f);
            camera_view = camera;

        }



        internal void SetMatrix_View()
        {
            GL.Clear(ClearBufferMask.ColorBufferBit); // clear the screen

            camera_view.SetMatrix();

        }

        internal void Resize(int width, int height)
        {
            camera_view.Resize(width, height);
        }


        /// <summary>
        /// Calculates points on a circle.
        /// </summary>
        /// <returns></returns>
        private static List<Vector2> CreateCirclePoints(int corners)
        {
            float delta = 2f * MathF.PI / corners;
            var points = new List<Vector2>();
            for (int i = 0; i < corners; ++i)
            {
                var alpha = i * delta;
                // step around the unit circle
                var x = MathF.Cos(alpha);
                var y = MathF.Sin(alpha);
                points.Add(new Vector2(x, y));
            }
            return points;
        }
    }
}


using OpenTK.Graphics.OpenGL;
using OpenTK.Mathematics;
using System;
using static Zenseless.OpenTK.Transformation2d;

namespace ConsoleApp1
{
    public class Camera
    {
        public Matrix4 CameraMatrix => _cameraMatrix;

        public Vector2 Center
        {
            get => _center;
            set
            {
                _center = value;
                UpdateMatrix();
            }
        }

        public Matrix4 GetWindowToWorld() => Combine(InvViewportMatrix, CameraMatrix.Inverted());

        public Matrix4 InvViewportMatrix { get; private set; } = Matrix4.Identity;

        public void Resize(int width, int height)
        {
            _invAspectRatio = height / (float)width;
            Matrix4 matrix = Translate(-1f, 1f);
            Matrix4 matrix2 = Scale(2f / (width - 1f), -2f / (height - 1f));
            InvViewportMatrix = matrix2 * matrix;

            UpdateMatrix();
        }

        public float Rotation
        {
            get => _rotation;
            set
            {
                _rotation = value;
                UpdateMatrix();
            }
        }

        public float Scale
        {
            get => _scale;
            set
            {
                _scale = MathF.Max(0.001f, value); // avoid division by 0 and negative
                UpdateMatrix();
            }
        }

        public void SetMatrix() => GL.LoadMatrix(ref _cameraMatrix);

        private Matrix4 _cameraMatrix = Matrix4.Identity;
        private float _scale = 1f;

        private Vector2 _center;
        private float _rotation;
        private float _invAspectRatio;

        private void UpdateMatrix()
        {
            //TODO: 1. Implement panning
            //TODO: 2. Implement rotation around '_center'. '_rotation' is in degrees!
            //TODO: 3. Implement camera scaling
            //TODO: 4. Implement window aspect ratio scaling. Hint: use _invAspectRatio


            var translate = Translate(-Center);
            var rotate = Rotation(MathHelper.DegreesToRadians(Rotation));
            var scale = Scale(1f / Scale);
            var aspect = Scale(_invAspectRatio, 1f);
            _cameraMatrix = Combine(translate, rotate, scale, aspect);

        }
    }
}

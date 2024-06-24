using OpenTK.Mathematics;
using OpenTK.Graphics.OpenGL;
using System.Drawing;

class Particles
{
    //instances of other classes
    Texturer Texturer = new Texturer();

    //variables
    public Vector2 position;
    public float size = 1f;
    
    //variables for the animation
    public List<int> ParticlesList { get; private set; }
    private int currentFrame = 0;
    public DateTime lastFrameTime = DateTime.MinValue;
    public double frameDuration = 0.025f;
    private float? initialAngle = null;

    public Particles(List<int> particlesList)
    {
        ParticlesList = particlesList;
    }

    public List<int> InitParticles()
    {
        // Load the textures
        Texturer texturer = new Texturer();
        List<int> ParticlesList = texturer.LoadTexture("assets/BloodParticles.png", 4, 4);
        return ParticlesList;
    }

    public bool Draw(Vector2 pos, float scale, Vector2 targetPosition)
    {
        if (!initialAngle.HasValue)
        {
            // Calculate the angle only once at the beginning of the animation
            initialAngle = (float)Math.Atan2(targetPosition.Y - pos.Y, targetPosition.X - pos.X);
            initialAngle = MathHelper.RadiansToDegrees(initialAngle.Value); // Convert the angle to degrees
        }

        // Update the position and size of the particles
        position = pos;
        this.size = scale * 3f;

        // Calculate the rectangle for drawing the particles
        var rect = new RectangleF(-size / 2, -size / 2, size, size); // Center around (0,0) before transformation

        // Perform transformations
        GL.PushMatrix(); // Save the current state of the matrix
        GL.Translate(position.X, position.Y, 0); // Move to the starting position
        GL.Rotate(initialAngle.Value, 0f, 0f, 1f); // Rotate by the initial angle

        // Draw the particles
        GL.Color4(Color4.White);
        if (currentFrame < ParticlesList.Count)
        {
            Texturer.Draw(ParticlesList[currentFrame], rect, new RectangleF(0, 0, 1, 1));

            if ((DateTime.Now - lastFrameTime).TotalSeconds >= frameDuration)
            {
                lastFrameTime = DateTime.Now;
                currentFrame++;
            }
        }
        else
        {
            currentFrame = 0;
            initialAngle = null; // Reset initialAngle
            GL.PopMatrix(); // Restore the previous state of the matrix
            return false;
        }

        GL.PopMatrix(); // Restore the previous state of the matrix
        return true;
    }
}
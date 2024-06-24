using OpenTK.Mathematics;
using OpenTK.Graphics.OpenGL;
using System.Drawing;
using ImageMagick;

class Texturer{
    public Vector2i WindowSize => Program.WindowSize;

    public Texturer()
    {
        GL.Enable(EnableCap.Texture2D);
        GL.BlendFunc(BlendingFactor.SrcAlpha, BlendingFactor.OneMinusSrcAlpha);
        GL.Enable(EnableCap.Blend);
    }
    public List<int> LoadTexture(string path, int horizontalFrameCount, int verticalFrameCount)
    {
        if (!File.Exists(path))
        {
            Console.WriteLine("Texture file not found: " + path);
            return null; // Return an invalid texture ID
        }
        using var image = new MagickImage(path);
        var format = PixelInternalFormat.Rgb;
        switch (image.ChannelCount)
        {
            case 3: break;
            case 4: format = PixelInternalFormat.Rgba; break;
            default: throw new ArgumentOutOfRangeException("Unsupported image format");
        }
        image.Flip();

        // Calculate the width and height of each frame
        int frameWidth = image.Width / horizontalFrameCount;
        int frameHeight = image.Height / verticalFrameCount;

        // Create a list to store the handles for each frame
        List<int> handles = new List<int>();

        // Loop through each row and then each column
        for (int j = verticalFrameCount -1; j >= 0; j--)
        {
            for (int i = 0; i < horizontalFrameCount; i++)
            {
                int xPosition = i * frameWidth;
                int yPosition = j * frameHeight;

                // Create a section of the image based on the current frame
                using var croppedImage = image.Clone(new MagickGeometry(xPosition, yPosition, frameWidth, frameHeight));

                // Convert the cropped section into a byte array
                var bytes = croppedImage.GetPixelsUnsafe().ToArray();

                // Generate a texture for this frame
                var handle = GL.GenTexture();
                GL.BindTexture(TextureTarget.Texture2D, handle);
                GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapS, (int)TextureWrapMode.ClampToEdge);
                GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapT, (int)TextureWrapMode.ClampToEdge);
                GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMagFilter, (int)TextureMinFilter.Linear);
                GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter, (int)TextureMinFilter.Linear);

                GL.TexImage2D(TextureTarget.Texture2D, 0, (OpenTK.Graphics.OpenGL.PixelInternalFormat)format, frameWidth, frameHeight, 0, (OpenTK.Graphics.OpenGL.PixelFormat)format, OpenTK.Graphics.OpenGL.PixelType.UnsignedByte, bytes);
                GL.GenerateMipmap(GenerateMipmapTarget.Texture2D);
                GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.GenerateMipmap, 1);
                GL.BindTexture(TextureTarget.Texture2D, 0);

                // Add the handle to the list
                handles.Add(handle);
            }
        }

        return handles;
    }

    public void Draw(int texture, RectangleF rect, RectangleF tex_rect)
    {
        GL.BindTexture(TextureTarget.Texture2D, texture);
        GL.Begin(PrimitiveType.Quads);

        GL.TexCoord2(tex_rect.Left, tex_rect.Top);
        GL.Vertex2(rect.Left, rect.Top);

        GL.TexCoord2(tex_rect.Right, tex_rect.Top);
        GL.Vertex2(rect.Right, rect.Top);

        GL.TexCoord2(tex_rect.Right, tex_rect.Bottom);
        GL.Vertex2(rect.Right, rect.Bottom);

        GL.TexCoord2(tex_rect.Left, tex_rect.Bottom);
        GL.Vertex2(rect.Left, rect.Bottom);

        GL.End();
        GL.BindTexture(TextureTarget.Texture2D, 0);
    }
}
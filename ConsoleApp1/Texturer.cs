using OpenTK.Mathematics;
using OpenTK.Graphics.OpenGL;
using System.Drawing.Drawing2D;
using System.Drawing.Printing;
using System.Drawing;
using System.Drawing.Imaging;
using ImageMagick;
using System.IO;
using Image = System.Drawing.Image;
using OpenTK.Windowing.GraphicsLibraryFramework;

class Texturer{

    public Vector2i WindowSize => Program.WindowSize;

    public Texturer()
    {
        GL.Enable(EnableCap.Texture2D);
        GL.BlendFunc(BlendingFactor.SrcAlpha, BlendingFactor.OneMinusSrcAlpha);
        GL.Enable(EnableCap.Blend);
    }
    public int LoadTexture(string path)
    {
        if (!File.Exists(path))
        {
            Console.WriteLine("Texture file not found: " + path);
            return -1; // Rückgabe einer ungültigen Textur-ID
        }
        using var image = new MagickImage(path);
        var format = PixelInternalFormat.Rgb;
        Console.WriteLine(image.ChannelCount);
        switch (image.ChannelCount)
        {
            case 3: break;
            case 4: format = PixelInternalFormat.Rgba; break;
            default: throw new ArgumentOutOfRangeException("Unsupported image format");
        }
        image.Flip();
        var bytes = image.GetPixelsUnsafe().ToArray();
        var handle = GL.GenTexture();
        GL.Color3(1.0f, 1.0f, 1.0f);
        GL.BindTexture(TextureTarget.Texture2D, handle);
        GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapS, (int)TextureWrapMode.ClampToEdge);
        GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapT, (int)TextureWrapMode.ClampToEdge);
        GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMagFilter, (int)TextureMinFilter.Linear);
        GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter, (int)TextureMinFilter.Linear);

        GL.TexImage2D(TextureTarget.Texture2D, 0, (OpenTK.Graphics.OpenGL.PixelInternalFormat)format, image.Width, image.Height, 0, (OpenTK.Graphics.OpenGL.PixelFormat)format, OpenTK.Graphics.OpenGL.PixelType.UnsignedByte, bytes);
        GL.GenerateMipmap(GenerateMipmapTarget.Texture2D);
        GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.GenerateMipmap, 1);
        GL.BindTexture(TextureTarget.Texture2D, 0);
        return handle;
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
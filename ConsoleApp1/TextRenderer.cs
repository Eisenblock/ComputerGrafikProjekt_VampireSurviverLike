using System;
using System.Drawing;
using System.Drawing.Imaging; 
using OpenTK.Graphics.OpenGL;

public class TextRenderer
{
    public Bitmap RenderText(string text, Font font, Color textColor, Color backgroundColor, float x, float y)
    {
        // Erstelle ein Bitmap-Objekt mit der Größe des Textes
        Bitmap bitmap = new Bitmap(text.Length * 20, 20);

        // Erstelle ein Graphics-Objekt, um auf das Bitmap zu zeichnen
        using (Graphics graphics = Graphics.FromImage(bitmap))
        {
            // Setze den Hintergrundfarbe des Bitmaps
            graphics.Clear(backgroundColor);

            // Erstelle einen Brush für die Textfarbe
            using (Brush brush = new SolidBrush(textColor))
            {
                // Zeichne den Text auf das Bitmap
                graphics.DrawString(text, font, brush, new PointF(0, 0));
            }
        }

        // Lade die Textur und zeichne das TexturedQuad
        int texture = LoadTexture(bitmap);
        DrawTexturedQuad(texture, x, y, bitmap.Width, bitmap.Height);

        // Lösche die Textur
        GL.DeleteTexture(texture);
    
        bitmap.Save("test.bmp");
        return bitmap;
    }

    private int LoadTexture(Bitmap bitmap)
    {
        int texId = GL.GenTexture();
        GL.BindTexture(TextureTarget.Texture2D, texId);

        BitmapData data = bitmap.LockBits(new Rectangle(0, 0, bitmap.Width, bitmap.Height), ImageLockMode.ReadOnly, System.Drawing.Imaging.PixelFormat.Format32bppArgb);

        GL.TexImage2D(TextureTarget.Texture2D, 0, PixelInternalFormat.Rgba, data.Width, data.Height, 0, OpenTK.Graphics.OpenGL.PixelFormat.Bgra, PixelType.UnsignedByte, data.Scan0);

        bitmap.UnlockBits(data);

        GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter, (int)TextureMinFilter.Linear);
        GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMagFilter, (int)TextureMagFilter.Linear);

        return texId;
    }

    private void DrawTexturedQuad(int texture, float x, float y, float width, float height)
    {
        GL.Enable(EnableCap.Texture2D);
        GL.BindTexture(TextureTarget.Texture2D, texture);

        GL.Begin(PrimitiveType.Quads);

        GL.TexCoord2(0, 0); GL.Vertex2(x, y);
        GL.TexCoord2(1, 0); GL.Vertex2(x + width, y);
        GL.TexCoord2(1, 1); GL.Vertex2(x + width, y + height);
        GL.TexCoord2(0, 1); GL.Vertex2(x, y + height);

        GL.End();

        GL.Disable(EnableCap.Texture2D);
    }
}
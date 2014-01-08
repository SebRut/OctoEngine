#if DEBUG
using System.Drawing;
using System.IO;
using Microsoft.Xna.Framework.Graphics;

namespace OctoEngine
{
    class TileAtlas
    {
        private int tileWidth;
        private int tileHeight;
        private int rows;
        private int columns;

        private Bitmap tileAtlasBitmap;

        public void Initialize(Texture2D texture)
        {
            MemoryStream stream = new MemoryStream();
            texture.SaveAsJpeg(stream, texture.Width, texture.Height);
            Color[] colors = new Color[] {};
            texture.GetData(colors);
            tileAtlasBitmap = new Bitmap(stream);
        }

        public Texture2D GetTile(int x, int y)
        {
            Bitmap tileBmp = tileAtlasBitmap.Clone(new Rectangle(tileWidth*x, tileHeight*y, tileWidth, tileHeight),
                tileAtlasBitmap.PixelFormat);
            Texture2D tile = new Texture2D();
            tile.SetData();
            return tile;
        }
    }
}
#endif
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace OctoEngine
{
    public class TextObject : GameObject
    {
        protected readonly SpriteFont Font;
        protected string Text;

        public override Rectangle Bounds
        {
            get
            {
                return new Rectangle((int)Position.X, (int)Position.Y, (int)Font.MeasureString(Text).X, (int)Font.MeasureString(Text).Y);
            }
        }

        public TextObject(Color color, SpriteFont font, Vector2 position = new Vector2(), string text = "") : base(position)
        {
            Color = color;
            Text = text;
            Font = font;
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.DrawString(Font, Text, Position, Color, Rotation, Origin, Scale, SpriteEffect, 1);
        }

        public void SetText(string newText)
        {
            Text = newText;
        }

        public void SetText(string newText, int screenWidth, int screenHeight, bool vertically = false)
        {
            Text = newText;
            CenterElement(screenWidth, screenHeight, vertically);
        }

        public void CenterElement(int screenWidth, int screenHeight, bool vertically = false)
        {
            Position = new Vector2(-(int)Font.MeasureString(Text).X / 2, vertically ? -(int)Font.MeasureString(Text).Y / 2 : Position.Y);
        }
    }
}

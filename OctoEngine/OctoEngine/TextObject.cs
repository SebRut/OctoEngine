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
                return new Rectangle((int)Position.X, (int)Position.Y, (int) (Font.MeasureString(Text).X * Scale), (int) (Font.MeasureString(Text).Y * Scale));
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

        public void CenterElement(Game game, bool vertically = false)
        {
            CenterElement(game.GameWidth, game.GameHeight, vertically);
        }

        public void CenterElement(int screenWidth, int screenHeight, bool vertically = false)
        {
            Position = new Vector2(-Bounds.Width / 2, vertically ? -Bounds.Y / 2 : Position.Y);
        }
    }
}

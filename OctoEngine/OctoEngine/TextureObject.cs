using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace OctoEngine
{
    public class TextureObject : GameObject
    {
        public Texture2D Texture;

        public override Rectangle Bounds
        {
            get
            {
                return new Rectangle((int) Position.X, (int) Position.Y, (int) (Texture.Width * Scale), (int) (Texture.Height * Scale));
            }
        }

        public TextureObject(Texture2D texture, Vector2 position = new Vector2())
            : base(position)
        {
            Texture = texture;   
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);
            spriteBatch.Draw(Texture, Position, new Rectangle(0, 0, Texture.Width, Texture.Height), Color, Rotation,
                Origin, Scale, SpriteEffect, 1);
        }

        public void CenterElement(bool vertically = true)
        {
            Position = new Vector2(-Texture.Width/2, vertically ? -Texture.Height/2 : Position.Y);
        }
    }
}
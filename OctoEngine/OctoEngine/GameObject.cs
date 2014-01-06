using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace OctoEngine
{
    public class GameObject :  IDisposable
    {
        public bool IsEnabled = true;

        public Vector2 Position { get; set; }

        public virtual Rectangle Bounds { get; set; }

        public delegate void MouseEvent(object element);

        public event MouseEvent ElementClicked = delegate { };
        public event MouseEvent ElementReleased = delegate { };
        public event MouseEvent ElementDown = delegate { };
        public event MouseEvent ElementHovered = delegate { };
        public event MouseEvent ElementUnhovered = delegate { };

        private bool hovered;
        private bool pressed;

        public float Rotation = 0;
        public float Scale = 1;
        public Color Color = Color.White;
        public Vector2 Origin = new Vector2();
        public SpriteEffects SpriteEffect;

        protected GameObject(Vector2 position)
        {
            Position = position;
        }

        public virtual void Dispose()
        {
            
        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            
        }

        public virtual void Update(GameTime gameTime, ResolutionIndependentRenderer independentRenderer)
        {
            if (Bounds.Contains(Vector2ToPoint(InputHelper.GetRelativeMousePosition(independentRenderer))))
            {
                if (!hovered)
                {
                    hovered = true;
                    ElementHovered(this);
                }
                if (pressed == false && Mouse.GetState().LeftButton == ButtonState.Pressed)
                {
                    pressed = true;
                    ElementClicked(this);
                    ElementDown(this);
                }
                if (pressed && Mouse.GetState().LeftButton == ButtonState.Released)
                {
                    pressed = false;
                    ElementReleased(this);
                }
            }
            else
            {
                if (hovered)
                {
                    hovered = false;
                    pressed = false;
                    ElementUnhovered(this);
                }
            }
        }

        private Point Vector2ToPoint(Vector2 vector)
        {
            return new Point((int)vector.X, (int)vector.Y);
        }
    }
}
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Tao.Sdl;

namespace OctoEngine
{
    public abstract class Animation
    {
        public float FramesPerSecond;
        public int CurrentFrameIndex;
        protected Texture2D[] Frames;

        public Texture2D CurrentFrame
        {
            get { return Frames[CurrentFrameIndex]; }
        }

        protected Animation(Texture2D[] frames, float fps = 1)
        {
            Frames = frames;
            FramesPerSecond = fps;
        }

        public abstract void Play(GameTime gameTime);
    }
}

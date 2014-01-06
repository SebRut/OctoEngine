using Microsoft.Xna.Framework;

namespace OctoEngine
{
    abstract class Animation
    {
        private int FramesPerSecond;

        public abstract void Play(GameTime gameTime);
    }
}

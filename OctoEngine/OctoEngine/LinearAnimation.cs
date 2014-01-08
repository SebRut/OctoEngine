using System;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace OctoEngine
{
    public class LinearAnimation : Animation
    {
        private TimeSpan lastPlay = new TimeSpan();

        public LinearAnimation(Texture2D[] frames, float fps = 1) : base(frames, fps)
        {
        }

        public override void Play(GameTime gameTime)
        {
            if (gameTime.TotalGameTime.TotalSeconds - lastPlay.Seconds >= FramesPerSecond)
            {
                lastPlay = gameTime.TotalGameTime;

                if (CurrentFrameIndex < Frames.Count() - 1) CurrentFrameIndex++;
                else CurrentFrameIndex = 0;
            }
        }
    }
}

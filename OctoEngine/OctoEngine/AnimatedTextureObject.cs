using Microsoft.Xna.Framework;

namespace OctoEngine
{
    class AnimatedTextureObject : TextureObject
    {
        private Animation animation;

        public AnimatedTextureObject(Animation animation, Vector2 position = new Vector2()) : base(animation.CurrentFrame, position)
        {
            this.animation = animation;
        }

        public override void Update(GameTime gameTime, ResolutionIndependentRenderer independentRenderer)
        {
            base.Update(gameTime, independentRenderer);

            animation.Play(gameTime);

            Texture = animation.CurrentFrame;
        }
    }
}

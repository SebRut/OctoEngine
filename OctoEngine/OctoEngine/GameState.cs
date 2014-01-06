using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace OctoEngine
{
    /// <summary>
    /// Saves informations about the curent gamestate
    /// </summary>
    public abstract class GameState : IDisposable
    {
        protected readonly List<GameObject> GameObjects = new List<GameObject>();
        protected ContentManager Content;

        public virtual void Dispose()
        {
            Content.Unload();
            foreach (GameObject gameObject in GameObjects)
            {
                gameObject.Dispose();
            }
        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            foreach (GameObject gameObject in GameObjects)
            {
                if(gameObject.IsEnabled)gameObject.Draw(spriteBatch);
            }
        }

        public virtual void Initialize( Game game)
        {
            Content = new ContentManager(game.Services, "Content");
            Load();
        }

        public abstract void Load();

        public virtual void Update(GameTime gameTime, ResolutionIndependentRenderer independentRenderer)
        {
            foreach (GameObject gameObject in GameObjects)
            {
                gameObject.Update(gameTime, independentRenderer);
            }
        }
    }
}
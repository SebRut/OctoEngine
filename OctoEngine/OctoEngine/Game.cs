using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace OctoEngine
{
    /// <summary>
    /// An extension of the Microsoft.Xna.framework.Game class
    /// </summary>
    public abstract class Game : Microsoft.Xna.Framework.Game
    {
        public readonly int GameWidth;
        public readonly int GameHeight;
        public readonly GraphicsDeviceManager GraphicsDeviceManager;
        public GameStateManager GameStateManager;
        public SpriteBatch SpriteBatch;
        public ResolutionIndependentRenderer ResolutionIndependentRenderer;
        public Camera2D Camera;

        /// <summary>
        /// Initializes the graphicsDevieManager and virtual screensizes
        /// </summary>
        /// <param name="gameWidth">virtual screen width</param>
        /// <param name="gameHeight">virtual screen height</param>
        public Game(int gameWidth, int gameHeight)
        {
            GraphicsDeviceManager = new GraphicsDeviceManager(this);

            GameWidth = gameWidth;
            GameHeight = gameHeight;
        }

        /// <summary>
        /// Disposes the gameStateMaanger and calls base.UnloadContent()
        /// </summary>
        protected override void UnloadContent()
        {
            GameStateManager.CurrentGameState.Dispose();
           
            base.UnloadContent();
        }

        /// <summary>
        /// Initializes display stuff, various fields and calls base.Initialize()
        /// </summary>
        protected override void Initialize()
        {
            GraphicsDeviceManager.IsFullScreen = true;
            GraphicsDeviceManager.ApplyChanges();

            SpriteBatch = new SpriteBatch(GraphicsDeviceManager.GraphicsDevice);

            ResolutionIndependentRenderer = new ResolutionIndependentRenderer(this, GraphicsDeviceManager.GraphicsDevice.DisplayMode.Width, GraphicsDeviceManager.GraphicsDevice.DisplayMode.Height, GameWidth, GameHeight);
            
            ResolutionIndependentRenderer.Initialize();

            Camera = new Camera2D(ResolutionIndependentRenderer) {Zoom = 1f};
            Camera.SetPosition(new Vector2());
            Camera.RecalculateTransformationMatrices();

            GameStateManager = new GameStateManager(this);

            base.Initialize();
        }

        /// <summary>
        /// Draws the GameStateMaanger and calls base.Draw(gameTime)
        /// </summary>
        /// <param name="gameTime"></param>
        protected override void Draw(GameTime gameTime)
        {
            GameStateManager.Draw(SpriteBatch);

            base.Draw(gameTime);
        }

        /// <summary>
        /// Updates the GameStateManager and calls base.Update(gameTime)
        /// </summary>
        /// <param name="gameTime"></param>
        protected override void Update(GameTime gameTime)
        {
            GameStateManager.Update(gameTime);
            
            base.Update(gameTime);
        }
    }
}

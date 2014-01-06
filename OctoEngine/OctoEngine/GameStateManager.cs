
using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace OctoEngine
{
    public class GameStateManager
    {
        public readonly List<Type> History;
        private readonly Game game;
        public GameState CurrentGameState;

        public GameStateManager(Game game )
        {
            this.game = game;
            History = new List<Type>();
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            CurrentGameState.Draw(spriteBatch);
        }

        public void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed ||
                Keyboard.GetState().IsKeyDown(Keys.Escape))
            {
                if (History.Count == 0) game.Exit();
                else History.RemoveAt(History.Count - 1);
                   
                if(History.Count >= 1)
                {
                    SwitchState((GameState) Activator.CreateInstance(History[History.Count-1]));
                    History.RemoveAt(History.Count - 2);
                }

            }
            CurrentGameState.Update(gameTime, game.ResolutionIndependentRenderer);
        }

        public void SwitchState(GameState gameState)
        {

            if (CurrentGameState != null)
            {
                CurrentGameState.Dispose();
            }
            CurrentGameState = gameState;
            gameState.Initialize(game);
            History.Add(CurrentGameState.GetType());
        }
    }
}
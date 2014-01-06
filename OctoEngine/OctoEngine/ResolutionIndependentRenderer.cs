using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace OctoEngine
{
    //Based on http://blog.roboblob.com/2013/07/27/solving-resolution-independent-rendering-and-2d-camera-using-monogame/
    public class ResolutionIndependentRenderer
    {
        private static Matrix scaleMatrix;
        public readonly int VirtualHeight;
        public readonly int VirtualWidth;
        private readonly Microsoft.Xna.Framework.Game game;
        public bool RenderingToScreenIsFinished;
        public Color BackgroundColor = Color.Black;
        public int ScreenWidth;
        public int ScreenHeight;
        private Viewport viewport;
        private float ratioX;
        private float ratioY;
        private Vector2 virtualMousePosition;

        private bool dirtyMatrix = true;

        public ResolutionIndependentRenderer(Microsoft.Xna.Framework.Game game, int screenWidth, int screenHeight, int virtualWidth = 1920, int virtualHeight = 1080)
        {
            this.game = game;
            VirtualWidth = virtualWidth;
            VirtualHeight = virtualHeight;

            ScreenWidth = screenWidth;
            ScreenHeight = screenHeight;
        }

        public void Initialize()
        {
            SetupVirtualScreenViewport();

            ratioX = (float)viewport.Width / VirtualWidth;
            ratioY = (float)viewport.Height / VirtualHeight;

            dirtyMatrix = true;
        }

        public void SetupVirtualScreenViewport()
        {
            var targetAspectRatio = VirtualWidth / (float) VirtualHeight;
            // figure out the largest area that fits in this resolution at the desired aspect ratio
            var width = ScreenWidth;
            var height = (int)(width / targetAspectRatio + .5f);

            if (height > ScreenHeight)
            {
                height = ScreenHeight;
                // PillarBox
                width = (int)(height * targetAspectRatio + .5f);
            }

            // set up the new viewport centered in the backbuffer
            viewport = new Viewport
            {
                X = (ScreenWidth / 2) - (width / 2),
                Y = (ScreenHeight / 2) - (height / 2),
                Width = width,
                Height = height
            };

            game.GraphicsDevice.Viewport = viewport;
        }

        public void BeginDraw()
        {
            // Start by reseting viewport to (0,0,1,1)
            SetupFullViewport();
            // Clear to Black
            game.GraphicsDevice.Clear(BackgroundColor);
            // Calculate Proper Viewport according to Aspect Ratio
            SetupVirtualScreenViewport();
            // and clear that
            // This way we are gonna have black bars if aspect ratio requires it and
            // the clear color on the rest
        }

        private void SetupFullViewport()
        {
            var vp = new Viewport();
            vp.X = vp.Y = 0;
            vp.Width = ScreenWidth;
            vp.Height = ScreenHeight;
            game.GraphicsDevice.Viewport = vp;
            dirtyMatrix = true;
        }

        public Matrix GetTransformationMatrix()
        {
            if (dirtyMatrix)
                RecreateScaleMatrix();

            return scaleMatrix;
        }

        private void RecreateScaleMatrix()
        {
            Matrix.CreateScale((float)ScreenWidth / VirtualWidth, (float)ScreenWidth / VirtualWidth, 1f, out scaleMatrix);
            dirtyMatrix = false;
        }

        public Vector2 ScaleMouseToScreenCoordinates(Vector2 screenPosition)
        {
            var realX = screenPosition.X - viewport.X;
            var realY = screenPosition.Y - viewport.Y;

            virtualMousePosition.X = realX / ratioX;
            virtualMousePosition.Y = realY / ratioY;

            return virtualMousePosition;
        }
    }
}
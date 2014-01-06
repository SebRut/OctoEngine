using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace OctoEngine
{
    public static class InputHelper
    {
        public static Vector2 GetRelativeMousePosition(ResolutionIndependentRenderer independentRenderer)
        {
            Vector2 result;
            Mouse.POINT lpPoint;
            Mouse.GetCursorPos(out lpPoint);
            result = independentRenderer.ScaleMouseToScreenCoordinates(new Vector2(lpPoint.X, lpPoint.Y));
            result.X = result.X - independentRenderer.VirtualWidth / 2;
            result.Y = result.Y - independentRenderer.VirtualHeight / 2;
            return result;
        }
    }
}
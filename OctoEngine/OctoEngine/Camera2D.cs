﻿using Microsoft.Xna.Framework;

namespace OctoEngine
{
    //Based on http://blog.roboblob.com/2013/07/27/solving-resolution-independent-rendering-and-2d-camera-using-monogame/
    public class Camera2D
    {
        protected ResolutionIndependentRenderer ResolutionIndependentRenderer;
        private float zoom;
        private float rotation;
        private Vector2 position;
        private Matrix transform = Matrix.Identity;
        private bool isViewTransformationDirty = true;
        private Matrix camTranslationMatrix = Matrix.Identity;
        private Matrix camRotationMatrix = Matrix.Identity;
        private Matrix camScaleMatrix = Matrix.Identity;
        private Matrix resTranslationMatrix = Matrix.Identity;


        private Vector3 camTranslationVector = Vector3.Zero;
        private Vector3 camScaleVector = Vector3.Zero;
        private Vector3 resTranslationVector = Vector3.Zero;

        public float Zoom
        {
            get { return zoom; }
            set
            {
                zoom = value;
                if (zoom < 0.1f)
                {
                    zoom = 0.1f;
                }
                isViewTransformationDirty = true;
            }
        }

        public float Rotation
        {
            get
            {
                return rotation;
            }
            set
            {
                rotation = value;
                isViewTransformationDirty = true;
            }
        }

        public Vector2 Position
        {
            get { return position; }
            set
            {
                position = value;
                isViewTransformationDirty = true;
            }
        }

        public Camera2D(ResolutionIndependentRenderer resolutionIndependence)
        {
            ResolutionIndependentRenderer = resolutionIndependence;

            zoom = 0.1f;
            rotation = 0.0f;
            position = Vector2.Zero;
        }

        public Matrix GetViewTransformationMatrix()
        {
            if (isViewTransformationDirty)
            {
                camTranslationVector.X = -position.X;
                camTranslationVector.Y = -position.Y;

                Matrix.CreateTranslation(ref camTranslationVector, out camTranslationMatrix);
                Matrix.CreateRotationZ(rotation, out camRotationMatrix);

                camScaleVector.X = zoom;
                camScaleVector.Y = zoom;
                camScaleVector.Z = 1;

                Matrix.CreateScale(ref camScaleVector, out camScaleMatrix);

                resTranslationVector.X = ResolutionIndependentRenderer.VirtualWidth*0.5f;
                resTranslationVector.Y = ResolutionIndependentRenderer.VirtualHeight * 0.5f;
                resTranslationVector.Z = 0;

                Matrix.CreateTranslation(ref resTranslationVector, out resTranslationMatrix);

                transform = camTranslationMatrix *
                             camRotationMatrix *
                             camScaleMatrix *
                             resTranslationMatrix *
                             ResolutionIndependentRenderer.GetTransformationMatrix();
                
                isViewTransformationDirty = false;
            }

            return transform;
        }

        public void RecalculateTransformationMatrices()
        {
            isViewTransformationDirty = true;
        }

        public void Move(Vector2 amount)
        {
            Position += amount;
        }

        public void SetPosition(Vector2 position)
        {
            Position = position;
        }
    }
}
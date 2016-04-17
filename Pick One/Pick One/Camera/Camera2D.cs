using Pick_One.BasicClasses;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Pick_One.Camera
{
    public class Camera2D
    {
        #region Protected Variables
        protected float _zoom;
        #endregion
        #region Public Variables
        public float Rotation { get; set; }
        public float Zoom
        {
            get
            {
                return _zoom;
            }
            set
            {
                _zoom = value;
                //stops negitive zoom, which will flip the image
                if(_zoom < 0.1f)
                {
                    _zoom = 0.1f;
                }
            }
        }
        public Vector3 FocusOffest { get; set; }

        public Matrix TransformationMatrix { get; set; }
        public Vector2 Location { get; set; }
        private GameState currentState;
        public ICameraFocus Focus { get; set; }
        #endregion
        public Camera2D(GameState state)
        {
            currentState = state;
            //IOC instead of constructor, but this will work for now
            _zoom = 1.0f;
            Rotation = 0.0f;
            Location = Vector2.Zero;
            FocusOffest = Vector3.Zero;
        }
        public void MoveCamera(Vector2 amount)
        {
            Location += amount;
        }
        public Matrix GetMatrixTransformation(GraphicsDevice graphicsDevice)
        {
            var ViewportWidth = graphicsDevice.Viewport.Width;
            var ViewportHeight = graphicsDevice.Viewport.Height;
            Vector3 cameraFocus = new Vector3(-Location.X, -Location.Y, 0);
            if(Focus != null)
            {
                cameraFocus = new Vector3(-Focus.Location.X, -Focus.Location.Y, cameraFocus.Z) + FocusOffest;
                
            }

            TransformationMatrix = Matrix.CreateTranslation(cameraFocus) *
                                                            Matrix.CreateRotationZ(Rotation) *
                                                            Matrix.CreateScale(new Vector3(Zoom, Zoom, 1)) *
                                                            Matrix.CreateTranslation(new Vector3(ViewportWidth * currentState.XOffset, ViewportHeight * currentState.YOffset, 0));
            return TransformationMatrix;
        }
    }
}
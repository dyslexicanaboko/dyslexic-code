using System;
using System.Collections.Generic;
using System.Text;
using FlatRedBall;
using FlatRedBall.Input;
using FlatRedBall.AI.Pathfinding;
using FlatRedBall.Graphics.Animation;
using FlatRedBall.Graphics.Particle;

using FlatRedBall.Math.Geometry;
using FlatRedBall.Math.Splines;
using BitmapFont = FlatRedBall.Graphics.BitmapFont;
using Cursor = FlatRedBall.Gui.Cursor;
using GuiManager = FlatRedBall.Gui.GuiManager;

#if FRB_XNA || SILVERLIGHT
using Keys = Microsoft.Xna.Framework.Input.Keys;
using Vector3 = Microsoft.Xna.Framework.Vector3;
using Texture2D = Microsoft.Xna.Framework.Graphics.Texture2D;


#endif

namespace DashRunning.Entities
{
	public partial class Dash
	{
        Xbox360GamePad _gamePad;

        public int PlayerIndex
        {
            set
            {
                _gamePad = InputManager.Xbox360GamePads[value];
            }
        }

        private void CustomInitialize()
        {
            //this.PlayerIndex = 0;
        }

        private void CustomActivity()
        {
            MovementActivity();
        }

        private void CustomDestroy()
        {


        }

        private static void CustomLoadStaticContent(string contentManagerName)
        {


        }

        private void MovementActivity()
        {
            //float movementSpeed = 10;

            //InputManager.Xbox360GamePads[0].ControlPositionedObject(this, MovementSpeed);
            //mGamePad.ControlPositionedObject(this, MovementSpeed);
            
            _gamePad.ControlPositionedObjectAcceleration(this, 10); //TODO: Setting this to a static value for now
        }
	}
}

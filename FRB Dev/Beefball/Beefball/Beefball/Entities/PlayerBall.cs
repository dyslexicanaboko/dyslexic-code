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

namespace Beefball.Entities
{
	public partial class PlayerBall
	{
        double mLastTimeDashed = -1000;

        Xbox360GamePad mGamePad;
        
        public int PlayerIndex
        {
            set
            {
                mGamePad = InputManager.Xbox360GamePads[value];
            }
        }

		private void CustomInitialize()
		{
            //this.PlayerIndex = 0;
		}

		private void CustomActivity()
		{
            MovementActivity();
            DashActivity();
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
            mGamePad.ControlPositionedObjectAcceleration(this, MovementSpeed);
        }

        private void DashActivity()
        {
            if (mGamePad.ButtonPushed(Xbox360GamePad.Button.A) && Screens.ScreenManager.CurrentScreen.PauseAdjustedSecondsSince(mLastTimeDashed) > DashFrequency)
            {
                mLastTimeDashed = Screens.ScreenManager.CurrentScreen.PauseAdjustedCurrentTime;
                
                float speed = mGamePad.LeftStick.Position.Length() * DashSpeed;
                double angle = mGamePad.LeftStick.Angle;
                
                XVelocity = (float)(System.Math.Cos(angle) * speed);
                YVelocity = (float)(System.Math.Sin(angle) * speed);

                CurrentState = VariableState.Tired;
                InterpolateToState(VariableState.Rested, DashFrequency);
            }
        }
	}
}

#region Usings

using System;
using System.Collections.Generic;
using System.Text;
using FlatRedBall;
using FlatRedBall.Input;
using FlatRedBall.Instructions;
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
#endregion

namespace LumberJack.Entities
{
	public partial class Axe
	{
		private void CustomInitialize()
		{


		}

		private void CustomActivity()
		{
            
		}

        public void ResetPosition(Jack jack)
        {
            ResetPositionX(jack);
            ResetPositionY(jack);
        }

        public void ResetPositionX(Jack jack)
        {
            X = jack.Position.X;

            XVelocity = 0;
        }

        public void ResetPositionY(Jack jack)
        {
            Y = jack.Position.Y;

            YVelocity = 0;
        }

        public void DebugPrintPosition()
        {
            FlatRedBall.Debugging.Debugger.Write("Weapon (X, Y) @ (Vx, Vy): (" + X + ", " + Y + ") @ (" + XVelocity + ", " + YVelocity + ")");
        }

        public void ShiftAccordingly()
        { 
            
        }

		private void CustomDestroy()
		{


		}

        private static void CustomLoadStaticContent(string contentManagerName)
        {


        }
	}
}

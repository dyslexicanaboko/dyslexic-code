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
	public partial class ScoreHud
	{
        public int Score1
        {
            set
            {
                this.Team1Score.DisplayText = value.ToString("00");
            }
        }

        public int Score2
        {
            set
            {
                this.Team2Score.DisplayText = value.ToString("00");
            }
        }

		private void CustomInitialize()
		{
            SetInitialScoreValues();
		}

		private void CustomActivity()
		{


		}

		private void CustomDestroy()
		{


		}

        private static void CustomLoadStaticContent(string contentManagerName)
        {


        }

        private void SetInitialScoreValues()
        {
            Score1 = 0;
            Score2 = 0;
        }
	}
}

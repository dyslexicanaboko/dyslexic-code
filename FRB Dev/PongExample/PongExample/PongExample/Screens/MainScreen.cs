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

using FlatRedBall.Graphics.Model;
using FlatRedBall.Math.Geometry;
using FlatRedBall.Math.Splines;

using Cursor = FlatRedBall.Gui.Cursor;
using GuiManager = FlatRedBall.Gui.GuiManager;
using FlatRedBall.Localization;

#if FRB_XNA || SILVERLIGHT
using Keys = Microsoft.Xna.Framework.Input.Keys;
using Vector3 = Microsoft.Xna.Framework.Vector3;
using Texture2D = Microsoft.Xna.Framework.Graphics.Texture2D;
using PongExample.Entities;
#endif
#endregion

namespace PongExample.Screens
{
	public partial class MainScreen
	{
		void CustomInitialize()
		{
            StartBallMoving();
		}

        private void StartBallMoving()
        {
            int num = FlatRedBallServices.Random.Next(0, 2);

            BallInstance.Position = Vector3.Zero;

            if (num == 0)
                BallInstance.XVelocity = BallInstance.StartingSpeed;
            else
                BallInstance.XVelocity = -BallInstance.StartingSpeed;
        }

		void CustomActivity(bool firstTimeCalled)
		{
            CollisionActivity();
		}

        private void CollisionActivity()
        {
            if (BallInstance.CollideAgainstBounce(Player1Paddle, 0, 1, 1))
                BallInstance.YVelocity = BallInstance.Y - Player1Paddle.Y;

            if(BallInstance.CollideAgainstBounce(Player2Paddle, 0, 1, 1))
                BallInstance.YVelocity = BallInstance.Y - Player2Paddle.Y;

            PaddleOnWall(Player1Paddle, TopWall);
            PaddleOnWall(Player1Paddle, BottomWall);
            PaddleOnWall(Player2Paddle, TopWall);
            PaddleOnWall(Player2Paddle, BottomWall);

            BallInstance.CollideAgainstBounce(TopWall, 0, 1, 1);
            BallInstance.CollideAgainstBounce(BottomWall, 0, 1, 1);

            if (BallInstance.CollideAgainst(RightGoal))
            {
                Player1ScoreDisplayText++;

                StartBallMoving();
            }
            else if (BallInstance.CollideAgainst(LeftGoal))
            {
                Player2ScoreDisplayText++;

                StartBallMoving();
            }
        }

        private void PaddleOnWall(Paddle paddle, AxisAlignedRectangle wall)
        {
            if (paddle.CollideAgainst(wall))
            {
                int polarity = wall.Equals(TopWall) ? -1 : 1;

                paddle.Collision.ShiftRelative(0, 10 * polarity, 0);
            }
        }

		void CustomDestroy()
		{


		}

        static void CustomLoadStaticContent(string contentManagerName)
        {


        }

	}
}

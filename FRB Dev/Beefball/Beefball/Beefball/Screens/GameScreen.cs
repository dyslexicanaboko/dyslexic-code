using System;
using System.Collections.Generic;
using System.Text;
using FlatRedBall;
using FlatRedBall.Input;
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
#endif

namespace Beefball.Screens
{
	public partial class GameScreen
	{
        int mScoreForTeam0 = 0;
        int mScoreForTeam1 = 0;

		void CustomInitialize()
		{
            AssignPlayerBallIndices();
		}

		void CustomActivity(bool firstTimeCalled)
		{
            CollisionActivity();
        }

		void CustomDestroy()
		{


		}

        static void CustomLoadStaticContent(string contentManagerName)
        {


        }

        // Add the CollisionActivity method to your GameScreen.cs custom code file:
        private void CollisionActivity()
        {
            //PlayerBallInstance.Body.CollideAgainstMove(CollisionFile, 0, 1);
            //PlayerBallInstance.Body.CollideAgainstBounce(CollisionFile, 0, 1, 1);
            //PuckInstance.Body.CollideAgainstBounce(CollisionFile, 0, 1, 1);
            //PlayerBallInstance.Body.CollideAgainstBounce(PuckInstance.Body, 1, .3f, 1);

            for (int i = 0; i < PlayerBallList.Count; i++)
            {
                PlayerBallList[i].Body.CollideAgainstBounce(CollisionFile, 0, 1, 1);
                PlayerBallList[i].Body.CollideAgainstBounce(GoalAreaFile, 0, 1, 1);
                PlayerBallList[i].Body.CollideAgainstBounce(PuckInstance.Body, 1, .3f, 1);

                for (int j = i + 1; j < PlayerBallList.Count; j++)
                    PlayerBallList[i].Body.CollideAgainstBounce(PlayerBallList[j].Body, 1, 1, 1);
            }

            PuckInstance.Body.CollideAgainstBounce(CollisionFile, 0, 1, 1);

            if (PuckInstance.Body.CollideAgainst(LeftGoal))
                AssignGoalToTeam(0);

            if (PuckInstance.Body.CollideAgainst(RightGoal))
                AssignGoalToTeam(1);
        }

        private void AssignPlayerBallIndices()
        {
            for (int i = 0; i < PlayerBallList.Count; i++)
                PlayerBallList[i].PlayerIndex = i;
        }

        private void AssignGoalToTeam(int teamIndex)
        {
            switch (teamIndex)
            {
                case 0:
                    mScoreForTeam0++;
                    ScoreHudInstance.Score1 = mScoreForTeam0;
                    break;
                case 1:
                    mScoreForTeam1++;
                    ScoreHudInstance.Score2 = mScoreForTeam1;
                    break;
                default:
                    throw new ArgumentException("Team index must be either 0 or 1");
            }

            // and move all Entities back to their starting spots:
            ResetAllPositionsAndStates();
        }

        private void ResetAllPositionsAndStates()
        {
            PlayerBallInstance.Position = PlayerBallInstancePositionReset;
            PlayerBallInstance.Velocity = Vector3.Zero;
            PlayerBallInstance.Acceleration = Vector3.Zero;

            PlayerBallInstance2.Position = PlayerBallInstance2PositionReset;
            PlayerBallInstance2.Velocity = Vector3.Zero;
            PlayerBallInstance2.Acceleration = Vector3.Zero;

            PuckInstance.X = 0;
            PuckInstance.Y = 0;
            PuckInstance.Velocity = Vector3.Zero;
        }
	}
}

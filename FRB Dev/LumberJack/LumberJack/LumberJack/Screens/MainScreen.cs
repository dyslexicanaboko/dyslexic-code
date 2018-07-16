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
using LumberJack.Entities;
#endif
#endregion

namespace LumberJack.Screens
{
	public partial class MainScreen
	{
		void CustomInitialize()
		{
            FlatRedBall.Debugging.Debugger.TextCorner = FlatRedBall.Debugging.Debugger.Corner.BottomLeft;

            BuildForest();
		}

        private void BuildForest()
        {
            Factories.TreeFactory.Initialize(TreeList, ContentManagerName);

            Tree tree = null;

            float xE = SpriteManager.Camera.AbsoluteLeftXEdgeAt(0);
            float yE = SpriteManager.Camera.AbsoluteTopYEdgeAt(0);
            float x = xE;
            float y = 0;

            for (int i = 0; i < 30; i++)
            {
                x += 25;
                y = 0;

                for (int j = 0; j < 25; j++)
                {
                    y += 25;

                    tree = Factories.TreeFactory.CreateNew();
                    tree.Position = new Vector3(x, y, 0);
                    tree.Size = FlatRedBallServices.Random.Next(10, 21);
                }
            }
        }

		void CustomActivity(bool firstTimeCalled)
		{
            CollisionActivity();

            JackInstance.ThrustWeapon(AxeInstance);
            
            //AxeInstance.DebugPrintPosition();
		}

        private void CollisionActivity()
        {
            foreach (Tree t in TreeList)
            {
                JackInstance.CollideAgainstTree(t, AxeInstance);

                if(t.IsFelled(JackInstance, AxeInstance))
                    TreesFelledDisplayText++;
            }

            //FlatRedBall.Debugging.Debugger.Write("Chops: " + Tree1.Chops + "\n Is Chopping? :" + JackInstance.IsChopping);
        }

		void CustomDestroy()
		{


		}

        static void CustomLoadStaticContent(string contentManagerName)
        {


        }

	}
}

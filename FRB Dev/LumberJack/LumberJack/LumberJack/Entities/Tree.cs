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
	public partial class Tree
	{
        private void CustomInitialize()
		{


		}

		private void CustomActivity()
		{


		}

        public bool IsFelled(Jack jack, Axe axe)
        {
            if (axe.CollideAgainst(this))
            {
                if (jack.NewAttack)
                {
                    jack.AcknowledgeAttack();
                    
                    Chops++;

                    WearDown(Chops);
                }

                if (Chops == MaxChops)
                {
                    Destroy();

                    return true;
                }

                #region Moving the Tree During Chop
                //float oX = Tree1.Position.X;
                //float oY = Tree1.Position.Y;

                //int x = 0;
                //int y = 0;

                //switch (JackInstance.Facing)
                //{
                //    case Direction.Forward:
                //        x = 0;
                //        y = 2;
                //        break;
                //    case Direction.Left:
                //        x = -2;
                //        y = 0;
                //        break;
                //    case Direction.Right:
                //        x = 2;
                //        y = 0;
                //        break;
                //    case Direction.Backward:
                //        x = 0;
                //        y = -2;
                //        break;
                //}

                //Tree1.Collision.ShiftRelative(x, y, 0);
                //Tree1.Position = new Vector3(oX, oY, 0);
                #endregion
            }

            return false;
        }

        private void WearDown(int chops)
        {
            switch (chops)
            { 
                case 0:
                    CircleInstance.Color = Microsoft.Xna.Framework.Color.DarkGreen;
                    break;
                case 1:
                    CircleInstance.Color = Microsoft.Xna.Framework.Color.Brown;
                    break;
                case 2:
                    CircleInstance.Color = Microsoft.Xna.Framework.Color.BurlyWood;
                    break;
                case 3:
                    CircleInstance.Color = Microsoft.Xna.Framework.Color.Chocolate;
                    break;
                case 4:
                    CircleInstance.Color = Microsoft.Xna.Framework.Color.Red;
                    break;
            }
        }

		private void CustomDestroy()
		{


		}

        private static void CustomLoadStaticContent(string contentManagerName)
        {


        }
	}
}

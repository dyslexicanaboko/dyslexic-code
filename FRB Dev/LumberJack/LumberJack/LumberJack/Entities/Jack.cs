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
	public partial class Jack
	{
        public Direction Facing { get; private set; }
        public bool IsChopping { get; private set; }
        public bool NewAttack { get; private set; }
        
		private void CustomInitialize()
		{
            Facing = Direction.Left;
            NewAttack = true;
		}

		private void CustomActivity()
		{
            if (InputManager.Keyboard.KeyDown(DownKey))
            {
                YVelocity = -MovementSpeed;
                
                Facing = Direction.Backward;
            }
            else if (InputManager.Keyboard.KeyDown(UpKey))
            {
                YVelocity = MovementSpeed;
             
                Facing = Direction.Forward;
            }
            else if (InputManager.Keyboard.KeyDown(LeftKey))
            {
                XVelocity = -MovementSpeed;
             
                Facing = Direction.Left;
            }
            else if (InputManager.Keyboard.KeyDown(RightKey))
            {
                XVelocity = MovementSpeed;

                Facing = Direction.Right;
            }
            else
            {
                XVelocity = 0;
                YVelocity = 0;
            }                                     
		}

        public void DebugPrintPosition()
        {
            FlatRedBall.Debugging.Debugger.Write("Jack (X, Y) @ (Vx, Vy): (" + X + ", " + Y + ") @ (" + XVelocity + ", " + YVelocity + ")");
        }

        public void ThrustWeapon(Axe weapon)
        {
            ThrustWeapon(Facing, weapon);
        }

        public bool CollideAgainstTree(Tree tree, Axe axe)
        {
            bool collision = this.CollideAgainst(tree);

            if (collision)
            {
                int x = 0;
                int y = 0;

                switch (Facing)
                {
                    case Direction.Forward:
                        x = 0;
                        y = -5;
                        break;
                    case Direction.Backward:
                        x = 0;
                        y = 5;
                        break;
                    case Direction.Left:
                        x = -5;
                        y = 0;
                        break;
                    case Direction.Right:
                        x = 5;
                        y = 0;
                        break;
                }

                Collision.ShiftRelative(x, y, 0);

                axe.Collision.ShiftRelative(x, y, 0);
            }

            return collision;
        }

        private void ThrustWeapon(Direction facing, Axe weapon)
        {
            IsChopping = InputManager.Keyboard.KeyDown(Microsoft.Xna.Framework.Input.Keys.Space);

            if (IsChopping)
            {
                switch (facing)
                {
                    case Direction.Forward:
                        weapon.ResetPositionX(this);
                        weapon.Y = Y + WeaponOffset;
                        weapon.YVelocity = weapon.AttackSpeed;
                        break;
                    case Direction.Backward:
                        weapon.ResetPositionX(this);
                        weapon.Y = Y - WeaponOffset;
                        weapon.YVelocity = -weapon.AttackSpeed;
                        break;
                    case Direction.Left:
                        weapon.ResetPositionY(this);
                        weapon.X = X - WeaponOffset;
                        weapon.XVelocity = -weapon.AttackSpeed;
                        break;
                    case Direction.Right:
                        weapon.ResetPositionY(this);
                        weapon.X = X + WeaponOffset;
                        weapon.XVelocity = weapon.AttackSpeed;
                        break;
                }
            }
            else
            {
                NewAttack = true;

                weapon.ResetPosition(this);
            }
        }

        public void AcknowledgeAttack()
        {
            NewAttack = false;
        }

		private void CustomDestroy()
		{


		}

        private static void CustomLoadStaticContent(string contentManagerName)
        {


        }
	}
}

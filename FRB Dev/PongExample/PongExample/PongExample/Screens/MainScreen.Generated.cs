#if ANDROID
#define REQUIRES_PRIMARY_THREAD_LOADING
#endif


using BitmapFont = FlatRedBall.Graphics.BitmapFont;

using Cursor = FlatRedBall.Gui.Cursor;
using GuiManager = FlatRedBall.Gui.GuiManager;

#if XNA4 || WINDOWS_8
using Color = Microsoft.Xna.Framework.Color;
#elif FRB_MDX
using Color = System.Drawing.Color;
#else
using Color = Microsoft.Xna.Framework.Graphics.Color;
#endif

#if FRB_XNA || SILVERLIGHT
using Keys = Microsoft.Xna.Framework.Input.Keys;
using Vector3 = Microsoft.Xna.Framework.Vector3;
using Texture2D = Microsoft.Xna.Framework.Graphics.Texture2D;
using Microsoft.Xna.Framework.Media;
#endif

// Generated Usings
using PongExample.Entities;
using FlatRedBall;
using FlatRedBall.Screens;
using System;
using System.Collections.Generic;
using System.Text;
using FlatRedBall.Math.Geometry;
using FlatRedBall.Graphics;

namespace PongExample.Screens
{
	public partial class MainScreen : Screen
	{
		// Generated Fields
		#if DEBUG
		static bool HasBeenLoadedWithGlobalContentManager = false;
		#endif
		
		private FlatRedBall.Math.Geometry.AxisAlignedRectangle mBottomWall;
		public FlatRedBall.Math.Geometry.AxisAlignedRectangle BottomWall
		{
			get
			{
				return mBottomWall;
			}
			private set
			{
				mBottomWall = value;
			}
		}
		private FlatRedBall.Math.Geometry.AxisAlignedRectangle mTopWall;
		public FlatRedBall.Math.Geometry.AxisAlignedRectangle TopWall
		{
			get
			{
				return mTopWall;
			}
			private set
			{
				mTopWall = value;
			}
		}
		private PongExample.Entities.Ball BallInstance;
		private PongExample.Entities.Paddle Player1Paddle;
		private PongExample.Entities.Paddle Player2Paddle;
		private FlatRedBall.Graphics.Text Player1Score;
		private FlatRedBall.Graphics.Text Player2Score;
		private FlatRedBall.Math.Geometry.AxisAlignedRectangle mRightGoal;
		public FlatRedBall.Math.Geometry.AxisAlignedRectangle RightGoal
		{
			get
			{
				return mRightGoal;
			}
			private set
			{
				mRightGoal = value;
			}
		}
		private FlatRedBall.Math.Geometry.AxisAlignedRectangle mLeftGoal;
		public FlatRedBall.Math.Geometry.AxisAlignedRectangle LeftGoal
		{
			get
			{
				return mLeftGoal;
			}
			private set
			{
				mLeftGoal = value;
			}
		}
		public int Player1ScoreDisplayText
		{
			get
			{
				return int.Parse(Player1Score.DisplayText);
			}
			set
			{
				Player1Score.DisplayText = value.ToString();
			}
		}
		public int Player2ScoreDisplayText
		{
			get
			{
				return int.Parse(Player2Score.DisplayText);
			}
			set
			{
				Player2Score.DisplayText = value.ToString();
			}
		}

		public MainScreen()
			: base("MainScreen")
		{
		}

        public override void Initialize(bool addToManagers)
        {
			// Generated Initialize
			LoadStaticContent(ContentManagerName);
			mBottomWall = new FlatRedBall.Math.Geometry.AxisAlignedRectangle();
			mBottomWall.Name = "mBottomWall";
			mTopWall = new FlatRedBall.Math.Geometry.AxisAlignedRectangle();
			mTopWall.Name = "mTopWall";
			BallInstance = new PongExample.Entities.Ball(ContentManagerName, false);
			BallInstance.Name = "BallInstance";
			Player1Paddle = new PongExample.Entities.Paddle(ContentManagerName, false);
			Player1Paddle.Name = "Player1Paddle";
			Player2Paddle = new PongExample.Entities.Paddle(ContentManagerName, false);
			Player2Paddle.Name = "Player2Paddle";
			Player1Score = new FlatRedBall.Graphics.Text();
			Player1Score.Name = "Player1Score";
			Player2Score = new FlatRedBall.Graphics.Text();
			Player2Score.Name = "Player2Score";
			mRightGoal = new FlatRedBall.Math.Geometry.AxisAlignedRectangle();
			mRightGoal.Name = "mRightGoal";
			mLeftGoal = new FlatRedBall.Math.Geometry.AxisAlignedRectangle();
			mLeftGoal.Name = "mLeftGoal";
			
			
			PostInitialize();
			base.Initialize(addToManagers);
			if (addToManagers)
			{
				AddToManagers();
			}

        }
        
// Generated AddToManagers
		public override void AddToManagers ()
		{
			ShapeManager.AddAxisAlignedRectangle(mBottomWall);
			ShapeManager.AddAxisAlignedRectangle(mTopWall);
			BallInstance.AddToManagers(mLayer);
			Player1Paddle.AddToManagers(mLayer);
			Player2Paddle.AddToManagers(mLayer);
			TextManager.AddText(Player1Score); if(Player1Score.Font != null) Player1Score.SetPixelPerfectScale(SpriteManager.Camera);
			if (Player1Score.Font != null)
			{
				Player1Score.SetPixelPerfectScale(mLayer);
			}
			TextManager.AddText(Player2Score); if(Player2Score.Font != null) Player2Score.SetPixelPerfectScale(SpriteManager.Camera);
			if (Player2Score.Font != null)
			{
				Player2Score.SetPixelPerfectScale(mLayer);
			}
			ShapeManager.AddAxisAlignedRectangle(mRightGoal);
			ShapeManager.AddAxisAlignedRectangle(mLeftGoal);
			base.AddToManagers();
			AddToManagersBottomUp();
			CustomInitialize();
		}


		public override void Activity(bool firstTimeCalled)
		{
			// Generated Activity
			if (!IsPaused)
			{
				
				BallInstance.Activity();
				Player1Paddle.Activity();
				Player2Paddle.Activity();
			}
			else
			{
			}
			base.Activity(firstTimeCalled);
			if (!IsActivityFinished)
			{
				CustomActivity(firstTimeCalled);
			}


				// After Custom Activity
				
            
		}

		public override void Destroy()
		{
			// Generated Destroy
			
			if (BottomWall != null)
			{
				ShapeManager.Remove(BottomWall);
			}
			if (TopWall != null)
			{
				ShapeManager.Remove(TopWall);
			}
			if (BallInstance != null)
			{
				BallInstance.Destroy();
				BallInstance.Detach();
			}
			if (Player1Paddle != null)
			{
				Player1Paddle.Destroy();
				Player1Paddle.Detach();
			}
			if (Player2Paddle != null)
			{
				Player2Paddle.Destroy();
				Player2Paddle.Detach();
			}
			if (Player1Score != null)
			{
				TextManager.RemoveText(Player1Score);
			}
			if (Player2Score != null)
			{
				TextManager.RemoveText(Player2Score);
			}
			if (RightGoal != null)
			{
				ShapeManager.Remove(RightGoal);
			}
			if (LeftGoal != null)
			{
				ShapeManager.Remove(LeftGoal);
			}

			base.Destroy();

			CustomDestroy();

		}

		// Generated Methods
		public virtual void PostInitialize ()
		{
			bool oldShapeManagerSuppressAdd = FlatRedBall.Math.Geometry.ShapeManager.SuppressAddingOnVisibilityTrue;
			FlatRedBall.Math.Geometry.ShapeManager.SuppressAddingOnVisibilityTrue = true;
			BottomWall.Height = 32f;
			BottomWall.Width = 800f;
			if (BottomWall.Parent == null)
			{
				BottomWall.Y = -300f;
			}
			else
			{
				BottomWall.RelativeY = -300f;
			}
			TopWall.Height = 32f;
			TopWall.Width = 800f;
			if (TopWall.Parent == null)
			{
				TopWall.Y = 300f;
			}
			else
			{
				TopWall.RelativeY = 300f;
			}
			if (Player1Paddle.Parent == null)
			{
				Player1Paddle.X = -370f;
			}
			else
			{
				Player1Paddle.RelativeX = -370f;
			}
			Player1Paddle.AxisAlignedRectangleInstanceRepositionDirections = FlatRedBall.Math.Geometry.RepositionDirections.Right;
			Player1Paddle.MoveUpKey = Microsoft.Xna.Framework.Input.Keys.W;
			Player1Paddle.MoveDownKey = Microsoft.Xna.Framework.Input.Keys.S;
			if (Player2Paddle.Parent == null)
			{
				Player2Paddle.X = 370f;
			}
			else
			{
				Player2Paddle.RelativeX = 370f;
			}
			Player2Paddle.AxisAlignedRectangleInstanceRepositionDirections = FlatRedBall.Math.Geometry.RepositionDirections.Left;
			Player2Paddle.MoveUpKey = Microsoft.Xna.Framework.Input.Keys.Up;
			Player2Paddle.MoveDownKey = Microsoft.Xna.Framework.Input.Keys.Down;
			if (Player1Score.Parent == null)
			{
				Player1Score.X = -200f;
			}
			else
			{
				Player1Score.RelativeX = -200f;
			}
			if (Player1Score.Parent == null)
			{
				Player1Score.Y = 250f;
			}
			else
			{
				Player1Score.RelativeY = 250f;
			}
			if (Player2Score.Parent == null)
			{
				Player2Score.X = 200f;
			}
			else
			{
				Player2Score.RelativeX = 200f;
			}
			if (Player2Score.Parent == null)
			{
				Player2Score.Y = 250f;
			}
			else
			{
				Player2Score.RelativeY = 250f;
			}
			RightGoal.Height = 600f;
			RightGoal.Width = 32f;
			if (RightGoal.Parent == null)
			{
				RightGoal.X = 400f;
			}
			else
			{
				RightGoal.RelativeX = 400f;
			}
			LeftGoal.Height = 600f;
			LeftGoal.Width = 32f;
			if (LeftGoal.Parent == null)
			{
				LeftGoal.X = -400f;
			}
			else
			{
				LeftGoal.RelativeX = -400f;
			}
			FlatRedBall.Math.Geometry.ShapeManager.SuppressAddingOnVisibilityTrue = oldShapeManagerSuppressAdd;
		}
		public virtual void AddToManagersBottomUp ()
		{
			CameraSetup.ResetCamera(SpriteManager.Camera);
			AssignCustomVariables(false);
		}
		public virtual void RemoveFromManagers ()
		{
			if (BottomWall != null)
			{
				ShapeManager.RemoveOneWay(BottomWall);
			}
			if (TopWall != null)
			{
				ShapeManager.RemoveOneWay(TopWall);
			}
			BallInstance.RemoveFromManagers();
			Player1Paddle.RemoveFromManagers();
			Player2Paddle.RemoveFromManagers();
			if (Player1Score != null)
			{
				TextManager.RemoveTextOneWay(Player1Score);
			}
			if (Player2Score != null)
			{
				TextManager.RemoveTextOneWay(Player2Score);
			}
			if (RightGoal != null)
			{
				ShapeManager.RemoveOneWay(RightGoal);
			}
			if (LeftGoal != null)
			{
				ShapeManager.RemoveOneWay(LeftGoal);
			}
		}
		public virtual void AssignCustomVariables (bool callOnContainedElements)
		{
			if (callOnContainedElements)
			{
				BallInstance.AssignCustomVariables(true);
				Player1Paddle.AssignCustomVariables(true);
				Player2Paddle.AssignCustomVariables(true);
			}
			mBottomWall.Height = 32f;
			mBottomWall.Width = 800f;
			if (mBottomWall.Parent == null)
			{
				mBottomWall.Y = -300f;
			}
			else
			{
				mBottomWall.RelativeY = -300f;
			}
			mTopWall.Height = 32f;
			mTopWall.Width = 800f;
			if (mTopWall.Parent == null)
			{
				mTopWall.Y = 300f;
			}
			else
			{
				mTopWall.RelativeY = 300f;
			}
			if (Player1Paddle.Parent == null)
			{
				Player1Paddle.X = -370f;
			}
			else
			{
				Player1Paddle.RelativeX = -370f;
			}
			Player1Paddle.AxisAlignedRectangleInstanceRepositionDirections = FlatRedBall.Math.Geometry.RepositionDirections.Right;
			Player1Paddle.MoveUpKey = Microsoft.Xna.Framework.Input.Keys.W;
			Player1Paddle.MoveDownKey = Microsoft.Xna.Framework.Input.Keys.S;
			if (Player2Paddle.Parent == null)
			{
				Player2Paddle.X = 370f;
			}
			else
			{
				Player2Paddle.RelativeX = 370f;
			}
			Player2Paddle.AxisAlignedRectangleInstanceRepositionDirections = FlatRedBall.Math.Geometry.RepositionDirections.Left;
			Player2Paddle.MoveUpKey = Microsoft.Xna.Framework.Input.Keys.Up;
			Player2Paddle.MoveDownKey = Microsoft.Xna.Framework.Input.Keys.Down;
			if (Player1Score.Parent == null)
			{
				Player1Score.X = -200f;
			}
			else
			{
				Player1Score.RelativeX = -200f;
			}
			if (Player1Score.Parent == null)
			{
				Player1Score.Y = 250f;
			}
			else
			{
				Player1Score.RelativeY = 250f;
			}
			if (Player2Score.Parent == null)
			{
				Player2Score.X = 200f;
			}
			else
			{
				Player2Score.RelativeX = 200f;
			}
			if (Player2Score.Parent == null)
			{
				Player2Score.Y = 250f;
			}
			else
			{
				Player2Score.RelativeY = 250f;
			}
			mRightGoal.Height = 600f;
			mRightGoal.Width = 32f;
			if (mRightGoal.Parent == null)
			{
				mRightGoal.X = 400f;
			}
			else
			{
				mRightGoal.RelativeX = 400f;
			}
			mLeftGoal.Height = 600f;
			mLeftGoal.Width = 32f;
			if (mLeftGoal.Parent == null)
			{
				mLeftGoal.X = -400f;
			}
			else
			{
				mLeftGoal.RelativeX = -400f;
			}
			Player1ScoreDisplayText = 0;
			Player2ScoreDisplayText = 0;
		}
		public virtual void ConvertToManuallyUpdated ()
		{
			BallInstance.ConvertToManuallyUpdated();
			Player1Paddle.ConvertToManuallyUpdated();
			Player2Paddle.ConvertToManuallyUpdated();
			TextManager.ConvertToManuallyUpdated(Player1Score);
			TextManager.ConvertToManuallyUpdated(Player2Score);
		}
		public static void LoadStaticContent (string contentManagerName)
		{
			if (string.IsNullOrEmpty(contentManagerName))
			{
				throw new ArgumentException("contentManagerName cannot be empty or null");
			}
			#if DEBUG
			if (contentManagerName == FlatRedBallServices.GlobalContentManager)
			{
				HasBeenLoadedWithGlobalContentManager = true;
			}
			else if (HasBeenLoadedWithGlobalContentManager)
			{
				throw new Exception("This type has been loaded with a Global content manager, then loaded with a non-global.  This can lead to a lot of bugs");
			}
			#endif
			PongExample.Entities.Ball.LoadStaticContent(contentManagerName);
			PongExample.Entities.Paddle.LoadStaticContent(contentManagerName);
			CustomLoadStaticContent(contentManagerName);
		}
		[System.Obsolete("Use GetFile instead")]
		public static object GetStaticMember (string memberName)
		{
			return null;
		}
		public static object GetFile (string memberName)
		{
			return null;
		}
		object GetMember (string memberName)
		{
			return null;
		}


	}
}

using System;
using System.Collections.Generic;
using System.Text;
using FlatRedBall.Math.Geometry;
using FlatRedBall.AI.Pathfinding;
using FlatRedBall.Input;
using FlatRedBall.IO;
using FlatRedBall.Instructions;
using FlatRedBall.Math.Splines;
using FlatRedBall.Utilities;
using BitmapFont = FlatRedBall.Graphics.BitmapFont;

using Cursor = FlatRedBall.Gui.Cursor;
using GuiManager = FlatRedBall.Gui.GuiManager;

#if XNA4
using Color = Microsoft.Xna.Framework.Color;
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
using FlatRedBall.Broadcasting;
using Beefball.Entities;
using FlatRedBall;
using FlatRedBall.Math;
using FlatRedBall.Math.Geometry;
using FlatRedBall.Graphics;

namespace Beefball.Screens
{
	public partial class GameScreen : Screen
	{
		// Generated Fields
		#if DEBUG
		static bool HasBeenLoadedWithGlobalContentManager = false;
		#endif
		private ShapeCollection CollisionFile;
		private ShapeCollection GoalAreaFile;

		private Beefball.Entities.Puck PuckInstance;
		private PositionedObjectList<PlayerBall> PlayerBallList;
		private Beefball.Entities.PlayerBall PlayerBallInstance;
		static Microsoft.Xna.Framework.Vector3 PlayerBallInstancePositionReset;
		private Beefball.Entities.PlayerBall PlayerBallInstance2;
		static Microsoft.Xna.Framework.Vector3 PlayerBallInstance2PositionReset;
		private AxisAlignedRectangle LeftGoal;
		private AxisAlignedRectangle RightGoal;
		private Beefball.Entities.ScoreHud ScoreHudInstance;
		private Layer HudLayer;

		public GameScreen()
			: base("GameScreen")
		{
		}

        public override void Initialize(bool addToManagers)
        {
			// Generated Initialize
			LoadStaticContent(ContentManagerName);
			CollisionFile = FlatRedBallServices.Load<ShapeCollection>("content/screens/gamescreen/collisionfile.shcx", ContentManagerName);
			GoalAreaFile = FlatRedBallServices.Load<ShapeCollection>("content/screens/gamescreen/goalareafile.shcx", ContentManagerName);
			PuckInstance = new Beefball.Entities.Puck(ContentManagerName, false);
			PuckInstance.Name = "PuckInstance";
			PlayerBallList = new PositionedObjectList<PlayerBall>();
			PlayerBallInstance = new Beefball.Entities.PlayerBall(ContentManagerName, false);
			PlayerBallInstance.Name = "PlayerBallInstance";
			PlayerBallInstance2 = new Beefball.Entities.PlayerBall(ContentManagerName, false);
			PlayerBallInstance2.Name = "PlayerBallInstance2";
			LeftGoal = GoalAreaFile.AxisAlignedRectangles.FindByName("LeftGoal");
			RightGoal = GoalAreaFile.AxisAlignedRectangles.FindByName("RightGoal");
			ScoreHudInstance = new Beefball.Entities.ScoreHud(ContentManagerName, false);
			ScoreHudInstance.Name = "ScoreHudInstance";
			PlayerBallList.Add(PlayerBallInstance);
			PlayerBallList.Add(PlayerBallInstance2);



			PostInitialize();
			PlayerBallInstancePositionReset = PlayerBallInstance.Position;
			PlayerBallInstance2PositionReset = PlayerBallInstance2.Position;
			if(addToManagers)
			{
				AddToManagers();
			}

        }
        
// Generated AddToManagers

        public override void AddToManagers()
        {
			HudLayer = SpriteManager.AddLayer();
			HudLayer.UsePixelCoordinates();
			AddToManagersBottomUp();
			CustomInitialize();

        }


		public override void Activity(bool firstTimeCalled)
		{
			// Generated Activity
			if(!IsPaused)
			{

				PuckInstance.Activity();
				for(int i = PlayerBallList.Count - 1; i > -1; i--)
				{
					PlayerBallList[i].Activity();
				}
				ScoreHudInstance.Activity();
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
			if(PuckInstance != null)
			{
				PuckInstance.Destroy();
			}
			for(int i = PlayerBallList.Count - 1; i > -1; i--)
			{
				PlayerBallList[i].Destroy();
			}
			if(ScoreHudInstance != null)
			{
				ScoreHudInstance.Destroy();
			}
			if(HudLayer != null)
			{
				SpriteManager.RemoveLayer(HudLayer);
			}
			CollisionFile.RemoveFromManagers(ContentManagerName != "Global");

			GoalAreaFile.RemoveFromManagers(ContentManagerName != "Global");



			base.Destroy();

			CustomDestroy();

		}

		// Generated Methods
		public virtual void PostInitialize()
		{
			PlayerBallInstance.X = 10f;
			PlayerBallInstance2.X = -10f;
		}
		public virtual void AddToManagersBottomUp()
		{
			CollisionFile.AddToManagers(mLayer);

			GoalAreaFile.AddToManagers(mLayer);

			PuckInstance.AddToManagers(mLayer);
			PlayerBallInstance.AddToManagers(mLayer);
			PlayerBallInstance.X = 10f;
			PlayerBallInstance.Position = PlayerBallInstancePositionReset;
			PlayerBallInstance2.AddToManagers(mLayer);
			PlayerBallInstance2.X = -10f;
			PlayerBallInstance2.Position = PlayerBallInstance2PositionReset;
			ScoreHudInstance.AddToManagers(HudLayer);
		}
		public virtual void ConvertToManuallyUpdated()
		{
			PuckInstance.ConvertToManuallyUpdated();
			for(int i = 0; i < PlayerBallList.Count; i++)
			{
				PlayerBallList[i].ConvertToManuallyUpdated();
			}
			ScoreHudInstance.ConvertToManuallyUpdated();
		}
		public static void LoadStaticContent(string contentManagerName)
		{
			#if DEBUG
			if(contentManagerName == FlatRedBallServices.GlobalContentManager)
			{
				HasBeenLoadedWithGlobalContentManager = true;
			}
			else if(HasBeenLoadedWithGlobalContentManager)
			{
				throw new Exception("This type has been loaded with a Global content manager, then loaded with a non-global.  This can lead to a lot of bugs");
			}
			#endif
			Beefball.Entities.Puck.LoadStaticContent(contentManagerName);
			Beefball.Entities.ScoreHud.LoadStaticContent(contentManagerName);
			CustomLoadStaticContent(contentManagerName);
		}
		object GetMember(string memberName)
		{
			switch(memberName)
			{
				case "CollisionFile":
					return CollisionFile;
				case "GoalAreaFile":
					return GoalAreaFile;
			}
			return null;
		}


	}
}

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
using DashRunning.Entities;
using FlatRedBall;
using FlatRedBall.Math;
using FlatRedBall.Graphics.Animation;

namespace DashRunning.Screens
{
	public partial class GameScreen : Screen
	{
		// Generated Fields
		#if DEBUG
		static bool HasBeenLoadedWithGlobalContentManager = false;
		#endif
		private AnimationChainList TrackAnimationChainListFile;
		private Scene TrackScene;

		private DashRunning.Entities.Dash DashInstance;
		private PositionedObjectList<Obstacles> ObstaclesList;
		private DashRunning.Entities.Obstacles ObstaclesInstance;
		private DashRunning.Entities.Obstacles ObstaclesInstance2;
		private Scene TrackAnimation;

		public GameScreen()
			: base("GameScreen")
		{
		}

        public override void Initialize(bool addToManagers)
        {
			// Generated Initialize
			LoadStaticContent(ContentManagerName);
			TrackAnimationChainListFile = FlatRedBallServices.Load<AnimationChainList>("content/screens/gamescreen/trackanimationchainlistfile.achx", ContentManagerName);
			TrackScene = FlatRedBallServices.Load<Scene>("content/screens/gamescreen/trackscene.scnx", ContentManagerName);
			TrackAnimation = TrackScene;
			for (int i = 0; i < TrackAnimation.Texts.Count; i++)
			{
				TrackAnimation.Texts[i].AdjustPositionForPixelPerfectDrawing = true;
			}
			DashInstance = new DashRunning.Entities.Dash(ContentManagerName, false);
			DashInstance.Name = "DashInstance";
			ObstaclesList = new PositionedObjectList<Obstacles>();
			ObstaclesInstance = new DashRunning.Entities.Obstacles(ContentManagerName, false);
			ObstaclesInstance.Name = "ObstaclesInstance";
			ObstaclesInstance2 = new DashRunning.Entities.Obstacles(ContentManagerName, false);
			ObstaclesInstance2.Name = "ObstaclesInstance2";
			ObstaclesList.Add(ObstaclesInstance);
			ObstaclesList.Add(ObstaclesInstance2);

			BackStackBehavior = FlatRedBall.Utilities.BackStackBehavior.Ignore;

			PostInitialize();
			if(addToManagers)
			{
				AddToManagers();
			}

        }
        
// Generated AddToManagers

        public override void AddToManagers()
        {
			AddToManagersBottomUp();
			CustomInitialize();

        }


		public override void Activity(bool firstTimeCalled)
		{
			// Generated Activity
			if(!IsPaused)
			{

				DashInstance.Activity();
				for(int i = ObstaclesList.Count - 1; i > -1; i--)
				{
					ObstaclesList[i].Activity();
				}
			}
			else
			{
			}
			AsyncActivity();
			base.Activity(firstTimeCalled);
			if (!IsActivityFinished)
			{
				CustomActivity(firstTimeCalled);
			}


				// After Custom Activity

			TrackScene.ManageAll();
		
		
            
		}

		public override void Destroy()
		{
			// Generated Destroy
			if(DashInstance != null)
			{
				DashInstance.Destroy();
			}
			for(int i = ObstaclesList.Count - 1; i > -1; i--)
			{
				ObstaclesList[i].Destroy();
			}

			TrackScene.RemoveFromManagers(ContentManagerName != "Global");



			base.Destroy();

			CustomDestroy();

		}

		// Generated Methods
		public virtual void PostInitialize()
		{
			ObstaclesInstance.X = -10f;
			ObstaclesInstance.Y = 10f;
			ObstaclesInstance2.X = 10f;
			ObstaclesInstance2.Y = 10f;
		}
		public virtual void AddToManagersBottomUp()
		{

			TrackScene.AddToManagers(mLayer);

			DashInstance.AddToManagers(mLayer);
			ObstaclesInstance.AddToManagers(mLayer);
			ObstaclesInstance.X = -10f;
			ObstaclesInstance.Y = 10f;
			ObstaclesInstance2.AddToManagers(mLayer);
			ObstaclesInstance2.X = 10f;
			ObstaclesInstance2.Y = 10f;
		}
		public virtual void ConvertToManuallyUpdated()
		{
			TrackScene.ConvertToManuallyUpdated();
			DashInstance.ConvertToManuallyUpdated();
			for(int i = 0; i < ObstaclesList.Count; i++)
			{
				ObstaclesList[i].ConvertToManuallyUpdated();
			}
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
			DashRunning.Entities.Dash.LoadStaticContent(contentManagerName);
			CustomLoadStaticContent(contentManagerName);
		}
		object GetMember(string memberName)
		{
			switch(memberName)
			{
				case "TrackAnimationChainListFile":
					return TrackAnimationChainListFile;
				case "TrackScene":
					return TrackScene;
			}
			return null;
		}
		static string mNextScreenToLoad;
		public static void TransitionToScreen(string screenName)
		{
			Screen currentScreen = ScreenManager.CurrentScreen;
			currentScreen.IsActivityFinished = true;
			currentScreen.NextScreen = typeof(GameScreen).FullName;
			mNextScreenToLoad = screenName;
		}
		void AsyncActivity()
		{
			switch (AsyncLoadingState)
			{
				case Screens.AsyncLoadingState.NotStarted:
					if (!string.IsNullOrEmpty(mNextScreenToLoad))
					{
						StartAsyncLoad(mNextScreenToLoad);
					}
					break;
				case Screens.AsyncLoadingState.LoadingScreen:
					break;
				case Screens.AsyncLoadingState.Done:
					IsActivityFinished = true;
					break;
				}
			}


	}
}

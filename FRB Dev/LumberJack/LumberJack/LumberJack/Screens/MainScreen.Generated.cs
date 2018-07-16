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
using LumberJack.Entities;
using LumberJack.Factories;
using FlatRedBall;
using FlatRedBall.Screens;
using System;
using System.Collections.Generic;
using System.Text;
using FlatRedBall.Graphics;
using FlatRedBall.Math;

namespace LumberJack.Screens
{
	public partial class MainScreen : Screen
	{
		// Generated Fields
		#if DEBUG
		static bool HasBeenLoadedWithGlobalContentManager = false;
		#endif
		
		private LumberJack.Entities.Tree Tree1;
		private LumberJack.Entities.Jack JackInstance;
		private FlatRedBall.Graphics.Text TreesFelled;
		private LumberJack.Entities.Axe AxeInstance;
		private PositionedObjectList<LumberJack.Entities.Tree> TreeList;
		private LumberJack.Entities.TreeSpawner TreeSpawnerInstance;
		public int TreesFelledDisplayText
		{
			get
			{
				return int.Parse(TreesFelled.DisplayText);
			}
			set
			{
				TreesFelled.DisplayText = value.ToString();
			}
		}
		public Microsoft.Xna.Framework.Input.Keys JackInstanceAttackKey
		{
			get
			{
				return JackInstance.AttackKey;
			}
			set
			{
				JackInstance.AttackKey = value;
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
			Tree1 = new LumberJack.Entities.Tree(ContentManagerName, false);
			Tree1.Name = "Tree1";
			JackInstance = new LumberJack.Entities.Jack(ContentManagerName, false);
			JackInstance.Name = "JackInstance";
			TreesFelled = new FlatRedBall.Graphics.Text();
			TreesFelled.Name = "TreesFelled";
			AxeInstance = new LumberJack.Entities.Axe(ContentManagerName, false);
			AxeInstance.Name = "AxeInstance";
			TreeList = new PositionedObjectList<LumberJack.Entities.Tree>();
			TreeList.Name = "TreeList";
			TreeSpawnerInstance = new LumberJack.Entities.TreeSpawner(ContentManagerName, false);
			TreeSpawnerInstance.Name = "TreeSpawnerInstance";
			
			
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
			TreeFactory.Initialize(TreeList, ContentManagerName);
			Tree1.AddToManagers(mLayer);
			JackInstance.AddToManagers(mLayer);
			TextManager.AddText(TreesFelled); if(TreesFelled.Font != null) TreesFelled.SetPixelPerfectScale(SpriteManager.Camera);
			if (TreesFelled.Font != null)
			{
				TreesFelled.SetPixelPerfectScale(mLayer);
			}
			AxeInstance.AddToManagers(mLayer);
			TreeSpawnerInstance.AddToManagers(mLayer);
			base.AddToManagers();
			AddToManagersBottomUp();
			CustomInitialize();
		}


		public override void Activity(bool firstTimeCalled)
		{
			// Generated Activity
			if (!IsPaused)
			{
				
				Tree1.Activity();
				JackInstance.Activity();
				AxeInstance.Activity();
				for (int i = TreeList.Count - 1; i > -1; i--)
				{
					if (i < TreeList.Count)
					{
						// We do the extra if-check because activity could destroy any number of entities
						TreeList[i].Activity();
					}
				}
				TreeSpawnerInstance.Activity();
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
			TreeFactory.Destroy();
			
			TreeList.MakeOneWay();
			if (Tree1 != null)
			{
				Tree1.Destroy();
				Tree1.Detach();
			}
			if (JackInstance != null)
			{
				JackInstance.Destroy();
				JackInstance.Detach();
			}
			if (TreesFelled != null)
			{
				TextManager.RemoveText(TreesFelled);
			}
			if (AxeInstance != null)
			{
				AxeInstance.Destroy();
				AxeInstance.Detach();
			}
			for (int i = TreeList.Count - 1; i > -1; i--)
			{
				TreeList[i].Destroy();
			}
			if (TreeSpawnerInstance != null)
			{
				TreeSpawnerInstance.Destroy();
				TreeSpawnerInstance.Detach();
			}
			TreeList.MakeTwoWay();

			base.Destroy();

			CustomDestroy();

		}

		// Generated Methods
		public virtual void PostInitialize ()
		{
			bool oldShapeManagerSuppressAdd = FlatRedBall.Math.Geometry.ShapeManager.SuppressAddingOnVisibilityTrue;
			FlatRedBall.Math.Geometry.ShapeManager.SuppressAddingOnVisibilityTrue = true;
			if (Tree1.Parent == null)
			{
				Tree1.X = -100f;
			}
			else
			{
				Tree1.RelativeX = -100f;
			}
			if (Tree1.Parent == null)
			{
				Tree1.Y = -200f;
			}
			else
			{
				Tree1.RelativeY = -200f;
			}
			if (TreesFelled.Parent == null)
			{
				TreesFelled.X = 200f;
			}
			else
			{
				TreesFelled.RelativeX = 200f;
			}
			if (TreesFelled.Parent == null)
			{
				TreesFelled.Y = 250f;
			}
			else
			{
				TreesFelled.RelativeY = 250f;
			}
			if (AxeInstance.Parent == null)
			{
				AxeInstance.X = -10f;
			}
			else
			{
				AxeInstance.RelativeX = -10f;
			}
			if (AxeInstance.Parent == null)
			{
				AxeInstance.Y = 0f;
			}
			else
			{
				AxeInstance.RelativeY = 0f;
			}
			AxeInstance.AttackSpeed = 100f;
			FlatRedBall.Math.Geometry.ShapeManager.SuppressAddingOnVisibilityTrue = oldShapeManagerSuppressAdd;
		}
		public virtual void AddToManagersBottomUp ()
		{
			CameraSetup.ResetCamera(SpriteManager.Camera);
			AssignCustomVariables(false);
		}
		public virtual void RemoveFromManagers ()
		{
			Tree1.RemoveFromManagers();
			JackInstance.RemoveFromManagers();
			if (TreesFelled != null)
			{
				TextManager.RemoveTextOneWay(TreesFelled);
			}
			AxeInstance.RemoveFromManagers();
			for (int i = TreeList.Count - 1; i > -1; i--)
			{
				TreeList[i].Destroy();
			}
			TreeSpawnerInstance.RemoveFromManagers();
		}
		public virtual void AssignCustomVariables (bool callOnContainedElements)
		{
			if (callOnContainedElements)
			{
				Tree1.AssignCustomVariables(true);
				JackInstance.AssignCustomVariables(true);
				AxeInstance.AssignCustomVariables(true);
				TreeSpawnerInstance.AssignCustomVariables(true);
			}
			if (Tree1.Parent == null)
			{
				Tree1.X = -100f;
			}
			else
			{
				Tree1.RelativeX = -100f;
			}
			if (Tree1.Parent == null)
			{
				Tree1.Y = -200f;
			}
			else
			{
				Tree1.RelativeY = -200f;
			}
			if (TreesFelled.Parent == null)
			{
				TreesFelled.X = 200f;
			}
			else
			{
				TreesFelled.RelativeX = 200f;
			}
			if (TreesFelled.Parent == null)
			{
				TreesFelled.Y = 250f;
			}
			else
			{
				TreesFelled.RelativeY = 250f;
			}
			if (AxeInstance.Parent == null)
			{
				AxeInstance.X = -10f;
			}
			else
			{
				AxeInstance.RelativeX = -10f;
			}
			if (AxeInstance.Parent == null)
			{
				AxeInstance.Y = 0f;
			}
			else
			{
				AxeInstance.RelativeY = 0f;
			}
			AxeInstance.AttackSpeed = 100f;
			TreesFelledDisplayText = 0;
		}
		public virtual void ConvertToManuallyUpdated ()
		{
			Tree1.ConvertToManuallyUpdated();
			JackInstance.ConvertToManuallyUpdated();
			TextManager.ConvertToManuallyUpdated(TreesFelled);
			AxeInstance.ConvertToManuallyUpdated();
			for (int i = 0; i < TreeList.Count; i++)
			{
				TreeList[i].ConvertToManuallyUpdated();
			}
			TreeSpawnerInstance.ConvertToManuallyUpdated();
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
			LumberJack.Entities.Tree.LoadStaticContent(contentManagerName);
			LumberJack.Entities.Jack.LoadStaticContent(contentManagerName);
			LumberJack.Entities.Axe.LoadStaticContent(contentManagerName);
			LumberJack.Entities.TreeSpawner.LoadStaticContent(contentManagerName);
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

#if ANDROID
#define REQUIRES_PRIMARY_THREAD_LOADING
#endif

using BitmapFont = FlatRedBall.Graphics.BitmapFont;
using Cursor = FlatRedBall.Gui.Cursor;
using GuiManager = FlatRedBall.Gui.GuiManager;
// Generated Usings
using LumberJack.Screens;
using FlatRedBall.Graphics;
using FlatRedBall.Math;
using LumberJack.Performance;
using LumberJack.Entities;
using LumberJack.Factories;
using FlatRedBall;
using FlatRedBall.Screens;
using System;
using System.Collections.Generic;
using System.Text;
using FlatRedBall.Math.Geometry;

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
#endif

#if FRB_XNA && !MONODROID
using Model = Microsoft.Xna.Framework.Graphics.Model;
#endif

namespace LumberJack.Entities
{
	public partial class Tree : PositionedObject, IDestroyable, IPoolable, FlatRedBall.Math.Geometry.ICollidable
	{
        // This is made global so that static lazy-loaded content can access it.
        public static string ContentManagerName
        {
            get;
            set;
        }

		// Generated Fields
		#if DEBUG
		static bool HasBeenLoadedWithGlobalContentManager = false;
		#endif
		static object mLockObject = new object();
		static List<string> mRegisteredUnloads = new List<string>();
		static List<string> LoadedContentManagers = new List<string>();
		
		private FlatRedBall.Math.Geometry.Circle mCircleInstance;
		public FlatRedBall.Math.Geometry.Circle CircleInstance
		{
			get
			{
				return mCircleInstance;
			}
			private set
			{
				mCircleInstance = value;
			}
		}
		static float CircleInstanceXReset;
		static float CircleInstanceYReset;
		static float CircleInstanceZReset;
		static float CircleInstanceXVelocityReset;
		static float CircleInstanceYVelocityReset;
		static float CircleInstanceZVelocityReset;
		static float CircleInstanceRotationXReset;
		static float CircleInstanceRotationYReset;
		static float CircleInstanceRotationZReset;
		static float CircleInstanceRotationXVelocityReset;
		static float CircleInstanceRotationYVelocityReset;
		static float CircleInstanceRotationZVelocityReset;
		public int Chops = 0;
		public float Size
		{
			get
			{
				return CircleInstance.Radius;
			}
			set
			{
				CircleInstance.Radius = value;
			}
		}
		public int MaxChops = 5;
		public int Index { get; set; }
		public bool Used { get; set; }
		private FlatRedBall.Math.Geometry.ShapeCollection mGeneratedCollision;
		public FlatRedBall.Math.Geometry.ShapeCollection Collision
		{
			get
			{
				return mGeneratedCollision;
			}
		}
		protected Layer LayerProvidedByContainer = null;

        public Tree()
            : this(FlatRedBall.Screens.ScreenManager.CurrentScreen.ContentManagerName, true)
        {

        }

        public Tree(string contentManagerName) :
            this(contentManagerName, true)
        {
        }


        public Tree(string contentManagerName, bool addToManagers) :
			base()
		{
			// Don't delete this:
            ContentManagerName = contentManagerName;
            InitializeEntity(addToManagers);

		}

		protected virtual void InitializeEntity(bool addToManagers)
		{
			// Generated Initialize
			LoadStaticContent(ContentManagerName);
			mCircleInstance = new FlatRedBall.Math.Geometry.Circle();
			mCircleInstance.Name = "mCircleInstance";
			mGeneratedCollision = new FlatRedBall.Math.Geometry.ShapeCollection();
			mGeneratedCollision.Circles.AddOneWay(mCircleInstance);
			
			PostInitialize();
			if (CircleInstance.Parent == null)
			{
				CircleInstanceXReset = CircleInstance.X;
			}
			else
			{
				CircleInstanceXReset = CircleInstance.RelativeX;
			}
			if (CircleInstance.Parent == null)
			{
				CircleInstanceYReset = CircleInstance.Y;
			}
			else
			{
				CircleInstanceYReset = CircleInstance.RelativeY;
			}
			if (CircleInstance.Parent == null)
			{
				CircleInstanceZReset = CircleInstance.Z;
			}
			else
			{
				CircleInstanceZReset = CircleInstance.RelativeZ;
			}
			if (CircleInstance.Parent == null)
			{
				CircleInstanceXVelocityReset = CircleInstance.XVelocity;
			}
			else
			{
				CircleInstanceXVelocityReset = CircleInstance.RelativeXVelocity;
			}
			if (CircleInstance.Parent == null)
			{
				CircleInstanceYVelocityReset = CircleInstance.YVelocity;
			}
			else
			{
				CircleInstanceYVelocityReset = CircleInstance.RelativeYVelocity;
			}
			if (CircleInstance.Parent == null)
			{
				CircleInstanceZVelocityReset = CircleInstance.ZVelocity;
			}
			else
			{
				CircleInstanceZVelocityReset = CircleInstance.RelativeZVelocity;
			}
			if (CircleInstance.Parent == null)
			{
				CircleInstanceRotationXReset = CircleInstance.RotationX;
			}
			else
			{
				CircleInstanceRotationXReset = CircleInstance.RelativeRotationX;
			}
			if (CircleInstance.Parent == null)
			{
				CircleInstanceRotationYReset = CircleInstance.RotationY;
			}
			else
			{
				CircleInstanceRotationYReset = CircleInstance.RelativeRotationY;
			}
			if (CircleInstance.Parent == null)
			{
				CircleInstanceRotationZReset = CircleInstance.RotationZ;
			}
			else
			{
				CircleInstanceRotationZReset = CircleInstance.RelativeRotationZ;
			}
			if (CircleInstance.Parent == null)
			{
				CircleInstanceRotationXVelocityReset = CircleInstance.RotationXVelocity;
			}
			else
			{
				CircleInstanceRotationXVelocityReset = CircleInstance.RelativeRotationXVelocity;
			}
			if (CircleInstance.Parent == null)
			{
				CircleInstanceRotationYVelocityReset = CircleInstance.RotationYVelocity;
			}
			else
			{
				CircleInstanceRotationYVelocityReset = CircleInstance.RelativeRotationYVelocity;
			}
			if (CircleInstance.Parent == null)
			{
				CircleInstanceRotationZVelocityReset = CircleInstance.RotationZVelocity;
			}
			else
			{
				CircleInstanceRotationZVelocityReset = CircleInstance.RelativeRotationZVelocity;
			}
			if (addToManagers)
			{
				AddToManagers(null);
			}


		}

// Generated AddToManagers
		public virtual void ReAddToManagers (Layer layerToAddTo)
		{
			LayerProvidedByContainer = layerToAddTo;
			SpriteManager.AddPositionedObject(this);
			ShapeManager.AddToLayer(mCircleInstance, LayerProvidedByContainer);
		}
		public virtual void AddToManagers (Layer layerToAddTo)
		{
			PostInitialize();
			LayerProvidedByContainer = layerToAddTo;
			SpriteManager.AddPositionedObject(this);
			ShapeManager.AddToLayer(mCircleInstance, LayerProvidedByContainer);
			AddToManagersBottomUp(layerToAddTo);
			CustomInitialize();
		}

		public virtual void Activity()
		{
			// Generated Activity
			
			CustomActivity();
			
			// After Custom Activity
		}

		public virtual void Destroy()
		{
			// Generated Destroy
			SpriteManager.RemovePositionedObject(this);
			if (Used)
			{
				TreeFactory.MakeUnused(this, false);
			}
			
			if (CircleInstance != null)
			{
				ShapeManager.RemoveOneWay(CircleInstance);
			}


			CustomDestroy();
		}

		// Generated Methods
		public virtual void PostInitialize ()
		{
			bool oldShapeManagerSuppressAdd = FlatRedBall.Math.Geometry.ShapeManager.SuppressAddingOnVisibilityTrue;
			FlatRedBall.Math.Geometry.ShapeManager.SuppressAddingOnVisibilityTrue = true;
			if (mCircleInstance.Parent == null)
			{
				mCircleInstance.CopyAbsoluteToRelative();
				mCircleInstance.AttachTo(this, false);
			}
			CircleInstance.Radius = 8f;
			CircleInstance.Color = Color.Green;
			FlatRedBall.Math.Geometry.ShapeManager.SuppressAddingOnVisibilityTrue = oldShapeManagerSuppressAdd;
		}
		public virtual void AddToManagersBottomUp (Layer layerToAddTo)
		{
			AssignCustomVariables(false);
		}
		public virtual void RemoveFromManagers ()
		{
			SpriteManager.ConvertToManuallyUpdated(this);
			if (CircleInstance != null)
			{
				ShapeManager.RemoveOneWay(CircleInstance);
			}
		}
		public virtual void AssignCustomVariables (bool callOnContainedElements)
		{
			if (callOnContainedElements)
			{
			}
			mCircleInstance.Radius = 8f;
			mCircleInstance.Color = Color.Green;
			if (CircleInstance.Parent == null)
			{
				CircleInstance.X = CircleInstanceXReset;
			}
			else
			{
				CircleInstance.RelativeX = CircleInstanceXReset;
			}
			if (CircleInstance.Parent == null)
			{
				CircleInstance.Y = CircleInstanceYReset;
			}
			else
			{
				CircleInstance.RelativeY = CircleInstanceYReset;
			}
			if (CircleInstance.Parent == null)
			{
				CircleInstance.Z = CircleInstanceZReset;
			}
			else
			{
				CircleInstance.RelativeZ = CircleInstanceZReset;
			}
			if (CircleInstance.Parent == null)
			{
				CircleInstance.XVelocity = CircleInstanceXVelocityReset;
			}
			else
			{
				CircleInstance.RelativeXVelocity = CircleInstanceXVelocityReset;
			}
			if (CircleInstance.Parent == null)
			{
				CircleInstance.YVelocity = CircleInstanceYVelocityReset;
			}
			else
			{
				CircleInstance.RelativeYVelocity = CircleInstanceYVelocityReset;
			}
			if (CircleInstance.Parent == null)
			{
				CircleInstance.ZVelocity = CircleInstanceZVelocityReset;
			}
			else
			{
				CircleInstance.RelativeZVelocity = CircleInstanceZVelocityReset;
			}
			if (CircleInstance.Parent == null)
			{
				CircleInstance.RotationX = CircleInstanceRotationXReset;
			}
			else
			{
				CircleInstance.RelativeRotationX = CircleInstanceRotationXReset;
			}
			if (CircleInstance.Parent == null)
			{
				CircleInstance.RotationY = CircleInstanceRotationYReset;
			}
			else
			{
				CircleInstance.RelativeRotationY = CircleInstanceRotationYReset;
			}
			if (CircleInstance.Parent == null)
			{
				CircleInstance.RotationZ = CircleInstanceRotationZReset;
			}
			else
			{
				CircleInstance.RelativeRotationZ = CircleInstanceRotationZReset;
			}
			if (CircleInstance.Parent == null)
			{
				CircleInstance.RotationXVelocity = CircleInstanceRotationXVelocityReset;
			}
			else
			{
				CircleInstance.RelativeRotationXVelocity = CircleInstanceRotationXVelocityReset;
			}
			if (CircleInstance.Parent == null)
			{
				CircleInstance.RotationYVelocity = CircleInstanceRotationYVelocityReset;
			}
			else
			{
				CircleInstance.RelativeRotationYVelocity = CircleInstanceRotationYVelocityReset;
			}
			if (CircleInstance.Parent == null)
			{
				CircleInstance.RotationZVelocity = CircleInstanceRotationZVelocityReset;
			}
			else
			{
				CircleInstance.RelativeRotationZVelocity = CircleInstanceRotationZVelocityReset;
			}
			Chops = 0;
			Size = 0f;
			MaxChops = 5;
		}
		public virtual void ConvertToManuallyUpdated ()
		{
			this.ForceUpdateDependenciesDeep();
			SpriteManager.ConvertToManuallyUpdated(this);
		}
		public static void LoadStaticContent (string contentManagerName)
		{
			if (string.IsNullOrEmpty(contentManagerName))
			{
				throw new ArgumentException("contentManagerName cannot be empty or null");
			}
			ContentManagerName = contentManagerName;
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
			bool registerUnload = false;
			if (LoadedContentManagers.Contains(contentManagerName) == false)
			{
				LoadedContentManagers.Add(contentManagerName);
				lock (mLockObject)
				{
					if (!mRegisteredUnloads.Contains(ContentManagerName) && ContentManagerName != FlatRedBallServices.GlobalContentManager)
					{
						FlatRedBallServices.GetContentManagerByName(ContentManagerName).AddUnloadMethod("TreeStaticUnload", UnloadStaticContent);
						mRegisteredUnloads.Add(ContentManagerName);
					}
				}
			}
			if (registerUnload && ContentManagerName != FlatRedBallServices.GlobalContentManager)
			{
				lock (mLockObject)
				{
					if (!mRegisteredUnloads.Contains(ContentManagerName) && ContentManagerName != FlatRedBallServices.GlobalContentManager)
					{
						FlatRedBallServices.GetContentManagerByName(ContentManagerName).AddUnloadMethod("TreeStaticUnload", UnloadStaticContent);
						mRegisteredUnloads.Add(ContentManagerName);
					}
				}
			}
			CustomLoadStaticContent(contentManagerName);
		}
		public static void UnloadStaticContent ()
		{
			if (LoadedContentManagers.Count != 0)
			{
				LoadedContentManagers.RemoveAt(0);
				mRegisteredUnloads.RemoveAt(0);
			}
			if (LoadedContentManagers.Count == 0)
			{
			}
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
		protected bool mIsPaused;
		public override void Pause (FlatRedBall.Instructions.InstructionList instructions)
		{
			base.Pause(instructions);
			mIsPaused = true;
		}
		public virtual void SetToIgnorePausing ()
		{
			FlatRedBall.Instructions.InstructionManager.IgnorePausingFor(this);
			FlatRedBall.Instructions.InstructionManager.IgnorePausingFor(CircleInstance);
		}
		public virtual void MoveToLayer (Layer layerToMoveTo)
		{
			LayerProvidedByContainer = layerToMoveTo;
		}

    }
	
	
	// Extra classes
	
}

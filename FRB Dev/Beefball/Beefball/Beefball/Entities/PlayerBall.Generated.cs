using System;
using System.Collections.Generic;
using System.Text;
using FlatRedBall.AI.Pathfinding;
using FlatRedBall.Graphics.Model;

using FlatRedBall.Input;
using FlatRedBall.Utilities;

using FlatRedBall.Instructions;
using FlatRedBall.Math.Splines;
using BitmapFont = FlatRedBall.Graphics.BitmapFont;
using Cursor = FlatRedBall.Gui.Cursor;
using GuiManager = FlatRedBall.Gui.GuiManager;
// Generated Usings
using Beefball.Screens;
using Matrix = Microsoft.Xna.Framework.Matrix;
using FlatRedBall.Broadcasting;
using Beefball.Entities;
using FlatRedBall;
using FlatRedBall.Graphics;
using FlatRedBall.Math;
using FlatRedBall.Math.Geometry;

#if XNA4
using Color = Microsoft.Xna.Framework.Color;
#else
using Color = Microsoft.Xna.Framework.Graphics.Color;
#endif

#if FRB_XNA || SILVERLIGHT
using Keys = Microsoft.Xna.Framework.Input.Keys;
using Vector3 = Microsoft.Xna.Framework.Vector3;
using Texture2D = Microsoft.Xna.Framework.Graphics.Texture2D;
#endif

#if FRB_XNA
using Model = Microsoft.Xna.Framework.Graphics.Model;
#endif

namespace Beefball.Entities
{
	public partial class PlayerBall : PositionedObject, IDestroyable
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
		public enum VariableState
		{
			Uninitialized, // This exists so that the first set call actually does something
			Tired,
			Rested
		}
		static object mLockObject = new object();
		static bool mHasRegisteredUnload = false;
		static bool IsStaticContentLoaded = false;

		private Circle mBody;
		public Circle Body
		{
			get{ return mBody;}
		}
		private Circle CooldownCircle;
		public float MovementSpeed = 33f;
		public float DashSpeed = 30f;
		public float DashFrequency = 2f;
		public float CooldownCircleRadius
		{
			get
			{
				return CooldownCircle.Radius;
			}
			set
			{
				CooldownCircle.Radius = value;
			}
		}
		public Color CooldownCircleColor
		{
			get
			{
				return CooldownCircle.Color;
			}
			set
			{
				CooldownCircle.Color = value;
			}
		}
		protected Layer LayerProvidedByContainer = null;

        public PlayerBall(string contentManagerName) :
            this(contentManagerName, true)
        {
        }


        public PlayerBall(string contentManagerName, bool addToManagers) :
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
			mBody = new Circle();
			CooldownCircle = new Circle();


			PostInitialize();
			if(addToManagers)
			{
				AddToManagers(null);
			}


		}

// Generated AddToManagers

        public virtual void AddToManagers(Layer layerToAddTo)
        {
			LayerProvidedByContainer = layerToAddTo;
			SpriteManager.AddPositionedObject(this);
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
			if(Body != null)
			{
				ShapeManager.Remove(Body);
			}
			if(CooldownCircle != null)
			{
				ShapeManager.Remove(CooldownCircle);
			}



			CustomDestroy();
		}

		// Generated Methods
		public virtual void PostInitialize()
		{
			MovementSpeed = 33f;
			Drag = 1f;
			X = 0f;
			Y = 0f;
			DashSpeed = 30f;
			DashFrequency = 2f;
			CooldownCircleRadius = 0f;
			CooldownCircleColor = Color.Blue;
		}
		public virtual void AddToManagersBottomUp(Layer layerToAddTo)
		{


            // We move this back to the origin and unrotate it so that anything attached to it can just use its absolute position
            float oldRotationX = RotationX;
            float oldRotationY = RotationY;
            float oldRotationZ = RotationZ;

            float oldX = X;
            float oldY = Y;
            float oldZ = Z;

            X = 0;
            Y = 0;
            Z = 0;
            RotationX = 0;
            RotationY = 0;
            RotationZ = 0;
			ShapeManager.AddToLayer(mBody, layerToAddTo);
			if(mBody.Parent == null)
			{
				mBody.AttachTo(this, true);
			}
			ShapeManager.AddToLayer(CooldownCircle, layerToAddTo);
			if(CooldownCircle.Parent == null)
			{
				CooldownCircle.AttachTo(this, true);
			}

            X = oldX;
            Y = oldY;
            Z = oldZ;
            RotationX = oldRotationX;
            RotationY = oldRotationY;
            RotationZ = oldRotationZ;
                		}
		public virtual void ConvertToManuallyUpdated()
		{
			this.ForceUpdateDependenciesDeep();
			SpriteManager.ConvertToManuallyUpdated(this);
		}
		public static void LoadStaticContent(string contentManagerName)
		{
			ContentManagerName = contentManagerName;
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
			if(IsStaticContentLoaded == false)
			{
				IsStaticContentLoaded = true;
				lock(mLockObject)
				{
					if(!mHasRegisteredUnload && ContentManagerName != FlatRedBallServices.GlobalContentManager)
					{
						FlatRedBallServices.GetContentManagerByName(ContentManagerName).AddUnloadMethod("PlayerBallStaticUnload", UnloadStaticContent);
						mHasRegisteredUnload = true;
					}
				}
				bool registerUnload = false;
			if(registerUnload && ContentManagerName != FlatRedBallServices.GlobalContentManager)
			{
				lock(mLockObject)
				{
					if(!mHasRegisteredUnload && ContentManagerName != FlatRedBallServices.GlobalContentManager)
					{
						FlatRedBallServices.GetContentManagerByName(ContentManagerName).AddUnloadMethod("PlayerBallStaticUnload", UnloadStaticContent);
						mHasRegisteredUnload = true;
					}
				}
			}
				CustomLoadStaticContent(contentManagerName);
			}
		}
		public static void UnloadStaticContent()
		{
			IsStaticContentLoaded = false;
			mHasRegisteredUnload = false;
		}
		static VariableState mLoadingState = VariableState.Uninitialized;
		public static VariableState LoadingState
		{
			get
			{
				return mLoadingState;
			}
			set
			{
				mLoadingState = value;
			}
		}
		VariableState mCurrentState = VariableState.Uninitialized;
		public VariableState CurrentState
		{
			get
			{
				return mCurrentState;
			}
			set
			{
				mCurrentState = value;
				switch(mCurrentState)
				{
					case VariableState.Tired:
						CooldownCircleRadius = 0f;
						break;
					case VariableState.Rested:
						CooldownCircleRadius = 1f;
						break;
				}
			}
		}
		public void InterpolateToState(VariableState stateToInterpolateTo, double secondsToTake)
		{
			switch(stateToInterpolateTo)
			{
				case VariableState.Tired:
						CooldownCircle.RadiusVelocity = (0f - CooldownCircle.Radius) / (float)secondsToTake;
						break;
				case VariableState.Rested:
						CooldownCircle.RadiusVelocity = (1f - CooldownCircle.Radius) / (float)secondsToTake;
						break;
			}
			this.Instructions.Add(new MethodInstruction<PlayerBall>(
				this, "StopStateInterpolation", new object[]{stateToInterpolateTo}, TimeManager.CurrentTime + secondsToTake));
		}

		public void StopStateInterpolation(VariableState stateToStop)
		{
			switch(stateToStop)
			{
				case VariableState.Tired:
						CooldownCircle.RadiusVelocity =  0;
						break;
				case VariableState.Rested:
						CooldownCircle.RadiusVelocity =  0;
						break;
			}
			CurrentState = stateToStop;
		}

		object GetMember(string memberName)
		{
			return null;
		}

    }
	
	
	// Extra classes
	public static class PlayerBallExtensionMethods
	{
	}
	
}

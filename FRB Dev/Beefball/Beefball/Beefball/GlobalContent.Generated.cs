using System.Collections.Generic;
using System.Threading;
using FlatRedBall;
using FlatRedBall.Math.Geometry;
using FlatRedBall.ManagedSpriteGroups;
using FlatRedBall.Graphics.Animation;
using FlatRedBall.Graphics.Particle;
using FlatRedBall.AI.Pathfinding;
using FlatRedBall.Utilities;
using Texture2D = Microsoft.Xna.Framework.Graphics.Texture2D;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Audio;
using BitmapFont = FlatRedBall.Graphics.BitmapFont;
using FlatRedBall.Localization;

namespace Beefball
{
	public static class GlobalContent
	{
		
		public static bool IsInitialized
		{
			get;
			private set;
		}
		static string ContentManagerName = "Global";
		public static void Initialize()
		{

			IsInitialized = true;
		}



	}
}

package level
{
	import flash.display.DisplayObjectContainer;
	
	public class PoongaCamera extends Camera2D
	{
		private var maxX:int ;
		private var wagons:int;
		public function PoongaCamera(canvas:DisplayObjectContainer, displayWidth:Number, displayHeight:Number, wagons:int)
		{
			this.wagons = wagons;
			this.maxX = displayWidth;
			super(canvas, displayWidth, displayHeight)
		}
		
		protected override function doFolow() : void
		{
			if (target && running)
			{
				x = target.x;
				y = target.y;
				if( x >1600 * wagons + ((740/2)*(wagons-1)))
					x = 1600 * wagons + ((740/2)*(wagons-1));
			}
		}

	}
}
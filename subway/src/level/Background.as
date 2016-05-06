     package level
{
	import flash.display.MovieClip;
	import flash.geom.Point;
	
	public class Background extends MovieClip
	{
		public var manijas:Manijas;
		private var wagonWidth:int;
		
		public function Background(levelNumber:int, qty:int)
		{			
			
			for(var a:int = 0; a<qty; a++)
			{		
				var asset:MovieClip = new background1MC();
				if(levelNumber>4)
					asset.gotoAndStop(3);
				else if(levelNumber>2)
					asset.gotoAndStop(2);
				
				wagonWidth = asset.width;
				this.manijas = new Manijas(asset.manijas);
				asset.x = this.width
				addChild(asset);	
					
			}	
					
		}
		public function getManijas():Array
		{
			var i:int = this.numChildren;
			var arr:Array = []
			while(i--)
			{
				var mc:MovieClip = MovieClip(this.getChildAt(i) );
				var a:int = mc.manijas.numChildren;
				while (a--)
				{
					var point:Point = new Point();
					point.x = mc.manijas.getChildAt(a).x + (wagonWidth*i);
					point.y = mc.manijas.getChildAt(a).y;
					arr.push( point );
				}
			}
			return arr;
		}

	}
}
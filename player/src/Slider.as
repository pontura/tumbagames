package
{
	import flash.display.MovieClip;

	public class Slider extends MovieClip
	{
		public var _width:int;
		public var activeID:int = 3;
		
		public function Slider(games:Array)
		{
			for each(var obj:Object in games)
			{
				var gb:GameButton = new GameButton(this, obj);					
				gb.x = width;
				addChild(gb);
				_width+=gb.width;
			}
			x = -650;
		}
		private var canMove:Boolean = true;
		
		public function go(left:Boolean):void
		{
			if(!canMove) return;
			
			if(left) 
				activeID--;
			else 
				activeID++;
			
			if(activeID==0) activeID=8;
			else if (activeID==9) activeID = 1;
			
			Main.I.board.hideTexts();
			canMove = false;
			var i:int = numChildren;
			while(i--)
			{
				var gb:GameButton = GameButton(getChildAt(i));
				gb.go(left);
			}
		}
		public function availableCick():void
		{
			Main.I.board.showTexts(activeID);
			canMove = true;
		}
	}
}
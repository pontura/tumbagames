package
{
	import flash.display.MovieClip;

	public class ProgressBar extends MovieClip
	{
		private var target:MovieClip;
		private var initialX:int
		public var state:String = "A";
		
		public function ProgressBar(target:MovieClip)
		{
			this.target = target;
			this.initialX= target.x
		}
		public function up(qty:Number):void
		{
			target.x += qty*25;
			if(target.x>initialX) target.x = initialX;
		}
		public function down(qty:Number):void
		{
			
			target.x -= qty*25;
			if(target.x<-target.width+initialX) 
			{
				Main.I.gameOver();
				target.x = initialX;
			}
			if(target.x<-target.width+initialX+(target.width/2))
				state = "B";
		}
	}
}
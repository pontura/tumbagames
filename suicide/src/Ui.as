package
{
	import flash.display.MovieClip;
	
	import flashlib.easing.Tween;
	import flashlib.tasks.Func;
	import flashlib.tasks.Loop;
	import flashlib.tasks.Sequence;
	import flashlib.tasks.Wait;

	public class Ui extends UiMC
	{
		public var lifeBar:ProgressBar
		//private var hateBar:ProgressBar
		public var points:int = 0;
		
		public function Ui()
		{
			this.points_txt.text = "0000";
			this.lifeBar = new ProgressBar(this.progressLife);
			//this.hateBar = new ProgressBar(this.progressHate);
			/*var task:Loop = new Loop(
					new Sequence (
						new Wait(1000),
						new Func(loseHate, 0.1)
					)
				)
			Main.I.tasks.add( task );*/
		}
		public function loseLife(qty:Number):void
		{
			lifeBar.down(qty);
		}
		public function winLife(qty:Number):void
		{
			lifeBar.up(qty);
		}
		/*public function loseHate(qty:Number):void
		{
			hateBar.down(qty);	
		}
		public function winHate(qty:Number):void
		{
			hateBar.up(qty);
		}*/
		public function addScore(qty:int = 10):void
		{
			points += qty;
			var pointsText:String = String(points); 
			if(pointsText.length<3)  this.points_txt.text = "0" + points;
			else if (pointsText.length<2)  this.points_txt.text = "00" + points;
			else this.points_txt.text = pointsText;
		}
	}
}
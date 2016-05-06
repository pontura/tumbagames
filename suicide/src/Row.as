package
{
	import flash.display.MovieClip;
	
	import flashlib.easing.Tween;
	import flashlib.tasks.Func;
	import flashlib.tasks.Sequence;
	import flashlib.tasks.Task;

	public class Row extends MovieClip
	{
		
		public function Row()
		{
		}
		public function addGuy(id:int = 1, speed:int = 3000):void
		{
			speed = (Math.random()*1500) + 1500
			var character:Character = new Character(id, speed);
			addChild(character);
			setChildIndex(character, 0);
		}
	}
}
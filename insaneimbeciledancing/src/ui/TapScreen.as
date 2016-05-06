package ui
{
	
	import flash.events.MouseEvent;
	import flash.utils.setTimeout;
	
	import flashlib.tasks.Func;
	import flashlib.tasks.Sequence;
	import flashlib.tasks.Wait;

	public class TapScreen extends TapMC
	{		
		public function TapScreen()
		{			
			hide();
		}
		public function show():void
		{
			this.visible = true;
			Game.I.tasks.add(
				new Sequence(
					new Wait(800),
					new Func(hide)	
					)
				)
		}
		public function hide():void
		{
			this.visible = false;
		}
		public function reset():void
		{
			setTimeout(hide, 200);
		}
	}
}
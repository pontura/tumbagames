package
{
	import flash.display.MovieClip;
	import flash.events.KeyboardEvent;
	import flash.utils.setTimeout;
	
	import flashlib.utils.DisplayUtil;
	
	import globals.audio;

	public class StagePresentation extends MovieClip
	{
		private var level:int;
		public function StagePresentation(level:int)
		{
			audio.stop("MainMenu");
			this.level = level;
			var mc:MovieClip;
			if(level == 1) mc = new Stage1MC;
			if(level == 2) mc = new Stage2MC;
			if(level == 3) mc = new Stage3MC;
			if(level == 4) mc = new Stage4MC;
			
			addChild(mc);
			
			setTimeout(reset, 4000);
			audio.play("MainMenuBtn");
		}
		private function reset():void
		{
			audio.stop("MainMenuBtn");
			Main.I.start(level);
			DisplayUtil.dispose( this );
		}
	}
}
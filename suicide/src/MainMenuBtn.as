package
{
	import flash.events.KeyboardEvent;
	import flash.utils.setTimeout;
	
	import flashlib.utils.DisplayUtil;
	
	import globals.audio;

	public class MainMenuBtn extends Stage1MC
	{
		public function MainMenuBtn()
		{
			setTimeout(reset, 4000);
			audio.play("MainMenuBtn");
		}
		private function reset():void
		{
			audio.stop("MainMenuBtn");
			Main.I.start();
			DisplayUtil.dispose( this );
		}
	}
}
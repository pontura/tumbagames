package
{
	import flash.events.KeyboardEvent;
	
	import flashlib.utils.DisplayUtil;
	
	import globals.audio;

	public class MainMenu extends MainMenuMC
	{
		public function MainMenu()
		{
			Main.I.stage.addEventListener(KeyboardEvent.KEY_DOWN, keyDown);
			audio.loop("MainMenu");
		}
		private function keyDown(e:KeyboardEvent):void
		{
			
			Main.I.stage.removeEventListener(KeyboardEvent.KEY_DOWN, keyDown);
			Main.I.addChild( new StageSelector );
			DisplayUtil.dispose( this );
		}
	}
}
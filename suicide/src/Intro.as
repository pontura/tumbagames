package
{
	import flash.events.KeyboardEvent;
	import flash.events.MouseEvent;
	
	import flashlib.utils.DisplayUtil;
	
	import globals.audio;

	public class Intro extends IntroCaretaMC
	{
		public function Intro()
		{
			Main.I.stage.addEventListener(KeyboardEvent.KEY_DOWN, keyDown);
			Main.I.stage.addEventListener(MouseEvent.MOUSE_DOWN, keyDown);
			audio.play("socolinsky");
		}
		private function keyDown(e:*):void
		{
			audio.stop("socolinsky");
			Main.I.stage.removeEventListener(KeyboardEvent.KEY_DOWN, keyDown);
			Main.I.stage.removeEventListener(MouseEvent.MOUSE_DOWN, keyDown);
			Main.I.addChild( new MainMenu );
			DisplayUtil.dispose( this );
		}
	}
}
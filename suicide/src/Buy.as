package
{
	import flash.events.KeyboardEvent;
	import flash.events.MouseEvent;
	
	import flashlib.utils.DisplayUtil;
	
	import globals.audio;

	public class Buy  extends BuyScreenMC
	{
		public function Buy()
		{
			Main.I.restart();
			Main.I.stage.addEventListener(KeyboardEvent.KEY_DOWN, keyDown);
			Main.I.stage.addEventListener(MouseEvent.MOUSE_DOWN, keyDown);
			audio.loop("MainMenu");
			
		}
		private function keyDown(e:*):void
		{
			audio.stop("Siren-AirRaid");
			Main.I.stage.removeEventListener(KeyboardEvent.KEY_DOWN, keyDown);
			Main.I.stage.removeEventListener(MouseEvent.MOUSE_DOWN, keyDown);
			Main.I.addChild( new Intro );
			DisplayUtil.dispose( this );
		}
	}
}
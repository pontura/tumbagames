package
{
	import flash.events.KeyboardEvent;
	import flash.events.MouseEvent;
	
	import flashlib.utils.DisplayUtil;
	
	import globals.audio;

	public class MensajeHigienico extends MensajeHigienicoMC
	{
		public function MensajeHigienico()
		{
			Main.I.stage.addEventListener(KeyboardEvent.KEY_DOWN, keyDown);
			Main.I.stage.addEventListener(MouseEvent.MOUSE_DOWN, keyDown);
			audio.play("mensaje");
		}
		private function keyDown(e:*):void
		{
			audio.stop("mensaje");
			Main.I.stage.removeEventListener(KeyboardEvent.KEY_DOWN, keyDown);
			Main.I.stage.removeEventListener(MouseEvent.MOUSE_DOWN, keyDown);
			Main.I.addChild( new Achtung );
			DisplayUtil.dispose( this );
		}
	}
}
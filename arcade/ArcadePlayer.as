package {
	import flash.display.MovieClip;
	import flash.events.KeyboardEvent;
	import flash.events.MouseEvent;
	import flash.external.ExternalInterface;
	import flash.net.URLRequest;
	import flash.net.navigateToURL;
	import flash.system.Security;
	import flash.system.fscommand;
	import flash.ui.Mouse;
	import flash.utils.setTimeout;
	
	public class ArcadePlayer extends MovieClip
	{
		public function ArcadePlayer()
		{	
			fscommand("trapallkeys", "True");
			fscommand("fullscreen", "true");
			Mouse.hide();
			Security.allowDomain("*");
			if(ExternalInterface.available) {
				ExternalInterface.call("flashLoaded");
			}
			stage.addEventListener(KeyboardEvent.KEY_DOWN, keyDowns);
		}
		private function keyDowns(e:KeyboardEvent):void{
			if(exiting) return;
			trace(e.keyCode);
			switch (e.keyCode)
			{
				//ESC
				case 49:
					exit();
					break;
				
				//salis del Fullscreen con la tecla "¡ / ¿"
				case 221:
					fscommand("fullscreen", "false");
					break;
			}
		}
		private var exiting:Boolean;
		public function exit():void
		{
			if(exiting) return;
			exiting = true;
			visible = false;
			fscommand("exec", "restart.bat");
			setTimeout(realExit, 3000);
		}
		public function realExit():void
		{
			fscommand("quit", "true");
			//navigateToURL( new URLRequest("../index.html"), "_self");
		}
		
	}
}

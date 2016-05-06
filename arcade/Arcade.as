package {
	import flash.display.MovieClip;
	import flash.events.KeyboardEvent;
	import flash.events.MouseEvent;
	import flash.system.fscommand;
	import flash.utils.setTimeout;
	
	public class Arcade extends MovieClip
	{
		public function Arcade()
		{	
			//fscommand("exec",".\\ACQ\\Acq.exe");
			setTimeout(setEvents, 1000);
			fscommand("fullscreen", "true");
		}
		private function setEvents():void
		{
			stage.addEventListener(KeyboardEvent.KEY_DOWN, checkForEsc);
		}
		private function checkForEsc(e:KeyboardEvent):void{
			switch (e.keyCode)
			{
				//ESC
				case 27:
					fscommand("quit", "true")
					break;
				
				//salis del Fullscreen con la tecla "¡ / ¿"
				case 221:
					fscommand("fullscreen", "false");
					break;
			}
		}
		private function fullscreenOff(e:MouseEvent):void
		{
			fscommand("fullscreen", "false");
		}
	}
}

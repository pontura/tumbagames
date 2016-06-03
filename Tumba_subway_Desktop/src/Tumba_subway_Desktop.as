package
{
	import flash.display.Sprite;
	import flash.display.StageDisplayState;
	import flash.display.StageScaleMode;
	import flash.system.fscommand;
	import flash.ui.Mouse;
	
	[SWF(width='760', height = '460', backgroundColor = '0x000000', frameRate = '12')]
	public class Tumba_subway_Desktop extends Main
	{
		public function Tumba_subway_Desktop()
		{
			stage.displayState = StageDisplayState.FULL_SCREEN_INTERACTIVE;
			stage.scaleMode = StageScaleMode.SHOW_ALL;
			
			fscommand("fullscreen", "true");
			Mouse.hide();
			init();
		}
	}
}
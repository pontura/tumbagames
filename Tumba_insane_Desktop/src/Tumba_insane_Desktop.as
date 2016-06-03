package
{
	import flash.display.Sprite;
	import flash.display.StageDisplayState;
	import flash.display.StageScaleMode;
	import flash.system.fscommand;
	import flash.ui.Mouse;
	
	[SWF(width='1000', height='700', backgroundColor='0x000', frameRate='30')]	
	public class Tumba_insane_Desktop extends Game
	{
		public function Tumba_insane_Desktop()
		{
			stage.displayState = StageDisplayState.FULL_SCREEN_INTERACTIVE;
			stage.scaleMode = StageScaleMode.SHOW_ALL;
			
			fscommand("fullscreen", "true");
			Mouse.hide();
			Init();
		}
	}
}
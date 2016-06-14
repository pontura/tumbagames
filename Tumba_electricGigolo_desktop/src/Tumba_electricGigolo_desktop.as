package
{
	import flash.display.Sprite;
	import flash.display.StageDisplayState;
	import flash.display.StageScaleMode;
	import flash.system.fscommand;
	import flash.ui.Mouse;
	
	[SWF(width='640', height='480', backgroundColor='0x000000', frameRate='25')]
	public class Tumba_electricGigolo_desktop extends Game
	{
		public function Tumba_electricGigolo_desktop()
		{
			stage.displayState = StageDisplayState.FULL_SCREEN_INTERACTIVE;
			stage.scaleMode = StageScaleMode.SHOW_ALL;
			
			fscommand("fullscreen", "true");
			Mouse.hide();
			Init();
		}
	}
}
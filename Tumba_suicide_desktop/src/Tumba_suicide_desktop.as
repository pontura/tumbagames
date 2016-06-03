package
{
	import flash.desktop.NativeApplication;
	import flash.display.FocusDirection;
	import flash.display.NativeWindow;
	import flash.display.Sprite;
	import flash.display.Stage;
	import flash.display.StageDisplayState;
	import flash.display.StageScaleMode;
	import flash.events.Event;
	import flash.events.FullScreenEvent;
	import flash.events.KeyboardEvent;
	import flash.system.fscommand;
	import flash.ui.Mouse;
	import flash.utils.setInterval;
	
	[SWF(width='480', height='600', backgroundColor='0x0000', frameRate='20')]
	public class Tumba_suicide_desktop extends Main
	{
		public function Tumba_suicide_desktop()
		{
			stage.displayState = StageDisplayState.FULL_SCREEN_INTERACTIVE;
			stage.scaleMode = StageScaleMode.SHOW_ALL;
			
			fscommand("fullscreen", "true");
			Mouse.hide();
			Init();
		} 
	}
}
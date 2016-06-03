package
{
	import flash.events.Event;
	
	import flash.events.KeyboardEvent;/*
	import flash.events.GameInputEvent;
	import flash.ui.GameInput;
	import flash.ui.GameInputControl;
	import flash.ui.GameInputDevice;
	import flash.ui.GameInputFinger;*/

	public class KeyboardEvents
	{
		
		public function KeyboardEvents()
		{
			Init();	
		}
		public function Init():void
		{
			Main.I.stage.addEventListener(KeyboardEvent.KEY_DOWN, keyDown);
			
			/*
			if ( GameInput.isSupported && GameInput.numDevices > 0 )
			{
			trace("hay joystick");
			var gid:GameInputDevice = GameInput.getDeviceAt(0);
			var gic:GameInputControl = gid.getControlAt(0);
			gic.addEventListener(Event.CHANGE, buttonPressed);
			}	*/		
		}
		public function RemoveListener():void
		{
			Main.I.stage.removeEventListener(KeyboardEvent.KEY_DOWN, keyDown);
		}
		private function buttonPressed(e:Event):void{
			//trace(e.currentTarget);
		}
		private function keyDown(e:KeyboardEvent):void{
			if(Main.I.board)
			{
				Main.I.board.shot();
			}
		}
	}
}
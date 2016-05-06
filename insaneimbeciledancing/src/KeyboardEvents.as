package
{
	import flash.events.Event;
	import flash.events.KeyboardEvent;
	import flash.system.fscommand;

	public class KeyboardEvents
	{
		private var speed:Number = 1;
		public var _x:Number = 0;
		public var _y:Number = 0;
		private var left:Boolean;
		private var right:Boolean;
		private var up:Boolean;
		private var down:Boolean;
		
		public function KeyboardEvents()
		{
			Game.I.stage.addEventListener(KeyboardEvent.KEY_DOWN, keyDown);
			Game.I.stage.addEventListener(KeyboardEvent.KEY_UP, keyUp);
		}
		public function tick():void{
			
			if(left) {
				_x = -speed;
			} else if(right)  {
				_x = speed;
			} else {
				_x = 0;
			}
			if(up) {
				_y = -speed;
			} else if(down)  {
				_y = speed;
			} else {
				_y = 0;
			}
			if(Game.I.virtualView)
				Game.I.virtualView.keysStatus(_x, _y);
		}
		private function keyDown(e:KeyboardEvent):void{
			switch(e.keyCode)
			{
				case 37:	left = true; 	break;
				case 38: 	up = true; 	break;
				case 39: 	right = true; break;
				case 40: 	down = true; break;
				case 32: 	break;
			}
		}
		private function keyUp(e:KeyboardEvent):void{
			switch(e.keyCode)
			{
				case 37:	left = false; 	break;
				case 38: 	up = false; 	break;
				case 39: 	right = false; break;
				case 40:	 down = false; break;
				case 32: 	break;
			}
		}
		public function reset():void
		{
			Game.I.stage.removeEventListener(KeyboardEvent.KEY_DOWN, keyDown);
			Game.I.stage.removeEventListener(KeyboardEvent.KEY_UP, keyUp);
		}
	}
}
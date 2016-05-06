package brain
{
	import flash.events.KeyboardEvent;
	
	public class KeyboardPress
	{
		private var lastPressed			:int = 0;
		
		function KeyboardPress() {
			Main.I().stage.addEventListener(KeyboardEvent.KEY_DOWN,move, false, 0, true);
			Main.I().stage.addEventListener(KeyboardEvent.KEY_UP,keyUp, false, 0, true);
		}
		
		public function keyUp(keyEvent:KeyboardEvent):void {
			if (Main.I().board)
			switch (keyEvent.keyCode) {
				case 32 ://left arrow
		            Main.I().board.keyUp("space");
		            break;
		        case 37 ://left arrow
		            Main.I().board.keyUp("left");
		            break;
		        case 38 ://up arrow
		            Main.I().board.keyUp("up");
		            break;
		        case 39 ://right arrow
		            Main.I().board.keyUp("right");
		            break;
		        case 40 ://down arrow
		            Main.I().board.keyUp("down");
		            break;
		        default :
		            break;
		    }
		    lastPressed = 0;
		}
		public function move(keyEvent:KeyboardEvent):void {
			
			if (lastPressed == keyEvent.keyCode)
				return;
				
			lastPressed = keyEvent.keyCode;
			if (Main.I().board)
		    switch (keyEvent.keyCode) {
		    	case 16 ://left arrow
		            Main.I().board.keyDown("shift");
		            break;
		    	case 32 ://left arrow
		            Main.I().board.keyDown("space");
		            break;
		        case 37 ://left arrow
		            Main.I().board.keyDown("left");
		            break;
		        case 38 ://up arrow
		            Main.I().board.keyDown("up");
		            break;
		        case 39 ://right arrow
		            Main.I().board.keyDown("right");
		            break;
		        case 40 ://down arrow
		            Main.I().board.keyDown("down");
		            break;
		        default :
		            break;
		    }
		}
	}
	
}
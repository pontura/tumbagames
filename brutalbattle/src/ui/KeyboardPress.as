package ui
{
	import flash.events.KeyboardEvent;
	
	public class KeyboardPress
	{
		public var pressed :Array = [];
		private var lastPressed:String;
		
		function KeyboardPress() {
			Game.I.stage.addEventListener(KeyboardEvent.KEY_DOWN,keyDown, false, 0, true);
			Game.I.stage.addEventListener(KeyboardEvent.KEY_UP,keyUp, false, 0, true);
		}
		
		public function keyUp(keyEvent:KeyboardEvent):void {
			
			
			var remove:String;
			
			 switch (keyEvent.keyCode) {
			 	
			 	
			 	case 80 ://left arrow
		            remove = "p";
		            break;
		    	case 76 ://left arrow
		            remove = "l";
		            break;
		        case 79 ://left arrow
		            remove = "o";
		            Game.I.board.character2.idle(true)
		            break;
		    	case 75 ://left arrow
		            remove = "k";
		            Game.I.board.character2.idle(true)
		            break;
		            
		            
		            
		    	case 81 ://left arrow
		            remove = "q";
		            break;
		    	case 65 ://left arrow
		            remove = "a";
		            break;
		        case 87 ://left arrow
		            remove = "w";
		            Game.I.board.character1.idle(true)
		            break;
				case 32:
		    	case 83 ://left arrow
		            remove = "s";
		             Game.I.board.character1.idle(true)
		            break;
		        case 37 ://left arrow
		            remove = "left"
		            break;
		        case 38 ://up arrow
		            remove = "up"
		            break;
		        case 39 ://right arrow
		            remove = "right"
		            break;
		        case 40 ://down arrow
		            remove = "down"
		            break;
		    }
		    
		    for (var a:int = 0; a<pressed.length;a++)
			{
				if(pressed[a]==remove)
					pressed.splice(a,1);
			}
		}
		public function keyDown(keyEvent:KeyboardEvent):void {
			
			var add:String;
		    switch (keyEvent.keyCode) {
		    	
		    	case 80 ://left arrow
		            add = "p";
		            break;
		    	case 76 ://left arrow
		            add = "l";
		            break;
		        case 79 ://left arrow
		            add = "o";
		            break;
		    	case 75 ://left arrow
		            add = "k";
		            break;
		            
		            
		            
		    	case 81 ://left arrow
		            add = "q";
		            break;
		    	case 65 ://left arrow
		            add = "a";
		            break;
		        case 87 ://left arrow
		            add = "w";
		            break;
				case 32:
		    	case 83 ://left arrow
		            add = "s";
		            break;
		        case 37 ://left arrow
		            add = "left"
		            break;
		        case 38 ://up arrow
		            add = "up"
		            break;
		        case 39 ://right arrow
		            add = "right"
		            break;
		        case 40 ://down arrow
		            add = "down"
		            break;
		    }
		    for (var a:int = 0; a<pressed.length;a++)
			{
				if(pressed[a]==add)
					return;
			}
			if(add == "q" || add=="a" || add=="z")
			{
				pressed = [];
				pressed[0] = add;
				
				if(add == "q") Game.I.board.ai.checkDefense("up");
				if(add == "a") Game.I.board.ai.checkDefense("down");
			}else
			pressed.push(add);
		}
	}
	
}
package view
{
	import flash.display.MovieClip;
	import flash.events.Event;
	
	public class AI extends MovieClip
	{
		private var character:Character;
		private var state:String;
		private var sec:int = 0;
		private var secFinish:int;
		private var separation:int = 160;
		private var attackDelay:int = 24;
		private var defenseDelay:int = 24;
		
		public function AI(character:Character)
		{
			this.character = character;
			addEventListener(Event.ENTER_FRAME, enterFrame);
			separation = character.settings.separation;
			attackDelay = character.settings.attackDelay;
			defenseDelay = character.settings.defenseDelay;
		}
		private function resetActions():void
		{
			state = "";	
			character.idle(true);
		}
		private function enterFrame(e:Event):void
		{	
			sec++;
			if (secFinish < sec)
				resetActions();
			if(state =="following")	
			{
				followWalk(Game.I.board.character1)
			} else
			if(state =="defending")	
			{
				//trace("defending: " + sec + " de " + secFinish )
			}
			else
			if(attackIfPosible()){
			}else if(!isNear())
			if(Math.random()*100<20)
				follow()
		}
		private function attackIfPosible():Boolean
		{
			if(state == "attacking" || Math.random()*100<20) return false;
			var difY:int = Math.abs(character.y - Game.I.board.character1.y);
			var difX:int = Math.abs(character.x - Game.I.board.character1.x);
			
			if(difY<21&& difX<separation+10)
			{
				sec = 0;
				state = "attacking";
				secFinish = attackDelay;
				if(Math.random()*100<50)
					Game.I.board.character2.hitDown();
				else
					Game.I.board.character2.hitUp();
				return true;
			}
			return false;
		}
		public function checkDefense(type:String):void
		{
			trace("check " + type)
			if(Game.I.board.character1.settings.characterId != 3)
			{
				if(Math.abs(character.y - Game.I.board.character1.y)>20) return;
				if(Math.random()*100> Game.I.board.character2.settings.defense) return;
			}
			
			sec = 0;
			secFinish = defenseDelay;
			state = "defending";
			if(type == "up")
				Game.I.board.character2.defenseUp();
			else
				Game.I.board.character2.defenseDown();
		}
		private function follow():void
		{
			sec = 0;
			secFinish = Math.random()*50;
			state = "following";			
		}
		private function isNear():Boolean
		{
			if(Math.abs(character.x - Game.I.board.character1.x)>separation)
				return false
			else if(Math.abs(character.y - Game.I.board.character1.y)>20)
				return false
			return true
		}
		private function followWalk(character2:Character):void
		{
			var _separation:int;
			if(Math.abs(character.x - character2.x)>separation)
			{
				if(character.x>0) _separation = separation; else _separation = -separation
				if(character2.x<character.x + _separation)
					character.walk("left");
				else character.walk("right");
			}
			if(Math.abs(character.y - character2.y)>20)
			{
				if(character2.y<character.y + _separation)
					character.walk("up");
				else character.walk("down");
			}
		}
		public function reset():void
		{
			removeEventListener(Event.ENTER_FRAME, enterFrame);
			
		}

	}
}
package view
{
	import com.qb9.flashlib.utils.DisplayUtil;
	
	import flash.display.DisplayObject;
	import flash.display.MovieClip;
	
	public class Brain
	{
		private var character1:Character;
		private var character2:Character;
		
		public function Brain()
		{
		}
		public function start():void
		{
		}
		public function checkHit():void
		{
			var difY:int = Math.abs(Game.I.board.character1.y - Game.I.board.character2.y);
			if(difY<=20)
			{
				if(checkHitTest(Game.I.board.character1))
				{
					if(Game.I.board.character1.asset.currentLabel == "hitUp" && Game.I.board.character2.asset.currentLabel!="defenseUp")
						{Game.I.board.character2.hitted("up", Game.I.board.character1.settings.power); Game.I.board.character1.hit() }
					else if(Game.I.board.character1.asset.currentLabel == "hitDown" && Game.I.board.character2.asset.currentLabel!="defenseDown")
						{Game.I.board.character2.hitted("down", Game.I.board.character1.settings.power); Game.I.board.character1.hit() }
				} 
				if(checkHitTest(Game.I.board.character2))
				{
					if(Game.I.board.character2.asset.currentLabel == "hitUp" && Game.I.board.character1.asset.currentLabel!="defenseUp")
						{Game.I.board.character1.hitted("up", Game.I.board.character2.settings.power); Game.I.board.character2.hit() }
					else if(Game.I.board.character2.asset.currentLabel == "hitDown" && Game.I.board.character1.asset.currentLabel!="defenseDown")
						{Game.I.board.character1.hitted("down", Game.I.board.character2.settings.power); Game.I.board.character2.hit() }
				}
			}
		}
		public function checkHitTest(character:Character):Boolean
		{
			
			var asset:MovieClip = MovieClip(character.asset.getChildAt(0))
			if(!asset) return false;
			var i:int = asset.numChildren;
			
			while( i-- )
			{
				var DO:DisplayObject = asset.getChildAt(i);
				if(DO is MovieClip)
				{
					if( DO is HitArea )
					{
						if(character === Game.I.board.character1)
							if(DO.hitTestObject(Game.I.board.character2))
								{ DisplayUtil.dispose(DO); return true }
								
						if(character === Game.I.board.character2)
							if(DO.hitTestObject(Game.I.board.character1))
								{ DisplayUtil.dispose(DO); return true }
					}
				}
			}
			return false;
		}

	}
}
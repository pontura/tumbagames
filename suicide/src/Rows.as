package
{
	import flash.display.MovieClip;

	public class Rows extends MovieClip
	{
		private var all:Array = [];
		private var tryLookForEmptyRow:int = 0;
		private var level:int;
		
		public function Rows(mc:MovieClip, level:int):void
		{
			this.level = level;
			var i:int = mc.numChildren;
			while(i--)
			{
				var a:int = mc.numChildren-1 - i;
				var targetMovieClip:MovieClip = MovieClip(mc.getChildAt(a));
				targetMovieClip.visible = false;
				var row:MovieClip = new Row();
				row.x = 0; row.y = targetMovieClip.y;
				addChild(row)
				
				var row2:MovieClip = new Row()
				row2.x = Main.I.stageW(); row2.y = targetMovieClip.y;
				row2.scaleX = -1;
				addChild(row2)
				
				var _row:rowMC = new rowMC();
				row2.addChild(_row);
				
				if(level != 1)
					_row.sillas.visible = false;
				
				all.push (row);
				all.push (row2);
			}
		}
		public function addGuys(qty:int = 1):void
		{
			var rows:Array = [];
			for(var a:int = 0; a<qty; a++)
			{
				tryLookForEmptyRow = 0
				if(Math.random()*10<5)
					lookForEmptyRow(rows);
			}
		}
		private function lookForEmptyRow(rows:Array):void
		{
			tryLookForEmptyRow++;
			var thisRow:Row = getRandomRow() as Row;
			for each(var row:Row in rows)
			{
				if(thisRow == row)
				{
					if(tryLookForEmptyRow<10)						
 						lookForEmptyRow(rows);
				}
			}
			rows.push (thisRow);
			addGuy(thisRow);
		}
		private function addGuy(row:Row):void{
			var characters:Array =	Main.I.settings.characters[level-1];			
			row.addGuy(characters[Math.floor(Math.random()*characters.length)], 2000);
		}
		private function getRandomRow():Row
		{
			return all[Math.floor(Math.random()*all.length)];
		}
		public function shootTheLastOne():void
		{
			var _last_x:int = Main.I.stageW();
			var _character:Character;
			for each( var row:Row in all)
			{
				var i:int = row.numChildren;				
				while (i--)
				{
					var this_x:int;
					if(row.getChildAt(i) is Character)
					{
						var character:Character = Character(row.getChildAt(i));
						if(character.isDead) continue
						if(character.direction == 1) this_x = character.x; else this_x = Main.I.stageW()-character.x;
						if(_last_x > this_x) 
						{
							_character = character;
							_last_x = this_x
						}
					}
				}
			}
			if(_character)
			{
				_character.shot();
				Main.I.board.ui.addScore();
			} else
				Main.I.board.shooter.hurt();
		}
		
		
	}
}
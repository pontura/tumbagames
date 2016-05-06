package view
{
	import flash.display.MovieClip;
	
	import globals.settings;
	
	public class CharacterSettings
	{
		public var id:int;
		public var characterId:int;
		public var speed:int;
		public var power:int;
		public var character:MovieClip;
		public var defense:int;
		public var separation:int;
		public var attackDelay:int;
		public var defenseDelay:int;
		
		
		public function CharacterSettings(characterId:int)
		{
			this.characterId = characterId;			
			getSettingsById(characterId);
		}
		public function getSettingsById(id:int):void
		{
			
			speed = settings["character" + id].speed;
			power = settings["character" + id].power;
			defense = settings["character" + id].defense;
			separation = settings["character" + id].separation;
			attackDelay = settings["character" + id].attackDelay;
			defenseDelay = settings["character" + id].defenseDelay;
			
		
					
			switch(id)
			{
				case 1:					
					character = new character1MC;break;
				case 2:
					character = new character2MC;break;
				case 3:
					character = new character3MC;break;
				case 4:
					character = new character4MC;break;
				default:
					character = new character5MC;break;
			}
		}
	}
	
}
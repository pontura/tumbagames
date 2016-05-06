package levels
{
	import flash.display.MovieClip;
	

	public class LevelCreator
	{
		private var totalProbabilidades:int;
		public var objects:Array = [];
		
		public function LevelCreator(level:int = 0)
		{
			/*
			totalProbabilidades = 0;
			for each (var obj:Object in settings.pibes)
			{
				totalProbabilidades += obj.probabilidad
			}*/
			
			var _level:MovieClip = Game.I.getCurrentLevel();
			var i:int = _level.numChildren;
			while(i--)
			{
				if(_level.getChildAt(i) is MovieClip)
				{
					var mc:MovieClip = MovieClip(_level.getChildAt(i));
					var name:String;
					
						if(mc is Pibe1) 
							name = "pibe1";
						else if(mc is Pibe2) 
							name = "pibe2";
						else if(mc is Pibe3) 
							name = "pibe3";
						else if(mc is Pibe4) 
							name = "pibe4";
						else if(mc is Pibe5) 
							name = "pibe5";
						else if(mc is Pibe6) 
							name = "pibe6";
						else if(mc is Pibe7) 
							name = "pibe7";
						else if(mc is BossMC) 
							name = "boss";
						else if(mc is CharacterMC) 
							name = "player";
						else if(mc is ItemBombMC) 
							name = "bomb";
						else if(mc is ItemMercaMC) 
							name = "merca";
						else if(mc is ItemBirraMC) 
							name = "birra";
					
					var obj:Object = 
					{
						name:name,
						x:mc.x,
						y:mc.y,
						friction:		settings[name].friction,
						restitution:	settings[name].restitution,
						density:		settings[name].density,
						linearDamping:	settings[name].linearDamping,
						mass:			settings[name].mass,
						radius:			settings[name].radius
						
					}
					
					objects.push(obj)	
				}
			}
		}
	}
}
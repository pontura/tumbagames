package game
{
	import settings.TimeLine;
	import game.enemies.Bottle;
	
	public class TimelineManager
	{
		private var lastActiveDist		:int = 0;
		
		
		public function manageEvents(dist:int):void {
			if (dist == lastActiveDist)
				return;
				
			var events:Array = TimeLine.poongaEvents;
			var nextEvent:int;
			var enemyType:String;
			var obstructionType:String;
			for (var a:int = 0; a<events.length; a++ ) {				
				if (dist> events[a].dist) {
					
					enemyType = "";
					obstructionType = "";
					
					nextEvent = events[a].dist;
					if (events[a].enemyType)
						enemyType = events[a].enemyType;
					if (events[a].obstructionType)
						obstructionType = events[a].obstructionType;
				}
			}
			if (nextEvent!=lastActiveDist){
				lastActiveDist = nextEvent;
				if (enemyType!="") {
					Main.I().board.newObstruction(enemyType);
				}
			}
		}
	}
}
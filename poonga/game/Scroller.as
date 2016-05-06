package game
{
	import flash.events.Event;
	import game.TimelineManager;
	
	public class Scroller
	{
		private var noMoreScroll	:Boolean = false
		public var distance			:int = 1;
		private var background		:Background;
		private var poonga			:Poonga;
		private var stepsToMove		:int = 8;
		private var timelineManager	:TimelineManager;
	
		function Scroller():void {
			timelineManager = new TimelineManager();
			
		}	
		public function stopScrolling():void {
		}
		private function isThereEnemy(sec:int):void {
			
		}
		public function startScrolling(e:Event = null):void {
			if (noMoreScroll)
				return;
			
			if (distance>=3388){
				Main.I().board.patova.attack();
				Main.I().board.poonga.maxX += 90;
				noMoreScroll = true;
				stopScrolling();
			} else {
				Main.I().board.background.x-=stepsToMove;					
				distance += stepsToMove;				
				Main.I().gameInterface.distanceText.text = String(distance);				
				timelineManager.manageEvents(distance);
			}
		}		
		
	}
}
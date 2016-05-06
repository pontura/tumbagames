package clock {
	
	import flash.events.MouseEvent;
	import flash.utils.setTimeout;
	
	public class Clock {
		
		private var clockView : ClockView;
		
		public function Clock() {
			
			countTimer = new CountTimer();
			clockView = new ClockView();
			countTimer.initialise(clockView);
			countTimer.startTimer(130,  timerFinished);
		}
		public function getView():ClockView
		{
			return clockView;
		}
		private function timerFinished() : void {
			Game.I.win();
		}
		
		private var countTimer : CountTimer
	}
}
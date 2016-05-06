package clock
{
	import flash.display.MovieClip;
	import flash.events.Event;
	import flash.events.TimerEvent;
	import flash.utils.Timer;
	import flash.utils.getTimer;

	public class CountTimer extends MovieClip {
		
		public function CountTimer() {
			trace("CountdownTimer!");
			
		}
		private var timer:Timer;
		public var isFinished : Boolean;
		public var paused : Boolean = true;
		private var _currentTime : int;
		private var _lastTime : int;
		private var clockView : ClockView;
		private var _recentTimePassed : int;
		private var _startTime : int;
		private var _totalTimeInMilliseconds : int
		private var _totalTimeInSeconds : int
		private var _totalTimePassed : int;
		private var _whenFinishedCallBack : Function;
		private var minutesLeft : int;
		private var secondsLeft : int;
		private var tensOfSecondsLeft : int;
		private var timeSecondsLeft : int;
		
		public function continueTimer() : void {
			_lastTime = getTimer();
			paused = false;
		}
		
		public function dispose() : void {
			stopTimer()
			removeChild(clockView)
			clockView = null
			_whenFinishedCallBack = null
		}
		
		public function initialise(clockView:ClockView) : void {
			this.clockView = clockView
			
		}
		public function pauseTimer() : void {
			//trace("pausing timer")
			paused = true;
		}
		
		public function startTimer(_seconds : int,_callBack : Function = null) : void {
			trace("starting timer for " + _seconds + " seconds")
			_totalTimeInSeconds = _seconds;
			_whenFinishedCallBack = _callBack;
			_totalTimeInMilliseconds = _seconds * 1000;
			//addEventListener(Event.ENTER_FRAME, loop);
			timer = new Timer(1000);
			timer.addEventListener(TimerEvent.TIMER, loop);
			timer.start();
			_startTime = getTimer();
			_lastTime = _startTime;
			_totalTimePassed = 0;
			paused = false;
			isFinished = false;
		}
		
		public function stopTimer() : void {
			timer.stop();
		}
		
		public function updateTimeDisplay() : void {
			_currentTime = getTimer();
			_recentTimePassed = _currentTime - _lastTime;
			_lastTime = _currentTime;
			_totalTimePassed += _recentTimePassed;
			
			timeSecondsLeft = Math.ceil((_totalTimeInMilliseconds - _totalTimePassed) / 1000);
			if (timeSecondsLeft < 1) {
				isFinished = true
			}
			minutesLeft = int(timeSecondsLeft / (60));
			timeSecondsLeft -= minutesLeft * 60;
			tensOfSecondsLeft = int(timeSecondsLeft / (10));
			timeSecondsLeft -= tensOfSecondsLeft * 10;
			secondsLeft = timeSecondsLeft;
			if (clockView.c_min.getValue() != minutesLeft) {
				clockView.c_min.setValue(minutesLeft);
			}
			if (clockView.c_tenSec.getValue() != tensOfSecondsLeft) {
				clockView.c_tenSec.setValue(tensOfSecondsLeft);
			}
			if (clockView.c_sec.getValue() != secondsLeft) {
				clockView.c_sec.setValue(secondsLeft);
			}
		}
		
		
		private function timerFinished() : void {
			trace("timer has finished countdown")
			stopTimer()
			if (_whenFinishedCallBack != null) {
				_whenFinishedCallBack.call()
			}
		}
		
		private function loop(event : TimerEvent) : void {
			if (!paused) {
				updateTimeDisplay();
				if (isFinished) {
					timerFinished()
				}
			}
		}
	}
}
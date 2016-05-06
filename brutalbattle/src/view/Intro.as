package view
{
	import com.qb9.flashlib.utils.DisplayUtil;
	
	import flash.events.Event;
	import flash.events.KeyboardEvent;
	import flash.events.MouseEvent;
	import flash.events.TimerEvent;
	import flash.utils.Timer;
	
	public class Intro extends IntroMC
	{
		private var sec:int = 0;
		private var diapo:int = 1;
		
		public function Intro()
		{
			addEventListener(Event.ENTER_FRAME, enterFrame);
			this.fuckOff.addEventListener(MouseEvent.CLICK, click);		
			Game.I.stage.addEventListener(KeyboardEvent.KEY_DOWN, click);	
			stop();	
		}
		private function enterFrame(e:Event):void
		{
			sec++;
			if(sec>90 * diapo)
				click();
		}
		public function click(e:* = null):void
		{
			diapo++;
			sec = 0;
			if(currentFrame<totalFrames)
				nextFrame();
			else
				reset();
		}
		public function reset():void
		{
			Game.I.stage.removeEventListener(KeyboardEvent.KEY_DOWN, click);
			removeEventListener(Event.ENTER_FRAME, enterFrame);
			this.fuckOff.removeEventListener(MouseEvent.CLICK, click);
			DisplayUtil.dispose(this);
			Game.I.characterSelector();
		}
	}
}
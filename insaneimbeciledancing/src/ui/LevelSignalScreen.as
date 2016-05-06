package ui
{
	import flash.events.MouseEvent;
	import flash.events.TimerEvent;
	import flash.utils.Timer;
	
	import flashlib.utils.DisplayUtil;
	
	public class LevelSignalScreen extends LevelSignalMC
	{		
		public function LevelSignalScreen():void
		{
			//Main.I.ui.visible = false;
			this.desc.field.text = settings.levels[Game.I.levelNum].name;
			this.title.field.text = "LEVEL " + (Game.I.levelNum+1);
			
			var timer:Timer = new Timer(2000,1);
			timer.addEventListener(TimerEvent.TIMER, reset);
			timer.start();
		}
		public function reset(e: TimerEvent):void
		{
			trace("reset");
			DisplayUtil.dispose(this);
		}
	}
}
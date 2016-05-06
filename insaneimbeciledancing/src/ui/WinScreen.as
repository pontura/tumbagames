package ui
{
	
	import flash.events.MouseEvent;
	import flash.utils.setTimeout;
	
	import flashx.textLayout.tlf_internal;
	
	public class WinScreen extends WinMC
	{		
		public function WinScreen(score:int)
		{
			setTimeout(end, 7000);
			this.score.text = String(score);
		}
		public function end():void
		{
			Game.I.refreshGame();
		}
	}
}
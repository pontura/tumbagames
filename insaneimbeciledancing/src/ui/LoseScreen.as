package ui
{
	
	import flash.events.MouseEvent;
	import flash.utils.setTimeout;
	
	import flashlib.utils.DisplayUtil;
	
	public class LoseScreen extends LoseMC
	{		
		public function LoseScreen()
		{
			trace("LoseScreen");
			Game.I._ui.visible = false;
			setTimeout(end, 4000);
		}
		public function end():void
		{
			Game.I._ui.visible = true;
			Game.I.refreshGame();
			flashlib.utils.DisplayUtil.dispose(this);
		}
	}
}
package ui
{
	import com.qb9.flashlib.utils.DisplayUtil;
	
	import flash.events.MouseEvent;
	
	public class LevelCompleteScreen extends WinMC
	{		
		public function LevelCompleteScreen()
		{
			trace("LevelCompleteScreen");
			Main.I.ui.visible = false;
			addEventListener(MouseEvent.CLICK, clicked);
		}
		public function clicked(e:MouseEvent):void
		{
			Main.I.ui.visible = true;
			removeEventListener(MouseEvent.CLICK, clicked);
			Main.I.board.startPlaying();
			DisplayUtil.dispose(this);
		}
	}
}
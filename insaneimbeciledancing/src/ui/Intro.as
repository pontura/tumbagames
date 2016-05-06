package ui
{
	import com.qb9.flashlib.utils.DisplayUtil;
	
	import flash.events.MouseEvent;
	import flash.utils.setTimeout;

	public class Intro extends IntroMC
	{		
		public function Intro()
		{
			Main.I.ui.visible = false;
			//addEventListener(MouseEvent.CLICK, clicked);
			setTimeout(clicked, 2500)
		}
		public function clicked(e:MouseEvent = null):void
		{
			Main.I.ui.visible = true;
			removeEventListener(MouseEvent.CLICK, clicked);
			Main.I.board.startPlaying();
			DisplayUtil.dispose(this);
		}
	}
}
package gui
{
	import flash.events.KeyboardEvent;
	import flash.events.MouseEvent;
	
	import flashlib.utils.DisplayUtil;

	public class MenuScreen extends MenuScreenMC
	{
		private var main:Main;
		
		public function MenuScreen(main:Main)
		{
			this.main = main;
			this.addEventListener(MouseEvent.CLICK, clicked);
			main.stage.addEventListener(KeyboardEvent.KEY_DOWN, clicked);
		}
		private function clicked(e:*):void
		{
			main.stage.removeEventListener(KeyboardEvent.KEY_DOWN, clicked);
			this.removeEventListener(MouseEvent.CLICK, clicked);
			Main(parent).showIntro();
			DisplayUtil.dispose(this);
		}
	}
}
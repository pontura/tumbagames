package gui
{
	import flash.display.MovieClip;
	
	public class AvatarProgressBar
	{
		private var num:Number = 100;
		private var bar:MovieClip;
		private var _gui:Gui;
		
		public function AvatarProgressBar(gui:Gui, avatarProgressBar:MovieClip)
		{
			this.bar = avatarProgressBar.bar;
			this._gui = gui;
		}
		public function reset():void
		{
			num = 100;
			this.bar.gotoAndStop(100-num);
		}
		public function setProgress(qty:Number):void
		{
			num -= qty;
			if(num<=0)
			{
				full();
			}
			this.bar.gotoAndStop(100-num);
		}
		private function full():void
		{
			_gui.showLoseScreen();
		}

	}
}
package gui
{
	import flash.display.MovieClip;
	
	public class PesosProgressBar
	{
		private var num:int = 100;
		private var bar:MovieClip;
		private var _gui:Gui;
		private var pesosTotales:int;
		
		public function PesosProgressBar(gui:Gui, pesosProgressBar:MovieClip)
		{
			
			this.bar = pesosProgressBar.bar;
			this._gui = gui;   
			reset();
		}
		public function reset():void
		{
			num = 100;
			this.bar.gotoAndStop(100-num);
			pesosTotales = 0;    
		}    
        public function setProgress(qty:int):void   
		{
			pesosTotales += qty;
			var realQty:int = qty * 100 /settings["level" + _gui._level.levelNumber].pesos;
			num -= realQty;    
			if(num<=0)
			{
				full();
			}
			this.bar.gotoAndStop(100-num);
		}
		private function full():void
		{
			_gui.showWinScreen();
			pesosTotales = 0;
			num = 100;
		}

	}
}
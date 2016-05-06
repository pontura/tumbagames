package ui
{
	
	import clock.Clock;
	
	import flash.utils.setTimeout;
	
	import flashlib.tasks.Func;
	import flashlib.tasks.Sequence;
	import flashlib.tasks.Wait;
	

	public class Ui extends UiMC
	{
		public var counts:int = 0;
		public var points:int = 0;
		public var totalPibes:int = 0;
		private var tapScreen:TapScreen;
		private var pogoSize:int = 0;
		private var _clock:Clock;
		
		public function Ui()
		{
			tapScreen = new TapScreen();
			addChild(tapScreen);
		}
		public function addClock():void
		{
			_clock = new Clock();
			addChild(_clock.getView());
			_clock.getView().x = (Game.I.stage.stageWidth/2)-_clock.getView().width/2;
			_clock.getView().y = 10;
		}
		public function start():void
		{
			//startNewLevel()
			addPoints(0);
			addCount(0);
		}
		public function winEnergy(value:Number):void
		{
			this.progressBar.bar.scaleX += value;
			if (this.progressBar.bar.scaleX>1)
				this.progressBar.bar.scaleX = 1;
		}
		public function lose(value:Number):void
		{
			tapScreen.show()
			this.progressBar.bar.scaleX -= value;
			if (this.progressBar.bar.scaleX<0)
			{
				this.progressBar.bar.scaleX = 0;
				Game.I.lose();	
			}
		}
		public function addPoints(value:int):void
		{
			points += value;
			this.coins_mc.text = String(points);	
		}
		public function addCount(value:int):void
		{
			counts += value;
			this.count_txt.text = String(counts);	
		}
		public function addPibeToPogo():void
		{
			addCount(1);
			addPoints(1);
			pogoSize++;
			drawPogoSize();
			if(pogoSize >= totalPibes)
			{
				Game.I.win();
				counts = 0;
				addCount(0);
			}
		}
		public function removePibeFromPogo():void
		{
			pogoSize--;
			drawPogoSize();
		}
		public function drawPogoSize():void
		{
			bar2.bar.scaleY = bar1.bar.scaleY = 1- (pogoSize * 100 / totalPibes)/100;
		}
		public function startNewLevel():void
		{
			totalPibes = 0;
			tapScreen.hide()
			pogoSize = 0;
			bar2.bar.scaleY = bar1.bar.scaleY = 1;
		}
		public function reset():void
		{
			startNewLevel()
			points = 0;
			this.progressBar.bar.scaleX = 1;
		}
	}
}
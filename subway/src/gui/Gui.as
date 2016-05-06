package gui
{
	import flash.events.Event;
	
	import flashlib.utils.DisplayUtil;
	
	import level.Level;
	
	public class Gui extends GuiMC
	{
		public var avatarProgressBar:AvatarProgressBar;
		public var pesosProgressBar:PesosProgressBar;
		public var _level:Level;
		private var loseScreen:LoseScreen;
		public var points:int = 0;
		private var winMC:WinMC;
		private var levelComplete:LevelComplete;
		private var alerta:AlertMC;
		
		public function Gui(_level:Level)
		{
			this._level = _level;
			avatarProgressBar = new AvatarProgressBar(this, this.avatarProgress);
			pesosProgressBar = new PesosProgressBar(this, this.pesosBar);
			this.alerta = new AlertMC();
			addChild(alerta)
			hideAlerta();
		}
		public function showLoseScreen():void
		{
			this.loseScreen= new LoseScreen(_level);
			this.addChild( loseScreen );
		}
        public function showWinScreen():void
		{
			_level.winLevel();
			checkResults();
		}
		public function showAlerta():void
		{
			setChildIndex(alerta, 0);
			alerta.visible = true;
		}
		public function hideAlerta():void
		{
			alerta.visible = false;
		}
		public function showWinSignal():void
		{
			winMC = new WinMC();
			addChild(winMC)
			setChildIndex(winMC, 0);
		}
		public function hideWinSignal():void
		{
			_level.winLevel();
			checkResults();
		}
		
		
		public function checkResults():void
		{
			levelComplete = new LevelComplete();
			addChild(levelComplete);
			setChildIndex(levelComplete, 0);
			this.addEventListener(Event.ENTER_FRAME, addCountDown);
		}
		
		public function addCountDown(e:Event):void
		{
			_level.time+=2;
			points+=2;
			this.score_txt.text = String(points);
			moveProgress( _level.time );
			if(_level.time > settings["level" + _level.levelNumber].duration/1000)
			{
				this.removeEventListener(Event.ENTER_FRAME, addCountDown);
			}
		}
		public function moveProgress(time:int):void
		{
			var levelTime:int = settings["level" + _level.levelNumber].duration/1000
			var frame:int = time * 100 / levelTime ;
			this.progress.gotoAndStop( frame );
		}
		public function addPoints(_points:int):void
		{
			this.points += _points;
			this.score_txt.text = String(points);
			this.pesosProgressBar.setProgress(_points);	
			pesos_txt.text = String("$" + points + " / " + "$" + settings["level" + _level.levelNumber].pesos)		
		}
		public function reset():void
		{
			if(loseScreen)
			{
				//loseScreen.reset();
				DisplayUtil.dispose(loseScreen);
			}
			avatarProgressBar.reset();
			this.removeEventListener(Event.ENTER_FRAME, addCountDown);
		}

	}
}
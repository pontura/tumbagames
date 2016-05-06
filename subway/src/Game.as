package {
	import flash.display.Sprite;
	
	import globals.audio;
	
	import gui.ProgressMap;
	import gui.WinScreen;
	
	import level.Level;

	public class Game extends Sprite
	{
		public var _level:Level;
		public var _main:Main;
		public var levelNum:int = 1
		
		public function Game(_main:Main)
		{	
			this._main = _main;
		}
		public function showProgressMap():void
		{
			if(_level)
				_level.reset();
			var pm:ProgressMap = new ProgressMap(this);
			addChild(pm);
		}
		public function showWinAll():void
		{
			if(_level)
				_level.reset();
			
			var ws:WinScreen = new WinScreen(this);
			this.addChild( ws );
		}
		public function nextLevel():void
		{
			stage.focus = stage
			this.levelNum++;
			if(levelNum==7)
				showWinAll();
			else
				showProgressMap();
		}
		public function startLevel():void
		{
			this._level = new Level(this);
			addChild(_level);
			_level.start(levelNum);
			audio.loop("loop1");
		}
	}
}
package gui
{
	import flash.display.MovieClip;
	import flash.events.KeyboardEvent;
	import flash.events.MouseEvent;
	import flash.utils.setTimeout;
	
	import flashlib.utils.DisplayUtil;
	
	import level.Level;
	
	public class LoseScreen extends MovieClip
	{
		private var _level:Level;
		
		public function LoseScreen(_level:Level)
		{
			this._level = _level;			
			_level.pause();
			addChild(_level._game._main.loseScreenLoaded.asset)
			_level._game._main.loseScreenLoaded.playAnimation();
			setTimeout(_level._game._main.refreshGame, 33000);
		}
		private function replay(e:MouseEvent):void
		{			
			reset()
			_level.restart();
		}
		private function main(e:*):void
		{						
			reset()
			_level._game.showProgressMap();
		}
		public function reset():void
		{
			stage.focus = stage
			DisplayUtil.dispose(this);
		}

	}
}
package gui
{
	import flash.display.MovieClip;
	import flash.utils.setTimeout;
	
	import flashlib.utils.DisplayUtil;
	
	public class ProgressMap extends ProgressMapMC
	{
		private var _game:Game;
		
		public function ProgressMap(_game:Game)
		{
			this._game = _game;
			gotoAndStop("level" + _game.levelNum);
			setTimeout(reset, 4000);
		}
		public function reset():void
		{
			_game.startLevel();
			DisplayUtil.dispose(this);
		}

	}
}
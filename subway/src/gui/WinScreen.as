package gui
{
	import flash.display.MovieClip;
	import flash.events.KeyboardEvent;
	import flash.events.MouseEvent;
	import flash.utils.setTimeout;
	
	import flashlib.utils.DisplayUtil;
	
	import level.Level;
	
	public class WinScreen extends MovieClip
	{
		private var _game:Game;
		
		public function WinScreen(_game:Game)
		{
			addChild(_game._main.winScreenLoaded.asset)
			_game._main.winScreenLoaded.playAnimation();
			
			/*this.replayBtn.addEventListener(MouseEvent.CLICK, replay);
			this.mainMenuBtn.addEventListener(MouseEvent.CLICK, main);
			
			addEventListener(KeyboardEvent.KEY_DOWN, main);
			mainMenuBtn.field.mouseEnabled = false;
			replayBtn.field.mouseEnabled = false;*/
			
			
			/*
			replayBtn.field.text = "RE-JUGAR";
			mainMenuBtn.field.text = "MAIN MENU";*/
			
			
			setTimeout(_game._main.WinScreenAnimationReady, 33000);
			
		}
		private function replay(e:MouseEvent):void
		{			
			reset()
			_game._level.restart();
		}
		private function main(e:*):void
		{						
			reset()
			_game._level._game.showProgressMap();
		}
		public function reset():void
		{
			stage.focus = stage
			removeEventListener(KeyboardEvent.KEY_DOWN, main);
		/*	this.replayBtn.removeEventListener(MouseEvent.CLICK, replay);
			this.mainMenuBtn.removeEventListener(MouseEvent.CLICK, replay);*/
			DisplayUtil.dispose(this);
		}

	}
}
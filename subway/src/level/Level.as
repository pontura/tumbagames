package level {
	import flash.display.Sprite;
	import flash.utils.clearInterval;
	import flash.utils.setInterval;
	
	import flashlib.easing.Tween;
	import flashlib.tasks.Func;
	import flashlib.tasks.Loop;
	import flashlib.tasks.Sequence;
	import flashlib.tasks.TaskRunner;
	import flashlib.tasks.Wait;
	
	import globals.audio;
	
	import gui.Gui;
	
	import input.Controls;
	
	import tasks.LoopFunc;

	public class Level extends Sprite
	{
		public var _game:Game;
		public var _tasks:TaskRunner;
		public var _gui:Gui;
		public var levelNumber:int;
		private var view:View;
		public var time:int = 0;
		private var timeoutID:int;
		public var levelAudio:String = "loop1";
		private var loopFunc:LoopFunc;
		
		public function Level(_game:Game)
		{	
			this._game = _game;	
			this._gui = new Gui(this);		
		}
		public function start(levelNumber:int):void
		{		
			time = 0;	
			_gui.reset();
			
			this.levelNumber = levelNumber;
			_tasks = new TaskRunner(_game._main);
			_tasks.start();
			
			this.view = new View(this);
			loopFunc = new LoopFunc(view.update)
			_tasks.add(loopFunc);
			addChild(view);
			view.draw();
			
			addChild(_gui);
			timeoutID = setInterval(addSec, 1000);
			
			_gui.addPoints(0);
		}
		public function winLevel():void{
			audio.stop(levelAudio);
			pause();
			_tasks.dispose();
			addChild(new WinMC);
			view.poonga.win();
			_tasks.add( 
				new Sequence(
					new Tween(	view.poonga, 3000, {x: view.poonga.x+600}	),
					new Wait(2000),
					new Func(resetLevel)
				)
			)
			_tasks.start();
		}
		private function resetLevel():void{
			_game.nextLevel();
		}
		public function pause():void
		{
			_tasks.stop();
			view.poonga.pause();
			view.tipos.pause();
			view.distractores.pause();
			clearInterval(timeoutID);
		}
		public function restart():void
		{
			reset();
			start(levelNumber);
		}
		public function reset():void
		{
			resetTasks();
			view.reset();
		}
		public function resetTasks():void
		{
			if(_tasks)
			{
				_tasks.stop();
				_tasks = null;
			}	
		}
		private function addSec():void
		{
			this.time++;
		}
		
	}
}
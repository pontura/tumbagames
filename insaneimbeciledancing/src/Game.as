package {
	import clock.Clock;
	import flash.display.StageAlign;
	import flash.display.StageDisplayState;
	import flash.display.StageScaleMode;
	
	import flash.display.Bitmap;
	import flash.display.BitmapData;
	import flash.display.Loader;
	import flash.display.MovieClip;
	import flash.display.Sprite;
	import flash.events.Event;
	import flash.external.ExternalInterface;
	import flash.net.URLRequest;
	import flash.net.navigateToURL;
	import flash.text.StyleSheet;
	import flash.ui.Keyboard;
	import flash.utils.setTimeout;
	
	import flashlib.net.LoadFile;
	import flashlib.net.LoadFileFormat;
	import flashlib.tasks.Func;
	import flashlib.tasks.Loop;
	import flashlib.tasks.Sequence;
	import flashlib.tasks.Task;
	import flashlib.tasks.TaskRunner;
	import flashlib.tasks.Wait;
	import flashlib.utils.DisplayUtil;
	
	import levels.LevelCreator;
	
	import spriteSheet.SpriteSheet;
	
	import spriteSheets.SpriteSheetExample;
	
	import ui.LevelSignalScreen;
	import ui.LoseScreen;
	import ui.Screenshot;
	import ui.Show;
	import ui.Ui;
	import ui.WinScreen;
	
	import view.View;
	
	import virtual.VirtualView;
	
	[SWF(width='1000', height='700', backgroundColor='0x000', frameRate='30')]	
	
	public class Game extends MovieClip {
		
		private var VIRTUAL_VIEW:Boolean = false;
		private var 		VIEW:Boolean = true;
		
		public static var I:Game;
		public var virtualView:VirtualView;
		public var _view:View;
		public var tasks:TaskRunner
		private var keyboardEvents:KeyboardEvents;
		public var _ui:Ui;
		public var show:Show;
		private var enterFrameTask:Task;
		public var levelNum:int = 0;
		public var currentLevel:MovieClip;
		public var spriteSheet:SpriteSheet;
		
		public function Game() {
			
			stage.displayState = StageDisplayState.FULL_SCREEN;
			stage.scaleMode = StageScaleMode.EXACT_FIT;
			
			stage.focus = this; 
			/*var myStarling:Starling = new Starling(SpriteSheetExample,stage);
			myStarling.start();
			return;*/
			I = this;
			tasks = new TaskRunner();
			loadSettings();
			keyboardEvents = new KeyboardEvents();
			this._ui = new Ui();
			this.virtualView = new VirtualView();
			//setNextLevel();
			
		}
		private function loadSettings():void
		{
			var settingsLoader:LoadFile = new LoadFile("settings.json");
			settings.addFile(settingsLoader);
			
			
			tasks.add( 
				new Sequence(
					settingsLoader,
					new Wait(100),
					new Func(loadFonts)
				)
			);
			tasks.start();
		}
		private function loadFonts():void
		{
			currentLevel = new Level0MC();
			_ui.addChild(new LevelSignalScreen());
			
			var fontsLoader:LoadFile = new LoadFile("fonts/Metrics.xml", LoadFileFormat.XML);
			fonts.addFile(fontsLoader);	
			
			/*			
			// audio load
			audio = new AudioManager(new PlayableFactory("sfx/"),"mp3");
			audio
			.registerFx("bounce", "bounce")
			.registerFx("bumper", "bounce")
			.registerFx("star", "fruit");
			*/
			tasks.add( 
				new Sequence(
					fontsLoader,
					new Wait(100),
					new Func(loadBitmap)
				)
			);
		}
		private function loadBitmap():void{
			var url:URLRequest = new URLRequest("fonts/Texture.png");
			
			var img:Loader = new Loader();
			img.load(url);
			
			img.contentLoaderInfo.addEventListener(Event.COMPLETE, BmpLoaded);
			
		}
		private function BmpLoaded(e:Event):void
		{
			var bitmap:Bitmap = e.target.content;
			spriteSheet = new SpriteSheet(bitmap);
			
			e.target.removeEventListener(Event.COMPLETE, BmpLoaded);
			_ui.addClock();
			addViews();
			addChild(_ui);
		}
		private function addViews():void {
			
			var levelCreator:LevelCreator = new LevelCreator(1);
			
			virtualView.start(levelCreator.objects);
			
			if(VIRTUAL_VIEW)
				addChild(virtualView);			
			
			this._view = new View();
			if(VIEW)
				addChild(_view);
			_view.start();
			
			tasks.add( 
				new Sequence(
					new Wait(100),
					new Func(startShow)
				)
			);
			tasks.start();
			
		}
		private function startShow():void
		{
			_ui.start();
			if(!show)
				show = new Show();
			else
				start();
		}
		public function start():void
		{
			enterFrameTask = new Loop(
				new Sequence(
					new Func(keyboardEvents.tick),
					new Wait(30)
				)
			)
			tasks.add(enterFrameTask);
			
			trace("levelNum " + levelNum);
		}
		public function win():void
		{
			setNextLevel();
			new Screenshot();
			_ui.addChild(new LevelSignalScreen());
		}
		public function setNextLevel():void
		{
			
			trace("setNextLevel  levelNum " + levelNum);
			levelNum++;
			if(levelNum == 1)
				currentLevel = new Level1MC();
			if(levelNum == 2)
				currentLevel = new Level2MC();
			else if(levelNum == 3)
				currentLevel = new Level3MC();
			else if(levelNum == 4)
				currentLevel = new Level4MC();
			else if(levelNum == 5)
				currentLevel = new Level5MC();
			else if(levelNum == 6)
				currentLevel = new Level6MC();
			else if(levelNum == 7)
				currentLevel = new Level7MC();
			else if(levelNum == 8)
				currentLevel = new Level8MC();
			else if(levelNum == 9)
				currentLevel = new Level9MC();
			else if(levelNum == 10)
				currentLevel = new Level10MC();
			else if(levelNum == 11)
				currentLevel = new Level11MC();
			else
				currentLevel = new Level1MC();
		}
		public function getCurrentLevel():MovieClip
		{
			return currentLevel;
		}
		public function lose():void
		{
			show.stop();
			reset();
			//setTimeout(exit, 7000);
			//addChild(new LoseMC);
			addChild(new LoseScreen());
		}
		public function endGame():void
		{
			tasks.remove(enterFrameTask);
			tasks.stop();
			tasks.dispose();
			show.stop();
			addChild(new WinScreen(_ui.points));
		}
		public function reset():void
		{
			tasks.remove(enterFrameTask);
			tasks.stop();
			tasks.dispose();			
			virtualView.reset();
			_ui.startNewLevel();
			_view.reset();
			//show.reset();
			
			if(VIRTUAL_VIEW)
				removeChild(virtualView);
			if(VIEW)
				removeChild(_view);
			
			//removeChild(_ui);
			_view = null;
			//show = null;			
		}
		public function restart():void
		{
			trace("restart");
			show.play();
			addViews();			
		}
		public function refreshGame():void
		{
			ExternalInterface.call("history.go", 0);
		}
		
	}
}
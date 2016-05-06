package {
	import brain.KeyboardPress;
	
	import flash.display.DisplayObject;
	import flash.display.DisplayObjectContainer;
	import flash.display.Sprite;
	import flash.events.KeyboardEvent;
	import flash.utils.setTimeout;
	
	import gInterface.EndGameScreen;
	import gInterface.GameInterface;
	import gInterface.GameOverScreen;
	import gInterface.IntroScreen;
	
	import game.Board;
	
	import ui.Audio;
	
	[SWF(width='570', height='390', backgroundColor='0x000000', frameRate='15')]
	
	public class Main extends Arcade
	{
		[Embed (source='/assets/fonts/STAN0758.TTF', fontName="_Standard", mimeType='application/x-font')]
		public static var _Standard		:Class;
		
		private static var instance		:Main; 
		public var board				:Board;
		public var keyboardPress		:KeyboardPress;
		public var gameInterface		:GameInterface;
		public var audio				:Audio;
		private var introScreen			:IntroScreen;
		
		
		public static function I():Main {      
			if (instance == null)
				instance = new Main();      
            return instance;       
        }
		public function Main()
		{	
			Main.instance = this;
			loadIntro();
		}
		public function loadIntro():void {
			audio = new Audio();
			Main.I().audio.start(5);
			introScreen = new IntroScreen();
			addChild(introScreen)
			stage.addEventListener(KeyboardEvent.KEY_UP,startIntro,false, 0, true);
		}
		private function keyUp(k:KeyboardEvent):void {
			startIntro();
			stage.removeEventListener(KeyboardEvent.KEY_UP,keyUp);
		}
		public function startIntro(k:KeyboardEvent = null):void
		{	
			if (k)
				stage.removeEventListener(KeyboardEvent.KEY_UP,startIntro);
			removeAllChilds()							
						
			board = new Board();
			this.addChild(board);			
			gameInterface = new GameInterface();
			addChild(gameInterface);
			this.keyboardPress = new KeyboardPress();
		}
		public function youWin():void {
			stage.removeEventListener(KeyboardEvent.KEY_DOWN,keyboardPress.move);
			stage.removeEventListener(KeyboardEvent.KEY_UP,keyboardPress.keyUp);
			setTimeout(youWinScreen, 4000);
			audio.setNewLoop(5);
			board.state = "gameOver";
			board.winScroll();
		}
		public function youWinScreen():void {			
			setTimeout(restartByClicking, 2000);		
			var endGameScreen:EndGameScreen = new EndGameScreen();
			addChild(endGameScreen);			
		}	
		public function gameOver():void {
			stage.removeEventListener(KeyboardEvent.KEY_DOWN,keyboardPress.move);
			stage.removeEventListener(KeyboardEvent.KEY_UP,keyboardPress.keyUp);
			setTimeout(restartByClicking, 2000);		
			audio.setNewLoop(5);
			board.state = "gameOver";
			board.reset();
			var gameOverScreen:GameOverScreen = new GameOverScreen();
			addChild(gameOverScreen);
		}		
		private function restartByClicking():void {
			stage.addEventListener(KeyboardEvent.KEY_UP,restart,false, 0, true);
		}		
		public function restart(k:KeyboardEvent = null):void {
			if (k.keyCode == 32) {
				stage.removeEventListener(KeyboardEvent.KEY_UP,restart);	
				reset();
				loadIntro();
			}
		}
		private function reset():void {	
			audio.setOff();
			removeAllChilds();
			board =null;
			keyboardPress=null;
			gameInterface=null;		
		}
		private function removeAllChilds():void {
			var i:int = this.numChildren
			while( i-- )
       			var a:DisplayObject = this.removeChildAt(i);
       			a = null;
			
		}
		
		
	}
}


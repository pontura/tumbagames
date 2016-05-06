package game
{
	import flash.display.Sprite;
	import game.Background;
	import game.enemies.Bottle;
	import flash.display.DisplayObject;
	import flash.utils.Timer;
	import flash.events.Event;
	import flash.events.TimerEvent;

	public class Board extends Sprite
	{
		public var poonga			:Poonga;
		public var patova			:Patova;
		public var background		:Background;
		public var scroller			:Scroller;
		public var obstructions		:Sprite;
		public var state			:String = "stopped";
		
		function Board () {
			background = new Background(1);
			addChild(background);
			
			obstructions = new Sprite();
			addChild(obstructions);
			
			poonga = new Poonga();
			addChild(poonga);
			
			patova = new Patova();
			background.addChild(patova);
			patova.x = background.width-280;
			patova.y = 330;
			
			scroller = new Scroller();
			poonga.y = 330;
			poonga.x = -80;
			
			
			
			/*background.x = -2900;
			scroller.distance = 2900;
			startGame()*/
			
			background.x = -background.width + Main.I().stage.stageWidth + 75;
			var gritaPatovaTimer:Timer = new Timer(2000, 1);
			gritaPatovaTimer.addEventListener(TimerEvent.TIMER, gritaPatova);
			gritaPatovaTimer.start();
			
			Main.I().audio.setNewLoop(2);
			
		}
		private function gritaPatova(e:TimerEvent):void {
			Main.I().audio.grita();
			patova.gotoAndStop("grita");
			var startScrollTimer:Timer = new Timer(1000, 1);
			startScrollTimer.addEventListener(TimerEvent.TIMER, startScroll);
			startScrollTimer.start();
			
		}
		private function startScroll(e:TimerEvent):void {
			this.addEventListener(Event.ENTER_FRAME, introEnterFrame);
		}
		private function introEnterFrame(e:Event):void {
			if (poonga.x >80){
				this.removeEventListener(Event.ENTER_FRAME, introEnterFrame);
				startGame();
			}				
			else
			if (background.x>-15) {
				background.x = 0;				
				poonga.x += 20;
				poonga.changeTo("run");							
			} else
				//background.x +=120;
				background.x +=30;
		}
		private function startGame():void {
		//	Main.I().gameInterface.x = 0;
			//background.x = 0;			
			Main.I().audio.setNewLoop(1);
			poonga.changeTo("idle");
			this.state = "playing";
			patova.gotoAndStop(1);
		}
		public function reset():void {			
			this.removeChild(poonga);
			poonga = null;
			scroller = null;
			patova.reset();
			this.background.removeChild(patova);
			patova = null;
		}
		public function keyUp(keyDown:String):void {
			if (Main.I().board.state == "playing")
			switch (keyDown) {
				case "space":
					//termina
				//	poonga.attack();
					break;
				case "left":
				case "right":
					poonga.idle();
					//scroller.stopScrolling();
					break;	
				case "up":
				case "down":					
					break;
			}
			
		}
		public function keyDown(keyDown:String):void {
			if (Main.I().board.state == "playing")
			switch (keyDown) {
				case "shift":
					poonga.changeWeapon();
					break;
				case "space":
					poonga.attack();
					break;
				case "left":
					poonga.run("left");
					break;
				case "right":
					poonga.run("right");
					break;
				case "up":
					poonga.jump();
					break;
				case "down":
					
					break;
			}
		}
		public function newObstruction(type:String, dO:DisplayObject = null):void {
			var bottle:Bottle = new Bottle(type, dO);
			Main.I().board.obstructions.addChild(bottle)
		}
		public function winScroll():void {
			this.addEventListener(Event.ENTER_FRAME, winEnterFrame);
		}
		private function winEnterFrame(e:Event):void {
			if (poonga.x >600){
				this.removeEventListener(Event.ENTER_FRAME, winEnterFrame);
			}				
			else {			
				poonga.x += 10;
				poonga.jump();
				poonga.run("right");
			}
		}
	}
}
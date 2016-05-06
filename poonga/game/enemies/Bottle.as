package game.enemies
{
	import assets.Vino;
	import assets.Whisky;
	import assets.Vodka;
	
	import flash.display.MovieClip;
	import flash.display.DisplayObject;
	import assets.Volador1;
	import flash.display.Sprite;
	import flash.events.TimerEvent;
	import flash.events.Event;
	import flash.utils.Timer;
	import flash.geom.Point;
	
	public class Bottle extends Sprite
	{
		public var asset		:MovieClip;
		private var explotion	:MovieClip;
		private var type		:String;
		private var speed		:int
		public var dO			:MovieClip;
		private var exploting	:Boolean;
		
		function Bottle(type:String, dO:DisplayObject = null) {
			this.type = type;
			speed = 14;
			switch (type) {
				case "vino":
					asset = new assets.Vino();
					break;
				case "whisky":
					asset = new assets.Whisky();
					break;
				case "vodka":
					asset = new assets.Vodka();
					break;
				case "volador1":
					asset = new assets.Volador1();
					break;
				case "volador2":
					asset = new assets.Volador2();
					break;
			}
			addChild(asset);
			move(asset, dO);
		}
		public function move(dO:MovieClip, dO1:DisplayObject):void {
			this.dO = dO;
			if (dO1)
				dO.x = Main.I().stage.stageWidth-180;
			else
				dO.x = Main.I().stage.stageWidth + Math.round(Math.random()*40);
			dO.y = Main.I().board.poonga.y;
			dO.addEventListener(Event.ENTER_FRAME, enterFrame, false,0,true);
		}
		public function explote():void {	
			var resta:int;
			switch (type) {
				case "vino":
					explotion = new VinoExplotion();
					resta = 15;
					break;
				case "whisky":
					explotion = new WhiskyExplotion();
					resta = 15;
					break;
				case "vodka":
					explotion = new VodkaExplotion();
					resta = 15;
					break;
				case "volador1":
					resta = 10;
					break;
				case "volador2":
					resta = 10;
					break;
			}
			Main.I().gameInterface.sumaPuntos(-resta);
			if (explotion) {
				Main.I().board.addChild(explotion);
				var clickPoint:Point = new Point(dO.x, dO.y);
				this.dO.localToGlobal(clickPoint);
				explotion.x = clickPoint.x + dO.hitZone.x;
				explotion.y = clickPoint.y + dO.hitZone.y;			
			}
			this.dO.alpha = 0;
			exploting = true;
			
			var myTimer:Timer = new Timer(1000, 1);
			myTimer.addEventListener(TimerEvent.TIMER, endHumo);
			myTimer.start();
			
		}
		private function endHumo(e:TimerEvent):void {
			reset();
			if (explotion) {
				if (Main.I().board.contains(explotion))
					Main.I().board.removeChild(explotion);
				explotion = null;
			}
		}
		public function collapse():void {
			explote();
			Main.I().board.poonga.hurt();
		}
		private function enterFrame(e:Event):void {
			if (Main.I().board.state=="gameOver" || Main.I().board.poonga.hitZone==null){
				reset();
				return;
			}
			if(!exploting && dO.hitZone.hitTestObject(MovieClip(Main.I().board.poonga.hitZone)))
				collapse();			
			dO.x -= speed;
			if (dO.x <0)
				reset();
		}
		private function reset():void {
			dO.removeEventListener(Event.ENTER_FRAME, enterFrame);
			if (Main.I().board.obstructions.contains(this))
				Main.I().board.obstructions.removeChild(this);
		}
		
	}
}
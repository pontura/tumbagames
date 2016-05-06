package game
{
	import flash.events.Event;
	import flash.display.DisplayObject;
	import flash.display.DisplayObjectContainer;
	import game.enemies.Bottle;
	import flash.display.MovieClip;
	import assets.Background1;
	import assets.Explosion;
	import flash.utils.Timer;
	import flash.events.TimerEvent;
	import flash.net.getClassByAlias;
	import flash.display.Sprite;
	
	public class Shot extends Sprite
	{
		private var explotion			:assets.Explosion;
		private var numShot				:int;
		public var hitOnObstruction		:Boolean;
		public var numFrames			:int = 0;
		public var asustando			:Boolean;
		
		public function startEventShot():void {
			hitOnObstruction = false;
			if (Main.I().board)
				if (Main.I().board.state!="gameOver")
					this.addEventListener(Event.ENTER_FRAME, start, false, 0, true);
		}
		public function start(e:Event):void {	
			if (!Main.I().board)
				return;		
			if (Main.I().board.state=="gameOver")
				return;
			if (Poonga(Main.I().board.poonga).getPoonga().brazos == null)
				return;
			if(Poonga(Main.I().board.poonga).getPoonga().brazos.hitZone) {
				trace (numFrames);
				numFrames++;
				if (numFrames>4) {
					numFrames = 0;
					numShot++;
					if (numShot>3) {
						numShot = 0;
						Main.I().board.poonga.changeWeapon(-1);
					}
					this.removeEventListener(Event.ENTER_FRAME, start);
					return;
				} 
				if (hitOnObstruction)
					return;
				var i:int = Main.I().board.obstructions.numChildren;
				while (i--) {
					var bottle:Bottle = Bottle(Main.I().board.obstructions.getChildAt(i) );
					if(bottle.asset.hitZone.hitTestObject(Poonga(Main.I().board.poonga).getPoonga().brazos.hitZone)) {
						collapseOn(bottle);	
						hitOnObstruction = true;
						Main.I().gameInterface.sumaPuntos(25);
						Main.I().audio.playFx("botellas");
					}
				}
				var container:DisplayObjectContainer = Main.I().board.background.perros;
				i =  container.numChildren;
				while (i--) {
					var perro:MovieClip = MovieClip(Main.I().board.background.perros.getChildAt(i) );
					if(perro.hitTestObject(Poonga(Main.I().board.poonga).getPoonga().brazos.hitZone)) {
						explotion = new assets.Explosion();
						explotion.x = perro.x;
						explotion.y = Main.I().board.background.perros.y+30;
						explotion.scaleX = 3;
						explotion.scaleY = 3;
						checkForExplotion(explotion.x);
						Main.I().board.background.addChild(explotion);
						Main.I().board.background.perros.removeChild(perro);
						var myTimer:Timer = new Timer(600, 1);
						myTimer.addEventListener(TimerEvent.TIMER, endExplotion);
						myTimer.start();
						Main.I().gameInterface.sumaPuntos(70);
						Main.I().audio.playFx("explosion");
					}
				}
				if (!hitOnObstruction)
					if(Main.I().board.patova.hitZone.hitTestObject(Poonga(Main.I().board.poonga).getPoonga().brazos.hitZone)) {
						Main.I().board.patova.hit();
					}
			}
			
		}
		public function checkForExplotion(_x:int):void {
			var chocho:Boolean = false;
			var container:DisplayObjectContainer = Main.I().board.background.cola;
				var i:int = container.numChildren;
				while (i--) {
					var mc:MovieClip = MovieClip(Main.I().board.background.cola.getChildAt(i));
					if (mc.x > _x - 200 && mc.x<_x + 70) {
						var objetoTirado:ObjetoTirado = new ObjetoTirado();
						Main.I().board.background.objetosTirados.addChild(objetoTirado);
						objetoTirado.x = mc.x;
						asustando = true;
						mc.gotoAndStop(2);
						chocho = true;
						}
				}
			if (chocho) {
				var asustaTimer:Timer = new Timer(2000, 1);
				asustaTimer.addEventListener(TimerEvent.TIMER, endAsusta);
				asustaTimer.start();
			}
		}
		public function endAsusta(e:TimerEvent):void {
			asustando = false;
			Main.I().board.background.resetAnimacion();
		}
		private function endExplotion(e:TimerEvent):void {
			if (Main.I().board.background.contains(explotion))
				Main.I().board.background.removeChild(explotion);
		}
		public function end():void {
			
		}
		private function collapseOn(dO:DisplayObject):void {
			Bottle(dO).explote();
		}
	}
}
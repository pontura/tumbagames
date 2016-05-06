package game
{
	import assets.PoongaIdle;
	import assets.PoongaJump
	import assets.PoongaRun;
	import flash.display.MovieClip;
	import flash.events.Event;
	import flash.utils.Timer;
	import flash.events.TimerEvent;
	import flash.geom.Point;
	import flash.utils.setTimeout;
	import assets.PoongaDie;
	
	public class Poonga extends MovieClip
	{
		private var jumpTime		:int = 900;
		private var punga_idle		:assets.PoongaIdle;
		private var punga_jump		:assets.PoongaJump
		private var punga_run		:assets.PoongaRun;
		private var punga_die		:assets.PoongaDie;
		private var direction		:String;
		private var speed			:int = 8;
		public var maxX				:int = 250;
		private var minX			:int = 78;
		private var state			:String;
		private var attackTimer		:Timer;
		private var myTimer			:Timer;
		public var hitZone			:MovieClip;
		private var cabezaState		:String = "headBasic";		
		private var weapons			:Array = ["armsPunch","armsPunch", "armsKnife",  "armsKnife","armsPistol","armsPistol", "armsAk47", "armsAk47", "armsPunga"]
		private var actualWeapon	:int = 0;
		private var weaponState		:String = weapons[actualWeapon];
		public var isWeaponOn		:Boolean = false;
		public var shot				:Shot;
		private var lastColaOn		:MovieClip;
		private var agarroUno		:Boolean;
		
		function Poonga(){
			shot = new Shot();
			punga_idle = new assets.PoongaIdle();
			punga_jump = new assets.PoongaJump();
			punga_run = new assets.PoongaRun();
			punga_die = new assets.PoongaDie();
			
			punga_idle.cabeza.stop();
			punga_idle.brazos.stop();
			
			punga_jump.cabeza.stop();
			punga_jump.brazos.stop();
			
			punga_run.cabeza.stop();
			punga_run.brazos.stop();
			
			idle();		
			this.scaleX = 3;	
			this.scaleY = 3;
			
			this.addEventListener(Event.REMOVED_FROM_STAGE, removedFromStage, false, 0, true);
		}
		public function die():void {
			if (myTimer)
				myTimer.stop();
				
			state = "die";
				
			var _y:int = Math.floor(this.hitZone.y + 32);			
			changeTo("die");						
			Main.I().board.state = "stopped";			
			this.removeEventListener(Event.ENTER_FRAME, runing);			
			if (_y < 0) {
				var poonga:MovieClip = getPoonga();
				poonga.y = _y;
				this.addEventListener(Event.ENTER_FRAME, cae, false, 0, true);
			}
			setTimeout(startAgain,4000);
		}
		private function cae(e:Event):void {			
			var poonga:MovieClip = getPoonga();
			if (poonga.y<0) {
				poonga.y+=6;
			}
		}
		public function startAgain():void {
			this.alpha = 1;
			Main.I().board.state = "playing";
			this.removeEventListener(Event.ENTER_FRAME, cae);
			idle();	
		}
		private function removedFromStage(e:Event):void {
			this.removeEventListener(Event.ENTER_FRAME, runing);
		}
		public function getPoonga():MovieClip {
			return MovieClip(this.getChildAt(0));
		}
		public function hurt():void {
			
			if (Main.I().board.state=="gameOver")
				return;
			Main.I().audio.playFx("golpe");
			this.x -= 4;
			var hurtTimer:Timer = new Timer(1000, 1);
			hurtTimer.addEventListener(TimerEvent.TIMER, resetCabeza);
			hurtTimer.start();
			changeCabezaTo("headCrash");
			Main.I().gameInterface.restaEnergia();
			
		}
		private function resetCabeza(e:TimerEvent):void {
			changeCabezaTo("headBasic");
		}
		private function changeCabezaTo(action:String):void {
			punga_idle.cabeza.gotoAndStop(action);
			punga_jump.cabeza.gotoAndStop(action);
			punga_run.cabeza.gotoAndStop(action);
			this.cabezaState = action;
		}
		public function madHead():void {
			changeCabezaTo("headMad");
			var madTimer:Timer = new Timer(1000, 1);
			madTimer.addEventListener(TimerEvent.TIMER, resetCabeza);
			madTimer.start();
		}
		private function changeWeaponTo(action:String, num:int):void {
			punga_idle.brazos.gotoAndStop(1);
			punga_jump.brazos.gotoAndStop(1);
			punga_run.brazos.gotoAndStop(1);
			if (MovieClip(this.getChildAt(0)).brazos)
				MovieClip(this.getChildAt(0)).brazos.gotoAndStop(action + String(num));
		}
		public function changeWeapon(num:int = 0):void {
			if ((this.actualWeapon == this.weapons.length-1 && num==1) || (this.actualWeapon == 0 && num==-1))
				return;
				
			this.actualWeapon+=num;
			this.weaponState = this.weapons[this.actualWeapon];
			changeWeaponTo (weaponState, 1);
			Main.I().audio.setNewLoop(actualWeapon+1);
			Main.I().gameInterface.changeWeapon(actualWeapon+1);
		}
		public function attack():void {	
			Main.I().audio.playFx(weaponState);
			shot.startEventShot();		
			changeWeaponTo (weaponState , 2);
			isWeaponOn = true;
			if (attackTimer) {
				attackTimer.stop(); 
				attackTimer.removeEventListener(TimerEvent.TIMER, attackEnd);
			}
			attackTimer = new Timer(500, 1);
			attackTimer.addEventListener(TimerEvent.TIMER, attackEnd);
			attackTimer.start();
			
		}
		private function attackEnd(e:TimerEvent):void {
			shot.end();
			changeWeaponTo (weaponState, 1);
			isWeaponOn = false;
		}
		
		public function idle():void {
			this.direction = "";
			if (state=="jump")
				return;
			
			this.removeEventListener(Event.ENTER_FRAME, runing);
			if (state!="idle")
				changeTo("idle");
			else
				return;
			
			if (state=="jump")
				return;			
			state = "idle";
						
		}
		public function jump():void {
			if (state!="jump")
				changeTo("jump");
			else
				return;			
			Main.I().audio.playFx("salto");
			state = "jump";
			if (myTimer) {
				myTimer.stop(); 
				myTimer.removeEventListener(TimerEvent.TIMER, endJump);
			}
			myTimer = new Timer(jumpTime, 1);
			myTimer.addEventListener(TimerEvent.TIMER, endJump);
			myTimer.start();
		}
		private function endJump(e:TimerEvent = null):void {
			state = "endJump";
			changeTo("idle");
		}
		public function run(direction:String):void {
			this.direction = direction;
			this.removeEventListener(Event.ENTER_FRAME, runing);
			this.addEventListener(Event.ENTER_FRAME, runing);
		}
		private function checkForObjetosTiradosCollapse():void {
			if (this.hitZone == null)
				return;
			var i:int = Main.I().board.background.objetosTirados.numChildren;
			while(i--) {
				var objetoTirado:ObjetoTirado = ObjetoTirado(Main.I().board.background.objetosTirados.getChildAt(i));
				if (objetoTirado.hitZone.hitTestObject(this.hitZone)) {
					Main.I().board.background.objetosTirados.removeChild(objetoTirado);					
						
					Main.I().audio.playFx("agarra");
					Main.I().gameInterface.sumaPuntos(15);
					changeWeapon(1);
					if (!agarroUno) {
						agarroUno = true;
					}else {
						
						madHead();
						agarroUno = false;	
					}
				}
			}
		}
		private function runing(e:Event = null):void {
			if (!Main.I().board)
				return;
			if (Main.I().board.state != "playing")
				return;
				
			checkForCola();
			if (Main.I().board.background.objetosTirados.numChildren>0)
				checkForObjetosTiradosCollapse();
			if (direction=="")
				this.removeEventListener(Event.ENTER_FRAME, runing);
			if (state == "idle" || state=="endJump")
				changeTo("run");
			if (state != "jump")
				this.state = "run";		
						
			if (this.x>maxX && direction == "right" ) {
				Main.I().board.scroller.startScrolling();
				return;
			}
			if (this.x<minX && direction == "left" )
				return;
			
			
			switch (direction) {
				case "left":
					this.x -= speed;
					break;
				case "right":
					this.x += speed;
					break;
					
			}
		}
		public function changeTo(text:String):void {
			reset();
			switch(text) {
				case "die":
					addChild(punga_die);
					punga_die.gotoAndPlay(1);
					this.hitZone= null;
					return;
				case "idle":
					addChild(punga_idle);
					this.hitZone= punga_idle.hitZone;
					break;	
				case "jump":
					addChild(punga_jump);
					this.hitZone= punga_jump.hitZone;
					punga_jump.gotoAndPlay(1);
					break;
				case "run":
					addChild(punga_run);
					this.hitZone= punga_run.hitZone;
					break;				
			}
			changeCabezaTo(this.cabezaState);
			if (!isWeaponOn)
				changeWeaponTo (weaponState, 1);
		}
		private function reset():void {
			var i:int = this.numChildren;
			while (i--)
				this.removeChildAt(i);
		}
		private function checkForCola():void {
			if (!this.hitZone)
				return
			var i:int = MovieClip(Main.I().board.background.cola).numChildren;
			if (!shot.asustando)
				while (i--) {
					var tipito:MovieClip = MovieClip(MovieClip(Main.I().board.background.cola).getChildAt(i));
					
					if (tipito.hitTestObject(this.hitZone)) {
						shot.asustando = true;
						tipito.gotoAndStop(2);
						var asustadosTimer:Timer = new Timer(1000, 1);
						asustadosTimer.addEventListener(TimerEvent.TIMER, shot.endAsusta,false, 0, true);
						asustadosTimer.start();
						if (Math.round(Math.random()*3) == 0 ){
							var objetoTirado:ObjetoTirado = new ObjetoTirado();
							Main.I().board.background.objetosTirados.addChild(objetoTirado);
							objetoTirado.x = tipito.x;
						}
							
					}
				}
		}
	}
}
package game
{
	import flash.events.Event;
	import flash.utils.Timer;
	import flash.events.TimerEvent;
	import assets.Patova;
	import game.enemies.Bottle;
	
	//stand_by1
	//run
	//whisky
	//vodka
	//vino
	
	public class Patova extends assets.Patova
	{		
		private var attacks			:Array = ["whisky", "vodka", "vino"];
		private var attacksBoliche	:Array = ["volador1", "volador2"];
		private var attackMode		:String;
		private var myTimer			:Timer;
		private var numHits			:int = 0;
		private var attackType		:String = "simple";
		private var numOfBolicheAttacks	:int = 0;
		
		function Patova(){
			this.scaleX = 3;
			this.scaleY = 3;
		}
		public function reset():void {
			this.gotoAndStop(1);
			if(myTimer)
				myTimer.stop();
		}
		public function attack():void {
			if (Main.I().board.state != "playing") {
				return;
			}
			var mathRand:int
			if (attackType=="simple") {
				mathRand = Math.round(Math.random()*5);
				if (mathRand>2){
					defiende();	
					attackMode = "patada";
				}else {	
					attackMode = attacks[mathRand];
					this.gotoAndStop(attackMode + "1");
				}
			} else
			if (attackType=="boliche") {
				mathRand = Math.round(Math.random()*1);			
				attackMode = attacksBoliche[mathRand];
				numOfBolicheAttacks++;
			}
			if (attackType=="boliche" && numOfBolicheAttacks>5) {
				saleBoliche();
				return
			}
			myTimer = new Timer(1000, 1);
			myTimer.addEventListener(TimerEvent.TIMER, attackNow);
			myTimer.start();
		}
		private function defiende():void {
			this.gotoAndStop("defiende");
		}
		private function patovaCerca():Boolean {
			if (Main.I().board.poonga.x<304)
				return false;
			return true;
		}
		private function checkSiPatea():void {
			if (Main.I().board.state != "playing") {
				return;
			}
			if (!patovaCerca())
				attack()
			else 
			{
				this.gotoAndStop("ataca");
				var patadaTimer:Timer = new Timer(400, 1);
				patadaTimer.addEventListener(TimerEvent.TIMER, patadaEnd);
				patadaTimer.start();
				
			}
		}
		public function hit():void {
			if (attackMode == "hitted")
				return
			Main.I().gameInterface.sumaPuntos(30);
			attackMode = "hitted";
			if (this.attackType == "none")
				return;
			this.gotoAndStop("stand_by2");
			numHits++;
			if (numHits==4) 
				gritaSacado();	
			else if (numHits==8) 
				entraBoliche();	
			else if (numHits==12) 
				gritaSacado();		
			else if (numHits==16) 
				dead();		
			Main.I().gameInterface.patovaresta();	
		}
		private function gritaSacado():void {
			this.gotoAndStop("run3");
			this.attackType = "none";
			myTimer = new Timer(6200, 1);
			myTimer.addEventListener(TimerEvent.TIMER, saliodelBoliche);
			myTimer.start();
			var asustaTimer:Timer = new Timer(1600, 1);
			asustaTimer.addEventListener(TimerEvent.TIMER, asusta);
			asustaTimer.start();			
		}
		private function asusta(e:TimerEvent):void {
			Main.I().audio.grita();
			Main.I().board.poonga.shot.checkForExplotion(this.x-190);
		}
		private function entraBoliche():void {
			this.gotoAndStop("run1");
			this.attackType = "none";
			myTimer = new Timer(2000, 1);
			myTimer.addEventListener(TimerEvent.TIMER, entroAlBoliche);
			myTimer.start();
		}
		private function saleBoliche():void {
			numOfBolicheAttacks = 0;
			this.gotoAndStop("run2");
			this.attackType = "none";
			myTimer = new Timer(2000, 1);
			myTimer.addEventListener(TimerEvent.TIMER, saliodelBoliche);
			myTimer.start();
		}
		private function entroAlBoliche(e:TimerEvent):void {
			this.gotoAndStop("boliche");
			this.attackType = "boliche";
			attack();
 		}
 		private function saliodelBoliche(e:TimerEvent):void {
			this.gotoAndStop(1);
			this.attackType = "simple";
			attack();
 		}
		private function attackNow(e:TimerEvent = null):void { 
			if (Main.I().board.state != "playing") {
				return;
			}
			if (attackType=="simple") {
				if (attackMode == "patada") {
					checkSiPatea();			
				} else if (attackMode=="hitted"){
					myTimer = new Timer(1000, 1);
					myTimer.addEventListener(TimerEvent.TIMER, attackEnd);
					myTimer.start();
				} else {
					this.gotoAndStop(attackMode+"2");
					Main.I().board.newObstruction(attackMode,this);				
					myTimer = new Timer(1000, 1);
					myTimer.addEventListener(TimerEvent.TIMER, attackEnd);
					myTimer.start();
				}
			} else if (attackType=="boliche") {
				Main.I().board.newObstruction(attackMode);
				myTimer = new Timer(1000, 1);
				myTimer.addEventListener(TimerEvent.TIMER, attackEnd);
				myTimer.start();
			}
		}
		private function dead():void {
			if(myTimer)
				myTimer.stop();
			this.gotoAndStop("muerto");
			Main.I().youWin();
 		}
		private function attackEnd(e:TimerEvent):void {	
			attack()			
		}
		private function patadaEnd(e:TimerEvent):void {	
			Main.I().audio.playFx("patada");
			Main.I().board.poonga.hurt();
			attack()			
		}
	}
}
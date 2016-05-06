package
{
	import flash.display.MovieClip;
	import flash.events.Event;
	import flash.utils.setTimeout;
	
	import flashlib.easing.Tween;
	import flashlib.utils.DisplayUtil;
	
	import globals.audio;

	public class Character extends MovieClip
	{
		public var id:int;
		public var direction:int = 0;
		private var mc:MovieClip;
		private var speed:int;
		public var isDead:Boolean
		
		public function Character(id:int, speed:int)
		{
			this.addEventListener(Event.REMOVED_FROM_STAGE, removed);
			this.speed = Math.floor(Math.random()*5) + 5;
			switch(id)
			{
				case 1:
					this.mc = new character1MC();	break;
				case 2:
					this.mc = new character2MC();	break;
				case 3:
					this.mc = new character3MC();	break;
				case 4:
					this.mc = new character4MC();	break;
				case 5:
					this.mc = new character5MC();	break;
				case 6:
					this.mc = new character6MC();	break;
				case 7:
					this.mc = new character7MC(); 	break;
				case 8:
					this.mc = new character8MC(); 	break;
				case 9:
					this.mc = new character9MC(); 	break;
				case 10:
					this.mc = new character10MC(); 	break;
				case 11:
					this.mc = new character11MC(); 	break;
				case 12:
					this.mc = new character12MC(); 	break;
				case 13:
					this.mc = new character13MC(); 	break;
				case 14:
					this.mc = new character14MC(); 	break;
				case 15:
					this.mc = new character15MC(); 	break;
				case 16:
					this.mc = new character16MC(); 	break;
				case 17:
					this.mc = new character17MC(); 	break;
				case 18:
					this.mc = new character18MC(); 	break;
				case 19:
					this.mc = new character19MC(); 	break;
				case 20:
					this.mc = new character20MC(); 	break;
				case 21:
					this.mc = new character21MC(); 	break;
				case 22:
					this.mc = new character22MC(); 	break;
				case 23:
					this.mc = new character23MC(); 	break;
				case 24:
					this.mc = new character24MC(); 	break;
			}
			this.id = id;
			this.mc.gotoAndStop(1)
			
			addChild(mc);
			addEventListener(Event.ENTER_FRAME, enterFrame);
		}
		private function changeDirection():void
		{
			//no siempre cambiar de direccion
			if(this.mc.currentFrame>mc.totalFrames-2 && Math.random()*10<5) return;
			//
			if(direction==0)direction = 1; else direction = 0;			
		}
		private function enterFrame(e:Event):void{
			var _endX:int 
			if(direction==0){
				scaleX=1;
				if(x<Main.I.stageW())
					x+=speed
				else
					exitScreen();
				
			} else {
				scaleX=-1;
				if(x>0)
					x-=speed
				else
					exitScreen();		
			}
		}
		public function shot():void
		{
			mc.nextFrame();
			if(direction==0 && x>Main.I.stageW()/2)
				changeDirection();
			else if(direction==1 && x<Main.I.stageW()/2)
				changeDirection();
			if(mc.currentFrame == mc.totalFrames)
			{
				audio.play ("death")
				removeEventListener(Event.ENTER_FRAME, enterFrame);
				isDead = true;
				setTimeout(die, 2000); 
			}
			else
				hurt();
			if(speed>2) speed--;
		}
		private function hurt():void
		{
			
		}
		private function die():void
		{
			dispose();
		}
		private function exitScreen():void
		{
			Main.I.board.ui.loseLife(1);
			dispose();
			audio.play("escape");
		}
		private function removed(e:*):void
		{
			mc = null;
			trace("_____character dispose");
			removeEventListener(Event.ENTER_FRAME, enterFrame);
			removeEventListener(Event.REMOVED_FROM_STAGE, removed);
		}
		public function dispose():void
		{
			mc = null;
			trace("_____character dispose");
			removeEventListener(Event.ENTER_FRAME, enterFrame);
			removeEventListener(Event.REMOVED_FROM_STAGE, removed);
			DisplayUtil.dispose(this);
		}
	}
}
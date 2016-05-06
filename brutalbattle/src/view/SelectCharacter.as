package view
{
	import com.qb9.flashlib.utils.DisplayUtil;
	
	import flash.events.KeyboardEvent;
	import flash.events.MouseEvent;
	
	import globals.audio;
	
	public class SelectCharacter extends SelectCharacterMC
	{
		public function SelectCharacter()
		{
			audio.loop("BrutalBattleMenu");
			
			Game.I.stage.addEventListener(KeyboardEvent.KEY_DOWN, click);	
			
			this.selector1.next.addEventListener(MouseEvent.CLICK, next1);
			this.selector2.next.addEventListener(MouseEvent.CLICK, next2);
			this.selector1.prev.addEventListener(MouseEvent.CLICK, prev1);
			this.selector2.prev.addEventListener(MouseEvent.CLICK, prev2);
			this.playBtn.addEventListener(MouseEvent.CLICK, start);
			this.help.addEventListener(MouseEvent.CLICK, openHelp);
			this.credits.addEventListener(MouseEvent.CLICK, openCredits);
			
			var rand1:int = Math.ceil(Math.random()*5);
			var rand2:int
			var i:int = 10;
			while(i--)
			{
				var randX:int = Math.ceil(Math.random()*5);
				if(randX!=rand1)
				{
					rand2 = randX;
					break;
				}
			}
			
			selector1.gotoAndStop(rand1);
			selector2.gotoAndStop(rand2);
		}
		private function start(e:MouseEvent):void
		{
			Game.I.stage.removeEventListener(KeyboardEvent.KEY_DOWN, click);	
			audio.stop("buttonhit");
			audio.play("buttonhit");
			reset();			
			Game.I.startGame(selector1.currentFrame, selector2.currentFrame);
			DisplayUtil.dispose(this);
		}
		private function openHelp(e:MouseEvent):void
		{
			audio.stop("buttonhit");
			audio.play("buttonhit");
			this.addChild( new helpScreen );
		}
		private function openCredits(e:MouseEvent):void
		{
			audio.stop("buttonhit");
			audio.play("buttonhit");
			this.addChild( new Credits );
		}
		public function reset():void
		{
			audio.stop("BrutalBattleMenu");
			this.selector1.next.removeEventListener(MouseEvent.CLICK, next1);
			this.selector2.next.removeEventListener(MouseEvent.CLICK, next2);
			this.selector1.prev.removeEventListener(MouseEvent.CLICK, prev1);
			this.selector2.prev.removeEventListener(MouseEvent.CLICK, prev2);
			this.playBtn.removeEventListener(MouseEvent.CLICK, start);
			this.help.removeEventListener(MouseEvent.CLICK, openHelp);
		}
		private function click(e:KeyboardEvent):void{
			switch (e.keyCode){
				case 38:
					next2(null);
					break;
				case 40:
					prev2(null);
					break;
				case 39:
					next1(null);
					break;
				case 37:
					prev1(null);
					break;
				case 32:
					start(null);
					break;
			}
		}
		private function next1(e:MouseEvent):void
		{
			audio.stop("buttonhit");
			audio.play("buttonhit");
			if(selector1.currentFrame == 5)
				selector1.gotoAndStop(1)
			else 
				selector1.nextFrame();
		}
		private function prev1(e:MouseEvent):void
		{
			audio.stop("buttonhit");
			audio.play("buttonhit");
			if(selector1.currentFrame == 1)
				selector1.gotoAndStop(5)
			else 
				selector1.prevFrame();
		}
		private function next2(e:MouseEvent):void
		{
			audio.stop("buttonhit");
			audio.play("buttonhit");
			if(selector2.currentFrame == 5)
				selector2.gotoAndStop(1)
			else 
				selector2.nextFrame();
		}
		private function prev2(e:MouseEvent):void
		{
			audio.stop("buttonhit");
			audio.play("buttonhit");
			if(selector2.currentFrame == 1)
				selector2.gotoAndStop(5)
			else 
				selector2.prevFrame();
		}

	}
}
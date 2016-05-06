package ui
{
	
	import flash.display.MovieClip;
	import flash.events.MouseEvent;
	
	import globals.audio;
	
	public class Gui extends GuiMC
	{
		public var distance:int = 0;
		public var tips:Tips;
		//public var score:Score;
		
		public function Gui()
		{
			this.alpha = 0;
			this.fuckOff.addEventListener(MouseEvent.CLICK, endGameClicked);
		}
		public function start():void
		{
			this.progress1.bar.gotoAndStop(1);
			this.progress2.bar.gotoAndStop(1);
			this.alpha = 1
			this.tips = new Tips();
			addChild(tips)
		}
		public function updateEnershy(id:int, qty:int):void
		{
			var progressBar:MovieClip
			switch(id)
			{
				case 1: progressBar = this.progress1; break;
				case 2: progressBar = this.progress2; break;
			}
			var goto:int = progressBar.bar.currentFrame+qty
			if(goto>100) 
			{	
				goto=100;
				end(id);
			}
			progressBar.bar.gotoAndStop(goto);
		}
		public function startLevel():void
		{			
		}
		public function startPlaying():void
		{
			audio.loop("musica");
		}
		public function end(idLoser:int):void
		{
			var win:Boolean;
			if(idLoser==2)
				win = true;
			Game.I.board.endLevel(win);
			reset();
		}				
		private function endGameClicked(e:MouseEvent):void
		{
			reset();
			Game.I.restart();
			audio.play("buttonhit");
		}
		public function restart():void
		{
			start();
		}
		public function reset():void
		{
			this.alpha = 0;
			
		}

	}
}
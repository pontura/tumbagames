package
{
	import com.qb9.flashlib.tasks.Func;
	import com.qb9.flashlib.tasks.Sequence;
	import com.qb9.flashlib.tasks.TaskRunner;
	import com.qb9.flashlib.tasks.Wait;
	import com.qb9.flashlib.utils.DisplayUtil;
	
	import flash.display.Loader;
	import flash.display.MovieClip;
	import flash.display.Sprite;
	import flash.events.KeyboardEvent;
	import flash.events.MouseEvent;
	import flash.external.ExternalInterface;
	import flash.net.URLRequest;
	import flash.net.navigateToURL;
	import flash.system.Security;
	import flash.system.System;
	import flash.system.fscommand;
	import flash.ui.Mouse;
	import flash.utils.setTimeout;
	
	import org.osmf.media.URLResource;
	
	[SWF(width='800', height='600', backgroundColor='0x000000', frameRate='25')]
	public class Main extends MovieClip
	{
		public static var I:Main; 
		
		//private var framesToLoadTheFukinLoading				:int = 1;
		private var framesToLoadTheFukinLoading				:int = 140;
		public var board:Board;
		public var tasks:TaskRunner;
		private var disclaimer:disclaimerMC;
		private var openingGame:Boolean;
		
		public function Main()
		{
			fscommand("trapallkeys", "True");
			fscommand("fullscreen", "true");
			Mouse.hide();
			
			this.tasks = new TaskRunner(this);
			tasks.start();
			I = this;
			setTimeout(setEvents, 1000);
			
			board = new Board();
			addChild(board);
		}
		
		private function openGame(id:int):void
		{
			var url:String;
						
			switch(id)
			{
				case 1: url = "suicide.bat"; break;
				case 2: url = "insaneImbecileDancing.bat"; break;
				case 3: url = "brutal.bat"; break;
				case 4: url = "subway.bat"; break;
				case 5: url = "PR.exe"; break;
				case 6: url = "MR.exe"; break;
				case 7: url = "eg.bat"; break;
				case 8: url = "WC.bat"; break;
			}
			fscommand("exec", url);
			//navigateToURL(new URLRequest(url), "_self");
		}
		private function KillAll():void
		{
			fscommand("exec", "killAll.bat");
		}
		private function setEvents():void
		{
			stage.addEventListener(KeyboardEvent.KEY_DOWN, keyDownDone);
		}
		private function keyDownDone(e:KeyboardEvent):void{
			if(openingGame) return;
			
			switch (e.keyCode) 	
			{
				case 38:
				case 40:
					break;
				case 37:
					board.slider.go(true);
					break;
				case 39:
					board.slider.go(false);
					break;
				//ESC
				case 27:
					fscommand("quit", "true")
					break;
				
				//salis del Fullscreen con la tecla "¡ / ¿"
				case 221:
					fscommand("fullscreen", "false");
					return;
					break;
				case 13:
					open();
					break;
			}
		}
		private function setOnAgain():void{
			openingGame = false;
			removeChild(disclaimer);
			//fscommand("quit", "true");			
		}
		private function setOff():void{
			visible = false;
		}
		
		private function open():void
		{	
			openingGame = true;
			disclaimer = new disclaimerMC;
			addChild(disclaimer);
			
			Main.I.tasks.add(
				new Sequence(
					new Func(KillAll),
					new Wait(2000),
					new Func(OpenDelay),
					new Wait(5000),
					new Func(setOnAgain)
				)
			)
		}
		private function OpenDelay():void
		{
			openGame(board.slider.activeID);			
		}
		private function fullscreenOff(e:MouseEvent):void
		{
			fscommand("fullscreen", "false");
		}
	}
}

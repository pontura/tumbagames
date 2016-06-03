package {
	import flash.desktop.NativeApplication;
	import flash.desktop.NativeProcess;
	import flash.desktop.NativeProcessStartupInfo;
	import flash.display.MovieClip;
	import flash.events.KeyboardEvent;
	import flash.events.MouseEvent;
	import flash.filesystem.File;
	import flash.filesystem.FileMode;
	import flash.filesystem.FileStream;
	import flash.system.fscommand;
	import flash.utils.setInterval;
	import flash.utils.setTimeout;
	
	public class Arcade extends MovieClip
	{
		public function Arcade()
		{	
			//fscommand("exec",".\\ACQ\\Acq.exe");
			setTimeout(setEvents, 1000);
			//fscommand("fullscreen", "true");
		}
		private function setEvents():void
		{
			stage.addEventListener(KeyboardEvent.KEY_DOWN, checkForEsc);
		}
		private function checkForEsc(e:KeyboardEvent):void{
			//trace((e.keyCode));
			switch (e.keyCode)
			{
				//ESC
				// case 27:
				case 49:
					//trace("QUITTT");
					Quit();
					break;
				
				//salis del Fullscreen con la tecla "¡ / ¿"
				case 221:
					fscommand("fullscreen", "false");
					break;
			}
		}
		
		private function fullscreenOff(e:MouseEvent):void
		{
			fscommand("fullscreen", "false");
		}
		
		public function SaveNewHiscore(prefix:String, hiscore:int):void
		{			
			trace("  ___________SaveToTXT: " + prefix + " hiscore: " + hiscore);
			var f:File = File.applicationDirectory.resolvePath("C:\\tumbagames\\hiscores\\data.txt");		
			
			var str:FileStream=new FileStream();
			str.open(f, FileMode.WRITE);
			str.writeUTFBytes(prefix + "_" + hiscore);			
			str.close();
			
			var myApp:File = File.applicationDirectory.resolvePath("C:\\tumbagames\\hiscores\\tumba_hiscores.exe");
			var myAppProcessStartupInfo:NativeProcessStartupInfo = new NativeProcessStartupInfo();
			var myAppProcess:NativeProcess = new NativeProcess();
			myAppProcessStartupInfo.executable = myApp;
			myAppProcess.start(myAppProcessStartupInfo);
			
			setInterval(Quit, 1000);
			
		}
		public function Quit():void
		{
			NativeApplication.nativeApplication.exit();
		}
	}
}

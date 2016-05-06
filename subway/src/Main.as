package {
	import flash.external.ExternalInterface;
	import flash.display.MovieClip;
	import flash.events.Event;
	
	import flashlib.audio.player.AudioPlayer;
	import flashlib.net.LoadFile;
	import flashlib.net.LoadFileFormat;
	import flashlib.tasks.Func;
	import flashlib.tasks.Parallel;
	import flashlib.tasks.Sequence;
	import flashlib.tasks.TaskEvent;
	import flashlib.utils.DisplayUtil;
	
	import globals.audio;
	
	import gui.IntroScreenLoaded;
	import gui.LoseScreenLoaded;
	import gui.MenuScreen;
	import gui.Settings;
	import gui.WinScreenLoaded;

	[SWF(width='760', height = '460', backgroundColor = '0x000000', frameRate = '12')]
	
	public class Main extends MovieClip
	{
		private var debbuging:Boolean = false;
		private var game:Game;
		public var loseScreenLoaded:LoseScreenLoaded;
		public var winScreenLoaded:WinScreenLoaded;
		public var introScreenLoaded:IntroScreenLoaded;
		
		private static const SOUNDS:Array = ["loop1", "sfx_punguear", "sfx_robaritem", "sfx_scream"];
		
		public function Main()
		{
			loadSounds();
		}
		private function loadSounds():void
		{
			
			audio = new AudioPlayer('sfx/');
			
			var sounds:Parallel = new Parallel();
			for each (var sound:String in SOUNDS)
			sounds.add(audio.register(sound));
			
			sounds.addEventListener(TaskEvent.COMPLETE, loadSettings);
			sounds.start();
		}

		private function loadSettings(e:Event):void
		{		
			var par: Parallel = new Parallel(
				fileTaskFor(settings, 'settings/settings.json')
			)
			if(!debbuging)
			{
				introScreenLoaded = new IntroScreenLoaded();
			
				var introScreen:LoadFile = new LoadFile("intro.swf",LoadFileFormat.SWF, "intro");
				introScreenLoaded.addFile(introScreen);
				par.add(introScreen);
			}
			
			new Sequence(
				par,
				new Func(whenSettingsReady)
			).start();
		}
		private function addMasker():void
		{
			addChild(new MaskerMC);
		}
		private function whenSettingsReady():void
		{
			audio.play("loop1");
			addChild (new MenuScreen(this));
			addMasker();
		}
		public function showIntro():void
		{
			audio.stop("loop1");
			if(!debbuging)
			{
				addChild(introScreenLoaded)
				introScreenLoaded.playAnimation();
				addMasker()
			} else showProgressBar()
				
		}
		public function showProgressBar():void
		{			
			game = new Game(this);
			addChild(game);
			game.showProgressMap();
			loadScreens();	
			addMasker()
		}
		private function fileTaskFor(s:Settings, filename:String):LoadFile
		{
			var ret:LoadFile;

			ret = new LoadFile(filename);
			s.addFile(ret);
			return ret;
		}
		
		private function loadScreens():void
		{
			
			
			loseScreenLoaded = new LoseScreenLoaded();
			
			var loadLoseScreen:LoadFile = new LoadFile("loseScreen.swf",LoadFileFormat.SWF, "loseScreen");
			loseScreenLoaded.addFile(loadLoseScreen);
			
			winScreenLoaded = new WinScreenLoaded();
			
			var loadWinScreen:LoadFile = new LoadFile("win.swf",LoadFileFormat.SWF, "win");
			winScreenLoaded.addFile(loadWinScreen);

			new Sequence(
				new Parallel( loadLoseScreen, loadWinScreen),
				new Func(screensLoaded)
			).start();
			addMasker()
		}
		private function screensLoaded():void
		{
			
		}
		public function refreshGame():void
		{
			ExternalInterface.call("history.go", 0);
		}
	}
}

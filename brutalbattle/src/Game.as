package {
	import com.qb9.flashlib.audio.player.AudioPlayer;
	import com.qb9.flashlib.config.Settings;
	import com.qb9.flashlib.net.LoadFile;
	import com.qb9.flashlib.tasks.Func;
	import com.qb9.flashlib.tasks.Parallel;
	import com.qb9.flashlib.tasks.Sequence;
	import com.qb9.flashlib.tasks.TaskEvent;
	import com.qb9.flashlib.tasks.TaskRunner;
	import com.qb9.flashlib.utils.DisplayUtil;
	
	import flash.display.MovieClip;
	import flash.events.Event;
	import flash.events.KeyboardEvent;
	import flash.system.fscommand;
	import flash.utils.setTimeout;
	
	import globals.audio;
	import globals.locale;
	import globals.settings;
	import globals.stageData;
	
	import ui.Gui;
	import ui.KeyboardPress;
	
	import view.Board;
	import view.CharacterSettings;
	import view.Intro;
	import view.ScreenCharacters;
	import view.SelectCharacter;

	[SWF(width='640', height='480', backgroundColor='0x000000', frameRate='25')]
	
	public class Game extends MovieClip
	{
		public static var I:Game; 
		
		public var gui:Gui;
		public var board:Board;
		
		//private var framesToLoadTheFukinLoading				:int = 1;
		private var framesToLoadTheFukinLoading				:int = 140;
		
		public var keyboardPress		:KeyboardPress;
		
		private static const SOUNDS:Array = 
		[ 
		"kick1", "kick2", "agachar", "jump","gethit1", "gethit2","buttonhit","deathmaster_win",
		"punch1", "punch2",
		"Gimme more", "Are you a homosexual", "I love this shit", "Have no mercy", "Gore is fun",
		"Destroy his spine", "More gore", "Kill or die", "Youre sick", "You suck", "You fight like a homosexual on", "Prepare to die",
		"nefasstoWin2", "musica1", "BrutalBattleMenu", 
		"character1", "character2", "character3", "character4", "character5"
		];

		private var _localizer:Localizer;
		private var loadingAsset:loadingMC;
		private var cs1:CharacterSettings;
		private var cs2:CharacterSettings;
		
		public var taskRunner:TaskRunner;

		public function Game()
		{
			I = this;	
			loading();
		}
		private function loading():void
		{
			this.loadingAsset = new loadingMC();
			addChild(loadingAsset);
			
			loadSounds();
			new DeathmasterStates
			new Nefassto_states
			new Needles_states
			new Pantera_states;
			new Narcommando_states;
		}
		private function loadSounds():void
		{
			audio = new AudioPlayer('sfx/');
			
			var sounds:Parallel = new Parallel();
			for each (var sound:String in SOUNDS)
				sounds.add(audio.register(sound));
			
			sounds.addEventListener(TaskEvent.COMPLETE, whenSoundsReady);
			sounds.start();
		}
		
		private function whenSoundsReady(e:Event):void
		{
			loadSettings();
		}

		private function loadSettings():void
		{
			new Sequence(
				new Parallel(
					fileTaskFor(locale, 'settings/locale.json'),
					fileTaskFor(settings, 'settings/settings.json')
				),
				new Func(whenSettingsReady)
			).start();
		}

		private function fileTaskFor(s:Settings, filename:String):LoadFile
		{
			var ret:LoadFile;

			ret = new LoadFile(filename);
			s.addFile(ret);
			return ret;
		}

		private function whenSettingsReady():void
		{
			this.addEventListener(Event.ENTER_FRAME, checkIfAllReady)
		}
		private function checkIfAllReady(e:Event):void
		{
			if(loadingAsset && loadingAsset.asset.currentFrame>framesToLoadTheFukinLoading)
			{
				this.removeEventListener(Event.ENTER_FRAME, checkIfAllReady)
				DisplayUtil.dispose(loadingAsset);
				init();
			}
		}
		
		
		
		private function init():void
		{
			gui = new Gui();			
			addChild(gui);
			intro();
			
		}
		public function intro():void
		{
			var intro:Intro = new Intro();			
			addChild(intro);
		}
		public function characterSelector():void
		{
			var selectCharacter:SelectCharacter = new SelectCharacter();			
			addChild(selectCharacter);
		}
		
		public function reset():void
		{
			board.reset();
			taskRunner.empty;
			taskRunner.stop();
			taskRunner.dispose();			
		}
		public function winLevel():void
		{
			reset();
			DisplayUtil.dispose(board);
			characterSelector();
		}
		public function restart():void
		{
			audio.stop();
			Game.I.taskRunner.stop();
			reset();
				
			DisplayUtil.dispose(board);
			characterSelector();
		}
		
		public function startGame(id1:int, id2:int):void
		{
			this.taskRunner = new TaskRunner(this);	
			this.taskRunner.start();
			
			cs1 = new CharacterSettings(id1);	
			cs1.id = 1;		
			cs2 = new CharacterSettings(id2);
			cs2.id = 2;
			
			var screenCharacters:ScreenCharacters = new ScreenCharacters(cs1, cs2);			
			addChild(screenCharacters);
			screenCharacters.start();
		}
		public function startBoard():void
		{
			gui.start();
			
			board = new Board(cs1, cs2);			
			addChild(board);
			this.keyboardPress = new KeyboardPress();
			board.x = stageData.width/2;
			setChildIndex(board, 0);
			gui.startLevel();
			taskRunner.start();
			stage.focus = stage;
		}
		public function dispose():void
		{

			_localizer.dispose();
			_localizer = null;

			setTimeout(disposeSounds, 50);
			
			//super.dispose();
		}
		
		private function disposeSounds():void
		{
			if(audio)
			{
				audio.dispose();
				audio = null;
			}
		}
		public function endGame():void
		{
			board.reset();
			gui.reset();
			disposeSounds();
			//close(gui.score.totalPoints);
		}
		
	}
}

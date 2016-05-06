package
{
	
	import flash.display.MovieClip;
	import flash.display.Sprite;
	import flash.display.StageAlign;
	import flash.display.StageDisplayState;
	import flash.display.StageScaleMode;
	import flash.external.ExternalInterface;
	import flash.net.FileReference;
	import flash.net.SharedObject;
	import flash.system.fscommand;
	import flash.utils.setTimeout;
	
	import flashlib.audio.player.AudioPlayer;
	import flashlib.tasks.Parallel;
	import flashlib.tasks.TaskEvent;
	import flashlib.tasks.TaskRunner;
	import flashlib.utils.DisplayUtil;
	
	import globals.audio;

	
	[SWF(width='480', height='600', backgroundColor='0x0000', frameRate='20')]
	
	
	//public class Main extends ArcadePlayer
	public class Main extends MovieClip
	{
		private var loading:LoadingMC;
		public static var I:Main;
		public var board:Board;
		public var tasks:TaskRunner;
		private var keyboardEvents:KeyboardEvents;
		private static const SOUNDS:Array = ["Siren-AirRaid", "mensaje", "SStheme1", "shot1", "shot2", "death", "socolinsky", "MainMenuBtn", "MainMenu", 
		"escape", "suicide1", "suicide2", "suicide3"];
		public var settings:Settings;
		public var hiscore:int;
		public var hiscores:Array = 
			new Array(
				{username:"PON", score:10},
				{username:"AGU", score:9},
				{username:"NEN", score:8},
				{username:"SDF", score:7},
				{username:"GDF", score:6},
				{username:"BSD", score:5},
				{username:"BSD", score:4},
				{username:"BSH", score:3},
				{username:"SDA", score:2},
				{username:"ASD", score:1}
			);
		//public var facebook:Facebook;
		private var so:SharedObject;
		public function Main()
		{
			//fscommand("fullscreen", "true");
			//fscommand("allowscale", "true");
			
			stage.displayState = StageDisplayState.FULL_SCREEN;
			stage.scaleMode = StageScaleMode.EXACT_FIT;
			
			so = SharedObject.getLocal("hiscoresData");
			this.settings = new Settings();
			tasks = new TaskRunner(this);
			I = this;
			loadSounds();
			keyboardEvents = new KeyboardEvents();
		}
		public function stageW():int{
			//150 es el tamaño de facebook
			return stage.stageWidth;
		}
		public function restart():void
		{
			tasks = new TaskRunner(this);
		}
		private function loadSounds():void
		{
			//facebook = new Facebook(tasks, 480);
			//addChild(facebook);
			//facebook.init();
			
			loading = new LoadingMC();
			addChild(loading);
			setChildIndex(loading, 0);
			audio = new AudioPlayer('sfx/');
			
			var sounds:Parallel = new Parallel();
			for each (var sound:String in SOUNDS)
			sounds.add(audio.register(sound));
			
			sounds.addEventListener(TaskEvent.COMPLETE, addIntro);
			sounds.start();
		}
		private function addIntro(e:TaskEvent = null):void
		{
			
			removeChild(loading);
			loading = null;
			this.addChild( new MensajeHigienico );
		}
		public function start(level:int):void
		{
			getHiscores();
			
			board = new Board(level);
			addChild(board);
			setChildIndex(board, 0);
			
			board.start();
			addChild(new maskerMC);
		}
		public function gameOver():void
		{
			//getHiscores();
			
			for (var b:int = 0; b<10; b++)
			{
				trace(b+1 + "________hiscore:" + hiscores[b].username + " score: " + hiscores[b].score);
			}
			hiscore = Main.I.board.ui.points;
			
			
			trace("game Over score: " + hiscore );
			//facebook.saveScore(board.ui.points)
			tasks.stop();
			tasks = null;
			
			board.boom.gotoAndStop(1)
			board.shooter.death();
			
			
			board.reset();	
			board = null;
			
			audio.stop("SStheme1");
			
			
			for (var a:int = 0; a<10; a++)
			{
				if(hiscores[a].score<hiscore)
				{
					addChild( new Hiscore );
					return;
				}
			}
			//ELSE
			addChild( new Summary );
			setTimeout(refreshGame, 3000);
		}
		public function SetNewHiscore(MyUsername:String):void
		{
			trace("SetNewHiscore: " + username + " score: "+ hiscore);
			
			var username:String = "";
			var score:int = 0;
			
			var str:String = "";
			for (var a:int = 0; a<10; a++)
			{
				trace(a + "hiscore:" + hiscores[a].username + " score: " + hiscores[a].score);
				if(username != "")
				{
					var newUsername:String = username;
					var newScore:int = score;
					username = hiscores[a].username;
					score = hiscores[a].score;
					hiscores[a].username = newUsername;
					hiscores[a].score = newScore;
					
				} else
				if(hiscores[a].score<hiscore)
				{
					username = hiscores[a].username;
					score = hiscores[a].score;
					hiscores[a].username = MyUsername;
					hiscores[a].score = hiscore;
				}
				str+=hiscores[a].username + "_" + hiscores[a].score + "*****";
			}
			saveFile(str);
			var rankingLine:Object = {"username":username, "score":hiscore};
			hiscores.push(rankingLine);
			
		}
		private function saveFile(str:String):void
		{
			trace("save: " + str);
			so.data.hiscores = str;
		}
		private function getHiscores():void
		{
			if(so == null || so.data.hiscores == null)
				return;
			trace("SO: " + so.data.hiscores)
			var str:String = so.data.hiscores;
			//PON_10*****asda_10*****AGU_9*****NEN_8*****SDF_7*****GDF_6*****BSD_5*****BSD_4*****BSH_3*****SDA_2*****
			var arr:Array = str.split("*****");
			var id:int = 0;
			for each(var data:String in arr)
			{
				var arr2:Array = data.split("_");
				if(arr2.length >1)
				{
					hiscores[id] = {username: arr2[0], score: arr2[1] };
				}
				id++;
			}
			
		}
		public function refreshGame():void
		{
		//	ExternalInterface.call("history.go", 0);
		}
	}
}
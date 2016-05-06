package view
{ 
	
	
	import com.qb9.flashlib.easing.Tween;
	import com.qb9.flashlib.tasks.Func;
	import com.qb9.flashlib.tasks.Sequence;
	import com.qb9.flashlib.tasks.Wait;
	
	import flash.display.MovieClip;
	import flash.events.Event;
	import flash.events.KeyboardEvent;
	import flash.events.MouseEvent;
	
	import globals.audio;
	import globals.stageData;
	
	public class Board extends MovieClip
	{
		public var state:String = "playing";
		public var paused:Boolean;
		public var character1:Character;
		public var character2:Character;
		public var brain:Brain;
		public var ai:AI;
		public var background:Background;
		private var lastKeyPressed:String;
		private var killAnotherMC:KillAnotherMC
		
		public function Board(cs1:CharacterSettings, cs2:CharacterSettings)
		{	
			this.background = new Background();
			addChild(background);
			background.x = -stageData.width/2;				
			character1 = new Character(cs1);
			character2 = new Character(cs2);
			addChild(character1);
			addChild(character2);
			this.ai = new AI( character2 );
			character1.y = character2.y = 300;
			character1.x = -100;
			character2.x = 100;
			character2.scaleX = -1;
			startLevel();
			this.brain = new Brain();
			brain.start();		
		}
		private function getCharacter(id:int):MovieClip
		{
			switch(id)
			{
				case 1:
					return new character1MC();
				case 2:
					return new character2MC();
				default:
					return new character3MC();
			}
		}
		
		public function startLevel():void
		{
			this.addEventListener(Event.ENTER_FRAME, enterFrame);
			audio.loop("musica1");
			Game.I.gui.tips.addStart();
		}
		public function endLevel(win:Boolean):void
		{
			reset();
			var winner:Character;
			var loser:Character;
			if(win)
			{
				winner = character1
				loser = character2;
				Game.I.gui.tips.addDie();
			} else
			{
				winner = character2
				loser = character1
			}
			
			
			
			loser.die();
			var _x:int = (((stageData.width/2) - winner.x)) - 10;
			var _y:int = -(winner.y*1.2) + (stageData.height/2 + 180);
				
			Game.I.taskRunner.add(
				new Sequence(
					new Wait(2000),
					new Func(winner.celebrate),
					new Tween (this, 5000, {x:_x, y:_y})	,
					new Func(readyToRestart)				
				)
			)
			Game.I.taskRunner.start();
		}
		private function readyToRestart():void
		{
			killAnotherMC =  new KillAnotherMC ;
			Game.I.addChild( killAnotherMC );
			killAnotherMC.addEventListener(MouseEvent.CLICK, clickRestart);
			Game.I.stage.addEventListener(KeyboardEvent.KEY_DOWN, clickRestart);
		}
		private function clickRestart(e:*):void
		{
			//Game.I.exit();
			//return;
			
			
			Game.I.stage.removeEventListener(KeyboardEvent.KEY_DOWN, clickRestart);
			Game.I.removeChild( killAnotherMC );
			killAnotherMC.removeEventListener(MouseEvent.CLICK, clickRestart);
			killAnotherMC = null;
			Game.I.restart();
		}
		public function pause():void
		{
			paused = true;
			Game.I.taskRunner.stop();
		}
		public function unpause():void
		{
			paused = false;
			Game.I.taskRunner.start();
		}
		public function enterFrame(e:Event):void {

			if (Game.I.board.state != "playing")
				return
				
			if (character1.asset.currentFrame==1)
				character1.idle();
			if (character2.asset.currentFrame==1)
				character2.idle();		
				
			brain.checkHit();
			
			
			
				
			if(Game.I.keyboardPress.pressed.length == 0)
			{
				if(character1.state == "walk" || character1.asset.currentFrame==1) 
					character1.idle();				
				if(character2.state == "walk" || character2.asset.currentFrame==1) 
					character2.idle();				
				return;
			}			
			
			for each(var pressed:String in Game.I.keyboardPress.pressed)
			{
				switch (pressed) {
					
					
					
					case "p":
						character2.hitUp();
						break;
					case "l":
						character2.hitDown();
						break;
					case "o":
						character2.defenseUp();
						break;
					case "k":
						character2.defenseDown();
						break;
						
						
						
					case "q":
						character1.hitUp();
						break;
					case "a":
						character1.hitDown();
						break;
					case "s":
						character1.defenseUp();
						break;
					case "w":
						character1.defenseDown();
						break;
					default:
						if(character1.y > character2.y)
							setChildIndex(character2,1);	
						else
							setChildIndex(character1,1);	
							
						if(character1.x > character2.x)
						{
							character1.scaleX = -1;
							character2.scaleX = 1;	
						}
						else
						{
							character2.scaleX = -1;
							character1.scaleX = 1;	
						}
							
						character1.walk(pressed);
						break;
				}
			}
			for (var a:int = 0; a<Game.I.keyboardPress.pressed.length; a++)
			{
				if(Game.I.keyboardPress.pressed[a] == "q" || Game.I.keyboardPress.pressed[a] == "a")
					Game.I.keyboardPress.pressed.splice(a,1);
			}
		}
		public function reset():void
		{
			this.ai.reset();
			this.removeEventListener(Event.ENTER_FRAME, enterFrame);
			pause()
		}
	}
}
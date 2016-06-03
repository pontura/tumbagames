package view
{
	import com.qb9.flashlib.easing.Tween;
	import com.qb9.flashlib.tasks.Func;
	import com.qb9.flashlib.tasks.Sequence;
	
	import flash.display.MovieClip;
	
	import globals.audio;
	
	public class Character extends MovieClip
	{
		public var settings:CharacterSettings;
		public var asset:MovieClip;
		public var state:String;
		public var damage:int = 1;
		private var hitsSequence:int = 0;
		
		public function Character(characterSettings:CharacterSettings)
		{
			this.settings = characterSettings;
			this.asset = settings.character;	
			addChild(asset);
			this.asset.damage = 1;
		}
		public function idle(reset:Boolean = false):void
		{
			if(state == "dead") return;
			this.state = "idle";
			if(asset && state != "dead" &&  state != "celebrate" && asset.currentLabel == "walk" || reset)
			{				
				goto("idle");
				checkPosition();
			}
				
		}
		private function goto(action:String):void
		{
			try {
				asset.gotoAndStop(action);
			}
			catch(error:Error){
				//here you process error
				//show it to user or make LOG
			}
		}
		private function checkIfCanDoAction():Boolean
		{
			if(this.state == "hit" || this.state == "defense"  || this.state == "hurt"  || this.state == "celebrate"  || this.state == "dead")
				return false
			else
				return true
		}
		public function walk(direction:String):void
		{
			if(!checkIfCanDoAction())
				return;
			switch (direction) {
				case "left":
					x -= settings.speed-3;
					break;
				case "right":
					x += settings.speed-3;
					break;
				case "up":
					y -=settings.speed;
					break;
				case "down":
					y +=settings.speed;
					break;
			}			
			checkPosition();
			this.state = "walk";
			if(asset.currentLabel != "walk")
				goto("walk");
		}
		private function checkPosition():void
		{
			if(x<-270) x = -270;		
			else if(x>270) x = 270;
			else if(y<200) y = 200;		
			else if(y>450) y = 450;	
		}
		public function hitDown():void
		{
			if(!checkIfCanDoAction())
				return;
			this.state = "hit";
			goto("hitDown");
			audio.play("kick" + Math.ceil(Math.random()*2));
			if(this.settings.id == 1)
				Game.I.board.score+=1;
		}
		public function hitUp():void
		{
			if(!checkIfCanDoAction())
				return;
			this.state = "hit";
			goto("hitUp");
			audio.play("punch" + Math.ceil(Math.random()*2));
			if(this.settings.id == 1)
				Game.I.board.score+=1;
		}
		public function celebrate():void
		{
			var song:String = "nefasstoWin2";
			if(this.settings.characterId == 2)
				song = "deathmaster_win";
				
			audio.loop(song);
			
			this.state = "celebrate";
			goto("celebrate");
			if(this.settings.id == 1)
				Game.I.board.score+=100;
		}
		public function die():void
		{
			this.state = "dead";
			goto("die");
			if(this.settings.id == 1)
				Game.I.board.score-=10;
		}
		
		public function defenseUp():void
		{
			if(!checkIfCanDoAction())
				return;
			this.state = "defense";
			goto("defenseUp");
			audio.play("agachar");
			if(this.settings.id == 1)
				Game.I.board.score++;
		}
		public function defenseDown():void
		{
			if(!checkIfCanDoAction())
				return;
			this.state = "defense";
			goto("defenseDown");
			audio.play("jump");
			if(this.settings.id == 1)
				Game.I.board.score++;
		}
		public function hit():void
		{
			if(Game.I.gui.progress2.bar.currentFrame>80 && this.settings.id == 1)
				{ Game.I.gui.tips.addWin(); hitsSequence = 0; }
			else if(Game.I.gui.progress1.bar.currentFrame>80 && this.settings.id == 2)
				{ Game.I.gui.tips.addLose(); hitsSequence = 0; }
			hitsSequence++;			
			//si sos vos
			if(hitsSequence>2)
			{
				if(this.settings.id == 1)
				{
					Game.I.gui.tips.addGood();
					Game.I.board.score+=57;
				}
				else
				{
					Game.I.gui.tips.addBad();
					Game.I.board.score-=6;
				}
				hitsSequence = 0;
			}
			//trace (this.settings.id + " seq: " + hitsSequence);
		}
		public function hitted(type:String, qty:int = 1):void
		{
			hitsSequence = 0;
			audio.play("gethit" + Math.ceil(Math.random()*2));
			if(type == "up")
			{				
				goto("hurtUp");
			}else{
				goto("hurtDown");
			}
			this.state = "hurt";
			var _x:int;
			if(this.scaleX == 1) _x=-20; else _x=+20;
			Game.I.taskRunner.add( 
				new Sequence(
					new Tween(this, 600, {x:x+_x},{transition:"easeout"}),
					new Func(idle, true)
					)
			)
			this.damage+=qty;
			this.asset.damage = Math.ceil(damage*4/100);
			Game.I.gui.updateEnershy(this.settings.id, qty);
			
			
		}

	}
}
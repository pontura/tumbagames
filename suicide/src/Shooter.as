package
{
	import flash.events.Event;
	import flash.utils.setTimeout;
	
	import flashlib.utils.DisplayUtil;
	
	import globals.audio;

	public class Shooter extends ShooterMC
	{
		private var lastShoot:String;
		public var paused:Boolean
		private var lastShotAudio:int = 1
		private var state:String = "A";
		
		public function Shooter()
		{
			
		}
 		public function shoot():void
		{
			trace("shootshootshootshootshootshoot");
			if(paused) return
			gotoAndStop("idle");
			
			var num:int = Math.ceil(Math.random()*7);
			
			audio.play("shot" + String(lastShotAudio));
			
			if(lastShotAudio == 2)lastShotAudio--; else lastShotAudio++;
				
			
			var shoot:String = "shot" + state + num;
			if(shoot!=lastShoot)
			{
				lastShoot = shoot;
				gotoAndStop("shot" + state + num);
			} else 
			{
				this.shoot()
			}
		}
		public function hurt():void
		{
			
			Main.I.tasks.stop();
			gotoAndStop("idle");
			var num:int = Math.floor(Math.random()*2)+1;
			gotoAndStop("hit"+ state + num);
			paused = true;
			setTimeout(continuePlaying, 100);
			Main.I.board.ui.loseLife(1);
			audio.play("suicide" + num);
			if( Main.I.board)
				this.state = Main.I.board.ui.lifeBar.state;
		}
		private function continuePlaying():void
		{
			if(currentLabel == "death") return;
			Main.I.tasks.start();
			paused = false;
		}
		public function death():void
		{
			paused = true;
			gotoAndStop("death");			
		}
	}
}
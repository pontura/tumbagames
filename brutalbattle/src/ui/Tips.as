package ui
{
	import flash.events.TimerEvent;
	import flash.utils.Timer;
	
	import globals.audio;
	import globals.locale;
	
	public class Tips extends TipsMC
	{
		private var voices:Object;
		private var timer:Timer;
		private var queue:Array = [];
		private var playing:Boolean;
		private var saidFinalSentence:Boolean;
		
		public function Tips()
		{
			this.voices = locale.voices;
			resetTexts();	
			this.mouseChildren = false;
			this.mouseEnabled = false;		
		}
		public function addStart():void
		{
			var randObj:Object = voices.start[Math.floor(Math.random()*voices.start.length)];
			add(randObj.eng, randObj.esp, randObj.sound);
		}
		public function addGood():void
		{
			var randObj:Object = voices.good[Math.floor(Math.random()*voices.good.length)];
			add(randObj.eng, randObj.esp, randObj.sound);
		}
		public function addBad():void
		{
			var randObj:Object = voices.bad[Math.floor(Math.random()*voices.bad.length)];
			add(randObj.eng, randObj.esp, randObj.sound);
		}		
		public function addLose():void
		{			
			var randObj:Object = voices.lose[Math.floor(Math.random()*voices.lose.length)];
			add(randObj.eng, randObj.esp, randObj.sound);
		}
		public function addDie():void
		{
			if(saidFinalSentence) return;
			
			saidFinalSentence = true;
			
			var randObj:Object = voices.die[Math.floor(Math.random()*voices.die.length)];
			add(randObj.eng, randObj.esp, randObj.sound);
		}
		public function addWin():void
		{
			if(saidFinalSentence) return;
			
			saidFinalSentence = true;
			var randObj:Object = voices.win[Math.floor(Math.random()*voices.win.length)];
			add(randObj.eng, randObj.esp, randObj.sound);
		}
		public function add(id1:String, id2:String, sound:String):void
		{
			audio.gain("musica1", 0.6);
			audio.play(sound);
			queue.push([id1, id2, sound]);
			if(!playing)
				playNext();
		}
		public function playNext():void
		{
			playing = true;
			timer = new Timer(3000);
			timer.addEventListener(TimerEvent.TIMER, resetTexts);
			timer.start();
			this.fields.field1.text = queue[0][0];
			this.fields.field2.text =  queue[0][1];
		}
		
		private function resetTexts(e:TimerEvent = null):void
		{
			audio.gain("musica1", 1);
			playing = false;
			if(timer)
			{
				timer.removeEventListener(TimerEvent.TIMER, resetTexts);
				timer.stop();
			}
			this.fields.field1.text = "";
			this.fields.field2.text = "";
			
			queue.splice(0,1);
			
			if(queue.length>0)
				playNext()
		}

	}
}
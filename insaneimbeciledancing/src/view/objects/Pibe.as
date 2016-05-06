package view.objects
{
	import flash.events.Event;
	import flash.events.TimerEvent;
	import flash.geom.ColorTransform;
	import flash.utils.Timer;
	
	import flashlib.tasks.Sequence;
	import flashlib.tasks.TimeBender;
	import flashlib.tasks.Wait;
	
	import org.osmf.traits.TimeTrait;
	
	import ui.PibeExplotion;
	import ui.PogoGore;

	public class Pibe extends SceneObject
	{
		private var timer:Timer;
		
		public function Pibe(characterID:int)
		{
			
			switch(characterID)
			{
				case 1: mc = new Pibe1; break;
				case 2: mc = new Pibe2; break;
				case 3: mc = new Pibe3; break;
				case 4: mc = new Pibe4; break;
				case 5: mc = new Pibe5; break;
				case 6: mc = new Pibe6; break;
				case 7: mc = new Pibe7; break;
			}
			addChild(mc);
			mc.stop();
			setOff();
			Game.I._ui.totalPibes++;
		}
		public override function setAction (action:String):void
		{
			
			if(action == "collisioned")
			{
				mc.gotoAndStop(2);				
				Game.I._ui.addPibeToPogo();
				addChild( new PogoGore );
				timer = new Timer(1000);
				timer.addEventListener(TimerEvent.TIMER, update);
				timer.start();
			}
		}
		private var sec:int = 0;
		private function update(e:TimerEvent):void
		{
			if(!mc || !Game.I._view) 
			{
				resetTimer();
				return;
			}
			sec++;
			var _sec:Number = 1-(sec/10);
			var c:ColorTransform;
			c = new ColorTransform(1,_sec,_sec,1,0,0,0);
			mc.transform.colorTransform=c;
			if(sec>9)
			{
				var pibeExplotion:PibeExplotion = new PibeExplotion(x, y);
				Game.I._view.bg.addChild( pibeExplotion );

				reset();
			}
		}
		private function resetTimer():void
		{
			if(timer)
			{
				timer.stop();
				timer.removeEventListener(TimerEvent.TIMER, update);
			}
		}
		public override function reset(e:Event = null):void
		{
			resetTimer()
			super.reset();
		}
		private function setOff():void
		{
			var c:ColorTransform
			c = new ColorTransform(1,1,1,1,0,0,0);
			//c = new ColorTransform(0.2,0.2,1,1,0,0,0);
			mc.transform.colorTransform=c;
			mc.gotoAndStop(1);
		}
	}
}
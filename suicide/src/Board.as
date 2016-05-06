package
{
	import flash.display.MovieClip;
	import flash.events.Event;
	import flash.events.TimerEvent;
	import flash.utils.Timer;
	
	import flashlib.tasks.Func;
	import flashlib.tasks.Loop;
	import flashlib.tasks.Sequence;
	import flashlib.tasks.Task;
	import flashlib.tasks.Wait;
	import flashlib.utils.DisplayUtil;
	
	import globals.audio;

	public class Board extends BoarMC
	{
		private var _rows:Rows;
		private var sec:int = 0;
		public var shooter:Shooter;
		public var ui:Ui;
		private var timer:Timer;
		private var level:int;
		private var task:Task;
		private var seq:Sequence;
		
		public function Board(level:int)
		{
			this.level = level;
			//if(level == 1)
				audio.loop("SStheme1");
			gotoAndStop(level)
		}
		public function start():void
		{
			this.addEventListener(Event.REMOVED_FROM_STAGE, removed);
			if(timer)
			{
				timer.stop();
				timer.removeEventListener(TimerEvent.TIMER, update);
				timer = null;
			}
			timer = new Timer(300);
			timer.addEventListener(TimerEvent.TIMER, update);
			timer.start();
			this._rows = new Rows(this.rows, level);
			this.rows.addChild(_rows)
			task = new Loop(
				new Sequence(
						new Func (addWave),
						new Wait(1000)
					)
			)
			Main.I.tasks.add( task );
			Main.I.tasks.start();
			shooter = new Shooter();
			addChild(shooter);
			shooter.x = Main.I.stageW()/2;
			shooter.y = Main.I.stage.stageHeight;
			ui = new Ui();
			addChild(ui);
		}
		
		private var second:int = 0;
		private var lastSecond:int;
		private function update(e:TimerEvent):void
		{
			second++;
		}
		
		private function addWave():void
		{
			sec++;
			_rows.addGuys(Math.ceil(Math.random()*(sec/12)));
		}
		private function showShootImage(violent:Boolean):void
		{
			var frame:int = 2;
			var boom_duration:int = 100;
			if(violent)
			{
				frame = Math.ceil(Math.random()*this.boom.totalFrames-1)+1;
				boom_duration = 200;
			}
			seq = new Sequence(
				new Func(doBoom, frame),
				new Wait(boom_duration),
				new Func(doBoom, 1)
			);
			if(Main.I.tasks)
			{
			Main.I.tasks.add(
				seq
				)
			}
		}
		private function doBoom(frame:int):void
		{
			this.boom.gotoAndStop(frame);
		}
		public function shot():void
		{
			if(shooter.paused)
				return;
			_rows.shootTheLastOne();
			if(shooter)
				shooter.shoot();
			
			if(second == lastSecond)
				showShootImage(true);
			else 
				showShootImage(false);
			lastSecond = second;
		}
		private function removed():void 
		{
			Main.I.tasks.remove(seq);
			if(Main.I.tasks)
			{
				Main.I.tasks.stop();
			}
			reset();
		}
		public function reset():void{
			
			if(Main.I.tasks)
			{
				if(seq)
				{
					Main.I.tasks.remove(seq);
				}
			}
			if(seq)
			{
				seq.stop();
				seq = null;
			}
			this.removeEventListener(Event.REMOVED_FROM_STAGE, removed);
			if(timer)
			{
				timer.stop();
				timer.removeEventListener(TimerEvent.TIMER, update);
				timer = null;
			}
			if(task)
			{
				task.stop();
				task = null;
			}
			if(_rows)
			{
				_rows = null;
			}
			if(shooter)
			{
				DisplayUtil.dispose(shooter);
				shooter = null;
			}
			DisplayUtil.dispose(this);
		}
		
	}
}
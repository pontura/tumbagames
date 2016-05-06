package
{
	import flash.display.MovieClip;
	import flash.events.KeyboardEvent;
	import flash.utils.setTimeout;
	
	import flashlib.tasks.Func;
	import flashlib.tasks.Loop;
	import flashlib.tasks.Sequence;
	import flashlib.tasks.Wait;
	import flashlib.utils.DisplayUtil;
	
	import globals.audio;

	public class StageSelector extends StageSelectorMC
	{
		private var loop:Loop;
		
		public function StageSelector()
		{
			gotoAndStop(Math.ceil(Math.random()*totalFrames))
			loop = new Loop(
				new Sequence(
					new Wait(400),
					new Func(next)
				)
			);
			Main.I.tasks.add(loop)
			Main.I.tasks.start();
			Main.I.stage.addEventListener(KeyboardEvent.KEY_DOWN, keyDown);
		}
		private function next():void
		{
			
			bg.play();
			if(currentFrame==totalFrames)
				gotoAndStop(1)
			else nextFrame();
		}
		private function keyDown(e:KeyboardEvent):void
		{
			Main.I.stage.removeEventListener(KeyboardEvent.KEY_DOWN, keyDown);
			//audio.play("suicide1");
			loop.stop();
			go();
		}
		private function go():void
		{
			Main.I.addChild( new StagePresentation(this.currentFrame) );
			DisplayUtil.dispose( this );
		}
	}
}
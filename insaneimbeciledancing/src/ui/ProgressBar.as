package ui
{
	import com.qb9.flashlib.tasks.Func;
	import com.qb9.flashlib.tasks.Loop;
	import com.qb9.flashlib.tasks.Sequence;
	import com.qb9.flashlib.tasks.Task;
	import com.qb9.flashlib.tasks.Wait;
	import com.qb9.flashlib.utils.DisplayUtil;
	
	import flash.display.DisplayObject;
	import flash.display.DisplayObjectContainer;
	import flash.utils.Timer;
	
	import view.droppableObjects.DroppableObject;

	public class ProgressBar extends ProgressBarMC
	{
		private var delay:Number = 0.05;
		private var task:Task;
		private var target:DroppableObject;
		
		public function ProgressBar(target:DroppableObject)
		{
			this.target = target;
			target.addChild(this);
			this.y -= 40;
			start();
		}
		public function start():void
		{
			task = new Task();
			task = 
				new Loop(
					new Sequence(
						new Func(countDown),
						new Wait(500)
						)	
					)		
			Main.I.tasks.add (task);
		}
		private function countDown():void
		{
			if(masker.scaleX < delay)
			{
				task.stop();
				task.dispose();
				target.activate();
				task = null;
				DisplayUtil.dispose(this);
			} else
				masker.scaleX -= delay;
		}
	}
}
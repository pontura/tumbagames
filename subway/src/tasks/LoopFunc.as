package tasks
{
	import flashlib.tasks.ITask;
	import flashlib.tasks.Task;

	public class LoopFunc extends Task
	{
		private var func:Function;

		public function LoopFunc(f:Function):void
		{
			func = f;
			super();
		}

		override public function update(m:uint):void
		{
			if(func.length)
				func.apply(null, [m]);
			else
				func.apply(null);
		}

		override public function clone():ITask
		{
			return new LoopFunc(func);
		}
	}
}

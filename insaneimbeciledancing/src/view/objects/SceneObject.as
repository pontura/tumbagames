package view.objects
{
	import flash.display.MovieClip;
	import flash.events.Event;
	
	import flashlib.tasks.Task;
	import flashlib.utils.DisplayUtil;

	public class SceneObject extends MovieClip
	{
		public var state:String;
		public var mc:MovieClip;
		public var task:Task;
		private var lastX:int = 0;
		public var canMove:Boolean = true;
		public var isOff:Boolean;
		
		
		public function SceneObject()
		{
			/*scaleX = 0.5;
			scaleY = 0.5;*/
		}
		public function addTask(_task:Task):void
		{
			if(!mc) return;
			if(this.task) 
			{
				this.task.stop();
				Game.I.tasks.remove(this.task)
				this.task = null;	
			}
			this.task = _task;
			Game.I.tasks.add(
				task
			)
		}
		public function setView(_x:int):void
		{
			if(lastX==_x) return;
			
			if(lastX>_x)
				lookAt(false)
			else
				lookAt(true)
			
			lastX = _x
		}
		public function lookAt(left:Boolean):void{
			if(left) mc.scaleX = -1 else mc.scaleX = 1;
		}
		public function collisioned(so:SceneObject):void
		{
			if(state == "collisioned") 
				return;
			state = "collisioned";
			setAction("collisioned");
			collisionedWith(so);
		}
		public function collisionedWith(so:SceneObject):void
		{
			
		}
		public function idle():void
		{
			if(state == "idle") 
				return;
			setAction("idle");
		}
		public function setAction (type:String):void
		{
			state = type;
		}
		public function reset(e:Event = null):void
		{
			if(isOff)	return;
			isOff = true;
			Game.I._view.objects.destroy(this);
			if(task)
			{
				task.stop();
				task = null;
			}
			this.mc = null;
			DisplayUtil.dispose( this );			
		}
	}
}
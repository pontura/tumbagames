package virtual
{
	import flash.events.Event;

	public class CollisionEvent extends Event
	{
		public var objects:Array = [];
		
		public function CollisionEvent(type:String, objects:Array)
		{
			this.objects = objects;
			super(type);			
		}
	}
}
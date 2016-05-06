package input.events
{
	import flash.events.Event;

	public class KeyEvent extends Event
	{
		static public const DOWN:String = 'keyDown';
		static public const UP:String = 'keyUp';

		private var _key:int;

		public function KeyEvent(type:String, code:int):void
		{
			super(type);

			_key = code;
		}

		public function get key():int
		{
			return _key;
		}
	}
}

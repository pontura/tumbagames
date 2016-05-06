package input
{
	import flash.display.Stage;
	import flash.events.EventDispatcher;
	import flash.events.KeyboardEvent;
	
	import flashlib.utils.ArrayUtil;
	
	import input.events.KeyEvent;

	public class Controls extends EventDispatcher
	{
		static public const LEFT:int = 37;
		static public const UP:int = 38;
		static public const RIGHT:int = 39;
		static public const DOWN:int = 40;
		static public const SPACE:int = 32;

		private var _listenTo:Array = [LEFT, RIGHT, SPACE];
		private var _pressed:Object;
		private var _stage:Stage;
		private var _locked:Boolean;

		public function Controls(stage:Stage):void
		{
			_stage = stage;

			init();
		}

		private function init():void
		{
			_locked = false;

			_pressed = {};

			_stage.addEventListener(KeyboardEvent.KEY_UP, onUp);
			_stage.addEventListener(KeyboardEvent.KEY_DOWN, onDown);
		}

		public function lock():void
		{
			_locked = true;
		}

		public function unlock():void
		{
			_locked = false;
		}

		public function isDown(code:int = 0):Boolean
		{
			return !_locked && code in _pressed;
		}
		public var keyIsDown:Boolean;
		private function onDown(e:KeyboardEvent):void
		{
			if(e.keyCode != 37 || e.keyCode != 39)
			{
				keyIsDown = true
			}
			if(!_stage)
				return;

			if(isDown(e.keyCode) || !ArrayUtil.contains(_listenTo, e.keyCode))
				return;

			if(!_locked && isDown(other(e.keyCode)))
				dispatchEvent(new KeyEvent(KeyEvent.UP, other(e.keyCode)));

			if(!_locked)
				dispatchEvent(new KeyEvent(KeyEvent.DOWN, e.keyCode));
			_pressed[e.keyCode] = true;
		}

		private function onUp(e:KeyboardEvent):void
		{
			if(e.keyCode != 37 || e.keyCode != 39)
			{
				keyIsDown = false
			}
			if(!ArrayUtil.contains(_listenTo, e.keyCode))
				return;

			if(!_locked)
				dispatchEvent(new KeyEvent(KeyEvent.UP, e.keyCode));

			if(!_locked && isDown(other(e.keyCode)))
				dispatchEvent(new KeyEvent(KeyEvent.DOWN, other(e.keyCode)));
			if(e.keyCode in _pressed)
				delete _pressed[e.keyCode];
		}

		private function other(code:int):int
		{
			if(code == UP)
				return DOWN;
			return UP;
		}

		public function dispose():void
		{
			_pressed = null;

			_stage.removeEventListener(KeyboardEvent.KEY_UP, onUp);
			_stage.removeEventListener(KeyboardEvent.KEY_DOWN, onDown);
			_stage = null;
		}
	}
}

package 
{
	import flash.display.Sprite;
	import flash.events.Event;
	import flash.text.TextField;
	import com.qb9.flashlib.utils.DisplayUtil;
	import com.qb9.flashlib.lang.foreach;
	import com.qb9.flashlib.lang.filter;
	import flash.display.DisplayObject;
	import flash.display.DisplayObjectContainer;
	import com.qb9.flashlib.interfaces.IDisposable;

	public class Localizer implements IDisposable
	{
		private var _container:Sprite;
		private var _textsSource:Object;

		public function Localizer(container:Sprite, source:Object):void
		{
			_container = container;
			_textsSource = source;

			init();
		}

		private function init():void
		{
			_container.addEventListener(Event.ADDED, onAdded);
		}

		private function onAdded(e:Event):void
		{
			var t:Object = e.target;

			if(t is DisplayObjectContainer)
				handleAddedContainer(t as DisplayObjectContainer);
			else if(t is TextField)
				tryToSetText(t as TextField);
		}

		private function handleAddedContainer(d:DisplayObjectContainer):void
		{
			var texts:Array;

			texts = filter(DisplayUtil.children(d, true).concat([d]), isTextField);
			foreach(texts, tryToSetText);
		}

		private function tryToSetText(t:TextField):void
		{
			var text:String;

			if(t.name in _textsSource)
				text = _textsSource[t.name];
			else if(t.parent.name in _textsSource)
				text = _textsSource[t.parent.name];
			else
				return;

			t.text = text.toUpperCase();
		}

		private function isTextField(d:DisplayObject):Boolean
		{
			return d is TextField;
		}

		public function dispose():void
		{
			_container.removeEventListener(Event.ADDED, onAdded);
		}
	}
}

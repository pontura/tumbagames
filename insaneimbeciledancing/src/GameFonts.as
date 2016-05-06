package 
{
	import adobe.utils.XMLUI;
	
	import flash.xml.XMLDocument;
	
	import flashlib.net.LoadFile;
	import flashlib.tasks.TaskEvent;
	import flashlib.utils.ObjectUtil;
	
	public dynamic class GameFonts
	{
		protected var decode:Function;
		public function GameFonts(decoder:Function = null):void
		{
			this.decode = decoder;
		}
		
		protected function fileLoaded(e:TaskEvent):void
		{
			var file:LoadFile = e.target as LoadFile;
			file.removeEventListener(e.type, fileLoaded);
			feed(file.data);
		}
		
		public function addFile(file:LoadFile):void
		{
			if (file.loaded)
				feed(file.data);
			else
				file.addEventListener(TaskEvent.COMPLETE, fileLoaded);
		}
		
		public function feed(obj:Object):void
		{			
			
			var xml:XML = new XML(obj);
			ObjectUtil.copy(xml, this);
		}
	}
}
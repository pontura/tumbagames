package gui
{
	import flash.display.MovieClip;
	import flash.events.Event;
	import flash.events.KeyboardEvent;
	import flash.events.MouseEvent;
	import flash.utils.clearInterval;
	import flash.utils.setInterval;
	import flash.utils.setTimeout;
	
	import flashlib.net.LoadFile;
	import flashlib.tasks.TaskEvent;
	import flashlib.utils.DisplayUtil;
	import flashlib.utils.ObjectUtil;
	

	public class IntroScreenLoaded extends MovieClip
	{
		public var asset:MovieClip;
		private var id:int;
		
		public function IntroScreenLoaded()
		{	
		}
		public function addFile(file:LoadFile):void
		{
			if (file.loaded)
				feed(file.data);
			else
				file.addEventListener(TaskEvent.COMPLETE, fileLoaded);
		}
		protected function fileLoaded(e:TaskEvent):void
		{
			var file:LoadFile = e.target as LoadFile;
			file.removeEventListener(TaskEvent.COMPLETE, fileLoaded);
			feed(file.data);
		}
		private function feed(obj:Object):void
		{
			asset = MovieClip(obj);
			asset.stop();
			asset.mc.stop();
		}
		public function playAnimation():void
		{	
			addChild(asset);
			asset.mc.play();
			id = setInterval(end, 20000);
			//this.addEventListener(MouseEvent.CLICK, end);
			//Main(parent).stage.addEventListener(KeyboardEvent.KEY_DOWN, end);
		}
		public function end(e:* = null):void
		{
			//Main(parent).stage.removeEventListener(KeyboardEvent.KEY_DOWN, end);
			//removeEventListener(MouseEvent.CLICK, end);
			clearInterval(id);
			asset.mc.gotoAndStop(1);
			if(MovieClip(parent))
			{
				Main(parent).showProgressBar();
				DisplayUtil.dispose(this);
			}
		}
	}
}
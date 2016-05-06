package gui
{
	import flash.display.MovieClip;
	
	import flashlib.net.LoadFile;
	import flashlib.tasks.TaskEvent;
	import flashlib.utils.ObjectUtil;
	

	public class LoseScreenLoaded extends MovieClip
	{
		public var asset:MovieClip;
		
		public function LoseScreenLoaded()
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
		}
		public function playAnimation():void
		{	
			if(!asset) 
			{
				end();  
				return;
			}
       			asset.mc.play();   
		}
		private function end():void
		{
			asset.stop();
			MovieClip(asset.parent).finishAnimation();
		}
	}
}
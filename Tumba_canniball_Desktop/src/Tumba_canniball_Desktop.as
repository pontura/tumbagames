package  {
	
	import flash.display.Loader;
	import flash.display.MovieClip;
	import flash.display.Sprite;
	import flash.events.Event;
	import flash.events.MouseEvent;
	import flash.filesystem.File;
	import flash.net.FileReference;
	import flash.net.URLRequest;
	import flash.system.ApplicationDomain;
	import flash.system.LoaderContext;
	import flash.system.SecurityDomain;
	import flash.utils.setInterval;
	
	[SWF(width='800', height='600', backgroundColor='0x000000', frameRate='25')]
	public class Tumba_canniball_Desktop extends Sprite {
		
		private var loader:Loader;
		private var mainSWF:MovieClip = new MovieClip();
		
		public function Tumba_canniball_Desktop() {
			
			addEventListener(Event.ADDED_TO_STAGE, initialise);
			
		}
		public function initialise(e:Event):void
		{
			var ldr:Loader = new Loader();
			var lc:LoaderContext = new LoaderContext(false, ApplicationDomain.currentDomain);
			lc.allowCodeImport = true;
			ldr.load(new URLRequest("Game.swf"), lc);
		
		}
		function stopLoadedContent(e:Event):void
		{
			trace("SS");
		}
		public function viewPreview(e:Event):void
		{
			trace("viewPreview");
			//addChild(mainSWF);
			//mainSWF.addChild(loader);
		}
	}
}
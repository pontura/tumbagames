package ui
{
	import flash.display.Loader;
	import flash.display.MovieClip;
	import flash.events.ErrorEvent;
	import flash.events.Event;
	import flash.net.URLRequest;
	import flash.utils.setTimeout;
	
	import flashlib.tasks.Func;
	import flashlib.tasks.Loop;
	import flashlib.tasks.Sequence;
	import flashlib.tasks.Wait;
	import flashlib.utils.DisplayUtil;
	
	import mx.core.MovieClipAsset;

	public class Show
	{
		public var mc:MovieClip;
		
		public function Show()
		{
			var mLoader:Loader = new Loader();
			var mRequest:URLRequest = new URLRequest("show.swf");
			mLoader.contentLoaderInfo.addEventListener(Event.COMPLETE, onCompleteHandler);
			mLoader.contentLoaderInfo.addEventListener(ErrorEvent.ERROR, onError);
			mLoader.load(mRequest);
		}
		private function onError(e:ErrorEvent):void
		{
			
		}
		private function onCompleteHandler(e:Event):void
		{
			mc = MovieClip(e.currentTarget.content);
			Game.I.addChild(mc);
			
			mc.x = 180;
			Game.I.start();
			mc.scaleX = mc.scaleY = 0.66
			mc.y = 350;
			Game.I.addEventListener(Event.ENTER_FRAME, update);
		}
		private function update(e:Event):void
		{
			var show:MovieClip = MovieClip(mc.getChildAt(0));
			if(show.currentFrame == show.totalFrames)
			{
				Game.I.endGame();
			}
		}
		public function startPlaying():void
		{
			Game.I.addChild(mc)
			play()
		}
		public function stop():void
		{
			var show:MovieClip = MovieClip(mc.getChildAt(0));
			show.gotoAndStop(1);			
		}
		public function play():void
		{
			var show:MovieClip = MovieClip(mc.getChildAt(0));
			show.play();
		}
		public function rewind():void
		{
			var show:MovieClip = MovieClip(mc.getChildAt(0));
			Game.I.tasks.add(
				new Sequence(
					new Loop(
						new Sequence(
							new Func (reverse, show),
							new Wait (1000/35)
						),100
					),
					new Func(show.play)
				)
			)
		}
		private function reverse(show:MovieClip):void
		{
			var fr:int = show.currentFrame;
			fr--
			show.gotoAndStop(fr);
		}
		public function reset():void
		{
			mc.stop();
			DisplayUtil.dispose(mc);
		}
		public function hide():void{
			this.mc.visible = false;
		}
		public function show():void{
			this.mc.visible = true;
		}
	}
}
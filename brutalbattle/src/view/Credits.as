package view
{
	import com.qb9.flashlib.utils.DisplayUtil;
	
	import flash.display.Shape;
	import flash.events.MouseEvent;
	
	import globals.audio;
	import globals.stageData;
	
	public class Credits extends CreditsMC
	{
		public function Credits()
		{
			var shape:Shape = new Shape();
			addChild(shape)
			shape.graphics.beginFill(0x000);
			shape.graphics.drawRect(0,0,stageData.width, stageData.height);
			shape.graphics.endFill();
			setChildIndex(shape, 0);
			this.fuckOff.addEventListener(MouseEvent.CLICK, close);
		}
		private function close(e:MouseEvent):void
		{
			audio.play("buttonhit");
			reset();
			DisplayUtil.dispose(this);
		}
		public function reset():void
		{
			this.fuckOff.removeEventListener(MouseEvent.CLICK, close);
		}

	}
}
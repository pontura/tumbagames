package clock
{
	import flash.display.DisplayObject;
	import flash.display.MovieClip;
	
	import spriteSheet.SpriteText;

	public class ClockView extends MovieClip
	{
		public var c_min:ClockSlot;
		public var c_tenSec:ClockSlot;
		public var c_sec:ClockSlot;
		
		public function ClockView()
		{
			c_min = new ClockSlot;
			var st:SpriteText = new SpriteText();
			c_tenSec = new ClockSlot;
			c_sec = new ClockSlot;
			
			addSlot(st.createBitmap("0"));
			addSlot(c_min);
			addSlot(st.createBitmap(":"));
			addSlot(c_tenSec);
			addSlot(c_sec);
		}
		private function addSlot(slot:DisplayObject):void
		{
			slot.x += this.width;
			addChild(slot);
		}
	}
}
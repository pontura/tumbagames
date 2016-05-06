package clock
{
	import flash.display.Bitmap;
	import flash.display.MovieClip;
	import flash.text.TextField;
	import flash.text.TextFieldAutoSize;
	
	import spriteSheet.SpriteText;

	public class ClockSlot extends MovieClip
	{
		private var value:int = 9;
		
		public function ClockSlot()
		{
			setValue(0);
		}
		public function setValue(value:int):void
		{
			if(this.value == value) return;
			this.value = value;
			reset();
			var st:SpriteText = new SpriteText();
			addChild( st.createBitmap(String(value)) );			
		}
		public function getValue():int
		{
			return value;
		}
		private function reset():void
		{
			if(numChildren>0)
				removeChildAt(0);
		}
	}
}
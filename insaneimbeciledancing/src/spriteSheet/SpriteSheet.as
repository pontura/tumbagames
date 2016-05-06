package spriteSheet
{
	import flash.display.Bitmap;
	import flash.display.BitmapData;
	import flash.display.Sprite;
	import flash.events.Event;
	import flash.geom.Point;
	import flash.geom.Rectangle;
	
	public class SpriteSheet extends Sprite 
	{   
		private var tileSheetBitmapData:BitmapData;
		private var canvasBitmapData:BitmapData;
		
		public function SpriteSheet(tileSheetBitmap:Bitmap) 
		{   
			tileSheetBitmapData = tileSheetBitmap.bitmapData;
		}
		
		public function drawTile(_x:int, _y:int, _width:int, _height:int):BitmapData
		{
			var tileRectangle:Rectangle = new Rectangle(_x, _y, _width, _height);
			canvasBitmapData = new BitmapData(_width, _height, true);			
			canvasBitmapData.copyPixels(tileSheetBitmapData, tileRectangle, new Point(0, 0));			
			return canvasBitmapData.clone();
		}
		
		public function remove(e:Event):void
		{
			tileSheetBitmapData.dispose();
			canvasBitmapData.dispose();
		}
	}
}
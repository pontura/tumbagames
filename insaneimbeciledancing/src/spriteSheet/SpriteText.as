package spriteSheet
{
	import flash.display.Bitmap;

	public class SpriteText
	{
		public function createBitmap(str:String):Bitmap
		{
			var bitmap:Bitmap;
			for each (var obj:Object in fonts[0].character)
			{ 
				if (String(obj.@key) == String(String(str).charCodeAt(0)))
				{
					var _x:int = obj.x;
					var _y:int = obj.y;
					var _w:int = obj.width;
					var _h:int = obj.height;
					bitmap = new Bitmap( Game.I.spriteSheet.drawTile(_x,_y,_w,_h) );
					trace(obj.@key + ":   " + _x+ " - " + _y+ " - " + _w+ " - " + _h);
					return bitmap;
				}
			}
			return bitmap;
		}
	}
}
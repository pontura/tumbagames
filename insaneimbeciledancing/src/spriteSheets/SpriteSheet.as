package spriteSheets  {	
	import flash.display.Sprite;
	import starling.display.Sprite;
	import starling.core.Starling;
	
	public class Spritesheet extends Sprite 
	{
		private var myStarling:Starling;		
		public function Spritesheet() 
		{
			//Create a new instance of starling 
			myStarling = new Starling(SpriteSheetExample,stage);
			myStarling.start();
		}
	}	
}
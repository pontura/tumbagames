package spriteSheets  {
	import starling.display.Sprite;
	import starling.events.Event;
	import starling.core.Starling;
	import starling.textures.TextureAtlas;
	import starling.textures.Texture;
	import starling.display.MovieClip;
	import flash.display.Bitmap;
	
	public class SpriteSheetExample extends Sprite
	{
		[Embed(source="assets/pibes.xml",mimeType="application/octet-stream")]
		private var AnimData:Class;
		[Embed(source="assets/pibes.png")]
		private var AnimTexture:Class;		
		
		public function SpriteSheetExample() 
		{
			super();	
			this.addEventListener(Event.ADDED_TO_STAGE, initialize);
		}
		protected function initialize(e:Event):void
		{
			var hungryHeroTexture:Texture = Texture.fromBitmap(new AnimTexture());
			var hungryHeroXmlData:XML = XML(new AnimData());
			var hungryHeroTextureAtlas:TextureAtlas = 
				new TextureAtlas(hungryHeroTexture, hungryHeroXmlData);
			//Fetch the sprite sequence form the texture using their name
			var _mc:MovieClip = new MovieClip(hungryHeroTextureAtlas.getTextures("pibe1a"), 20);		 
			addChild(_mc);
			Starling.juggler.add(_mc);
		}		
	}	
}
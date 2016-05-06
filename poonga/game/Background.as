package game
{
	import flash.display.Sprite;
	import assets.Background1;
	import flash.display.MovieClip;

	public class Background extends Sprite
	{
		public var perros			:MovieClip;
		public var cola				:Cola;
		public var objetosTirados	:MovieClip
		
		function Background(num:int){			
			var background1:Background1 = new assets.Background1();
			addChild(background1);
			this.cola = new Cola();
			background1.ph_cola.addChild(cola);
			resetAnimacion()
			this.perros = background1.perros;
			this.objetosTirados = new MovieClip();
			addChild(objetosTirados);
		}
		public function resetAnimacion():void {
			var i:int = cola.numChildren;
			while (i--) {
				MovieClip(cola.getChildAt(i)).gotoAndStop(1);
			}
		}
	}
}
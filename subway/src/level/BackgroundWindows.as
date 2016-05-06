package level
{
	import flash.display.Sprite;
	
	public class BackgroundWindows extends Sprite
	{
		private var _width:int;
		private var wagonWidth:int;
		private var estacionWidth:int;
		private var inStation:Boolean = true;
		private var fondoEstacion:fondoEstacionMC;
		private var loopFondosQty:int = 0;
		private var wagons:int;
		
		public function BackgroundWindows(wagons:int)
		{
			this.wagons = wagons;
		}
		public function update(speed:Number):void
		{
			this.y = 0;
			this.x -= speed;
			if(x<-(estacionWidth+wagonWidth))
			{
				x = -estacionWidth;
			}
		}
		public function start(_width:int):void
		{
			this._width = _width;
			createStation();
			createFondos();			
			this.cacheAsBitmap = true
		}
		private function createStation():void
		{
			for (var a:int = 0; a<wagons; a++)
			{
				fondoEstacion = new fondoEstacionMC();	
				wagonWidth= fondoEstacion.width;			
				fondoEstacion.x = this.width;
				addChild (fondoEstacion);
				estacionWidth+=fondoEstacion.width;
			}
			//fondoEstacion.x = this.width;
		}
		public function createFondos():void
		{
			for (var a:int = 0; a<(wagons+1); a++)
			{
				var fondo:fondoInteriorMC = new fondoInteriorMC();				
				fondo.x = this.width;
				addChild (fondo);
			}
			
		}

	}
}
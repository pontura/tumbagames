package level
{
	import flash.display.DisplayObject;
	import flash.display.DisplayObjectContainer;
	import flash.display.MovieClip;
	import flash.geom.Point;
	
	import flashlib.utils.ArrayUtil;
	import flashlib.utils.DisplayUtil;
	
	public class Tipos extends MovieClip
	{
		private var _view:View;
		private var state:String; 
		private var tapa:TapaMC;
		
		public function Tipos(_view:View)
		{
			this._view = _view;
			
			
			
			var manijasArr:Array = _view.background.getManijas()
			manijasArr = ArrayUtil.shuffle( manijasArr );
			
			/* tapa = new TapaMC();
			addChild(tapa); */
			
			var tipos:Array = settings["level" + _view._level.levelNumber].tipos;
				
			for(var a:int = 0; a<tipos.length; a++)
			{
				var manija:Point = Point(manijasArr[a]);
				var tipo:Tipo = new Tipo( tipos[a], _view );
				addChild( tipo );
				tipo.y = manija.y;
				tipo.x = manija.x;
			}
			//tapaOff();
		}
		public function nada():void
		{
			if(state == "nada")
				return;
			state = "nada";
			var i:int = this.numChildren;
			while(i--)
			{
				if(this.getChildAt(i) is Tipo)
				{
				var tipo:Tipo = Tipo(this.getChildAt(i));
				if(tipo.state !="enBolas")
					tipo.nada();
				tipo.resetEnergy();
				}
			}
		}
		public function resetProgressShow():void{
			var i:int = this.numChildren;
			while(i--)
			{
				if(this.getChildAt(i) is Tipo)
				{
					var tipo:Tipo = Tipo(this.getChildAt(i));
					tipo.energyProgress.hide();
				}
			}
		}
		public function checkAfana():void
		{
			state = "checkAfana";
			var tipo:Tipo = checkForAfano();
			if(tipo)
			{
				tipo.afanado();
				_view.poonga.afana();
			}
		}
		public function checkForAfano():Tipo
		{
			var i:int = this.numChildren;
			while(i--)
			{
				if(this.getChildAt(i) is Tipo)
				{
					var tipo:Tipo = Tipo(this.getChildAt(i));
					if(tipo.x> _view.poonga.x-settings.areaDeRobo && tipo.x< _view.poonga.x+settings.areaDeRobo
						&& tipo.state !="enBolas" && tipo.state !="reacciona")
					{
						return tipo
					}
				}
			}
			return null
		}
		public function pause():void
		{
			var i:int = this.numChildren;
			while(i--)
			{
				if(this.getChildAt(i) is Tipo)
				{
				var tipo:Tipo = Tipo(this.getChildAt(i));
				
				var container:DisplayObjectContainer = tipo as DisplayObjectContainer;
				if (container)
					for each (var child:DisplayObject in DisplayUtil.children(container, true))
						DisplayUtil.stopMovieClip(child);
				
				DisplayUtil.stopMovieClip(tipo);
				}
			}
		}
		/* public function tapaOn(tipo:Tipo):void
		{
			tapa.x -= _view.x
			tapa.visible = true;
			setChildIndex(tapa, this.numChildren-1);
			setChildIndex(tipo, this.numChildren-1);
		}
		public function tapaOff():void
		{
			this.tapa.visible = false;
		} */
	}
}
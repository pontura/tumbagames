package level
{
	import flash.display.MovieClip;
	
	import flashlib.utils.DisplayUtil;
	
	public class TipoManager
	{
		public var asset:MovieClip;
		public var area:int;
		public var reaccion:Array;
		public var dificultad:int
		public var duracionAmague:int
		public var miraAlrededor:Array;
		public var duracionMirada:int
		public var inventario:Array;
		
		private var _settings:Object;
		
		public function TipoManager(id:int)
		{
			var tipos:Array = settings.tipos;
			for each (var tipo:Object in tipos)
			{
				if (tipo.id == id)
				{
					this._settings = tipo;
					switch(tipo.asset)
					{
						case "tipoMC1":
							asset = new tipoMC1;
							break;
						case "tipoMC2":
							asset = new tipoMC2;
							break;
						case "tipoMC3":
							asset = new tipoMC3;
							break;
						case "tipoMC4":
							asset = new tipoMC4;
							break;
						case "tipoMC5":
							asset = new tipoMC5;
							break;
						case "tipoMC6":
							asset = new tipoMC6;
							break;
						case "tipoMC7":
							asset = new tipoMC7;
							break;
						case "tipoMC8":
							asset = new tipoMC8;
							break;
						case "tipoMC9":
							asset = new tipoMC9;
							break;
						case "tipoMC10":
							asset = new tipoMC10;
							break;
					}
					this.area = tipo.area;
					this.reaccion = tipo.reaccion;
					this.dificultad = tipo.dificultad;
					this.duracionAmague = tipo.duracionAmague;
					this.miraAlrededor = tipo.miraAlrededor;
					this.duracionMirada = tipo.duracionMirada;
					//this.inventario = ArrayUtil.shuffle( tipo.inventario );
					this.inventario = [];
					var arr:Array = tipo.inventario as Array;
					var e:int = arr.length
					for (var a:int = 0; a<arr.length; a++)
					{
						e--;
						this.inventario.push(arr[e]);
					}
				}
			}
			showActiveItems()
		}
		public function lose(itemName:String):void
		{
			for (var a:int = 0; a<inventario.length; a++)
			{
				if( inventario[a].name == itemName)
					DisplayUtil.dispose( MovieClip(asset[inventario[a].name]) );
			}
		}
		public function showActiveItems():void
		{
			var i:int = asset.numChildren;
			while(i--)
			{
				if(MovieClip(asset.getChildAt(i)).name != "cuerpo")
					MovieClip(asset.getChildAt(i)).visible = false;
			}
			for (var a:int = 0; a<inventario.length; a++)
			{
				if(!MovieClip(asset[inventario[a].name]))
					trace("no existe " + inventario[a].name + " en " + asset);
				MovieClip(asset[inventario[a].name]).visible = true;
			}
		}
		public function getPointsInventory(id:int):Object
		{
			for (var a:int = 0; a<inventario.length; a++)
			{
				if (a == id)
				{
					return inventario[a];
				}
			}
			return null;
		}

	}
}
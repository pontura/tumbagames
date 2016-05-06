package level
{
	public class TipoDistractor
	{
		public var asset:MovieClip;
		public var energyProgress:EnergyProgress;
		public var state:String = "nada";
		
		private var _view:View;
		private var _settings:Object;		
		private var afanoTask:Task;
		private var nadaTask:Task;
		private var sospechaTask:Task;
		
		public function TipoDistractor( _settings:Object , _view:View)
		{
			this._settings = _settings;
			this._view = _view;
		}

	}
}
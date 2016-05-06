package level
{
	import flash.display.MovieClip;
	
	import flashlib.easing.Tween;
	import flashlib.tasks.Func;
	import flashlib.tasks.Sequence;
	import flashlib.tasks.Task;
	import flashlib.tasks.Wait;
	
	public class Distractor extends MovieClip
	{
		public var asset:MovieClip;
		public var area:int = 200;
		
		private var _view:View;
		private var _settings:Object;		
		private var move:Task;
		
		private var _duration:int;
		private var _x:int;
		private var caminataMaxima:int = 600;
		
		public function Distractor( _settings:Object , _view:View)
		{
			this._settings = _settings;
			this._view = _view;
			switch(_settings.tipo)
			{
				case 1:
					this.asset = new distractor1MC();
					break;
				case 2:
					this.asset = new distractor2MC();
					break;
				case 3:
					this.asset = new distractor3MC();
					break;
			}
			addChild(asset);
			asset.stop();
			move = new Task();				
			move = new Sequence(
				new Wait(1000),
				new Func(startSequence)
			);		
			_view._level._tasks.add( move );
		}
		private function startSequence():void
		{
			getNewX();
			move = new Sequence(
				new Wait( Math.random()*(_settings.intervalos[1] - _settings.intervalos[0]) + _settings.intervalos[0] ),
				new Func(camina),
				new Tween(this,_duration,{x:_x}),
				new Func(frena),
				new Func(startSequence)
			)
			_view._level._tasks.add( move );
		}
		private function getNewX():void
		{
			
			var _d:int = Math.round((Math.random()*(caminataMaxima*2)) - caminataMaxima);
			var _distanciaReal:int = this.x + _d;
			_duration = _settings.speed * Math.abs(_d);
			
			if(_distanciaReal<0 || _distanciaReal>_view.width-20)
				getNewX();
			else
				_x = _distanciaReal;
				
			
			
		}
		private function camina():void
		{
			this.asset.gotoAndStop(2);
			if(_x<this.x)
				this.scaleX = -1
			else
				this.scaleX = 1;
		}
		private function frena():void
		{
			this.asset.gotoAndStop(1)
		}
	}
}
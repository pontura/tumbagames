package level
{
	import flash.display.DisplayObject;
	import flash.display.MovieClip;
	import flash.geom.Point;
	
	import flashlib.utils.DisplayUtil;
	
	import input.Controls;
	
	public class View extends MovieClip
	{
		public var _controls:Controls;
		public var tipos:Tipos;
		public var distractores:Distractores;
		public var background:Background;
		private var backgroundWindow:BackgroundWindows;
		public var poonga:Poonga;	
		public var _level:Level;	
				
		private var poongaSpeed:int;
		public var camera:PoongaCamera;
		private var duration:int;
		
		public function View(_level:Level)
		{
			
			this._level = _level;	
			duration = settings["level" + _level.levelNumber].duration/1000;		
			this.poongaSpeed = settings.poongaSpeed;	
		}
		public function draw():void
		{
			var wagons:int = settings["level" + _level.levelNumber].wagons;
			this._controls = new Controls(stage);
			this.poonga = new Poonga(this);		
			this.background = new Background(_level.levelNumber, wagons);	
			this.backgroundWindow = new BackgroundWindows(wagons);	
			this.tipos = new Tipos( this );	
			this.distractores = new Distractores( this );
			addToStage(backgroundWindow,0,0);		
			addToStage(background, 0, 0);
			addToStage(poonga, stage.stageWidth/2, 395);
			addToStage(tipos, 0,0);
			addToStage(distractores, 0,0);		
			camera = new PoongaCamera(this, stage.stageWidth, stage.stageHeight, wagons);
			
			camera.follow(poonga);
			_level._tasks.add(camera);
			distractores.start();
			backgroundWindow.start(this.width);
		}
		private function getSpeed():Number
		{
			return Math.abs(_level.time - (duration/2));
		}
		public function update():void
		{
			var speed:Number = getSpeed()
			if(_level.time>=duration)
			{
				_level._gui.showLoseScreen();
				return;
			}
			_level._gui.moveProgress( _level.time );
			backgroundWindow.update(duration/2-speed);
			
			
			if(_controls.isDown(Controls.LEFT))
				move(Controls.LEFT);
			else if(_controls.isDown(Controls.RIGHT))
				move(Controls.RIGHT);
			else if(_controls.keyIsDown)
			{
				tipos.checkAfana();
				return;
			} 
			else
			{
				tipos.nada();
				poonga.quieto();
			}
		}
		private function addToStage(DO:DisplayObject, _x:int, _y:int):void
		{
			addChild(DO);
			DO.x = _x;
			DO.y = _y;
		}
		public function move(direction:int):void
		{
			if (
				(poonga.localToGlobal(new Point(0,0)).x>stage.stageWidth-poonga.width && direction == Controls.RIGHT)
				|| 
				(poonga.localToGlobal(new Point(0,0)).x<poonga.width && direction == Controls.LEFT)
				)
				return;
			switch(direction)
			{
				case Controls.LEFT:					
					poonga.x -= poongaSpeed;
					poonga.move(direction);
					break;
				case Controls.RIGHT:
					poonga.x += poongaSpeed;
					poonga.move(direction);
					break;
			}
			var tipo:Tipo = tipos.checkForAfano();
			if(tipo)
				tipo.energyProgress.show();
			else
				tipos.resetProgressShow();
			
		}
		public function reset():void
		{
			this.poonga.reset();	
			camera.dispose();
			DisplayUtil.dispose(this);			
			poonga = null;
			_controls = null;
			tipos = null;
			distractores = null;
		}

	}
}
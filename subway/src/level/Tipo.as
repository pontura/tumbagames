package level
{
	import flash.display.MovieClip;
	
	import flashlib.tasks.Func;
	import flashlib.tasks.Sequence;
	import flashlib.tasks.Task;
	import flashlib.tasks.Wait;
	
	import globals.audio;
	
	import gui.PointsWin;
	import gui.energyProgress.EnergyProgress;
	
	import tasks.LoopFunc;
	
	public class Tipo extends MovieClip
	{
		public var asset:MovieClip;
		public var energyProgress:EnergyProgress;
		public var state:String = "nada";
		public var tipoManager:TipoManager;
		
		private var _view:View;
		private var enterFrameTask:Task;
		private var afanoTask:Task;
		private var nadaTask:Task;
		private var sospechaTask:Task;
		private var direction:int;
		
		public function Tipo( id:int, _view:View)
		{
			this.tipoManager = new TipoManager(id);
			this._view = _view;
			this.asset = tipoManager.asset;
			addChild( asset );
			asset.stop();
			energyProgress = new EnergyProgress();
			energyProgress.setEnergy( this );
			enterFrameTask = new Task();
			enterFrameTask = new LoopFunc(enterFrame);
			_view._level._tasks.add(enterFrameTask);		
		}
		private function enterFrame():void
		{
			//fuera de la vista
			if(Math.abs(_view.poonga.x - this.x)>tipoManager.area)
			{
				resetTask();
				return
			}
			if(state == "afanado" && !afanoTask)
			{
				afanoTask = new Task()
				afanoTask = new Sequence
					(
						new Wait(Math.random()*tipoManager.reaccion[0] + Math.random()*tipoManager.reaccion[1]),
						new Func(amaga),
						new Wait(tipoManager.duracionAmague),
						new Func(reaccionaCheck)
					)
				_view._level._tasks.add( afanoTask );
			} else if(miraDistractor() && state != "reacciona" && !afanoTask)
			{
				amaga(direction);
				if(descubreAPoonga())
				{
					afanoTask = new Task()
					afanoTask = new Sequence
						(
							new Wait(tipoManager.duracionAmague),
							new Func(reacciona)
						)
					_view._level._tasks.add( afanoTask );
				}
			}else if((state == "nada" || state == "enBolas" ) && !nadaTask && Math.floor(Math.random()*6) == 0)
			{
				miraAlrededor();
			}
		}
		private function miraDistractor():Boolean
		{
			var i:int = _view.distractores.numChildren;
			var masCercaDist:int = 0;
			while(i--)
			{
				var distractor:Distractor = Distractor(_view.distractores.getChildAt(i));
				var dist:int = distractor.x - this.x;
				if(Math.abs(dist)<distractor.area && Math.abs(dist)>Math.abs(masCercaDist))
				{
					masCercaDist = dist;
				}
			}
			if(masCercaDist!=0)
			{
				direction = masCercaDist;
				return true;
			}
			return false;
		}
		private function miraAlrededor():void
		{
				nadaTask = new Task()
				nadaTask = new Sequence
					(
						new Wait(Math.random()*tipoManager.miraAlrededor[0] + Math.random()*tipoManager.miraAlrededor[1]),
						new Func(amaga),
						new Wait(tipoManager.duracionMirada),
						new Func(botoneaCheck)
					)
				_view._level._tasks.add( nadaTask );
		}
		private function amaga(direction:int = 0):void
		{
			state = "amaga";
			var randomVuelta:int
			if(direction>0)
				randomVuelta = 3;
			else if(direction<0)
				randomVuelta = 2;
			else
				randomVuelta = Math.floor(Math.random()*2)+2;
			asset.cuerpo.gotoAndStop(randomVuelta);			
		}
		private function botoneaCheck():void
		{
			if(descubreAPoonga())
			{
				
				reacciona();
			} else
			{
				
				nadaTask = new Sequence
					(
						new LoopFunc(loopBotoneaCheck)
					)
				_view._level._tasks.add( nadaTask );
			}
		}
		private function loopBotoneaCheck():void
		{
			if(descubreAPoonga())
			{
				
				reacciona();
			}else if(Math.random()*500 < 10)
			{
			
				nada();
			}
		}
		private function descubreAPoonga():Boolean
		{
			 if (_view.poonga.state != "afana")
				return false;
					
			if(
				_view.poonga.x>this.x && this.asset.cuerpo.currentFrame == 3
				||
				_view.poonga.x<this.x && this.asset.cuerpo.currentFrame == 2
		 	){
		 		return true;
			}
			return false;
		}
		private function reaccionaCheck():void
		{
			if(tipoManager.dificultad>Math.random()*(100-tipoManager.dificultad))
				reacciona();
			else
				nada();
		}
		private function reacciona():void
		{
			this._view._level._gui.showAlerta();
			state = "reacciona";
			
			audio.gain(_view._level.levelAudio, 0);
			audio.stop("sfx_punguear");
			audio.play("sfx_scream");
			
			if(_view.poonga.x<this.x)
				asset.cuerpo.gotoAndStop(5);
			else
				asset.cuerpo.gotoAndStop(4);
			resetTask();
			sospechaTask = new Sequence
				(
					new LoopFunc(loopSospecha)
				)
			_view._level._tasks.add( sospechaTask );
		}
		private function loopSospecha():void
		{
			this._view._level._gui.avatarProgressBar.setProgress( Number(settings.sospechaSpeed) );
		}
		public function nada():void
		{
			resetTask();			
			asset.cuerpo.gotoAndStop(1);			
			this.state = "nada";
		}
		public function resetEnergy():void
		{
			energyProgress.hide();
			energyProgress.resetLast();
		}
		public function afanado():void
		{			
			this.state = "afanado";
			energyProgress.resta();	
			energyProgress.show();
		}
		public function enBolas():void
		{
			resetTask();
			this.state = "enBolas";
			asset.cuerpo.gotoAndStop(1);
		}
		public function pierdeRopa(inventory_id:int):void
		{
			var points:int = tipoManager.getPointsInventory(inventory_id).points
			var name:String =  tipoManager.getPointsInventory(inventory_id).name;
			
			this._view._level._gui.addPoints(points);
			
			tipoManager.lose(name);			
			if(asset.numChildren == 1)
				enBolas();
				
			var pw:PointsWin = new PointsWin(points);
			this.addChild(pw);
			pw.y = -180;
			audio.play("sfx_robaritem");
		}
		private function resetTask():void
		{
			if(afanoTask)
				afanoTask.stop();
			afanoTask = null;
			if(nadaTask)
				nadaTask.stop();
			nadaTask = null;
			if(sospechaTask)
			{
				sospechaTask.stop();
				this._view._level._gui.hideAlerta();	
			}
			sospechaTask = null;
			
			
		}
		public function reset():void
		{
			resetTask()
			if(enterFrameTask)
				enterFrameTask.stop();
			enterFrameTask = null;
		}
	}
}
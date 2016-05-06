package level
{
	import flashlib.utils.DisplayUtil;
	
	import globals.audio;
	
	import input.Controls;
	
	public class Poonga extends PoongaMC
	{
		public var state:String;
		
		private var view:View;
		private var direction:int;
		
		
		public function Poonga(view:View)
		{
			this.view = view;
		}
		public function quieto():void
		{
			hace("nada");
			audio.stop("sfx_punguear");
			audio.gain(view._level.levelAudio, 1);
			audio.stop("sfx_scream");
		}
		public function win():void{
			this.scaleX = 1;
			gotoAndStop("camina");
			
		}
		public function afana():void
		{
			hace("afana");
		}
		public function move(direction:int):void
		{
			if(this.direction==direction && state =="camina")
				return;
			this.direction=direction
			if(direction == Controls.RIGHT)
				this.scaleX = 1;
			else
				this.scaleX = -1;
			hace("camina");
		}
		public function hace(accion:String):void
		{
			if(state == accion)
				return;
			state = accion;
			this.gotoAndStop(accion);
		}
		public function reset():void
		{
			view = null;
		}
		public function pause():void
		{
			DisplayUtil.stopMovieClip(this);
		}
	}
}
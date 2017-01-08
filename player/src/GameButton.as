package
{
	import com.qb9.flashlib.easing.Tween;
	import com.qb9.flashlib.tasks.Func;
	import com.qb9.flashlib.tasks.Sequence;
	
	import flash.display.MovieClip;

	public class GameButton extends GameButtonMC
	{
		public var obj:Object;
		private var slider:Slider;
		
		public function GameButton(slider:Slider, obj:Object)
		{
			this.slider= slider;
			var mc:MovieClip;
			
			switch(obj.id)
			{
				case 1: mc = new game1MC; break;
				case 2: mc = new game2MC; break;
				case 3: mc = new game3MC; break;
				case 4: mc = new game4MC; break;
				case 5: mc = new game5MC; break;
				case 6: mc = new game6MC; break;
				case 7: mc = new game7MC; break;
				case 8: mc = new game8MC; break;
			}
			ph.addChild(mc);
		}
		public function go(left:Boolean):void
		{
			if(left && x>=slider._width) x = 0;
			else if(!left && x<=0) x =slider._width;
			
			var _x:int;
			
			if(left)
				_x = x+width;
			else _x = x-width;
			
			Main.I.tasks.add(
				new Sequence(
					new Tween(this, 500, {x:_x} , {transition:"easeout"}),
					new Func(ready)
					)
				)
		}
		private function ready():void
		{
			Main.I.board.slider.availableCick();
		}
	}
}
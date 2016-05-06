package ui
{
	import flash.events.Event;
	import flash.utils.clearInterval;
	import flash.utils.setInterval;
	import flash.utils.setTimeout;

	public class MultiplierSignal extends MultiplierSignalMC
	{
		private var id:int;
		
		public function MultiplierSignal(qty:int, _x:int, _y:int)
		{
			Main.I.board.level.addChild(this);
			this.x = _x; this.y = _y;
			this.signal.field.text = "X" + String(qty);
			addEventListener(Event.REMOVED_FROM_STAGE, reset);
			id = setInterval(reset, 3000);
			signal.gotoAndStop(qty);
		}
		private function reset(e:Event = null):void
		{
			removeEventListener(Event.REMOVED_FROM_STAGE, reset);
			clearInterval(id);
			Main.I.board.level.removeChild(this);
		}
	}
}
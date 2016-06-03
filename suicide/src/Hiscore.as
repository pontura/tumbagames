package
{
	import flash.events.KeyboardEvent;
	import flash.events.MouseEvent;
	import flash.net.SharedObject;
	import flash.utils.setTimeout;
	
	import flashlib.utils.DisplayUtil;
	
	import globals.audio;

	public class Hiscore  extends HiscoresMC
	{
		public function Hiscore()
		{
			trace("Hiscore");
			//Main.I.stage.focus = inputField;
			Main.I.stage.addEventListener(KeyboardEvent.KEY_DOWN, keyDown);
			btn.addEventListener(MouseEvent.CLICK, Closed);
			//this.btn.visible = false;
			
			//Main.I.board.ui.visible = false;
			
			this.points_txt.text = String(Main.I.hiscore);
			//setTimeout(Main.I.exit, 5000);
			return;
			
			//Main.I.stage.addEventListener(MouseEvent.MOUSE_DOWN, keyDown);
			
			audio.play("Siren-AirRaid");
			
		}
		private function keyDown(e:KeyboardEvent):void
		{
			if(e.keyCode == 13)
			{
				Closed(e);
				return;
			}
		}
		private function Closed(e:*):void
		{
			trace("Closed");
			if(inputField.text.length <2) return;
			
			Main.I.stage.removeEventListener(KeyboardEvent.KEY_DOWN, keyDown);
			
			Main.I.SetNewHiscore(inputField.text);
			
			btn.removeEventListener(MouseEvent.CLICK, Closed);
			
			Main.I.addChild( new Summary );
			DisplayUtil.dispose( this );
		}
	}
}
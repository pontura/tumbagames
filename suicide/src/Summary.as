package
{
	import flash.display.MovieClip;
	import flash.events.KeyboardEvent;
	import flash.events.MouseEvent;
	import flash.net.SharedObject;
	import flash.utils.setTimeout;
	
	import flashlib.utils.DisplayUtil;
	
	import globals.audio;

	public class Summary  extends SummaryMC
	{
		public function Summary()
		{
			
			var mySharedObject:SharedObject = SharedObject.getLocal("hiscores");
			var username1:String  = mySharedObject.data.uno;
			var username2:String  = mySharedObject.data.dos;
			var i:int = hiscores.numChildren;
			while(i--)
			{
				var linea:MovieClip = hiscores.getChildAt(i) as MovieClip;
				linea.username.text = Main.I.hiscores[i].username;
				linea.score.text = Main.I.hiscores[i].score;
			}
			//mc.username = "ASDAS";	
			
			Main.I.stage.addEventListener(KeyboardEvent.KEY_DOWN, keyDown);
			//this.btn.visible = false;
			btn.addEventListener(MouseEvent.CLICK, Close);
			
			//Main.I.board.ui.visible = false;
			//this.points_txt.text = String(Main.I.board.ui.points)
			//setTimeout(Main.I.exit, 5000);
			return;
		}
		private function keyDown(e:*):void
		{
			Main.I.stage.removeEventListener(MouseEvent.MOUSE_DOWN, keyDown);
			Main.I.stage.removeEventListener(KeyboardEvent.KEY_DOWN, keyDown);
			Close(e);
		}
		private function Close(e:*):void
		{
			/*audio.stop("Siren-AirRaid");*/
			//Main.I.stage.removeEventListener(KeyboardEvent.KEY_DOWN, keyDown);
			
			btn.removeEventListener(MouseEvent.CLICK, Close);
			Main.I.addChild( new Buy );
			DisplayUtil.dispose( this );
		}
	}
}
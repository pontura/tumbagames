package ui
{
	import flash.display.Bitmap;
	import flash.display.BitmapData;
	import flash.display.MovieClip;
	import flash.geom.Matrix;
	import flash.geom.Rectangle;
	
	import flashlib.easing.Tween;
	import flashlib.tasks.Func;
	import flashlib.tasks.Sequence;
	import flashlib.tasks.Wait;
	import flashlib.utils.DisplayUtil;

	public class Screenshot extends MovieClip
	{
		function Screenshot(  ):void
		{
			Game.I._ui.alpha = 0;
			Game.I.show.hide();
			var bmp:Bitmap = new Bitmap( getBitmapData( MovieClip(Game.I) ) );
			addChild(bmp);
			var bmp2:Bitmap = new Bitmap(getBitmapData( MovieClip(Game.I.getCurrentLevel()) ) );
			addChild(bmp2);
			bmp2.y = Game.I.stage.stageHeight;
			Game.I.show.show();
			Game.I.addChild(this);
			Game.I.setChildIndex(this, 0);
			Game.I._ui.alpha = 1;
			Game.I.reset();
			Game.I.tasks.add(
				new Sequence(
					new Wait(1000),
					new Tween(this,1000,{y:-Game.I.stage.stageHeight}),
					new Func(restart)
					)
				)
			Game.I.tasks.start();
		}
		private function restart():void
		{
			Game.I.restart();
			DisplayUtil.dispose(this);
		}
		private function getBitmapData( target:MovieClip ) : BitmapData
		{
			var bd : BitmapData = new BitmapData( Game.I.stage.stageWidth, Game.I.stage.stageHeight);
			bd.draw( target );
			return bd;
		}
	}
}
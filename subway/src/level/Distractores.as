package level
{
	import flash.display.DisplayObject;
	import flash.display.DisplayObjectContainer;
	import flash.display.MovieClip;
	
	import flashlib.utils.DisplayUtil;
	
	public class Distractores extends MovieClip
	{
		private var _view:View;
		private var state:String;
		
		public function Distractores(_view:View)
		{
			this._view = _view;		
		}
		public function start():void
		{
			var distractoresArray:Array = settings["level" + _view._level.levelNumber].distractores;
				
			for(var a:int = 0; a<distractoresArray.length; a++)
			{				
				var distractor:Distractor = new Distractor( distractoresArray[a], _view );
				addChild( distractor );
				distractor.x = Math.floor(Math.random()*(_view.width-200) + 100);
				distractor.y = 440;
			}
		}
		public function pause():void
		{
			var i:int = this.numChildren;
			while(i--)
			{
				if(this.getChildAt(i) is Distractor)
				{
				var tipo:Distractor = Distractor(this.getChildAt(i));
				
				var container:DisplayObjectContainer = tipo as DisplayObjectContainer;
				if (container)
					for each (var child:DisplayObject in DisplayUtil.children(container, true))
						DisplayUtil.stopMovieClip(child);
				
				DisplayUtil.stopMovieClip(tipo);
				}
			}
		}
	}
}
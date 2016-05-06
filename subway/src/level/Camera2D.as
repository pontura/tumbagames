package level
{
	import flash.display.DisplayObject;
	import flash.display.DisplayObjectContainer;
	
	import flashlib.math.QMath;
	import flashlib.tasks.Task;
	
	import gui.Anchor;
	
	public class Camera2D extends Task
	{
		protected var canvas:DisplayObjectContainer;
		protected var target:DisplayObject;
		
		protected var _displayWidth:Number;
		protected var _displayHeight:Number;
		
		protected var anchor:Anchor;
		
		protected var _zoom:Number = 1;
		
		public function Camera2D(canvas:DisplayObjectContainer, displayWidth:Number, displayHeight:Number, anchor:Anchor = null)
		{
			this.canvas = canvas;
			this.anchor = anchor || Anchor.center;
			
			_displayWidth = displayWidth;
			_displayHeight = displayHeight;
		}
		
		protected function get displayWidth() : Number
		{
			return _displayWidth * invZoom;
		}
		
		protected function get displayHeight() : Number
		{
			return _displayHeight * invZoom;
		}
		
		protected function get rx() : Number
		{
			return displayWidth * anchor.u;
		}
		
		protected function get ry() : Number
		{
			return displayHeight * anchor.v;
		}
		
		// Top, left, bottom, right
		
		public function get left() : Number
		{
			return -canvas.x * invZoom;
		}
		
		public function get right() : Number
		{
			return left + displayWidth;
		}
		
		public function get top() : Number
		{
			return -canvas.y * invZoom;
		}
		
		public function get bottom() : Number
		{
			return top + displayHeight;
		}
		
		// X & Y (center)
		
		public function get x() : Number
		{
			return left + rx;
		}
		
		public function set x(v:Number) : void
		{
			canvas.x = limit(-v + rx, -canvas.width + displayWidth) * zoom;
		}
		
		public function get y() : Number
		{
			return top + ry;
		}
		
		public function set y(v:Number) : void
		{
			canvas.y = limit(-v + ry, -canvas.height + displayHeight) * zoom;
		}
		
		public function get zoom() : Number
		{
			return _zoom;
		}
		
		protected function get invZoom() : Number
		{
			return 1 / zoom;
		}
		
		public function set zoom( val:Number ) : void
		{
			if (val == 0)
				throw new ArgumentError('Camera2D > Can\'t set zoom to 0');
			
			_zoom = canvas.scaleX = canvas.scaleY = val;
			doFolow();
		}
		
		// Methods
		
		public function follow(obj:DisplayObject) : void
		{
			target = obj;
			doFolow();
		}
		
		public override function update(milliseconds:uint):void
		{
			super.update(milliseconds);
			
			doFolow();
			
			/*if (target)
			{
				//var step:Number = settings.camera.speed * (milliseconds / 1000);
				var step:Number = 1;
				var dx:Number = target.x - x;
				var dy:Number = target.y - y;
				 
				x += dx * step;
				y += dy * step;
			}*/
		}
		
		protected function doFolow() : void
		{
			if (target && running)
			{
				x = target.x;
				y = target.y;
			}
		}
		
		protected function limit(num:Number, min:int) : int
		{
			return Math.round(QMath.clamp(num, min, 0));
		}

	}
}
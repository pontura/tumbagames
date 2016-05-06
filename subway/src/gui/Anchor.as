package gui
{
	public class Anchor
	{
		protected var _u:Number;
		protected var _v:Number;
		
		public function Anchor(u:Number, v:Number)
		{
			_u = u;
			_v = v;
		}
		
		public function get u() : Number
		{
			return _u;
		}
		
		public function get v() : Number
		{
			return _v;
		}
		
		// STATICS
		
		private static function make(u:Number,v:Number):Anchor
		{
			return new Anchor(u,v); 
		}
		
		// Built-in
		public static const centerLeft	:Anchor = make(0  , 0.5);
		public static const centerRight	:Anchor = make(1  , 0.5);
		public static const center		:Anchor = make(0.5, 0.5);
		public static const topLeft		:Anchor = make(0  , 0  );
		public static const topRight	:Anchor = make(1  , 0  );
		public static const topCenter	:Anchor = make(0.5, 0  );
		public static const bottomLeft	:Anchor = make(0  , 1  );
		public static const bottomRight	:Anchor = make(1  , 1  );
		public static const bottomCenter:Anchor = make(0.5, 1  );
	}
}
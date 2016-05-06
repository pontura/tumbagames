package ui
{
	import com.qb9.flashlib.utils.ArrayUtil;
	import com.qb9.flashlib.utils.DisplayUtil;
	
	import flash.events.KeyboardEvent;
	import flash.events.MouseEvent;
	
	import view.droppableObjects.Bomb;
	import view.droppableObjects.DroppableObject;

	public class Inventory
	{
		private var items:Array;
		private var readCoords:Array = [];
		
		public function Inventory()
		{
			items = [];
		}
		public function addItem(obj:DroppableObject):void
		{
			if(items[0])
				return;
			items[0] = obj;
			Main.I.stage.addEventListener(KeyboardEvent.KEY_DOWN, dropObject);
			readCoords = [obj.x, obj.y];
			obj.x = obj.y = 0;
			Main.I.board.level.character.container.addChild( obj );			
		}
		private function resetContainer():void{
			DisplayUtil.empty(Main.I.board.level.character.container);
			items = [];
		}
		private function dropObject(e:KeyboardEvent = null):void
		{
			switch(e.keyCode)
			{
				case 32: 
					Main.I.stage.removeEventListener(KeyboardEvent.KEY_DOWN, dropObject);
					if(items.length==0) return;
					Main.I.board.level.character.container.removeChild( DroppableObject(items[0]) );
					DroppableObject(items[0]).drop();
					resetContainer();
			}
			
		}
		public function ifHasitDestroyIt(obj:DroppableObject):void{
			if(items[0])
				if(items[0] == obj)
					items = [];
		}
	}
}
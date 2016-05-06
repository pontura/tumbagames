package virtual{
	
	
	import Box2D.Collision.Shapes.b2CircleShape;
	import Box2D.Collision.Shapes.b2MassData;
	import Box2D.Collision.Shapes.b2PolygonShape;
	import Box2D.Common.Math.b2Vec2;
	import Box2D.Dynamics.b2Body;
	import Box2D.Dynamics.b2BodyDef;
	import Box2D.Dynamics.b2DebugDraw;
	import Box2D.Dynamics.b2FixtureDef;
	import Box2D.Dynamics.b2World;
	
	import flash.display.Sprite;
	import flash.display.StageAlign;
	import flash.display.StageScaleMode;
	import flash.events.Event;
	import flash.media.ID3Info;
	
	import flashlib.utils.DisplayUtil;
	
	import view.objects.Boss;
	import view.objects.Player;
	import view.objects.SceneObject;
	
	
	/**
	 * @author Joshua Granick
	 */
	public class VirtualView extends Sprite {
		
		public var world:b2World;
		public var PHYSICS_SCALE:Number = 1 / 30;		
		private var PhysicsDebug:Sprite;
		private var espacioBordes:int = 120;
		public var player:b2Body;
		public var playerSpeed:int = 5;
		
		private var gameStarted:Boolean;
		private var movableObjects:Array = []
		public var characterContactListener:CharactersContactListener;
		private var objects:Array
		
		public function start (objects:Array):void {
			this.objects = objects;
			initialize ();
			construct ();	
		}	
		
		public function reset():void{
			movableObjects = [];
			var node:b2Body = world.GetBodyList();
			while (node)
			{
				var b:b2Body = node;
				node = node.GetNext();
				b.DestroyFixture(b.GetFixtureList());
				b.ResetMassData();
				world.DestroyBody(b);            
				b = null;
				
			}
			DisplayUtil.dispose(PhysicsDebug);
			PhysicsDebug = null;
			world.ClearForces();			
		}
		
		private function construct ():void {
			
			world = new b2World (new b2Vec2 (0, 0), true);
			characterContactListener = new CharactersContactListener();
			world.SetContactListener(characterContactListener);
			
			addChild (PhysicsDebug);
			
			var debugDraw:b2DebugDraw = new b2DebugDraw ();
			debugDraw.SetSprite (PhysicsDebug);
			debugDraw.SetDrawScale (1 / PHYSICS_SCALE);
			debugDraw.SetFlags (b2DebugDraw.e_shapeBit);
			
			world.SetDebugDraw (debugDraw);
			var stageW:int = Game.I.stage.stageWidth;
			var stageH:int = Game.I.stage.stageHeight;
			
			
			//bordes
			createBox (stageW/2,stageH-20 , stageW, 60, false);
			createBox (stageW/2,60		, stageW, 120, false);
			createBox (stageW,stageH/2 , 20, stageH, false);
			createBox (0,stageH/2	 , 20, stageH, false);
			//
			
			for each (var obj:Object in objects)
			{
				createObject(obj)
			}
			
			//createFloor (stageW/2,stageH/2 , stageW, stageH, false);
			
		}
		
		
		
		private function createObject (obj:Object):void {
			
			var bodyDefinition:b2BodyDef = new b2BodyDef ();
			bodyDefinition.position.Set (obj.x * PHYSICS_SCALE, obj.y * PHYSICS_SCALE);
			
			bodyDefinition.type = b2Body.b2_dynamicBody;
			
			var circle:b2CircleShape = new b2CircleShape (obj.radius * PHYSICS_SCALE);
			
			var fixtureDefinition:b2FixtureDef = new b2FixtureDef ();
			fixtureDefinition.shape = circle;
			
			var massData:b2MassData = new b2MassData();
			massData.mass = obj.mass
				
			fixtureDefinition.friction=				obj.friction;
			fixtureDefinition.restitution = 		obj.restitution;
			fixtureDefinition.density=				obj.density;
			bodyDefinition.linearDamping = 			obj.linearDamping;
			
			if(obj.name == "player") 
			{
				fixtureDefinition.userData = {name:obj.name};
				player = world.CreateBody (bodyDefinition);
				player.CreateFixture (fixtureDefinition);
				player.SetMassData( massData);
			}
			else
			{
				fixtureDefinition.userData = {name:obj.name, characterID:getCharacter()};
				var body:b2Body = world.CreateBody (bodyDefinition);
				body.CreateFixture (fixtureDefinition);
				body.SetMassData( massData);
			}
			if(obj.name == "boss")
				movableObjects.push(body)
			
		}
		private function getCharacter():int{
			var arr:Array = settings.levels[Game.I.levelNum].characters;
			return arr[Math.floor(Math.random()*arr.length)]
		}
		private function createBox (x:Number, y:Number, width:Number, height:Number, dynamicBody:Boolean):void {
			
			var bodyDefinition:b2BodyDef = new b2BodyDef ();
			bodyDefinition.position.Set (x * PHYSICS_SCALE, y * PHYSICS_SCALE);
			
			if (dynamicBody)
				bodyDefinition.type = b2Body.b2_dynamicBody;
			
			bodyDefinition.linearDamping = 1;
			
			var polygon:b2PolygonShape = new b2PolygonShape ();
			polygon.SetAsBox ((width / 2) * PHYSICS_SCALE, (height / 2) * PHYSICS_SCALE);
			
			var fixtureDefinition:b2FixtureDef = new b2FixtureDef ();
			fixtureDefinition.shape = polygon;
			
			fixtureDefinition.restitution = 1;
			fixtureDefinition.friction = 0.2;			
			
			var body:b2Body = world.CreateBody (bodyDefinition);
			body.CreateFixture (fixtureDefinition);
			
			
			
		}
		
		private function initialize ():void {			
			PhysicsDebug = new Sprite ();
			gameStarted = false;
		}
		
		public function destroyObject(body:b2Body):void
		{
			//trace("DESAPARECIO: " + SceneObject(Object(body.GetUserData()).sceneObject) + "hay: " + world.GetBodyCount());
			SceneObject(Object(body.GetUserData()).sceneObject).reset();
			world.DestroyBody(body);
			//trace("y quedan: " + world.GetBodyCount());
		}
		
		public function setMovements(canMove:Boolean):void
		{
			for each (var boss:b2Body in movableObjects)
			{
				Boss(Object(boss.GetUserData()).sceneObject).canMove = canMove;
			}
			getPlayerSceneObject().canMove = canMove;
		}
		public function getPlayerSceneObject():Player
		{
			return Player(Object(player.GetUserData()).sceneObject);
		}
		
		// Event Handlers
		public function keysStatus(_x:int = 0, _y:int = 0):void {
			
			world.Step(1 / 30, 1, 1);
			world.ClearForces ();
			world.DrawDebugData ();	
			
			if(!player.IsAwake())
			{
				player.SetAwake(true);
			}
				
			
			var _vec:b2Vec2 = player.GetPosition().Copy()
			
				
				
			for each (var boss:b2Body in movableObjects)
			{
				_vec = player.GetPosition().Copy()
				if(!gameStarted && _x!=0 || _y!=0)
				{
					gameStarted	= true;
					Boss(Object(boss.GetUserData()).sceneObject).canMove = true;
					Boss(Object(boss.GetUserData()).sceneObject).jump();
				} else if(SceneObject(Object(boss.GetUserData()).sceneObject).canMove)
				{
					_vec.Subtract(boss.GetPosition());
					boss.ApplyForce(_vec, new b2Vec2(0,0));										
				} else if(gameStarted){
					_vec.Subtract(boss.GetPosition());
					boss.SetLinearVelocity(new b2Vec2(0,0));
				}
			}
			
			
			var playerSceneObject:SceneObject = SceneObject(Object(player.GetUserData()).sceneObject);
			if(!playerSceneObject.canMove)
			{
				if(playerSceneObject.state == "merca")
					player.SetLinearVelocity(new b2Vec2(0,0));
				else
					player.SetLinearVelocity(new b2Vec2(_vec.x/3,_vec.y/3));
				return;
			}
			
			
			var speedX:int;
			var speedY:int;
			
			
			_x = _x*playerSpeed;
			_y= _y*playerSpeed;
			
			if(_x == 0 && _y == 0 && player.GetLinearVelocity().x ==0 && player.GetLinearVelocity().y == 0)
				return;
			
			player.SetLinearVelocity(new b2Vec2(_x,_y));
			
			
		}
		
		
	}
	
	
}
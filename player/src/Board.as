package
{
	import flash.events.Event;
	import flash.net.URLLoader;
	import flash.net.URLRequest;

	public class Board extends MainMC
	{
		public var slider:Slider;
		private var games:Array = 
		[
			
{id:1, prefix:"SS", name:"SUICIDE SNIPER", 			desc:"Suicide Sniper has been designed exclusively for those who want to get revenge of humanity but don't want to go to jail. Did you ever desire to get a few weapons and kill a lot of bastards in the streets? Then you might enjoy this game as hell!"},
{id:2, prefix:"IID", name:"INSANE IMBECILE DANCING", 	desc:"You are a specialist in pogo dancing: the most imbecile and destructive dance style ever developed by the human. Your mission is to start a massive deadly dance in an ultraviolent massacre metal show by Eugenetics, but beware of the security guys! If you dig the violence of extreme metal music, you must play Insane Imbecile Dancing... or die!"},
{id:3, prefix:"BB", name:"BRUTAL BATTLE", 			desc:"Kill the meanest criminals in the ring. Become the master of the world in an age of anarchy and gore."},
{id:4, prefix:"SD", name:"METRO PUNGA", 				desc:"Help Punga Punga, a teenage outlaw from the third world get rid of the fucking middle class by punging their stuff in the subway trains."},
{id:5, prefix:"PR", name:"PUNGA RAID", 				desc:"Pungueá todo lo que logres antes que te agarren los ratis."},
{id:6, prefix:"MR", name:"(*) MAD ROLLERS TRASH", 	desc:""},
{id:7, prefix:"", name:"(*) ELECTRIC GIGOLO", 		desc:""}
				
		]
		public function Board()
		{
			slider = new Slider(games);
			ph.addChild(slider);			
		}
		public function showTexts(id:int):void
		{
			this.title.text = games[id-1].name;
			this.desc.text = games[id-1].desc;
			
			LoadRanking(id);
		}
		public function hideTexts():void
		{
			this.title.text = "";
			this.desc.text = "";
			ranking.text = "";
		}
		private function LoadRanking(id:int):void
		{
			var prefix:String;
			for(var a:int = 0; a<games.length; a++)
				if(games[a].id ==id) prefix = games[a].prefix;
			if(prefix!= null && prefix != "")
			{
				var myTextLoader:URLLoader = new URLLoader();
				myTextLoader.addEventListener(Event.COMPLETE, onLoaded);
				myTextLoader.load(new URLRequest("C:\\tumbagames\\hiscores\\" + prefix + ".txt"));
			}			
		}
		private function onLoaded(e:Event):void {
			ranking.text = "---- scores ----\n";
			var t:String = e.target.data;
			var texto:Array = t.split("\n");			
			for (var a:int = 0; a<texto.length; a++)
			{
				var arr:Array = texto[a].split("_");
				ranking.text += (a+1) + ". " + arr[0] + "........" + arr[1];
			}
		}
	}
}
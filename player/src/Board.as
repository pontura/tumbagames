package
{
	public class Board extends MainMC
	{
		public var slider:Slider;
		private var games:Array = 
		[
			
{id:1, name:"SUICIDE SNIPER", 			desc:"Suicide Sniper has been designed exclusively for those who want to get revenge of humanity but don't want to go to jail. Did you ever desire to get a few weapons and kill a lot of bastards in the streets? Then you might enjoy this game as hell!"},
{id:2, name:"INSANE IMBECILE DANCING", 	desc:"You are a specialist in pogo dancing: the most imbecile and destructive dance style ever developed by the human. Your mission is to start a massive deadly dance in an ultraviolent massacre metal show by Eugenetics, but beware of the security guys! If you dig the violence of extreme metal music, you must play Insane Imbecile Dancing... or die!"},
{id:3, name:"BRUTAL BATTLE", 			desc:"Kill the meanest criminals in the ring. Become the master of the world in an age of anarchy and gore."},
{id:4, name:"METRO PUNGA", 				desc:"Help Punga Punga, a teenage outlaw from the third world get rid of the fucking middle class by punging their stuff in the subway trains."}
				
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
		}
		public function hideTexts():void
		{
			this.title.text = "";
			this.desc.text = "";
		}
	}
}
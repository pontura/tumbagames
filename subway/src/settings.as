package
{
	import com.adobe.serialization.json.JSON;
	
	import gui.Settings;

	public const settings:Settings = new Settings(com.adobe.serialization.json.JSON.decode);
	
}

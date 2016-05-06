package globals
{
	import com.adobe.serialization.json.JSON;
	import com.qb9.flashlib.config.Settings;

	//import com.adobe.serialization.json.JSON;

	public const settings:Settings = new Settings(com.adobe.serialization.json.JSON.decode);
}

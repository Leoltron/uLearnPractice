namespace Monobilliards
{
	public class Factory
	{
		public static IMonobilliards CreateMonobilards()
		{
			return new Monobilliards();
		}
	}
}